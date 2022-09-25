using CardStorageService.Utils;

var res = PasswordUtils.CreatePasswordHash("qwerty");
Console.WriteLine(res.passwordSalt);
Console.WriteLine(res.passwordHash);
Console.ReadKey(true);