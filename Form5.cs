using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

namespace prak
{
    public partial class Form5 : Form
    {
        Button bt1;
        ComboBox cb1;
        Chart ch1;
        Label lb1;
        CultureInfo cultureInfo = CultureInfo.InvariantCulture;
        public Form5()
        {
            InitializeComponent();

            this.Width = 600;
            this.Height = 350;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;//фиксируем размер форму
            this.MaximizeBox = false;
            this.Text = "График продаж";

            lb1 = new Label();
            lb1.Text = "Выберите месяц";
            lb1.Location = new Point(10, 90);
            lb1.Height = 30;
            lb1.TextAlign = ContentAlignment.TopCenter;
            this.Controls.Add(lb1);

            cb1 = new ComboBox();
            cb1.Location = new Point(10, 120);
            cb1.Width = 100;
            cb1.Height = 20;
            cb1.Items.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);
            cb1.DataSource = DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12).ToArray();
            this.Controls.Add(cb1);

            ch1 = new Chart();
            ch1.Location = new Point(120, 10);
            ch1.Width = 450;
            ch1.Height = 290;
            ch1.Series.Add("Series1");
            ch1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            ch1.Series["Series1"].Color = Color.Crimson;
            ChartArea chA = new ChartArea();
            ch1.ChartAreas.Add(chA);
            this.Controls.Add(ch1);
            ch1.Show();

            bt1 = new Button();
            bt1.Location = new Point(10, 150);
            bt1.Width = 100;
            bt1.Height = 30;
            bt1.Text = "Построить";
            bt1.Click += new EventHandler(this.click_b);//событие нажатия
            this.Controls.Add(bt1);

        }

        //событие нажатя кнопки
        private void click_b(object sender, EventArgs ev)
        {
            int mn1 = cb1.SelectedIndex + 1;
            ch1.Titles.Clear();
            ch1.Titles.Add(cb1.GetItemText(cb1.SelectedItem));
            DateTime dt1 = new DateTime();
            try
            {
                StreamReader streamReader = new StreamReader("..\\..\\files\\receipt.txt", Encoding.UTF8);
                string str;
                double[] days = new double[DateTime.DaysInMonth(2021, mn1)];
                while ((str = streamReader.ReadLine()) != null)
                {
                    string[] strN = str.Split(';');
                    dt1 = DateTime.ParseExact(strN[1], "dd.MM.yyyy", cultureInfo);
                    int mn2 = dt1.Month;
                    if (mn1.CompareTo(mn2) == 0)//сравниваем введенный месяц и месяц из файла
                    {
                        days[dt1.Day - 1] += Convert.ToDouble(strN[3]);
                    }
                    else if (mn2 < mn1)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                streamReader.Close();
                ch1.Series[0].Points.Clear();
                //Добавляем точки на график
                foreach (int val in days)
                    ch1.Series["Series1"].Points.Add(val);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}