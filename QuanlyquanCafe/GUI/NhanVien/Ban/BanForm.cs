using QuanlyquanCafe.GUI.NhanVien.Ban.Subforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanlyquanCafe.GUI.NhanVien.Ban
{
    public partial class BanForm : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button5;
        private Button button6;
        private Label label1;
        private Panel panelTables;

        public BanForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeButtonImage();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new CombineTableForm().ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new BookTableForm().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new MoveTableForm().ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new SplitBill().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new PaymentForm().ShowDialog();
        }

        private void ResizeButtonImage()
        {
            try
            {
                // Lấy hình ảnh từ Resources với tên đúng
                Image originalImage = Properties.Resources._7113274;
                
                // Tạo một bản thu nhỏ theo kích thước button
                Image resizedImage = new Bitmap(originalImage, button9.Width - 10, button9.Height - 10);
                
                // Gán cho button
                button9.Image = resizedImage;
                
                // Đảm bảo vị trí hình ảnh đúng
                button9.ImageAlign = ContentAlignment.MiddleCenter;
                button9.TextImageRelation = TextImageRelation.ImageAboveText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load hình ảnh: " + ex.Message);
            }
        }

        private void Button9_Resize(object sender, EventArgs e)
        {
            ResizeButtonImage();
        }
    }
}
