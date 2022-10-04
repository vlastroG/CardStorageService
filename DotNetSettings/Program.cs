namespace DotNetSettings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ConnectionString> connections = new List<ConnectionString>()
            {
                new ConnectionString(
                    "Database1",
                    "localhost",
                    "Vladimir",
                    "qwerty"),
                new ConnectionString(
                    "Database2",
                    "127.0.0.1",
                    "Alex",
                    "123456")
            };

            CacheProvider cacheProvider = new CacheProvider();
            cacheProvider.CacheConnections(connections);

            connections = cacheProvider.GetConnectionsFromCeche().ToList();
            foreach (var connection in connections)
            {
                Console.WriteLine(connection);
            }
            Console.ReadKey(true);
        }
    }
}