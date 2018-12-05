using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public class DBconnection
    {
        public string server { get; set; }
        public string DB { get; set; }
        public string UID { get; set; }
        public string pass { get; set; }
        public string connectionString = "SERVER=localhost;DATABASE=mobiledb;UID=root;PASSWORD=123456;";

        public DBconnection() {    //стандартный конструктор соединения
            server = "localhost";
            DB = "blabla";
            UID = "root";
            pass = "123456";
            connectionString = makeConnectionString(server, DB, UID, pass);
        }

        public DBconnection(string server, string DB, string UID, string pass)
        {    //конструктор соединения с параметрами
            connectionString = makeConnectionString(server, DB, UID, pass);
        }

        public string makeConnectionString(string server, string DB, string UID, string pass) {
            string connectionString = "SERVER=" + server + ";" +
                "DATABASE=" + DB + ";" +
                "UID=" + UID + ";" +
                "PASSWORD=" + pass + ";";
            return connectionString;
        }

        public string makeConnectionString()
        {
            string connectionString = "SERVER=" + server + ";" +
                "DATABASE=" + DB + ";" +
                "UID=" + UID + ";" +
                "PASSWORD=" + pass + ";";
            return connectionString;
        }

        public void setParams(string server, string DB, string UID, string pass) {
            this.server = server;
            this.DB = DB;
            this.UID = UID;
            this.pass = pass;
        }

        public void setParams(DBconnection connection) {
            server = connection.server;
            DB = connection.DB;
            UID = connection.UID;
            pass = connection.pass;
        }
    }
}