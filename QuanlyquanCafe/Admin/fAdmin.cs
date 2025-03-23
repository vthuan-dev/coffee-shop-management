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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using ComboBox = System.Windows.Forms.ComboBox;
//using QuanlyquanCafe.Admin.Subform;

namespace QuanlyquanCafe.Admin
{
    public partial class fAdmin : Form
    {
        BindingSource menuList = new BindingSource();
        BindingSource facilityList = new BindingSource();
        BindingSource accountList = new BindingSource();

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public fAdmin()
        {
            InitializeComponent();

            this.MouseDown += new MouseEventHandler(fAdmin_MouseDown);
            this.MouseMove += new MouseEventHandler(fAdmin_MouseMove);
            this.MouseUp += new MouseEventHandler(fAdmin_MouseUp);

            //LoadAccountList();
            this.Load += (s, e) => mdiProp();
            Loading();
        }

        private void fAdmin_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void fAdmin_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void fAdmin_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        void Loading() {
            dtgvMenu.DataSource = menuList;
            dtgvFac.DataSource = facilityList;
            dtgvStaff.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dateBill1.Value, dateBill2.Value);

            LoadMenuList();
            AddMenuBinding();
            LoadCategoryIntoCombobox(cbxMenuCate);

            LoadFacilityList();
            AddFacilityBinding();
            LoadFacCategoryIntoCombobox(cbxFacCate, "Category");
            LoadFacCategoryIntoCombobox(cbxFacLocation, "Location");

            LoadAccountList();
            AddAccountBinding();

            foreach (DataGridViewColumn column in dtgvStaff.Columns)
            {
                Console.WriteLine(column.HeaderText); // In tên cột
            }

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

        void LoadFacilityList()
        {
            facilityList.DataSource = FacilityDAO.Instance.GetListFac();
            dtgvFac.Columns["Id"].Visible = false;
            dtgvFac.Columns["FacilityCateID1"].Visible = false;

            dtgvFac.Columns["Name"].HeaderText = "Tên";
            dtgvFac.Columns["Location1"].HeaderText = "Vị trí";
            dtgvFac.Columns["Status"].HeaderText = "Trạng thái";
        }

        void LoadAccountList()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
            dtgvStaff.Columns["Id"].Visible = false;
            dtgvStaff.Columns["PassWord"].Visible = false;
            //dtgvStaff.Columns["FullName"].Visible = false;

            dtgvStaff.Columns["FullName"].HeaderText = "Tên người dùng";
            dtgvStaff.Columns["Role"].HeaderText = "Chức vụ";
            dtgvStaff.Columns["Phone"].HeaderText = "Số điện thoại";
        }

