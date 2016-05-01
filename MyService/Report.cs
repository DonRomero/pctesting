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

        public void makeTrafficReport()
        {
            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM TRAFFIC;", sql);
            sql.Open();
            SQLiteDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            sdr.Close();
            var doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(root + @"pctesting\traffic.pdf", FileMode.Create));
            doc.Open();
            PdfPTable table = new PdfPTable(dt.Columns.Count);
            PdfPCell cell = new PdfPCell(new Phrase("URL"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Время"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Компьютер"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Пользователь"));
            table.AddCell(cell);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                table.AddCell(dt.Rows[i][0].ToString());
                table.AddCell(new DateTime(Convert.ToInt64(dt.Rows[i][1])).ToString());
                SQLiteCommand scComp = new SQLiteCommand("SELECT NAME FROM COMPUTER WHERE ID = " + dt.Rows[i][2].ToString() + ";", sql);
                table.AddCell(scComp.ExecuteScalar().ToString());
                SQLiteCommand scUser = new SQLiteCommand("SELECT NAME FROM USER WHERE ID = " + dt.Rows[i][3].ToString() + ";", sql);
                table.AddCell(scUser.ExecuteScalar().ToString());
            }
            doc.Add(table);
            doc.Close();
            sql.Close();
        }

        public void makeFileReport()
        {

            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM FILE;", sql);
            sql.Open();
            SQLiteDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            sdr.Close();
            var doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(root + @"pctesting\file.pdf", FileMode.Create));
            doc.Open();
            PdfPTable table = new PdfPTable(dt.Columns.Count);
            PdfPCell cell = new PdfPCell(new Phrase("Имя"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Путь"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Время"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Тип"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Компьютер"));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Пользователь"));
            table.AddCell(cell);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                table.AddCell(dt.Rows[i][0].ToString());
                table.AddCell(dt.Rows[i][1].ToString());
                table.AddCell(new DateTime(Convert.ToInt64(dt.Rows[i][2])).ToString());
                //TODO добавить типы работы с файлами
                string type = "";
                switch(dt.Rows[i][3].ToString())
                {
                    case "":
                        break;
                }
                table.AddCell(type);
                SQLiteCommand scComp = new SQLiteCommand("SELECT NAME FROM COMPUTER WHERE ID = " + dt.Rows[i][4].ToString() + ";", sql);
                table.AddCell(scComp.ExecuteScalar().ToString());
                SQLiteCommand scUser = new SQLiteCommand("SELECT NAME FROM USER WHERE ID = " + dt.Rows[i][5].ToString() + ";", sql);
                table.AddCell(scUser.ExecuteScalar().ToString());
            }
            doc.Add(table);
            doc.Close();
            sql.Close();
        }
    }
}
