using Microsoft.Data.Sqlite;
using Oracle.ManagedDataAccess.Client;
using Patterns.Sample_1_Singleton;
using Patterns.Sample_2_AbstractFabric;
using Patterns.Sample_3_FabricMethod;

namespace Patterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Sample Singleton
            var singletonInstance1 = SampleSingleton.Instance;
            singletonInstance1.Count++;
            var singletonInstance2 = SampleSingleton.Instance;
            singletonInstance2.Count++;
            Console.WriteLine(singletonInstance1.Count);
            bool test = singletonInstance1 == singletonInstance2;

            // Sample AbstractFabric
            LogSaver oracleSaver = new LogSaver(new OracleClientFactory());
            oracleSaver.Save(new LogEntry[] { new LogEntry(), new LogEntry() });

            LogSaver sqliteSaver = new LogSaver(SqliteFactory.Instance);
            sqliteSaver.Save(new LogEntry[] { new LogEntry(), new LogEntry() });

            // Sample fabric method
            var obj1 = ProductFactory.Create<ProductMarkI>();
            Console.WriteLine(obj1.GetType());
            var obj2 = ProductFactory.Create<ProductMarkII>();
            Console.WriteLine(obj2.GetType());

            var obj3 = ProductFactory.Create(typeof(ProductMarkI));
            Console.WriteLine(obj3.GetType());
            var obj4 = ProductFactory.Create(typeof(ProductMarkII));
            Console.WriteLine(obj4.GetType());

            Console.ReadKey(true);


        }
    }
}