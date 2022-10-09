using Microsoft.Data.Sqlite;
using Oracle.ManagedDataAccess.Client;
using Patterns.Sample_1_Singleton;
using Patterns.Sample_2_AbstractFabric;

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
        }
    }
}