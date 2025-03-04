using QuanlyquanCafe.GUI.NhanVien.BanForm.Subforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanlyquanCafe.GUI.NhanVien.BanForm
{
    public partial class BanForm: Form
    {
        public BanForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            new BookTableForm().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new CombineTableForm().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new PaymentForm().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new SplitBill().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MoveTableForm().ShowDialog();
        }
    }
}
