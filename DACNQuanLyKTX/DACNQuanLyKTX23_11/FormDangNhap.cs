using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DACNQuanLyKTX.Models;

namespace DACNQuanLyKTX
{
    public partial class FormDangNhap : Form
    {
        ModelQLKTX db = new ModelQLKTX();
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
            if(db.QuanLies.Find(textBox2.Text) != null && db.QuanLies.Find(textBox2.Text).Pass == textBox1.Text )
            {
                FormQLKTX dn = new FormQLKTX(db.QuanLies.Find(textBox2.Text).MaQL, this);              
                dn.ShowDialog();
                this.Hide();
                FormQLKTX f = new FormQLKTX();
                f.Closed += (s, args) => this.Close();
                f.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác !!!");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
