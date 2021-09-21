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
    public partial class Form1 : Form
    {
        MainMenu mainMenu1;
        MenuItem menuItem1;
        TabControl tabControl;
        TabPage tabPage1, tabPage2, tabPage3;
        DataGridView dataGridView1, dataGridView2, dataGridView3;
        List<string>[] recList;

        public Form1()
        {
            InitializeComponent();
            this.Width = 750;
            this.Text = "Таблицы";

            CreateMainMenu();//создание главного меню
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            CreateTablePrep();//создание таблицы Препараты
            CreateTableZak();
            CreateTableSel();
            this.Controls.Add(tabControl);
            tabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_F2);

        }
        private void CreateMainMenu()
        {
            mainMenu1 = new MainMenu();

            menuItem1 = new MenuItem("Запросы");
            menuItem1.MenuItems.Add("Закупки по датам", new EventHandler(ZakPoDate));
            menuItem1.MenuItems.Add("Поиск препарата по коду или названию", new EventHandler(Poisk));
            mainMenu1.MenuItems.Add(menuItem1);

            this.Menu = mainMenu1;
        }
        private void CreateTablePrep()
        {
            dataGridView1 = new DataGridView();
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
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

            //заполнение таблицы
            try
            {
                //StreamReader streamReader = new StreamReader(@"C:\Users\shage\Desktop\c#\prak\files\data.txt", Encoding.UTF8);
                StreamReader streamReader = new StreamReader("..\\..\\files\\data.txt", Encoding.UTF8);
                string str;
                int row = 0;
                while ((str = streamReader.ReadLine()) != null)
                {
                    dataGridView1.Rows.Add();
                    string[] strN = str.Split(';');
                    dataGridView1.Rows[row].Cells["code"].Value = strN[0];
                    dataGridView1.Rows[row].Cells["name"].Value = strN[1];
                    dataGridView1.Rows[row].Cells["quantity"].Value = strN[2];
                    dataGridView1.Rows[row].Cells["group"].Value = strN[3];
                    dataGridView1.Rows[row].Cells["codeGr"].Value = strN[4];
                    dataGridView1.Rows[row].Cells["price"].Value = strN[5];
                    dataGridView1.Rows[row].Cells["country"].Value = strN[6];
                    dataGridView1.Rows[row].Cells["sellQ"].Value = strN[7];
                    ++row;
                }
                streamReader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            //добавляем страницу TabControl
            tabPage1 = new TabPage("Препараты");
            tabPage1.Controls.Add(dataGridView1);
            tabControl.TabPages.Add(tabPage1);
        }
        private void CreateTableZak()
        {
            dataGridView2 = new DataGridView();
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersVisible = false;

            var dataGridViewColumn1 = new DataGridViewColumn();
            dataGridViewColumn1.HeaderText = "Код договора";
            dataGridViewColumn1.Name = "codeD";
            dataGridViewColumn1.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns.Add(dataGridViewColumn1);

            var dataGridViewColumn2 = new DataGridViewColumn();
            dataGridViewColumn2.HeaderText = "Дата поставки";
            dataGridViewColumn2.Name = "date";
            dataGridViewColumn2.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns.Add(dataGridViewColumn2);

            var dataGridViewColumn3 = new DataGridViewColumn();
            dataGridViewColumn3.HeaderText = "Поставщик";
            dataGridViewColumn3.Name = "supplier";
            dataGridViewColumn3.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns.Add(dataGridViewColumn3);

            var dataGridViewColumn4 = new DataGridViewColumn();
            dataGridViewColumn4.HeaderText = "Телефон";
            dataGridViewColumn4.Name = "phone";
            dataGridViewColumn4.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns.Add(dataGridViewColumn4);

            var dataGridViewColumn5 = new DataGridViewColumn();
            dataGridViewColumn5.HeaderText = "Препарат";
            dataGridViewColumn5.Name = "prep";
            dataGridViewColumn5.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns.Add(dataGridViewColumn5);

            var dataGridViewColumn6 = new DataGridViewColumn();
            dataGridViewColumn6.HeaderText = "Количество";
            dataGridViewColumn6.Name = "quant";
            dataGridViewColumn6.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns.Add(dataGridViewColumn6);

            //заполнение таблицы
            try
            {
                StreamReader streamReader = new StreamReader("..\\..\\files\\zak.txt", Encoding.UTF8);
                string str;
                int row = 0;
                while ((str = streamReader.ReadLine()) != null)
                {
                    dataGridView2.Rows.Add();
                    string[] strN = str.Split(';');
                    dataGridView2.Rows[row].Cells["codeD"].Value = strN[0];
                    dataGridView2.Rows[row].Cells["date"].Value = strN[1];
                    dataGridView2.Rows[row].Cells["supplier"].Value = strN[2];
                    dataGridView2.Rows[row].Cells["phone"].Value = strN[3];
                    dataGridView2.Rows[row].Cells["prep"].Value = strN[4];
                    dataGridView2.Rows[row].Cells["quant"].Value = strN[5];
                    ++row;
                }
                streamReader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            tabPage2 = new TabPage("Закупка");
            tabPage2.Controls.Add(dataGridView2);
            tabControl.TabPages.Add(tabPage2);
        }
        private void CreateTableSel()
        {
            dataGridView3 = new DataGridView();
            dataGridView3.Dock = DockStyle.Fill;
            dataGridView3.ReadOnly = true;
            dataGridView3.RowHeadersVisible = false;

            var dataGridViewColumn1 = new DataGridViewColumn();
            dataGridViewColumn1.HeaderText = "№ чека";
            dataGridViewColumn1.Name = "receipt";
            dataGridViewColumn1.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.Columns.Add(dataGridViewColumn1);

            var dataGridViewColumn2 = new DataGridViewColumn();
            dataGridViewColumn2.HeaderText = "Дата";
            dataGridViewColumn2.Name = "dateOfSale";
            dataGridViewColumn2.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.Columns.Add(dataGridViewColumn2);

            var dataGridViewColumn3 = new DataGridViewColumn();
            dataGridViewColumn3.HeaderText = "Время";
            dataGridViewColumn3.Name = "time";
            dataGridViewColumn3.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.Columns.Add(dataGridViewColumn3);

            var dataGridViewColumn4 = new DataGridViewColumn();
            dataGridViewColumn4.HeaderText = "Сумма";
            dataGridViewColumn4.Name = "sum";
            dataGridViewColumn4.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.Columns.Add(dataGridViewColumn4);
            
            try
            {
                StreamReader streamReader = new StreamReader("..\\..\\files\\receipt.txt", Encoding.UTF8);
                string str;
                int row = 0, i0 = 0;
                int rows = System.IO.File.ReadAllLines("..\\..\\files\\receipt.txt").Length;
                recList = new List<string>[rows];
                while ((str = streamReader.ReadLine()) != null)
                {
                    dataGridView3.Rows.Add();
                    string[] strN = str.Split(';');
                    dataGridView3.Rows[row].Cells["receipt"].Value = strN[0];
                    dataGridView3.Rows[row].Cells["dateOfSale"].Value = strN[1];
                    dataGridView3.Rows[row].Cells["time"].Value = strN[2];
                    dataGridView3.Rows[row].Cells["sum"].Value = strN[3];
                    recList[i0] = new List<string>();
                    for (int i = 4; i < strN.Length; i++) 
                    {
                        recList[i0].Add(strN[i]);
                    }
                    ++i0;
                    ++row;
                }
                streamReader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            tabPage3 = new TabPage("Продажи");
            tabPage3.Controls.Add(dataGridView3);
            tabControl.TabPages.Add(tabPage3);
        }
        //событие нажатия клавиши F2
        private void KeyDown_F2(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.F2 && tabControl.SelectedIndex == 2)
             {
                int index = dataGridView3.CurrentRow.Index;
                Form2 f2 = new Form2(recList, index);
                f2.Show();
            }
        } 
        private void ZakPoDate(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }
        private void Poisk(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }
    }
}
