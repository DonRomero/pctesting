using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Data;
using System.Web;

namespace MyService
{
    public class UserReport
    {
        static string root;
        SQLiteConnection sql = new SQLiteConnection("DataSource = " + root + @"mydb.sqlite;Version=3");
        SQLiteCommand sc = new SQLiteCommand("");
        public UserReport(string str)
        {
            root = str;
        }
        public List<List<string>> FindFileActivity(string UserName)
        {
            sql.Open();
            List<List<string>> Frame = new List<List<string>>();
            sc = new SQLiteCommand("SELECT F.NAME, TIME FROM FILE F JOIN USER U ON F.USERID = U.ID WHERE U.NAME = '" + UserName + "';", sql);
            SQLiteDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            Random rand = new Random();
            int begin = 0;
            bool next = false;
            try
            {
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        if (dt.Rows[i][0].ToString() != dt.Rows[i + 1][0].ToString())
                        {
                            next = false;
                            Frame.Add(new List<string>());
                            Frame[Frame.Count - 1].Add(UserName);
                            Frame[Frame.Count - 1].Add("Работал с файлом: ");
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (j == 0)
                                {
                                    Frame[Frame.Count - 1][1] += dt.Rows[i][j].ToString();
                                }
                                if (j == 1)
                                {
                                    Frame[Frame.Count - 1].Add(rand.Next(10000, 20000).ToString());
                                    Frame[Frame.Count - 1].Add(new DateTime(Convert.ToInt64(dt.Rows[i][j]) * 10000).ToString());
                                    Frame[Frame.Count - 1].Add(new DateTime(Convert.ToInt64(dt.Rows[i][j]) * 10000).ToString());
                                }
                            }
                        }
                        else
                        {
                            if (!next)
                            {
                                Frame.Add(new List<string>());                                                   
                                Frame[Frame.Count - 1].Add(UserName);
                                Frame[Frame.Count - 1].Add("Работал с файлом: ");
                                Frame[Frame.Count - 1][1] += dt.Rows[i][0].ToString();
                                Frame[Frame.Count - 1].Add("");
                                Frame[Frame.Count - 1].Add(new DateTime(Convert.ToInt64(dt.Rows[i][1]) * 10000).ToString());
                                Frame[Frame.Count - 1].Add(new DateTime(Convert.ToInt64(dt.Rows[i][1]) * 10000).ToString());
                                next=true;
                                begin = i;
                            }
                             Frame[Frame.Count - 1][2] = ((Convert.ToInt64(dt.Rows[i][1]) * 10000) - (Convert.ToInt64(dt.Rows[begin][1]) * 10000)).ToString();
                             Frame[Frame.Count - 1][4] = new DateTime(Convert.ToInt64(dt.Rows[i][1]) * 10000).ToString();
                        }                        
                    }
                }
                return Frame;
            }
            catch(Exception e)
            {
                return new List<List<string>>();
            }
        }

        public List<List<string>> FindInternetActivity(string UserName)
        {
            sql.Open();
            List<List<string>> Frame = new List<List<string>>();
            sc=new SQLiteCommand("SELECT TIME FROM TRAFFIC T JOIN USER U ON T.USERID = U.ID WHERE U.NAME = '" + UserName + "';", sql);
            SQLiteDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            int begin = 0;
            dt.Load(sdr);
            try
            {
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count - 2; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if ((Convert.ToInt64(dt.Rows[i + 1][j]) - Convert.ToInt64(dt.Rows[i][j])) > 80000)
                            {
                                Frame.Add(new List<string>());
                                Frame[Frame.Count - 1].Add(UserName);
                                Frame[Frame.Count - 1].Add("Использовал интернет");
                                Frame[Frame.Count - 1].Add(((Convert.ToInt64(dt.Rows[i][j]) * 10000) - (Convert.ToInt64(dt.Rows[begin][j]) * 10000)).ToString());
                                Frame[Frame.Count - 1].Add(new DateTime(Convert.ToInt64(dt.Rows[begin][j]) * 10000).ToString());
                                Frame[Frame.Count - 1].Add(new DateTime(Convert.ToInt64(dt.Rows[i][j]) * 10000).ToString());
                                begin = i+1;
                            }

                        }
                    }

                    Frame.Add(new List<string>());
                    Frame[Frame.Count - 1].Add(UserName);
                    Frame[Frame.Count - 1].Add("Использовал интернет");
                    Frame[Frame.Count - 1].Add(((Convert.ToInt64(dt.Rows[dt.Rows.Count - 1][dt.Columns.Count - 1]) * 10000) - (Convert.ToInt64(dt.Rows[begin][dt.Columns.Count - 1]) * 10000)).ToString());
                    Frame[Frame.Count - 1].Add(new DateTime(Convert.ToInt64(dt.Rows[begin][dt.Columns.Count - 1]) * 10000).ToString());
                    Frame[Frame.Count - 1].Add(new DateTime(Convert.ToInt64(dt.Rows[dt.Rows.Count - 1][dt.Columns.Count - 1]) * 10000).ToString());
                }
                sql.Close();
                return Frame;
            }
            catch(Exception e)
            {
                return new List<List<string>>();
            }

        }
        private SQLiteDataReader execute(string query)
        {
            SQLiteCommand sc = new SQLiteCommand(query, sql);
            return sc.ExecuteReader();
        }
    }
}