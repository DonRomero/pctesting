using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SQLite;
using System.IO;


namespace MyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataService.svc or DataService.svc.cs at the Solution Explorer and start debugging.
    public class DataService : IDataService
    {
        SQLiteConnection sql = new SQLiteConnection("DataSource = mydb.sqlite;Version=3");
        string adminLogin = "admin";
        string adminPassword = "admin";
        string fk_computer = "CONSTRAINT fk_computer " +
                "FOREIGN KEY (computerID) " +
                "REFERENCES COMPUTER(id)";
        string fk_user = "CONSTRAINT fk_user " +
                "FOREIGN KEY (userID) " +
                "REFERENCES USER(id))";

        public string login(string name, string password, string compName)
        {
            sql.Open();
            string ansString = "denied";
            setConnection();
            SQLiteCommand sc = new SQLiteCommand("SELECT COUNT(*) FROM USER WHERE name = '" + name + "' AND password = '" + password + "';", sql);
            if (Convert.ToInt32(sc.ExecuteScalar()) > 0)
            {
                sc = new SQLiteCommand("SELECT COUNT(*) FROM COMPUTER WHERE name = '" + compName + "';", sql);
                if (Convert.ToInt32(sc.ExecuteScalar()) == 0)
                {
                    execute("INSERT INTO COMPUTER VALUES(NULL, '" + compName + "');", sql);
                }
                sc = new SQLiteCommand(String.Format("SELECT COUNT(*) FROM USER WHERE name = '{0}' AND password = '{1}' AND admin = 1;", name, password), sql);
                if (Convert.ToInt32(sc.ExecuteScalar()) > 0)
                    ansString = "admin";
                else
                    ansString = "user";
            }
            sql.Close();
            return ansString;
        }
        public void execute(string query, SQLiteConnection sql)
        {
            SQLiteCommand sc = new SQLiteCommand(query, sql);
            sc.ExecuteNonQuery();
        }

        public void setConnection()
        {
            //if (!Directory.Exists(@"C:\pctesting"))
            //    Directory.CreateDirectory(@"C:\pctesting");
            //if (!File.Exists(@"C:\pctesting\mydb.sqlite"))
            //    SQLiteConnection.CreateFile(@"C:\pctesting\mydb.sqlite");
            if (!File.Exists(@"mydb.sqlite"))
                SQLiteConnection.CreateFile(@"mydb.sqlite");//файл хранится в папке с IIS Express
            execute("CREATE TABLE IF NOT EXISTS COMPUTER(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT);", sql);
            execute("CREATE TABLE IF NOT EXISTS USER(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, password TEXT, admin INTEGER);", sql);
            execute("INSERT INTO USER VALUES(NULL, '" + adminLogin + "', '" + adminPassword + "', '" + 1 + "');", sql);
            execute("CREATE TABLE IF NOT EXISTS FILE(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, path TEXT, time INTEGER, type TEXT, computerID INTEGER, userID INTEGER, " +
                fk_computer + ", " +
                fk_user + ";", sql);
            execute("CREATE TABLE IF NOT EXISTS TRAFFIC(id INTEGER PRIMARY KEY AUTOINCREMENT, URL TEXT, time INTEGER, computerID INTEGER, userID INTEGER, " +
                fk_computer + ", " +
                fk_user + ";", sql);
            execute("CREATE TABLE IF NOT EXISTS TEST(id INTEGER PRIMARY KEY AUTOINCREMENT, time INTEGER, computerID INTEGER, " +
                fk_computer, sql);
            execute("CREATE TABLE IF NOT EXISTS CHARACTERISTIC(id INTEGER PRIMARY KEY AUTOINCREMENT, RAM TEXT, CRU TEXT, VideoRAM TEXT, freeRAM TEXT testID INTEGER, " +
                "CONSTRAINT fk_test " +
                "FOREIGN KEY (testID) " +
                "REFERENCES TEST(id)", sql);
            execute("CREATE TABLE IF NOT EXISTS PROCESS(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, startTime INTEGER, finishTime INTEGER, computerID INTEGER, userID INTEGER, " +
                fk_computer + ", " +
                fk_user + ";", sql);
            execute("CREATE TABLE IF NOT EXISTS ACTIVITY(id INTEGER PRIMARY KEY AUTOINCREMENT, allTime INTEGER, activeTime INTEGER, passiveTime INTEGER, computerID INTEGER, userID INTEGER, " +
                fk_computer + ", " +
                fk_user + ";", sql);
        }

        public void saveFileDataToDB(string name, string path, int time, string type, int compID, int userID)
        {
            sql.Open();
            setConnection();
            execute(String.Format("INSERT INTO FILE VALUES( NULL, '{0}', '{1}', {2}, '{3}', {4}, {5});", name, path, time, type, compID, userID), sql);
            sql.Close();
        }

        public void saveTrafficDataToDB(string URL, int time, int compID, int userID)
        {
            sql.Open();
            setConnection();
            execute("INSERT INTO TRAFFIC VALUES( NULL, '" + URL + "'," + time + "," + compID + "," + userID + ");", sql);
            sql.Close();
        }
    }
}
