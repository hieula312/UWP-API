using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace UWP_API.Model
{
    class SQLLiteHelper
    {
        private static SQLLiteHelper _instance;
        private SQLiteConnection conn;
        private static string DatabaseName = "hellosqllite.db";


        public SQLLiteHelper()
        {
            conn = new SQLiteConnection(DatabaseName);
        }

        public void createTables()
        {

        }

        public void createMemberTable()
        {
            var sql = "";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
        }
    }
}
