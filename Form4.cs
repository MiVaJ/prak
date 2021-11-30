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

namespace prak
{
    public partial class Form4 : Form
    {
        TextBox tb1;
        Button bt1;

        DataGridView dataGridView1;


        public Form4()
        {
            InitializeComponent();

            this.Width = 1000;
            this.Height = 110;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;//фиксируем размер окна
            this.MaximizeBox = false;
            this.Text = "Поиск препарата по коду или названию";

            tb1 = new TextBox();
            tb1.Location = new Point(10, 10);
            tb1.Width = 150;
            this.Controls.Add(tb1);

            bt1 = new Button();
            bt1.Location = new Point(10, 39);
            bt1.Text = "Поиск";
            bt1.Width = 151;
            bt1.Height = 23;
            bt1.Click += new EventHandler(bt_click);//событие нажатия кнопки
            this.Controls.Add(bt1);
            
            dataGridView1 = new DataGridView();
            dataGridView1.Location = new Point(170, 10);
            dataGridView1.Width = 800;
            dataGridView1.Height = 52;
            dataGridView1.ReadOnly = true;//только чтение
            dataGridView1.RowHeadersVisible = false;//убираем пустую колонку
            dataGridView1.AllowUserToAddRows = false;//запрещаем добавлять данные
            //создаем колонки
            var dataGridViewColumn1 = new DataGridViewColumn();
            dataGridViewColumn1.HeaderText = "Код препарата";
            dataGridViewColumn1.Name = "code";
            dataGridViewColumn1.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn1);

            var dataGridViewColumn2 = new DataGridViewColumn();
            dataGridViewColumn2.HeaderText = "Наименование";
            dataGridViewColumn2.Name = "name";
            dataGridViewColumn2.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn2);

            var dataGridViewColumn3 = new DataGridViewColumn();
            dataGridViewColumn3.HeaderText = "Остаток";
            dataGridViewColumn3.Name = "quantity";
            dataGridViewColumn3.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn3);

            var dataGridViewColumn4 = new DataGridViewColumn();
            dataGridViewColumn4.HeaderText = "Группа";
            dataGridViewColumn4.Name = "group";
            dataGridViewColumn4.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn4);

            var dataGridViewColumn5 = new DataGridViewColumn();
            dataGridViewColumn5.HeaderText = "Код группы";
            dataGridViewColumn5.Name = "codeGr";
            dataGridViewColumn5.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn5);

            var dataGridViewColumn6 = new DataGridViewColumn();
            dataGridViewColumn6.HeaderText = "Стоимость";
            dataGridViewColumn6.Name = "price";
            dataGridViewColumn6.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn6);

            var dataGridViewColumn7 = new DataGridViewColumn();
            dataGridViewColumn7.HeaderText = "Страна производства";
            dataGridViewColumn7.Name = "country";
            dataGridViewColumn7.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn7);

            var dataGridViewColumn8 = new DataGridViewColumn();
            dataGridViewColumn8.HeaderText = "Продано";
            dataGridViewColumn8.Name = "sellQ";
            dataGridViewColumn8.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn8.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn8);

            dataGridView1.ColumnHeadersHeight = 26;
            this.Controls.Add(dataGridView1);

        }
        //событие нажатя кнопки
        public void bt_click(object sender, EventArgs eventArgs)
        {
            dataGridView1.Rows.Clear();
            string s = tb1.Text;
            try
            {
                StreamReader streamReader = new StreamReader("..\\..\\files\\data.txt", Encoding.UTF8);
                string str;
                int row = 0;
                while ((str = streamReader.ReadLine()) != null)
                {
                    
                    string[] strN = str.Split(';');
                    if (s.CompareTo(strN[1]) == 0 || s.CompareTo(strN[0]) == 0)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[0].Height = 26;
                        dataGridView1.Rows[row].Cells["code"].Value = strN[0];
                        dataGridView1.Rows[row].Cells["name"].Value = strN[1];
                        dataGridView1.Rows[row].Cells["quantity"].Value = strN[2];
                        dataGridView1.Rows[row].Cells["group"].Value = strN[3];
                        dataGridView1.Rows[row].Cells["codeGr"].Value = strN[4];
                        dataGridView1.Rows[row].Cells["price"].Value = strN[5];
                        dataGridView1.Rows[row].Cells["country"].Value = strN[6];
                        dataGridView1.Rows[row].Cells["sellQ"].Value = strN[7];
                        ++row;
                        break;
                    }
                }
                streamReader.Close();    
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
