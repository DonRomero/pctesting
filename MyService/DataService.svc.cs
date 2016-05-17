﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;


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
                execute("CREATE TABLE IF NOT EXISTS TRAFFIC(id INTEGER PRIMARY KEY AUTOINCREMENT, URL TEXT, host TRXT, referer TEXT, time INTEGER, computerID INTEGER, userID INTEGER, " +
                    fk_computer + ", " +
                    fk_user + ";");
                execute("CREATE TABLE IF NOT EXISTS CHARACTERISTIC(id INTEGER PRIMARY KEY AUTOINCREMENT, time TEXT, teapots INTEGER, RAM TEXT, freeRAM TEXT, CPU TEXT, VideoRAM TEXT, computerID INTEGER, " +
                    fk_computer+");");
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
            sql.Open();
            int[] IDs = selectIDs(comp, user);
            execute("INSERT INTO TRAFFIC VALUES( NULL, '" + URL + "', '" + host + "', '" + referer + "'," + time + "," + IDs[0] + "," + IDs[1] + ");");
            sql.Close();
        }
        public void SaveActivityToDB(TimeSpan AllTime, TimeSpan ActivityTime, TimeSpan NotActivityTime, string comp, string user)
        {
            sql.Open();
            int[] IDs = selectIDs(comp, user);
            execute("INSERT INTO ACTIVITY VALUES(NULL, " + (long)AllTime.Ticks/10000 + ", " + (long)ActivityTime.Ticks/10000 + ", " + (long)NotActivityTime.Ticks/10000 + ", " + IDs[0] + ", " + IDs[1] + ");");
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
            int[] IDs = selectIDs(comp,user);
            execute("INSERT INTO CHARACTERISTIC VALUES( NULL, " + (long)time.Ticks / 10000 + ", "+teapots+", '" + RAM + "', '" + freeRAM + "', '" + CPU + "', '" + VideoRAM + "', " + IDs[0] + ");");
            sql.Close();
        }
    }
}
