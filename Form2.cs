using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prak
{
    public partial class Form2 : Form
    {
        DataGridView dataGridView1;
        public Form2(List<string>[]list, int index)//массив списков, размер массива
        {
            InitializeComponent();
            this.Text = "";
            
            dataGridView1 = new DataGridView();
            dataGridView1.Dock = DockStyle.Fill;//заполняем на всю форму
            dataGridView1.ReadOnly = true;//только чтение
            //создание колонок
            var dataGridViewColumn1 = new DataGridViewColumn();
            dataGridViewColumn1.HeaderText = "Препарат";
            dataGridViewColumn1.Name = "prep";
            dataGridViewColumn1.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn1);

            var dataGridViewColumn2 = new DataGridViewColumn();
            dataGridViewColumn2.HeaderText = "Количество";
            dataGridViewColumn2.Name = "quant";
            dataGridViewColumn2.CellTemplate = new DataGridViewTextBoxCell();
            dataGridViewColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(dataGridViewColumn2);

            bool pr = true;
            int j = 0;
            //заполнение таблицы препаратми из массива списков
            while (pr)
            {
                for (int n = 0; n < (list[index].Count - 1)/2; ++n, j += 2)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["prep"].Value = list[index].ElementAt(j);
                    dataGridView1.Rows[n].Cells["quant"].Value = list[index].ElementAt(j + 1);
                }
                pr = false;
            }
            this.Controls.Add(dataGridView1);
        }
    }
}
