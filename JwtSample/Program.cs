using JwtSample;
//https://jwt.io/
while (true)
{
    Console.WriteLine("Enter user name: ");
    string userName = Console.ReadLine();
    if (userName == "exit") break;
    Console.WriteLine("Enter user password: ");
    string userPassword = Console.ReadLine();
    UserService userService = new UserService();
    string token = userService.Authenticate(userName, userPassword);
    Console.WriteLine(token);
    Console.WriteLine();
    Console.ReadKey(true);
}