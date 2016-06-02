using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Text;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using AngleSharp.Dom;

namespace MyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataService.svc or DataService.svc.cs at the Solution Explorer and start debugging.
    public class DataService : IDataService
    {
        static string diskLetter = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
        Report report = new Report(diskLetter + @"\pctesting\");
        SQLiteConnection sql = new SQLiteConnection("DataSource = " + diskLetter + @"pctesting\mydb.sqlite;Version=3");
        string adminLogin = "admin";
        string adminPassword = "admin";
        string fk_computer = "CONSTRAINT fk_computer " +
                "FOREIGN KEY (computerID) " +
                "REFERENCES COMPUTER(id)";
        string fk_user = "CONSTRAINT fk_user " +
                "FOREIGN KEY (userID) " +
                "REFERENCES USER(id))";

        public List<string> getUsers()
        {
            sql.Open();
            SQLiteCommand sc = new SQLiteCommand("SELECT NAME FROM USER;", sql);
            SQLiteDataReader reader = sc.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader[0].ToString());
            }
            sql.Close();
            return list;
        }

        public bool addUser(string name, string password)
        {
            //try
            //{
            sql.Open();
            SQLiteCommand sc = new SQLiteCommand("SELECT COUNT(*) FROM USER WHERE NAME = '" + name + "';", sql);
            if (Convert.ToInt32(sc.ExecuteScalar()) > 0)
                return false;
            execute(String.Format("INSERT INTO USER VALUES(NULL, '{0}', '{1}', {2});", name, password, 0));
            //sql.Close();
            return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }

        public bool makeReport()
        {
            //try
            //{
            report.makeReport();
            return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }

        public string login(string name, string password, string compMAC, string compName)
        {
            if (!Directory.Exists(diskLetter + @"pctesting"))
                Directory.CreateDirectory(diskLetter + @"pctesting");
            if (!File.Exists(diskLetter + @"pctesting\mydb.sqlite"))
                SQLiteConnection.CreateFile(diskLetter + @"pctesting\mydb.sqlite");
            //if (!File.Exists(@"mydb.sqlite"))
            //    SQLiteConnection.CreateFile(@"mydb.sqlite");//файл хранится в папке с IIS Express
            string ansString = "denied";
            sql.Open();
            setConnection();
            SQLiteCommand sc = new SQLiteCommand("SELECT COUNT(*) FROM USER WHERE name = '" + name + "' AND password = '" + password + "';", sql);
            if (Convert.ToInt32(sc.ExecuteScalar()) > 0)
            {
                sc = new SQLiteCommand("SELECT COUNT(*) FROM COMPUTER WHERE MAC = '" + compMAC + "';", sql);
                if (Convert.ToInt32(sc.ExecuteScalar()) == 0)
                {
                    execute("INSERT INTO COMPUTER VALUES(NULL, '" + compMAC + "' , '" + compName + "');");
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

        public void execute(string query)
        {
            SQLiteCommand sc = new SQLiteCommand(query, sql);
            sc.ExecuteNonQuery();
        }

        public void setConnection()
        {
            //try
            //{
            execute("CREATE TABLE IF NOT EXISTS COMPUTER(id INTEGER PRIMARY KEY AUTOINCREMENT, MAC TEXT, name TEXT);"
                    + "CREATE TABLE IF NOT EXISTS USER(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, password TEXT, admin INTEGER);");
            SQLiteCommand sc = new SQLiteCommand("SELECT COUNT(*) FROM USER WHERE admin = 1;", sql);
            if (Convert.ToInt32(sc.ExecuteScalar()) == 0)
            {
                execute("INSERT INTO USER VALUES(NULL, '" + adminLogin + "', '" + adminPassword + "', " + 1 + ");");
            }
            execute("CREATE TABLE IF NOT EXISTS FILE(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, path TEXT, extension TEXT, time INTEGER, type TEXT, computerID INTEGER, userID INTEGER, " +
                fk_computer + ", " +
                fk_user + ";");
            execute("CREATE TABLE IF NOT EXISTS TRAFFIC(id INTEGER PRIMARY KEY AUTOINCREMENT, URL TEXT, host TRXT, referer TEXT, time INTEGER, refID INTEGER, computerID INTEGER, userID INTEGER, theme TEXT, " +
                fk_computer + ", " +
                fk_user + ";");
            //execute("CREATE TABLE IF NOT EXISTS TRAFFIC_THEME(value TEXT, trafficID INTEGER, themeID INTEGER, CONSTRAINT trafficFK FOREIGN KEY (trafficID) REFERENCES TRAFFIC(id), CONSTRAINT themeFK FOREIGN KEY (themeID) REFERENCES THEME(id));");
            //execute("CREATE TABLE IF NOT EXISTS THEME(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, trafficID INTEGER, CONSTRAINT trafficFK FOREIGN KEY (trafficID) REFERENCES TRAFFIC(id));");
            execute("CREATE TABLE IF NOT EXISTS CHARACTERISTIC(id INTEGER PRIMARY KEY AUTOINCREMENT, time TEXT, teapots INTEGER, RAM TEXT, freeRAM TEXT, CPU TEXT, VideoRAM TEXT, computerID INTEGER, " +
                fk_computer + ");");
            execute("CREATE TABLE IF NOT EXISTS PROCESS(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, startTime INTEGER, finishTime INTEGER, allTime INTEGER, computerID INTEGER, userID INTEGER, " +
                fk_computer + ", " +
                fk_user + ";");
            execute("CREATE TABLE IF NOT EXISTS ACTIVITY(id INTEGER PRIMARY KEY AUTOINCREMENT, allTime INTEGER, activeTime INTEGER, passiveTime INTEGER, computerID INTEGER, userID INTEGER, " +
                fk_computer + ", " +
                fk_user + ";");
            //}
            //catch (Exception ex)
            //{

            //}
        }

        //public DataTable getThemeTable()
        //{
        //    if (!Directory.Exists(diskLetter + @"pctesting"))
        //        Directory.CreateDirectory(diskLetter + @"pctesting");
        //    if (!File.Exists(diskLetter + @"pctesting\mydb.sqlite"))
        //        SQLiteConnection.CreateFile(diskLetter + @"pctesting\mydb.sqlite");
        //    sql.Open();
        //    setConnection();
        //    SQLiteCommand sc = new SQLiteCommand("SELECT NAME FROM THEME ORDER BY ID;", sql);
        //    DataTable dt = new DataTable();
        //    dt.Load(sc.ExecuteReader());
        //    sql.Close();
        //    return dt;
        //}

        public DataTable getTrafficTable()
        {
            if (!Directory.Exists(diskLetter + @"pctesting"))
                Directory.CreateDirectory(diskLetter + @"pctesting");
            if (!File.Exists(diskLetter + @"pctesting\mydb.sqlite"))
                SQLiteConnection.CreateFile(diskLetter + @"pctesting\mydb.sqlite");
            sql.Open();
            setConnection();
            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM TRAFFIC ORDER BY ID;", sql);
            DataTable dt = new DataTable();
            dt.Load(sc.ExecuteReader());
            sql.Close();
            return dt;
        }

        //public DataTable getTTTable()
        //{
        //    if (!Directory.Exists(diskLetter + @"pctesting"))
        //        Directory.CreateDirectory(diskLetter + @"pctesting");
        //    if (!File.Exists(diskLetter + @"pctesting\mydb.sqlite"))
        //        SQLiteConnection.CreateFile(diskLetter + @"pctesting\mydb.sqlite");
        //    sql.Open();
        //    setConnection();
        //    SQLiteCommand sc = new SQLiteCommand("SELECT * FROM TRAFFIC_THEME;", sql);
        //    DataTable dt = new DataTable();
        //    dt.Load(sc.ExecuteReader());
        //    sql.Close();
        //    return dt;
        //}

        private int[] selectIDs(string comp, string user)
        {
            using (SQLiteConnection sql = new SQLiteConnection("DataSource = " + diskLetter + @"pctesting\mydb.sqlite;Version=3"))
            {
                sql.Open();
                SQLiteCommand scComp = new SQLiteCommand("SELECT ID FROM COMPUTER WHERE MAC = '" + comp + "';", sql);
                SQLiteCommand scUser = new SQLiteCommand("SELECT ID FROM USER WHERE NAME = '" + user + "';", sql);
                return new int[] { Convert.ToInt32(scComp.ExecuteScalar()), Convert.ToInt32(scUser.ExecuteScalar()) };
            }
        }

        public void saveFileDataToDB(string name, string path, string ext, long time, string type, string comp, string user)
        {
            sql.Open();
            int[] IDs = selectIDs(comp, user);
            execute(String.Format("INSERT INTO FILE VALUES( NULL, '{0}', '{1}', '{2}', {3}, '{4}', {5}, {6});", name, path, ext, time, type, IDs[0], IDs[1]));
            sql.Close();
        }

        public void saveTrafficDataToDB(string URL, string host, string referer, long time, string comp, string user)
        {
            SQLiteCommand sc;
            sql.Open();
            int[] IDs = selectIDs(comp, user);
            int refId = 0;
            if (referer.Length > 0)
            {
                sc = new SQLiteCommand("SELECT ID FROM TRAFFIC WHERE URL = '" + referer + "' AND TIME IN(SELECT MAX(TIME) FROM TRAFFIC WHERE URL = '" + referer + "');", sql);
                refId = Convert.ToInt32(sc.ExecuteScalar());
                sc = new SQLiteCommand("SELECT MAX(TIME) FROM TRAFFIC WHERE URL = '" + referer + "';", sql);
                if (refId == 0)
                    return;
            }
            string Parameters = "cat=ling-themurl&url=" + URL;
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            string resp = GetResponse("http://www.keva.ru/?cat=ling-themurl", bytes, "POST");
            IHtmlDocument angle = new HtmlParser().Parse(resp);
            IHtmlCollection<IElement> themeCol = angle.QuerySelectorAll("table ul.tree > li a");
            IHtmlCollection<IElement> valCol = angle.QuerySelectorAll("table ul.tree > li a sup");
            string[] theme = new string[themeCol.Length], val = new string[themeCol.Length];

            //sc = new SQLiteCommand("SELECT MAX(ID) FROM TRAFFIC;", sql);
            //int traffId = Convert.ToInt32(sc.ExecuteScalar());
            for (int i = 0; i < themeCol.Length; i++)
            {
                //val[i] = valCol[i].TextContent;
                theme[i] = themeCol[i].TextContent.Remove(themeCol[i].TextContent.IndexOf(val[i]));
                //sc = new SQLiteCommand("SELECT COUNT(*) FROM THEME WHERE NAME = '" + theme[i] + "';", sql);
                //if (Convert.ToInt32(sc.ExecuteScalar()) > 0)
                //{
                //    sc = new SQLiteCommand("SELECT ID FROM THEME WHERE NAME = '" + theme[i] + "';", sql);
                //}
                //else
                //{
                //    execute(String.Format("INSERT INTO THEME VALUES( NULL, '{0}');", theme[i]));
                //    sc = new SQLiteCommand("SELECT MAX(ID) FROM THEME;", sql);
                //}
                //int themeId = Convert.ToInt32(sc.ExecuteScalar());
                //execute(String.Format("INSERT INTO TRAFFIC_THEME VALUES('{0}', {1}, {2});", val[i], traffId, themeId));
                //sql.Close();
                execute(String.Format("INSERT INTO TRAFFIC VALUES( NULL, '{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, '{7}');", URL, host, referer, time, refId, IDs[0], IDs[1], theme[i]));
                return;
            }

        }

        public void SaveActivityToDB(TimeSpan AllTime, TimeSpan ActivityTime, TimeSpan NotActivityTime, string comp, string user)
        {
            sql.Open();
            int[] IDs = selectIDs(comp, user);
            execute("INSERT INTO ACTIVITY VALUES(NULL, " + (long)AllTime.Ticks / 10000 + ", " + (long)ActivityTime.Ticks / 10000 + ", " + (long)NotActivityTime.Ticks / 10000 + ", " + IDs[0] + ", " + IDs[1] + ");");
            sql.Close();
        }

        public void SaveProcessesToDB(string Name, DateTime StartTime, DateTime FinishTime, TimeSpan GeneralTime, string comp, string user)
        {
            sql.Open();
            int[] IDs = selectIDs(comp, user);
            execute("INSERT INTO PROCESS VALUES( NULL, '" + Name + "', " + (long)StartTime.Ticks / 10000 + ", " + (long)FinishTime.Ticks / 10000 + ", " + (long)GeneralTime.Ticks / 10000 + "," + IDs[0] + "," + IDs[1] + ");");
            sql.Close();
        }

        public void SaveTestCharacteristic(DateTime time, int teapots, string RAM, string freeRAM, string CPU, string VideoRAM, string comp, string user)
        {
            sql.Open();
            int[] IDs = selectIDs(comp, user);
            execute("INSERT INTO CHARACTERISTIC VALUES( NULL, " + (long)time.Ticks / 10000 + ", " + teapots + ", '" + RAM + "', '" + freeRAM + "', '" + CPU + "', '" + VideoRAM + "', " + IDs[0] + ");");
            sql.Close();
        }

        private string GetResponse(string url, byte[] byteData, string method)
        {
            if (url == null) return null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.56 Safari/535.11";
            request.Method = method;
            if (byteData.Length > 0)
            {
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteData.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(byteData, 0, byteData.Length);
                }
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                Encoding responseEncoding = Encoding.Default;
                if (!String.IsNullOrEmpty(response.CharacterSet))
                {
                    responseEncoding = Encoding.GetEncoding(response.CharacterSet);
                }
                using (var reader = new StreamReader(responseStream, responseEncoding, true))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
