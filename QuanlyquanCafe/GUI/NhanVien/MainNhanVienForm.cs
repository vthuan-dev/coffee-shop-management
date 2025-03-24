using System;
using System.Drawing;
using System.Windows.Forms;
using QuanlyquanCafe.GUI.NhanVien.Menu;

namespace QuanlyquanCafe.GUI.NhanVien
{
    public partial class MainNhanVienForm : Form
    {
        private TabControl tabControl;
        private TabPage tabMenu;

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

            // Thêm tab vào TabControl
            tabControl.TabPages.Add(tabMenu);

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

        private void MainNhanVienForm_Load(object sender, EventArgs e)
        {

        }
    }
} 