using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using QuanlyquanCafe.Admin.Subform;

namespace QuanlyquanCafe.Admin
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            this.Load += (s, e) => mdiProp();
        }
        private void mdiProp()
        {
            this.SetBevel(false);
            var mdiClient = Controls.OfType<MdiClient>().FirstOrDefault();
            if (mdiClient != null)
            {
                mdiClient.BackColor = Color.FromArgb(232, 234, 237);
            }
        }


        bool sidebarExpand = true;

        private void SidebarTrans_Tick(object sender, EventArgs e)
        {
            int sidebarMinWidth = 55; // Kích thước sidebar nhỏ nhất
            int sidebarMaxWidth = 254; // Kích thước sidebar lớn nhất
            int spaceBetween = 10; // Khoảng cách giữa sidebar và panel chính

            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= sidebarMinWidth)
                {
                    sidebarExpand = false;
                    SidebarTrans.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= sidebarMaxWidth)
                {
                    sidebarExpand = true;
                    SidebarTrans.Stop();
                }
            }

            this.DoubleBuffered = true;



            if (!SidebarTrans.Enabled)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    pnMain.Left = sidebar.Width + spaceBetween;
                    pnMain.Width = this.ClientSize.Width - pnMain.Left - spaceBetween;
                });
            }

            //this.SuspendLayout();

            //pnMain.SetBounds(sidebar.Width + spaceBetween,
            //                 pnMain.Top,
            //                 this.ClientSize.Width - (sidebar.Width + spaceBetween) - spaceBetween,
            //                 pnMain.Height);

            //this.ResumeLayout();


        }


        private void ptboxHam_Click(object sender, EventArgs e)
        {
            SidebarTrans.Start();
        }

        private Form activeForm = null; // Biến toàn cục để lưu form hiện tại

        private async void OpenChildForm(Form childForm, string title)
        {
            // Nếu đã có form nào mở, đóng nó trước
            if (activeForm != null)
            {
                activeForm.Close(); // Đóng form hiện tại
                activeForm.Dispose(); // Giải phóng bộ nhớ
            }

            // Thiết lập form mới
            activeForm = childForm;
            activeForm.TopLevel = false;
            activeForm.FormBorderStyle = FormBorderStyle.None;
            activeForm.Dock = DockStyle.Fill;

            pnInside.Controls.Clear(); // Xóa form cũ
            pnInside.Controls.Add(activeForm); // Thêm form mới vào panel
            pnInside.Tag = activeForm; // Lưu form hiện tại
            activeForm.BringToFront();
            activeForm.Show();

            for (double i = 0.0; i <= 1.0; i += 0.1)
            {
                childForm.Opacity = i;
                await Task.Delay(30);
            }

            // Cập nhật tiêu đề
            lblTitle.Text = title;
        }

        // Nút mở quản lý nhân viên
        private void btnStaff_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formStaff(), "Quản lý nhân viên");
        }

        // Nút mở quản lý thực đơn
        private void btnDrink_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formMenu(), "Quản lý thực đơn");
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formTables(), "Quản lý bàn");
        }

        private void btnPaper_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formPaper(), "Quản lý hóa đơn");
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formStat(), "Thống kê");
        }


    }
}
