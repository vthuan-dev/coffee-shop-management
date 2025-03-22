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
