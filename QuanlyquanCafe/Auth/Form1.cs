using QuanlyquanCafe.Admin;
using QuanlyquanCafe.Admin.DAO;
using QuanlyquanCafe.GUI.NhanVien;
using QuanlyquanCafe.Auth;
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
            string email = textBoxEmail.Text.Trim();
            string password = textBoxPasswd.Text.Trim();

            string role = Login(email, password); // Gọi hàm Login() đã sửa

            if (string.IsNullOrEmpty(role)) // Kiểm tra nếu không có role
            {
                MessageBox.Show("Email hoặc mật khẩu không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();

            Form nextForm;
            switch (role)
            {
                case "Quản Trị":
                    nextForm = new fAdmin(); 
                    break;
                case "Thu Ngân":
                    nextForm = new MainNhanVienForm(); 
                    break;
                case "Pha Chế":
                    nextForm = new MainNhanVienForm(); 
                    break;
                default:
                    MessageBox.Show("Tài khoản không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Show();
                    return;
            }

            nextForm.ShowDialog();
            this.Close();
        }



        string Login(string email, string password)
        {
            return AccountDAO.Instance.Login(email, password);
        }

        private void textBoxEmail_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide(); // Ẩn form hiện tại (FormLogin)

            sign_up registerForm = new sign_up();
            registerForm.ShowDialog(); // Mở form đăng ký dưới dạng hộp thoại

            this.Close(); // Đóng FormLogin sau khi đăng ký xong
        }

        private void checkBoxPasswd_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBoxPasswd.Checked)
            {
                textBoxPasswd.UseSystemPasswordChar = false; // Hiển thị mật khẩu
            }
            else
            {
                textBoxPasswd.UseSystemPasswordChar = true; // Ẩn mật khẩu
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxPasswd.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxEmail.Clear();
            textBoxPasswd.Clear();
        }
    }
}

