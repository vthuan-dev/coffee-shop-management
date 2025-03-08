using QuanlyquanCafe.Admin.DAO;
using QuanlyquanCafe.Admin.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
//using QuanlyquanCafe.Admin.Subform;

namespace QuanlyquanCafe.Admin
{
    public partial class fAdmin : Form
    {
        BindingSource menuList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            //LoadAccountList();
            this.Load += (s, e) => mdiProp();
            Loading();
        }

        void Loading() {
            dtgvMenu.DataSource = menuList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dateBill1.Value, dateBill2.Value);
            LoadMenuList();
            AddMenuBinding();
            LoadCategoryIntoCombobox(cbxMenuCate);
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



        #region methods
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dateBill1.Value = new DateTime(today.Year, today.Month, 1);
            dateBill2.Value = dateBill1.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDate(checkIn, checkOut);
        }

        void LoadMenuList()
        {
            menuList.DataSource = MenuDAO.Instance.GetListMenu();
            dtgvMenu.Columns["Id"].Visible = false;
            dtgvMenu.Columns["CategoryID"].Visible = false;

            // Manually change the column names
            dtgvMenu.Columns["Name"].HeaderText = "Tên";
            dtgvMenu.Columns["Price"].HeaderText = "Giá";
            dtgvMenu.Columns["Category"].HeaderText = "Danh mục";
        }

        void AddMenuBinding()
        {
            txbMenuID.DataBindings.Add(new Binding("Text", dtgvMenu.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbMenuName.DataBindings.Add(new Binding("Text", dtgvMenu.DataSource, "Name", true, DataSourceUpdateMode.Never));
            numMenuPrice.DataBindings.Add(new Binding("Value", dtgvMenu.DataSource, "Price", true, DataSourceUpdateMode.Never));
            //txbMenuCate.DataBindings.Add(new Binding("Text", dtgvMenu.DataSource, "Category", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListMenuCategory();
            cb.DisplayMember = "Name";
        }

        List<MenuDTO> SearchMenuName(string name)
        {
            List<MenuDTO> listMenu = MenuDAO.Instance.SearchMenuByName(name);
            return listMenu;
        }

        #endregion
        #region events
        private void btnBillStat_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dateBill1.Value, dateBill2.Value);
        }

        private void btnMenuView_Click(object sender, EventArgs e)
        {
            LoadMenuList();
        }

        private void txbMenuID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvMenu.SelectedCells.Count > 0)
                {
                    var cellValue = dtgvMenu.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;
                    if (cellValue != null)
                    {
                        int id = (int)cellValue;
                        Category cate = CategoryDAO.Instance.GetCategoryByID(id);
                        cbxMenuCate.SelectedItem = cate;

                        int index = -1;
                        int i = 0;
                        foreach (Category item in cbxMenuCate.Items)
                        {
                            if (item.Id == cate.Id)
                            {
                                index = i;
                                break;
                            }
                            i++;
                        }

                        cbxMenuCate.SelectedIndex = index;
                    }
                }
            }
            catch { }
        }

        private void btnMenuAdd_Click(object sender, EventArgs e)
        {
            string name = txbMenuName.Text;
            float price = (float)numMenuPrice.Value;
            int category = (cbxMenuCate.SelectedItem as Category).Id;

            if(MenuDAO.Instance.AddNewItem(name, price, category))
            {
                MessageBox.Show("Thêm món thành công");
                LoadMenuList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm món");
            }
        }

        private void btnMenuEdit_Click(object sender, EventArgs e)
        {
            string name = txbMenuName.Text;
            float price = (float)numMenuPrice.Value;
            int category = (cbxMenuCate.SelectedItem as Category).Id;
            int id = Convert.ToInt32(txbMenuID.Text);

            if (MenuDAO.Instance.UpdateItem(id, name, price, category))
            {
                MessageBox.Show("Sửa món thành công");
                LoadMenuList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa món");
            }
        }

        private void btnMenuDel_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbMenuID.Text);

            if (MenuDAO.Instance.DeleteItem(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadMenuList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa");
            }
        }

        private void btnMenuSearch_Click(object sender, EventArgs e)
        {
            menuList.DataSource = SearchMenuName(txbMenuSearch.Text);
        }


        #endregion


    }
}
