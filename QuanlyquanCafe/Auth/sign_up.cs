using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanlyquanCafe.Admin.DAO;
using QuanlyquanCafe.Auth;

namespace QuanlyquanCafe.Auth
{
    public partial class sign_up : Form
    {
        public sign_up()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

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

        private void sign_up_Load(object sender, EventArgs e)
        {
            textBoxPasswd.UseSystemPasswordChar = true;
            
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            string password = textBoxPasswd.Text.Trim();
            string role = cBoxRole.SelectedItem?.ToString();
            string fullName = textBoxName.Text.Trim();
            string phone = textBoxPhone.Text.Trim();
            string email = textBoxEmail.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem email hoặc số điện thoại đã tồn tại
            if (AccountDAO.Instance.IsEmailOrPhoneExists(email, phone))
            {
                MessageBox.Show("Email hoặc số điện thoại đã tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gọi hàm Register
            bool success = AccountDAO.Instance.Register(password, role, fullName, phone, email);

            if (success)
            {
                MessageBox.Show("Đăng ký thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Form1 loginForm = new Form1();
                loginForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxPasswd.Clear();
            cBoxRole.SelectedIndex = -1;
            textBoxName.Clear();
            textBoxPhone.Clear();
            textBoxEmail.Clear();
        }
    }
}

