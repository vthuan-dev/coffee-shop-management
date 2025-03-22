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
            
            // Tạo label tiêu đề
            label1 = new Label();
            label1.Text = "Quản lý bàn";
            label1.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
            label1.Location = new Point(20, 20);
            label1.AutoSize = true;
            this.Controls.Add(label1);
            
            // Tạo panel chứa các button
            Panel panelButtons = new Panel();
            panelButtons.Dock = DockStyle.Left;
            panelButtons.Width = 150;
            panelButtons.Padding = new Padding(10);
            this.Controls.Add(panelButtons);
            
            // Tạo panel chứa bàn
            panelTables = new Panel();
            panelTables.Dock = DockStyle.Fill;
            panelTables.BackColor = Color.WhiteSmoke;
            this.Controls.Add(panelTables);
            
            // Tạo các buttons
            button1 = new Button();
            button1.Text = "Gộp bàn";
            button1.Size = new Size(130, 40);
            button1.Location = new Point(10, 70);
            button1.Click += new EventHandler(button1_Click);
            panelButtons.Controls.Add(button1);
            
            button2 = new Button();
            button2.Text = "Đặt bàn";
            button2.Size = new Size(130, 40);
            button2.Location = new Point(10, 120);
            button2.Click += new EventHandler(button2_Click);
            panelButtons.Controls.Add(button2);
            
            button3 = new Button();
            button3.Text = "Chuyển bàn";
            button3.Size = new Size(130, 40);
            button3.Location = new Point(10, 170);
            button3.Click += new EventHandler(button3_Click);
            panelButtons.Controls.Add(button3);
            
            button5 = new Button();
            button5.Text = "Thanh toán";
            button5.Size = new Size(130, 40);
            button5.Location = new Point(10, 220);
            button5.Click += new EventHandler(button5_Click);
            panelButtons.Controls.Add(button5);
            
            button6 = new Button();
            button6.Text = "Tách bill";
            button6.Size = new Size(130, 40);
            button6.Location = new Point(10, 270);
            button6.Click += new EventHandler(button6_Click);
            panelButtons.Controls.Add(button6);
            
            // Thêm ví dụ một số bàn
            CreateExampleTables();
        }
        
        private void CreateExampleTables()
        {
            // Tạo một số bàn mẫu
            for (int i = 1; i <= 12; i++)
            {
                Button tableButton = new Button();
                tableButton.Text = "Bàn " + i;
                tableButton.Size = new Size(100, 100);
                tableButton.BackColor = Color.LightGreen; // Bàn trống
                
                // Tính toán vị trí cho bàn
                int row = (i - 1) / 4;
                int col = (i - 1) % 4;
                tableButton.Location = new Point(20 + col * 120, 20 + row * 120);
                
                panelTables.Controls.Add(tableButton);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
    }
}
