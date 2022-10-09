using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Sample_2_AbstractFabric
{
    internal class LogSaver
    {
        private readonly DbProviderFactory _factory;

        public LogSaver(DbProviderFactory factory)
        {
            _factory = factory;
        }

        public void Save(IEnumerable<LogEntry> logs)
        {
            using (var dbConnection = _factory.CreateConnection())
            {
                SetConnectionString(dbConnection);

                using (var dbCommand = _factory.CreateCommand())
                {
                    SetCommandArguments(logs);

                    dbCommand.ExecuteNonQuery();
                }
            }
        }

        private void SetCommandArguments(IEnumerable<LogEntry> logs) { }

        private void SetConnectionString(DbConnection? dbConnection) { }
    }
}
