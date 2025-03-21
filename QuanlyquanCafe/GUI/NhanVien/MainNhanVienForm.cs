using System;
using System.Drawing;
using System.Windows.Forms;
using QuanlyquanCafe.GUI.NhanVien.Ban;
using QuanlyquanCafe.GUI.NhanVien.Menu;
using QuanlyquanCafe.GUI.NhanVien.PhaChe;

namespace QuanlyquanCafe.GUI.NhanVien
{
    public partial class MainNhanVienForm : Form
    {
        private TabControl tabControl;
        private TabPage tabBan;
        private TabPage tabMenu;
        private TabPage tabPhaChe;

        public MainNhanVienForm()
        {
            InitializeComponent();
            SetupTabs();
        }

        private void SetupTabs()
        {
            // Tạo TabControl
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 12)
            };
            
            // Thêm xử lý sự kiện khi chuyển tab
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            // Tab Bàn
            tabBan = new TabPage("Quản lý bàn");
            var banForm = new BanForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                Visible = true  // Đảm bảo form hiển thị
            };
            tabBan.Controls.Add(banForm);
            banForm.Show();

            // Tab Menu
            tabMenu = new TabPage("Menu");
            var menuForm = new MenuForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            tabMenu.Controls.Add(menuForm);
            menuForm.Show();

            // Tab Pha chế
            tabPhaChe = new TabPage("Pha chế");
            var phaCheForm = new PhaCheForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            tabPhaChe.Controls.Add(phaCheForm);
            phaCheForm.Show();

            // Thêm các tab vào TabControl
            tabControl.TabPages.AddRange(new TabPage[] {
                tabBan,
                tabMenu,
                tabPhaChe
            });

            // Thêm TabControl vào form
            this.Controls.Add(tabControl);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kích hoạt form con trong tab được chọn
            TabPage selectedTab = tabControl.SelectedTab;
            foreach (Control ctrl in selectedTab.Controls)
            {
                if (ctrl is Form)
                {
                    ((Form)ctrl).Refresh();
                    ((Form)ctrl).Activate();
                    break;
                }
            }
        }
    }
} 