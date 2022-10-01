using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardStorageService.Services.Implements
{
    public class AuthenticationService : IAuthenticateService
    {
        private const string _secretKey = "23^%#&gfdnb6!$53eSFGT";

        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly ConcurrentDictionary<string, SessionInfo> _sessions =
            new ConcurrentDictionary<string, SessionInfo>();

        public AuthenticationService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            CardStorageServiceDbContext context
                = scope.ServiceProvider.GetRequiredService<CardStorageServiceDbContext>();

            Account account = !String.IsNullOrWhiteSpace(authenticationRequest.Login) ?
                FindAccountByLogin(context, authenticationRequest.Login) : null;

            if (account == null)
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.UserNotFound
                };
            }

            if (!PasswordUtils.VerifyPassword(authenticationRequest.Password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.InvalidPassword
                };
            }

            AccountSession session = new AccountSession
            {
                AccountId = account.AccountId,
                SessionToken = CreateSessionToken(account),
                TimeCreated = DateTime.Now,
                TimeLastRequest = DateTime.Now,
                IsClosed = false
            };

            context.AccountSessions.Add(session);

            context.SaveChanges();

            SessionInfo sessionInfo = GetSessionInfo(account, session);

            _sessions.AddOrUpdate(sessionInfo.SessionToken, sessionInfo, (k, v) => v = sessionInfo);

            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.Success,
                SessionInfo = sessionInfo
            };
        }

        public SessionInfo GetSessionInfo(string sessionToken)
        {
            SessionInfo sessionInfo;

            _sessions.TryGetValue(sessionToken, out sessionInfo);

            if (sessionInfo == null)
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                CardStorageServiceDbContext context
                    = scope.ServiceProvider.GetRequiredService<CardStorageServiceDbContext>();

                AccountSession session = context.AccountSessions.FirstOrDefault(sess => sess.SessionToken == sessionToken);

                if (session == null) return null;

                Account account = context.Accounts.FirstOrDefault(acc => acc.AccountId == session.AccountId);

                sessionInfo = GetSessionInfo(account, session);

                if (sessionInfo is not null)
                {
                    _sessions.AddOrUpdate(sessionToken, sessionInfo, (k, v) => v = sessionInfo);
                }
            }
            return sessionInfo;
        }

        public static string SecretKey { get { return _secretKey; } }

        private SessionInfo GetSessionInfo(Account account, AccountSession accountSession)
        {
            return new SessionInfo
            {
                SessionId = accountSession.SessionId,
                SessionToken = accountSession.SessionToken,
                Account = new AccountDto
                {
                    AccountId = account.AccountId,
                    EMail = account.EMail,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    SecondName = account.SecondName,
                    Locked = account.Locked
                }
            };
        }

        private string CreateSessionToken(Account account)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_secretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                        new Claim(ClaimTypes.Email, account.EMail),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private Account FindAccountByLogin(CardStorageServiceDbContext context, string login)
        {
            return context.Accounts.FirstOrDefault(acc => acc.EMail == login);
        }
    }
}
