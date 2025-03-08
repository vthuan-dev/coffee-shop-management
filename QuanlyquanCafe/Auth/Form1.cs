using QuanlyquanCafe.Admin;
using QuanlyquanCafe.Admin.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;

namespace QuanlyquanCafe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPasswd_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;
            string password = textBoxPasswd.Text;
            if (Login(email, password))
            {
                MessageBox.Show("Đăng nhập thành công");
                this.Hide();
                fAdmin f = new fAdmin();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Email hoặc mật khẩu không đúng!");
            }
        }

        bool Login(string email, string password)
        {
            return AccountDAO.Instance.Login(email, password);
        }

    }
}

