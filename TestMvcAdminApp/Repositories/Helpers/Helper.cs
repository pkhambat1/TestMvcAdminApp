using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp {
    public static class Helper {
        public static string Connection() {
            var conn = Startup.GetConnectionString();
            return conn;
        }
    }
}
