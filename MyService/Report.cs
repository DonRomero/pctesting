using System;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace MyService
{
    class Report
    {
        static string root;
        SQLiteConnection sql = new SQLiteConnection("DataSource = " + root + @"mydb.sqlite;Version=3");
        //Excel.Application excelapp = new Excel.Application();
        int book = 0;

        public Report(string str)
        {
            root = str;
        }

        private void report(SQLiteDataReader reader, string subject)
        {
            while (reader.Read())
            {
                string name = reader.GetString(0);
                if (!Directory.Exists(root + @"report\" + subject + "\\" + name))
                    Directory.CreateDirectory(root + @"report\" + subject + "\\" + name);
                makeReport(name, subject, "TRAFFIC");
                makeReport(name, subject, "FILE");
                makeReport(name, subject, "PROCESS");
                makeReport(name, subject, "ACTIVITY");
            }
        }

        private SQLiteDataReader execute(string query)
        {
            SQLiteCommand sc = new SQLiteCommand(query, sql);
            return sc.ExecuteReader();
        }

        public void makeReport()
        {
            sql.Open();
            report(execute("SELECT NAME FROM USER WHERE ADMIN NOT LIKE 1;"), "user");
            report(execute("SELECT NAME FROM COMPUTER;"), "computer");
            sql.Close();
            //excelapp.Quit();
        }

        private void makeReport(string name, string subject, string table)
        {
            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            SQLiteCommand sc = new SQLiteCommand("");
            List<string> columns = new List<string>();
            Document doc = new Document();
            List<string> TSnames = new List<string> { "Общее время", "Общее время работы", "Время активности", "Время простоя" };
            List<string> chartQuery = new List<string>();
            List<string> chartName = new List<string>();
            List<string> chartX = new List<string>();
            List<string> chartY = new List<string>();
            switch (table)
            {
                case "TRAFFIC":
                    columns.Add("URL");
                    columns.Add("Время");
                    chartX.Add("URL");
                    chartY.Add("COUNT(URL)");
                    switch (subject)
                    {
                        case "computer":
                            sc = new SQLiteCommand("SELECT URL, TIME, USERID FROM TRAFFIC T JOIN COMPUTER C ON T.COMPUTERID = C.ID WHERE C.NAME = '" + name + "';", sql);
                            PdfWriter.GetInstance(doc, new FileStream(root + @"report\computer\" + name + @"\traffic.pdf", FileMode.Create));
                            chartQuery.Add("SELECT URL, COUNT(URL) FROM TRAFFIC T JOIN COMPUTER C ON T.COMPUTERID = C.ID WHERE C.NAME = '" + name + "' GROUP BY URL ORDER BY COUNT(URL) DESC LIMIT 9;");
                            chartName.Add("Самый популярные URL на этом компьютере");
                            break;
                        case "user":
                            sc = new SQLiteCommand("SELECT URL, TIME, COMPUTERID FROM TRAFFIC T JOIN USER U ON T.USERID = U.ID WHERE U.NAME = '" + name + "';", sql);
                            PdfWriter.GetInstance(doc, new FileStream(root + @"report\user\" + name + @"\traffic.pdf", FileMode.Create));
                            chartQuery.Add("SELECT URL, COUNT(URL) FROM TRAFFIC T JOIN USER U ON T.USERID = U.ID WHERE U.NAME = '" + name + "' GROUP BY URL ORDER BY COUNT(URL) DESC LIMIT 9");
                            chartName.Add("Самый популярные URL этого пользователя");
                            break;
                    }
                    break;
                case "FILE":
                    columns.Add("Имя");
                    columns.Add("Путь");
                    columns.Add("Время");
                    columns.Add("Тип");
                    chartX.Add("EXTENSION");
                    chartY.Add("COUNT(EXTENSION)");
                    chartName.Add("Часто используемые форматы файлов");
                    switch (subject)
                    {
                        case "computer":
                            sc = new SQLiteCommand("SELECT F.NAME, PATH, TIME, TYPE, USERID FROM FILE F JOIN COMPUTER C ON F.COMPUTERID = C.ID WHERE C.NAME = '" + name + "';", sql);
                            PdfWriter.GetInstance(doc, new FileStream(root + @"report\computer\" + name + @"\file.pdf", FileMode.Create));
                            chartQuery.Add("SELECT EXTENSION, COUNT(EXTENSION) FROM FILE F JOIN COMPUTER C ON F.COMPUTERID = C.ID WHERE C.NAME = '" + name + "' GROUP BY EXTENSION ORDER BY COUNT(EXTENSION) DESC LIMIT 9;");
                            break;
                        case "user":
                            sc = new SQLiteCommand("SELECT F.NAME, PATH, TIME, TYPE, COMPUTERID FROM FILE F JOIN USER U ON F.USERID = U.ID WHERE U.NAME = '" + name + "';", sql);
                            PdfWriter.GetInstance(doc, new FileStream(root + @"report\user\" + name + @"\file.pdf", FileMode.Create));
                            chartQuery.Add("SELECT EXTENSION, COUNT(EXTENSION) FROM FILE F JOIN USER U ON F.USERID = U.ID WHERE U.NAME = '" + name + "' GROUP BY EXTENSION ORDER BY COUNT(EXTENSION) DESC LIMIT 9");
                            break;
                    }
                    break;
                case "PROCESS":
                    columns.Add("Имя");
                    columns.Add("Время начала");
                    columns.Add("Время окончания");
                    columns.Add("Общее время");
                    switch (subject)
                    {
                        case "computer":
                            sc = new SQLiteCommand("SELECT P.NAME, STARTTIME, FINISHTIME, P.ALLTIME, USERID FROM PROCESS P JOIN COMPUTER C ON P.COMPUTERID = C.ID WHERE C.NAME='" + name + "';", sql);
                            PdfWriter.GetInstance(doc, new FileStream(root + @"report\computer\" + name + @"\process.pdf", FileMode.Create));
                            break;
                        case "user":
                            sc = new SQLiteCommand("SELECT P.NAME, STARTTIME, FINISHTIME, P.ALLTIME, COMPUTERID FROM PROCESS P JOIN USER U ON P.USERID = U.ID WHERE U.NAME = '" + name + "';", sql);
                            PdfWriter.GetInstance(doc, new FileStream(root + @"report\user\" + name + @"\process.pdf", FileMode.Create));
                            break;
                    }
                    break;
                case "ACTIVITY":
                    columns.Add("Общее время работы");
                    columns.Add("Время активности");
                    columns.Add("Время простоя");
                    switch (subject)
                    {
                        case "computer":
                            sc = new SQLiteCommand("SELECT A.ALLTIME, ACTIVETIME,PASSIVETIME, USERID FROM ACTIVITY A JOIN COMPUTER C ON A.COMPUTERID = C.ID WHERE C.NAME='" + name + "';", sql);
                            PdfWriter.GetInstance(doc, new FileStream(root + @"report\computer\" + name + @"\activity.pdf", FileMode.Create));
                            break;
                        case "user":
                            sc = new SQLiteCommand("SELECT A.ALLTIME, ACTIVETIME,PASSIVETIME ,COMPUTERID FROM ACTIVITY A JOIN USER U ON A.USERID = U.ID WHERE U.NAME = '" + name + "';", sql);
                            PdfWriter.GetInstance(doc, new FileStream(root + @"report\user\" + name + @"\activity.pdf", FileMode.Create));
                            break;
                    }
                    break;
            }
            switch (subject)
            {
                case "computer":
                    columns.Add("Пользователь");
                    break;
                case "user":
                    columns.Add("Компьютер");
                    break;
            }
            SQLiteDataReader sdr = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            sdr.Close();
            doc.Open();
            PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(columns[i], font));
                pdfTable.AddCell(cell);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string phrase = "";
                    if (j == dt.Columns.Count - 1)
                    {
                        SQLiteDataReader reader = execute("");
                        switch (columns[j])
                        {
                            case "Пользователь":
                                reader = execute("SELECT NAME FROM USER WHERE ID = " + dt.Rows[i][j].ToString() + ";");
                                break;
                            case "Компьютер":
                                reader = execute("SELECT NAME FROM COMPUTER WHERE ID = " + dt.Rows[i][j].ToString() + ";");
                                break;
                        }
                        while (reader.Read())
                            phrase = reader.GetString(0);
                    }
                    else
                    {
                        if (table.Equals("FILE") && columns[j].Equals("Тип"))
                        {
                            switch (dt.Rows[i][j].ToString())
                            {
                                case "Renamed":
                                    phrase = "Переименован";
                                    break;
                                case "Changed":
                                    phrase = "Изменён";
                                    break;
                                case "Created":
                                    phrase = "Создан";
                                    break;
                                case "Deleted":
                                    phrase = "Удалён";
                                    break;
                            }
                        }
                        else
                        {
                            if (TSnames.Exists(n => n == columns[j]))
                            {
                                phrase = new TimeSpan(Convert.ToInt64(dt.Rows[i][j]) * 10000).ToString();
                            }
                            else
                            {
                                if (columns[j].Equals("Время") || columns[j].Equals("Время начала") || columns[j].Equals("Время окончания"))
                                    phrase = new DateTime(Convert.ToInt64(dt.Rows[i][j]) * 10000).ToString();
                                else
                                    phrase = dt.Rows[i][j].ToString();
                            }
                        }
                    }
                    PdfPCell cell = new PdfPCell(new Phrase(phrase, font));
                    pdfTable.AddCell(cell);
                }
            }
            for (int i = 0; i < chartName.Count; i++)
            {
                makeChart(chartQuery[i], chartX[i], chartY[i]);
                doc.Add(new Phrase(chartName[i]));
                Image chart = Image.GetInstance(root + @"chart.bmp");
                doc.Add(chart);
            }
            doc.Add(new Phrase(" "));
            doc.Add(pdfTable);
            doc.Close();
        }

        private void makeChart(string query, string xName, string yName)
        {
            Chart chart = new Chart();
            DataTable dt = new DataTable();
            ChartArea ChartArea1 = new ChartArea();
            chart.Series.Add("Series1");
            chart.ChartAreas.Add("ChartArea1");
            //chart.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
            chart.Size = new System.Drawing.Size(500, 500);
            chart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 6F, System.Drawing.FontStyle.Regular);
            chart.Series["Series1"].ChartType = SeriesChartType.Column;
            //chart.Series["Series1"]["DrawingStyle"] = "Emboss";
            chart.Series["Series1"].IsValueShownAsLabel = true;
            dt.Load(execute(query));
            chart.DataSource = dt;
            chart.Series["Series1"].YValueMembers = yName;
            chart.Series["Series1"].XValueMember = xName;
            chart.DataBind();
            chart.SaveImage(root + @"chart.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            ////excelapp.SheetsInNewWorkbook = 3;
            //excelapp.Workbooks.Add(Type.Missing);
            //Excel.Sheets excelsheets = excelapp.Workbooks[++book].Worksheets;
            //DataTable dt = new DataTable();
            //dt.Load(execute(query));
            //Excel.Worksheet excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
            //excelworksheet.Activate();
            //Excel.Range excelcells;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        excelcells = excelworksheet.Cells[j + 1][i + 1];
            //        excelcells.Value2 = dt.Rows[i][j];
            //    }
            //}
            //excelcells = excelworksheet.get_Range("A1", "B" + Math.Max(dt.Rows.Count, 1));
            //excelcells.Select();
            //Excel.Chart excelchart = (Excel.Chart)excelapp.Charts.Add(Type.Missing,
            //    Type.Missing, Type.Missing, Type.Missing);
            //excelchart.Activate();
            //excelchart.Select(Type.Missing);
            //excelapp.ActiveChart.ChartType = Excel.XlChartType.xlColumnClustered;

            //excelapp.ActiveChart.HasTitle = true;
            //excelapp.ActiveChart.ChartTitle.Text = name;

            //excelapp.ActiveChart.ChartTitle.Font.Size = 13;

            //excelapp.ActiveChart.ChartTitle.Shadow = true;
            //excelapp.ActiveChart.ChartTitle.Border.LineStyle = Excel.Constants.xlSolid;

            //Excel.SeriesCollection seriesCollection = (Excel.SeriesCollection)excelapp.ActiveChart.SeriesCollection(Type.Missing);
            //Excel.Series series = seriesCollection.Item(1);

            //series.XValues = excelworksheet.get_Range("A1", "A" + Math.Max(dt.Rows.Count, 1));
            //excelapp.ActiveChart.Export(root + @"chart.bmp", "BMP", Type.Missing);
            //excelapp.Workbooks[book].Saved = true; ;
        }
    }
}
