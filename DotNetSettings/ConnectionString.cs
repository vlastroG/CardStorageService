using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSettings
{
    public class ConnectionString
    {
        public string Host { get; set; }

        public string DatabaseName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return $"Host: {Host}; DatabaseName: {DatabaseName}; UserName: {UserName}; Password: {Password}";
        }

        public ConnectionString()
        {
            Host = "localhost";
            DatabaseName = "DefaultDB";
            UserName = "User";
            Password = "000000";
        }

        public ConnectionString(string Host, string DatabaseName, string UserName, string Password)
        {
            this.Host = Host;
            this.DatabaseName = DatabaseName;
            this.UserName = UserName;
            this.Password = Password;
        }
    }
}