        void AddMenuBinding()
        {
            txbMenuID.DataBindings.Add(new Binding("Text", dtgvMenu.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbMenuName.DataBindings.Add(new Binding("Text", dtgvMenu.DataSource, "Name", true, DataSourceUpdateMode.Never));
            numMenuPrice.DataBindings.Add(new Binding("Value", dtgvMenu.DataSource, "Price", true, DataSourceUpdateMode.Never));
            //txbMenuCate.DataBindings.Add(new Binding("Text", dtgvMenu.DataSource, "Category", true, DataSourceUpdateMode.Never));
        }

        void AddFacilityBinding()
        {
            txbFacID.DataBindings.Add(new Binding("Text", dtgvFac.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbFacName.DataBindings.Add(new Binding("Text", dtgvFac.DataSource, "Name", true, DataSourceUpdateMode.Never));
            
            //txbMenuCate.DataBindings.Add(new Binding("Text", dtgvMenu.DataSource, "Category", true, DataSourceUpdateMode.Never));
        }

        void AddAccountBinding()
        {
            txbStaffID.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbStaffName.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "FullName", true, DataSourceUpdateMode.Never));
            txbStaffPhone.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Phone", true, DataSourceUpdateMode.Never));
            txbStaffEmail.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Email", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = MenuCategoryDAO.Instance.GetListMenuCategory();
            cb.DisplayMember = "Name";
        }
        void LoadFacCategoryIntoCombobox(ComboBox cb, string type, int? facilityCateID = null)
        {
            switch (type)
            {
                case "Category":
                    cb.DataSource = FacCategoryDAO.Instance.GetListFacCategory();
                    cb.DisplayMember = "Name";
                    break;

                case "Location":
                    cb.DataSource = FacCategoryDAO.Instance.GetLocations(); 
                    break;
                case "Status":
                    // Kiểm tra facilityCateID để xác định trạng thái
                    if (facilityCateID.HasValue)
                    {
                        // Nếu facilityCateID là 1, 2, hoặc 3, áp dụng trạng thái dành cho bàn
                        if (facilityCateID == 1 || facilityCateID == 2 || facilityCateID == 3)
                        {
                            cb.Items.Clear();
                            cb.Items.Add("Trống");
                            cb.Items.Add("Có người");
                            cb.Items.Add("Đã đặt");
                        }
                        else
                        {
                            // Nếu không phải các ID trên, áp dụng trạng thái dành cho loa, máy lạnh
                            cb.Items.Clear();
                            cb.Items.Add("Hoạt động");
                            cb.Items.Add("Hỏng");
                            cb.Items.Add("Đang sửa chữa");
                        }
                    }
                    break;
            }
        }

        //void LoadRolesToComboBox()
        //{
        //    DataTable accountList = AccountDAO.Instance.GetListAccount(); // Lấy tất cả tài khoản
        //    cbxStaffRole.Items.Clear(); // Xóa các mục hiện tại trong ComboBox

        //    // Duyệt qua danh sách tài khoản và thêm Role vào ComboBox
        //    foreach (DataRow row in accountList.Rows)
        //    {
        //        string role = row["Role"].ToString(); // Lấy giá trị Role từ DataRow
        //        if (!cbxStaffRole.Items.Contains(role)) // Tránh trùng lặp
        //        {
        //            cbxStaffRole.Items.Add(role); // Thêm Role vào ComboBox
        //        }
        //    }
        //}


        List<MenuDTO> SearchMenuName(string name)
        {
            List<MenuDTO> listMenu = MenuDAO.Instance.SearchMenuByName(name);
            return listMenu;
        }

        List<Facility> SearchFacName(string name)
        {
            List<Facility> listFac = FacilityDAO.Instance.SearchFacByName(name);
            return listFac;
        }

        List<Account> SearchStaffName(string name)
        {
            List<Account> listFac = AccountDAO.Instance.SearchEmployeeByName(name);
            return listFac;
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

        private void btnFacView_Click(object sender, EventArgs e)
        {
            LoadFacilityList(); 
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
                        MenuCategory cate = MenuCategoryDAO.Instance.GetCategoryByID(id);
                        cbxMenuCate.SelectedItem = cate;

                        int index = -1;
                        int i = 0;
                        foreach (MenuCategory item in cbxMenuCate.Items)
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

        private void txbFacID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFac.SelectedCells.Count > 0)
                {
                    // 📌 Kiểm tra và lấy FacilityCategoryID từ DataGridView
                    var cellValue2 = dtgvFac.SelectedCells[0].OwningRow.Cells["FacilityCateID1"].Value;
                    Debug.WriteLine("📌 FacilityCateID1 lấy được: " + (cellValue2 != null ? cellValue2.ToString() : "NULL"));

                    if (cellValue2 != null)
                    {
                        int id2 = Convert.ToInt32(cellValue2);

                        if (id2 > 0) // Chỉ lấy nếu ID hợp lệ
                        {
                            FacilityCategory cate = FacCategoryDAO.Instance.GetFacCategoryByID(id2);
                            if (cate != null)
                            {
                                Debug.WriteLine("✅ Đã lấy được FacilityCategory: " + cate.Name);

                                // 📌 Kiểm tra danh sách ComboBox trước khi gán SelectedIndex
                                if (cbxFacCate.Items.Count == 0)
                                {
                                    Debug.WriteLine("⚠ cbxFacCate.Items đang rỗng, thêm dữ liệu trước khi gán!");
                                    return; // Không tiếp tục nếu danh sách rỗng
                                }

                                int index = -1;
                                for (int i = 0; i < cbxFacCate.Items.Count; i++)
                                {
                                    if (((FacilityCategory)cbxFacCate.Items[i]).Id == cate.Id)
                                    {
                                        index = i;
                                        break;
                                    }
                                }

                                if (index >= 0 && index < cbxFacCate.Items.Count)
                                {
                                    Debug.WriteLine("✅ Gán SelectedIndex: " + index);
                                    cbxFacCate.SelectedIndex = index;
                                }
                                else
                                {
                                    Debug.WriteLine("⚠ Không tìm thấy ID trong danh sách, đặt SelectedIndex = -1");
                                    cbxFacCate.SelectedIndex = -1;
                                }
                            }
                            else
                            {
                                Debug.WriteLine("⚠ Không tìm thấy dữ liệu FacilityCategory!");
                            }
                        }
                        else
                        {
                            Debug.WriteLine("⚠ FacilityCateID1 không hợp lệ (<= 0), bỏ qua!");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("⚠ FacilityCateID1 là NULL, không thể lấy dữ liệu!");
                    }

                    // 📌 Kiểm tra và lấy FacilityID từ DataGridView
                    var cellValue = dtgvFac.SelectedCells[0].OwningRow.Cells[columnName: "Id"].Value;
                    Debug.WriteLine("📌 FacilityID lấy được: " + (cellValue != null ? cellValue.ToString() : "NULL"));

                    if (cellValue != null)
                    {
                        int id = Convert.ToInt32(cellValue);

                        // 🔹 Lấy thông tin Location & Status
                        FacilityDetails details = FacCategoryDAO.Instance.GetFacilityDetailsByID(id);
                        if (details != null)
                        {
                            cbxFacLocation.SelectedItem = details.Location ?? "Không xác định";
                            cbxFacStatus.SelectedItem = details.Status ?? "Không có trạng thái phù hợp";
                            Debug.WriteLine("✅ Đã cập nhật Location & Status");
                        }
                        else
                        {
                            Debug.WriteLine("⚠ Không tìm thấy dữ liệu FacilityDetails!");
                        }
                    }

                    
                }
                else
                {
                    Debug.WriteLine("⚠ Không có dòng nào được chọn trong DataGridView!");
                }
            }
            catch 
            {
                //Debug.WriteLine("❌ Lỗi: " + ex.Message);
                //MessageBox.Show("Lỗi Index Out Of Range! Hãy kiểm tra dữ liệu đầu vào.");
            }
            
        }

        private void txbStaffID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvStaff.SelectedCells.Count > 0)
                {
                    var cellValue = dtgvStaff.SelectedCells[0].OwningRow.Cells["Id"].Value;
                    if (cellValue != null)
                    {
                        int id = (int)cellValue;
                        string role = AccountDAO.Instance.GetRoleByID(id);
                        Debug.WriteLine("📌 Role lấy được: " + role);

                        switch (role)
                        {
                            case "Quản trị":
                                cbxStaffRole.SelectedIndex = 0;
                                break;
                            case "Pha chế":
                                cbxStaffRole.SelectedIndex = 1;
                                break;
                            case "Thu ngân":
                                cbxStaffRole.SelectedIndex = 2;
                                break;
                        }
                    }
                }
            }
            catch
            {
                
            }
        }

        private void cbxFacCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id2 = (cbxFacCate.SelectedItem as FacilityCategory).Id;
            LoadFacCategoryIntoCombobox(cbxFacStatus, "Status", id2);
        }

        private void btnMenuAdd_Click(object sender, EventArgs e)
        {
            string name = txbMenuName.Text;
            float price = (float)numMenuPrice.Value;
            int category = (cbxMenuCate.SelectedItem as MenuCategory).Id;

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
            int category = (cbxMenuCate.SelectedItem as MenuCategory).Id;
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

        private void btnFacAdd_Click(object sender, EventArgs e)
        {
            int category = (cbxFacCate.SelectedItem as FacilityCategory).Id;
            string name = txbFacName.Text;
            string location = cbxFacLocation.SelectedItem.ToString();
            string status = cbxFacStatus.SelectedItem.ToString();

            if (FacilityDAO.Instance.AddNewItem(category, name, location, status))
            {
                MessageBox.Show("Thêm thành công");
                LoadFacilityList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm");
            }
        }

        private void btnFacEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFacID.Text);
            int category = (cbxFacCate.SelectedItem as FacilityCategory).Id;
            string name = txbFacName.Text;
            string location = cbxFacLocation.SelectedItem.ToString();
            string status = cbxFacStatus.SelectedItem.ToString();


            if (FacilityDAO.Instance.UpdateItem(id, category, name, location, status))
            {
                MessageBox.Show("Sửa thành công");
                LoadFacilityList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa");
            }
        }

        private void btnFacDel_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFacID.Text);
            if (FacilityDAO.Instance.DeleteItem(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadFacilityList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa");
            }
        }

        private void btnFacSearch_Click(object sender, EventArgs e)
        {
            facilityList.DataSource = SearchFacName(txbFacSearch.Text);
        }

        private void btnStaffAdd_Click_1(object sender, EventArgs e)
        {
            string FullName = txbStaffName.Text;
            string Role = cbxStaffRole.SelectedItem.ToString();  // Đảm bảo đây là một giá trị hợp lệ
            string Phone = txbStaffPhone.Text;
            string Email = txbStaffEmail.Text;

            if (AccountDAO.Instance.AddNewItem(Role, FullName, Phone, Email))
            {
                MessageBox.Show("Thêm người dùng thành công");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm người dùng");
            }
        }

        private void btnViewEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbStaffID.Text);
            string FullName = txbStaffName.Text;
            string Role = cbxStaffRole.SelectedItem.ToString();  // Đảm bảo đây là một giá trị hợp lệ
            string Phone = txbStaffPhone.Text;
            string Email = txbStaffEmail.Text;

            if (AccountDAO.Instance.UpdateItem(id, Role, FullName, Phone, Email))
            {
                MessageBox.Show("Sửa người dùng thành công");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa người dùng");
            }
        }

        private void btnStaffDel_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbStaffID.Text);

            if (AccountDAO.Instance.DeleteItem(id))
            {
                MessageBox.Show("Xóa người dùng thành công");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa người dùng");
            }
        }

        private void btnStaffSearch_Click(object sender, EventArgs e)
        {
            accountList.DataSource = SearchStaffName(txbStaffSearch.Text);
        }



        #endregion

        private void btnFirst_Click(object sender, EventArgs e)
        {
            txbPageBill.Text = "1";
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillByDate(dateBill1.Value, dateBill2.Value);
            int lastPage = sumRecord / 10;
            if(sumRecord % 10 != 0)
            {
                lastPage++;
            }

            txbPageBill.Text = lastPage.ToString();
        }

        private void txbPageBill_TextChanged(object sender, EventArgs e)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDateAndPage(dateBill1.Value, dateBill2.Value, Convert.ToInt32(txbPageBill.Text));
        }
    }
}
