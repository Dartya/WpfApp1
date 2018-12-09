using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public class DBconnection
    {
        public string Server { get; set; }
        public string DB { get; set; }
        public string Table { get; set; }
        public string UID { get; set; }
        public string Pass { get; set; }
        public string connectionString = "SERVER=localhost;DATABASE=tradesassistant;UID=root;PASSWORD=123456;";

        public DBconnection() {    //стандартный конструктор соединения
            Server = "localhost";
            DB = "tradesassistant";
            Table = "trades";
            UID = "root";
            Pass = "123456";
            connectionString = makeConnectionString(Server, DB, UID, Pass);
        }

        public DBconnection(string server, string DB, string UID, string pass) {    //конструктор соединения с параметрами
            Server = server;
            this.DB = DB;
            Table = "trades";
            this.UID = UID;
            Pass = pass;

            connectionString = makeConnectionString(server, DB, UID, pass);
        }

        public string makeConnectionString(string server, string DB, string UID, string pass) {
            string connectionString = "SERVER=" + server + ";" +
                "DATABASE=" + DB + ";" +
                "UID=" + UID + ";" +
                "PASSWORD=" + pass + ";";
            return connectionString;
        }

        public string makeConnectionString() {
            string connectionString = "SERVER=" + Server + ";" +
                "DATABASE=" + DB + ";" +
                "UID=" + UID + ";" +
                "PASSWORD=" + Pass + ";";
            return connectionString;
        }

        public void setParams(string server, string DB, string UID, string pass) {
            this.Server = server;
            this.DB = DB;
            this.UID = UID;
            this.Pass = pass;
        }

        public void setParams(string DB, string Table)
        {
            this.DB = DB;
            this.Table = Table;
        }

        public void setParams(DBconnection connection) {
            Server = connection.Server;
            DB = connection.DB;
            UID = connection.UID;
            Pass = connection.Pass;
        }
    }
}