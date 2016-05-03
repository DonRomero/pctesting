using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp;
using System.IO;
using iTextSharp.text.pdf;
using System.Data.SQLite;
using System.Data;
using System.Drawing;


namespace MyService
{
    class Report
    {
        static string root;
        SQLiteConnection sql;

        public Report(string str)
        {
            root = str;
            sql = new SQLiteConnection("DataSource = " + root + @"pctesting\mydb.sqlite;Version=3");
        }

        public void makeReport()
        {
            sql.Open();
            SQLiteCommand sc = new SQLiteCommand("SELECT NAME FROM USER;", sql);
            SQLiteDataReader reader = sc.ExecuteReader();
            while(reader.Read())
            {
                string name = reader.GetString(0);
                makeTrafficReport(name, "user");
                makeFileReport(name, "user");
            }
            sc = new SQLiteCommand("SELECT NAME FROM COMPUTER;", sql);
            reader = sc.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString(0);
                makeTrafficReport(name, "computer");
                makeFileReport(name, "computer");
            }
            sql.Close();
        }

        private void makeTrafficReport(string name, string type)
        {
            SQLiteCommand sc = new SQLiteCommand("");
            switch(type)
            {
                case "computer":
                    sc = new SQLiteCommand("SELECT URL, TIME, USERID FROM TRAFFIC T JOIN COMPUTER C ON T.COMPUTERID = C.ID WHERE C.NAME = '" + name + "';", sql);
                    break;
                case "user":
                    sc = new SQLiteCommand("SELECT URL, TIME, COMPUTERID FROM TRAFFIC T JOIN USER U ON T.USERID = U.ID WHERE U.NAME = '" + name + "' AND ADMIN NOT LIKE 1;", sql);
                    break;
            }
            SQLiteDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            sdr.Close();
            var doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(root + @"pctesting\traffic.pdf", FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph(name));
            PdfPTable table = new PdfPTable(dt.Columns.Count);
            PdfPCell cell = new PdfPCell(new Phrase("URL"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Время"));
            table.AddCell(cell);
            switch (type)
            {
                case "computer":
                    cell = new PdfPCell(new Phrase("Пользователь"));
                    table.AddCell(cell);
                    break;
                case "user":
                    cell = new PdfPCell(new Phrase("Компьютер"));
                    table.AddCell(cell);
                    break;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                table.AddCell(dt.Rows[i][0].ToString());
                table.AddCell(new DateTime(Convert.ToInt64(dt.Rows[i][1])).ToString());
                table.AddCell(dt.Rows[i][2].ToString());
            }
            doc.Add(table);
            doc.Close();
        }

        private void makeFileReport(string name, string type)
        {
            SQLiteCommand sc = new SQLiteCommand("");
            switch (type)
            {
                case "computer":
                    sc = new SQLiteCommand("SELECT NAME, PATH, TIME, TYPE, USERID FROM TRAFFIC T JOIN COMPUTER C ON T.COMPUTERID = C.ID WHERE C.NAME = '" + name + "';", sql);
                    break;
                case "user":
                    sc = new SQLiteCommand("SELECT NAME, PATH, TIME, TYPE, COMPUTERID FROM TRAFFIC T JOIN USER U ON T.USERID = U.ID WHERE U.NAME = '" + name + "' AND ADMIN NOT LIKE 1;", sql);
                    break;
            }
            SQLiteDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            sdr.Close();
            var doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(root + @"pctesting\file.pdf", FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph(name));
            PdfPTable table = new PdfPTable(dt.Columns.Count);
            PdfPCell cell = new PdfPCell(new Phrase("Имя"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Путь"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Время"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Тип"));
            table.AddCell(cell);
            switch (type)
            {
                case "computer":
                    cell = new PdfPCell(new Phrase("Пользователь"));
                    table.AddCell(cell);
                    break;
                case "user":
                    cell = new PdfPCell(new Phrase("Компьютер"));
                    table.AddCell(cell);
                    break;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                table.AddCell(dt.Rows[i][0].ToString());
                table.AddCell(dt.Rows[i][1].ToString());
                table.AddCell(new DateTime(Convert.ToInt64(dt.Rows[i][2])).ToString());
                switch(dt.Rows[i][3].ToString())
                {
                    case "Renamed":
                        table.AddCell("Переименован");
                        break;
                    case "Changed":
                        table.AddCell("Изменён");
                        break;
                    case "Created":
                        table.AddCell("Создан");
                        break;
                    case "Deleted":
                        table.AddCell("Удалён");
                        break;
                }
                table.AddCell(dt.Rows[i][4].ToString());
            }
            doc.Add(table);
            doc.Close();
        }
    }
}
