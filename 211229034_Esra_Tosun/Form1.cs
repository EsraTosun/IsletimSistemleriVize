﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _211229034_Esra_Tosun
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            // the code that you want to measure comes here

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Refresh();
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();

            ThreadCalistir s = new ThreadCalistir();


            s.ThreadleriCalistir();

            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Width = 200; // Tüm sütunlara aynı genişliği atar


            var Count = ThreadCalistir.asalSayilarList.Count;

            for (int rowIndex = 0; rowIndex < Count; ++rowIndex)
            {
                var row = new DataGridViewRow();

                row.Cells.Add(new DataGridViewTextBoxCell()
                {
                    Value = ThreadCalistir.asalSayilarList[rowIndex],
                });

                dataGridView1.Rows.Add(row);

            }

            dataGridView2.ColumnCount = 1;
            dataGridView2.Columns[0].Width = 200; // Tüm sütunlara aynı genişliği atar


            var Count2 = ThreadCalistir.ciftSayilarList.Count;

            for (int rowIndex = 0; rowIndex < Count; ++rowIndex)
            {
                var row = new DataGridViewRow();

                row.Cells.Add(new DataGridViewTextBoxCell()
                {
                    Value = ThreadCalistir.ciftSayilarList[rowIndex],
                });

                dataGridView2.Rows.Add(row);

            }


            dataGridView3.ColumnCount = 1;
            dataGridView3.Columns[0].Width = 200; // Tüm sütunlara aynı genişliği atar

            var Count3 = ThreadCalistir.tekSayilarList.Count;

            for (int rowIndex = 0; rowIndex < Count; ++rowIndex)
            {
                var row = new DataGridViewRow();

                row.Cells.Add(new DataGridViewTextBoxCell()
                {
                    Value = ThreadCalistir.tekSayilarList[rowIndex],
                });

                dataGridView3.Rows.Add(row);

            }


            watch.Stop();
        }


        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Sadece sayıları göstermek istediğimiz sütunu belirleyin
            if (e.ColumnIndex == 0 && e.Value != null)
            {
                // Sayıyı al
                int sayi;
                if (int.TryParse(e.Value.ToString(), out sayi))
                {
                    // Hücre değerini sayıya formatla
                    e.Value = sayi;
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
