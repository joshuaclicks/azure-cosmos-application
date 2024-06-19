using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Configurations
{
    public class DatabaseConfiguration
    {
        public string Endpoint { get; set; } = null!;
        public string AccountKey { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public int RequestTimeoutInSeconds { get; set; }
        public int IdleTcpConnectionTimeoutInSeconds { get; set; }
        public int OpenTcpConnectionTimeoutInSeconds { get; set; }
    }
}
