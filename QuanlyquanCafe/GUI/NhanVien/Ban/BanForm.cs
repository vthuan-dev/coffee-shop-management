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
        public BanForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            
            // Đăng ký sự kiện resize
            button9.Resize += new EventHandler(Button9_Resize);
            
            // Tạo các button bàn
            CreateTableButtons();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeButtonImage();
            CreateTableButtons();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        
        // Thêm phương thức xử lý sự kiện panel1_Paint_1
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            // Xử lý vẽ panel nếu cần
        }

        // Thêm phương thức xử lý sự kiện button4_Click
        private void button4_Click(object sender, EventArgs e)
        {
            // Xác định button nào đã được nhấp
            Button clickedButton = sender as Button;
            
            if (clickedButton != null)
            {
                // Kiểm tra text của button để xác định hành động
                switch (clickedButton.Text)
                {
                    case "Đặt bàn":
                        new BookTableForm().ShowDialog();
                        break;
                    case "Gợp bàn":
                        new CombineTableForm().ShowDialog();
                        break;
                    case "Thanh toán":
                        new PaymentForm().ShowDialog();
                        break;
                }
            }
        }

        // Thêm phương thức xử lý sự kiện button9_Click
        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn đã chọn " + ((Button)sender).Text);
            // Hiển thị thông tin bàn hoặc menu khi click vào bàn
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
                Image originalImage = Properties.Resources.ResourceManager.GetObject("7113274") as Image;
                
                if (originalImage != null && button9 != null)
                {
                    // Tạo một bản thu nhỏ theo kích thước button
                    Image resizedImage = new Bitmap(originalImage, button9.Width - 10, button9.Height - 10);
                    
                    // Gán cho button
                    button9.Image = resizedImage;
                    
                    // Đảm bảo vị trí hình ảnh đúng
                    button9.ImageAlign = ContentAlignment.MiddleCenter;
                    button9.TextImageRelation = TextImageRelation.ImageAboveText;
                }
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

        private void CreateTableButtons()
        {
            // Xóa các button hiện có trong flowLayoutPanel1 (nếu cần)
            flowLayoutPanel1.Controls.Clear();
            
            // Lấy hình ảnh từ Resources
            Image tableImage = Properties.Resources.ResourceManager.GetObject("7113274") as Image;
            
            // Điều chỉnh kích thước nhỏ hơn của button bàn
            int buttonWidth = 140;  // Giảm từ 179
            int buttonHeight = 85;  // Giảm từ 101
            
            // Tăng số lượng cột trong FlowLayoutPanel
            flowLayoutPanel1.Padding = new Padding(5);
            
            // Tạo 12 bàn (có thể điều chỉnh số lượng)
            for (int i = 1; i <= 12; i++)
            {
                Button btnTable = new Button();
                btnTable.Name = "btnTable" + i;
                btnTable.Text = "Bàn " + i;
                btnTable.Size = new Size(buttonWidth, buttonHeight);
                btnTable.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                btnTable.UseVisualStyleBackColor = true;
                btnTable.Margin = new Padding(8); // Thêm khoảng cách giữa các button
                
                // Thiết lập hình ảnh
                if (tableImage != null)
                {
                    // Tạo bản thu nhỏ của hình ảnh
                    Image resizedImage = new Bitmap(tableImage, buttonWidth - 30, buttonHeight - 25);
                    btnTable.Image = resizedImage;
                    
                    // Định dạng hiển thị hình ảnh
                    btnTable.ImageAlign = ContentAlignment.TopCenter;
                    btnTable.TextAlign = ContentAlignment.BottomCenter;
                    btnTable.TextImageRelation = TextImageRelation.ImageAboveText;
                }
                
                // Tùy chỉnh giao diện để làm đẹp hơn
                btnTable.FlatStyle = FlatStyle.Flat;
                btnTable.FlatAppearance.BorderSize = 1;
                btnTable.FlatAppearance.BorderColor = Color.LightGray;
                btnTable.BackColor = Color.White;
                
                // Đăng ký sự kiện Click
                btnTable.Click += new EventHandler(table_Click);
                
                // Thêm vào flowLayoutPanel
                flowLayoutPanel1.Controls.Add(btnTable);
            }
            
            // Đảm bảo flowLayoutPanel có đủ không gian hiển thị tất cả button
            flowLayoutPanel1.AutoScroll = true;
        }

        private void table_Click(object sender, EventArgs e)
        {
            Button clickedTable = sender as Button;
            if (clickedTable != null)
            {
                MessageBox.Show("Bạn đã chọn " + clickedTable.Text);
                // Thêm code xử lý khi chọn bàn
            }
        }
    }
}
