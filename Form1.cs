using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<List<string>> reader = XuLyNghiepVu.QuanLyKho.getHangHetHang();

            dataGridView1.Columns.Add("1", "1");
            dataGridView1.Columns.Add("2", "2");
            dataGridView1.Columns.Add("3", "3");
            dataGridView1.Columns.Add("4", "4");
            for (int i = 0; i < reader.Count; i++)
            {
                dataGridView1.Rows.Add(reader[i].ToArray());
            }


            List<List<string>> reader1 = XuLyNghiepVu.QuanLyKho.getAllNguyenLieu(ngungSuDung: true, conSuDung: false); 
        }
    }
}
