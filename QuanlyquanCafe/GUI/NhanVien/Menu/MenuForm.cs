using QuanlyquanCafe.Admin.DAO;
using QuanlyquanCafe.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Drawing.Printing;

namespace QuanlyquanCafe.GUI.NhanVien.Menu
{
    public partial class MenuForm : Form
    {
        private int? currentTableID = null;
        private int? currentBillID = null;
        //private FlowLayoutPanel flpTables;
        private Button selectedTableButton = null;
        private Dictionary<string, FlowLayoutPanel> floorPanels;
        private System.Windows.Forms.Timer autoRefreshTimer;
        private bool hasSelectedMenuItem = false;
        public List<TempOrderItem> tempOrderItems = new List<TempOrderItem>();
        private bool hasChangedOrder = false;
        private System.Windows.Forms.Timer timerRefresh;

        public MenuForm()
        {
            // Xóa dòng khởi tạo tabFloors vì đã được khởi tạo trong Designer
            // tabFloors = new System.Windows.Forms.TabControl();
            
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            
            // Đăng ký các sự kiện
            this.Load += MenuForm_Load;
            this.btnSearch.Click += BtnSearch_Click;
            this.dgvMenuItems.CellClick += DgvMenuItems_CellClick;
            this.btnDeleteItem.Click += BtnDeleteItem_Click;
            this.btnUpdate.Click += BtnUpdate_Click;
            this.btnCheckout.Click += BtnCheckout_Click;
            this.btnSendToKitchen.Click += BtnSendToKitchen_Click;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            //this.btnRefresh.Click += BtnRefresh_Click;
            SetupTableView();
            
            // Thêm xử lý sự kiện form đóng để dọn dẹp timer
            this.FormClosing += MenuForm_FormClosing;
            
            // Khởi tạo timer tự động refresh
            SetupAutoRefresh();
            
            // Khởi tạo nút Tạo hóa đơn
            InitializeCreateBillButton();
            InitializeTimers();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            // Đầu tiên, thiết lập cấu trúc bảng
            SetupMenuTable();
            SetupOrderDetailsTable();
            SetupActiveBillsTable();
            
            // Sau đó mới tải dữ liệu
            LoadCategories();
            LoadTables();
            LoadAllMenuItems();
            LoadActiveBills();

            // Thêm sự kiện cho nút Tạo hóa đơn
            Button btnCreateBill = this.Controls.Find("btnTaoHoa", true).FirstOrDefault() as Button;
            if (btnCreateBill != null)
            {
                btnCreateBill.Click += BtnCreateBill_Click;
            }
        }

        private void SetupMenuTable()
        {
            // Xóa tất cả cột hiện tại nếu có
            dgvMenuItems.Columns.Clear();
            
            // Thêm cột ID
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "ID";
            idColumn.Visible = false;
            dgvMenuItems.Columns.Add(idColumn);
            
            // Thêm cột Tên món
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.Name = "Tên món";
            nameColumn.Width = 200;
            dgvMenuItems.Columns.Add(nameColumn);
            
            // Thêm cột Giá
            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Name = "Giá";
            priceColumn.Width = 100;
            dgvMenuItems.Columns.Add(priceColumn);
            
            // Thêm cột button
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "+/-";
            btnColumn.Text = "+";
            btnColumn.UseColumnTextForButtonValue = true;
            btnColumn.Width = 50;
            dgvMenuItems.Columns.Add(btnColumn);
        }

        private void SetupOrderDetailsTable()
        {
            // Thiết lập cột cho bảng chi tiết đơn hàng
            dgvOrderDetails.ColumnCount = 5;
            dgvOrderDetails.Columns[0].Name = "ID";
            dgvOrderDetails.Columns[0].Visible = false;
            dgvOrderDetails.Columns[1].Name = "Món";
            dgvOrderDetails.Columns[1].Width = 200;
            dgvOrderDetails.Columns[2].Name = "Số lượng";
            dgvOrderDetails.Columns[2].Width = 80;
            dgvOrderDetails.Columns[3].Name = "Giá";
            dgvOrderDetails.Columns[3].Width = 100;
            dgvOrderDetails.Columns[4].Name = "Thành tiền";
            dgvOrderDetails.Columns[4].Width = 150;
        }

        private void SetupActiveBillsTable()
        {
            // Xóa tất cả cột hiện tại nếu có
            dgvActiveBills.Columns.Clear();
            
            // Thêm các cột cần thiết
            dgvActiveBills.Columns.Add("ID", "Mã hóa đơn");
            dgvActiveBills.Columns.Add("TableName", "Bàn");
            dgvActiveBills.Columns.Add("CheckInTime", "Thời gian vào");
            dgvActiveBills.Columns.Add("TotalItems", "Số món");
            dgvActiveBills.Columns.Add("TotalAmount", "Tổng tiền");
            dgvActiveBills.Columns.Add("Status", "Trạng thái");
            dgvActiveBills.Columns.Add("Staff", "Nhân viên");
            
            // Thiết lập sự kiện khi nhấp đúp vào hóa đơn
            dgvActiveBills.CellDoubleClick += DgvActiveBills_CellDoubleClick;
        }

        private void LoadCategories()
        {
            // Xóa tất cả các nút cũ
            flpCategories.Controls.Clear();

            // Tạo nút "Tất cả món"
            Button btnAllItems = new Button
            {
                Text = "Tất cả món",
                Width = 150,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft,
                Tag = 0
            };
            btnAllItems.Click += (s, e) => LoadAllMenuItems();
            flpCategories.Controls.Add(btnAllItems);

            try
            {
                // Lấy danh sách danh mục từ database
                List<MenuCategory> categories = MenuCategoryDAO.Instance.GetListMenuCategory();
                
                // Tạo các nút danh mục
                foreach (MenuCategory category in categories)
                {
                    Button btnCategory = new Button
                    {
                        Text = category.Name,
                        Width = 150,
                        Height = 40,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Tag = category.Id
                    };
                    btnCategory.Click += (s, e) => LoadMenuItemsByCategory((int)((Button)s).Tag);
                    flpCategories.Controls.Add(btnCategory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllMenuItems()
        {
            try
            {
                // Lấy tất cả các món từ cơ sở dữ liệu
                List<MenuDTO> menuItems = MenuDAO.Instance.GetListMenu();
                DisplayMenuItems(menuItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải menu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMenuItemsByCategory(int categoryId)
        {
            try
            {
                // Lọc các món theo danh mục
                List<MenuDTO> menuItems = MenuDAO.Instance.GetListMenu().FindAll(item => item.CategoryID == categoryId);
                DisplayMenuItems(menuItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải menu theo danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayMenuItems(List<MenuDTO> menuItems)
        {
            // Xóa dữ liệu cũ
            dgvMenuItems.Rows.Clear();

            // Thêm dữ liệu mới
            foreach (MenuDTO item in menuItems)
            {
                int rowId = dgvMenuItems.Rows.Add();
                DataGridViewRow row = dgvMenuItems.Rows[rowId];
                
                row.Cells[0].Value = item.Id;
                row.Cells[1].Value = item.Name;
                row.Cells[2].Value = string.Format("{0:N0} VNĐ", item.Price);
            }
        }

        private void SetupTableView()
        {
            // Khởi tạo Dictionary floorPanels để dễ dàng quản lý các FlowLayoutPanel 
            floorPanels = new Dictionary<string, FlowLayoutPanel>
            {
                { "Tầng 1", flpTables1 },
                { "Tầng 2", flpTables2 },
                { "Tầng 3", flpTables3 },
                { "Tầng 4", flpTables4 }
            };
            
            // Không khởi tạo tabFloors vì nó đã được tạo trong Designer
            
            // Thêm sự kiện cho txtBillSearch nếu chưa được thêm
            this.txtBillSearch.Enter += txtBillSearch_Enter;
            this.txtBillSearch.Leave += txtBillSearch_Leave;
        }

        private void LoadTables()
        {
            try
            {
                // Xóa tất cả các tab hiện tại
                tabFloors.TabPages.Clear();
                floorPanels.Clear();
                
                // Tạo ToolTip để hiển thị thông tin khi hover
                ToolTip tableToolTip = new ToolTip();
                
                // Lấy danh sách bàn từ database với trạng thái
                string query = @"
                    SELECT f.id, f.Name, tf.Status, f.Location  
                    FROM Facility f
                    INNER JOIN TableFacility tf ON f.id = tf.id
                    ORDER BY f.Location, f.Name";
                
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                
                // Lấy danh sách các tầng duy nhất
                HashSet<string> floors = new HashSet<string>();
                foreach (DataRow row in data.Rows)
                {
                    string location = row["Location"].ToString();
                    // Lấy tầng từ location (ví dụ: "Tầng 1", "Tầng 2")
                    string floor = ExtractFloorFromLocation(location);
                    floors.Add(floor);
                }
                
                // Tạo các tab cho từng tầng
                foreach (string floor in floors)
                {
                    // Tạo tab page mới
                    TabPage tabPage = new TabPage(floor);
                    
                    // Tạo FlowLayoutPanel cho tầng
                    FlowLayoutPanel flpTablesForFloor = new FlowLayoutPanel();
                    flpTablesForFloor.Dock = DockStyle.Fill;
                    flpTablesForFloor.AutoScroll = true;
                    flpTablesForFloor.Padding = new Padding(5);
                    flpTablesForFloor.Margin = new Padding(0);
                    
                    // Thêm FlowLayoutPanel vào tab
                    tabPage.Controls.Add(flpTablesForFloor);
                    
                    // Thêm tab vào TabControl
                    tabFloors.TabPages.Add(tabPage);
                    
                    // Lưu FlowLayoutPanel vào Dictionary
                    floorPanels.Add(floor, flpTablesForFloor);
                }
                
                // Thêm các button bàn vào FlowLayoutPanel tương ứng
                foreach (DataRow row in data.Rows)
                {
                    int tableId = Convert.ToInt32(row["id"]);
                    string tableName = row["Name"].ToString();
                    string status = row["Status"].ToString();
                    string location = row["Location"].ToString();
                    string floor = ExtractFloorFromLocation(location);
                    
                    // Kiểm tra xem bàn có hóa đơn đang mở không
                    string billQuery = "SELECT id FROM Bill WHERE TableID = " + tableId + " AND Status = 0";
                    DataTable billData = DataProvider.Instance.ExecuteQuery(billQuery);
                    bool hasOpenBill = billData.Rows.Count > 0;
                    
                    Button btn = new Button()
                    {
                        Width = 80,
                        Height = 80,
                        Text = tableName,
                        Tag = tableId,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        Margin = new Padding(5),
                        FlatStyle = FlatStyle.Flat
                    };
                    
                    // Thiết lập màu sắc và trạng thái giống code cũ
                    switch (status)
                    {
                        case "Trống":
                            btn.BackColor = Color.LightGreen;
                            btn.Enabled = true;
                            tableToolTip.SetToolTip(btn, $"{tableName} - {location} (Trống)");
                            break;
                            
                        case "Có người":
                            btn.BackColor = Color.Orange;
                            // Chỉ cho phép click vào bàn có người nếu có hóa đơn mở
                            btn.Enabled = hasOpenBill;
                            if (hasOpenBill)
                            {
                                tableToolTip.SetToolTip(btn, $"{tableName} - {location} (Có người - Có đơn)");
                            }
                            else
                            {
                                tableToolTip.SetToolTip(btn, $"{tableName} - {location} (Có người - Không thể chọn)");
                                // Làm mờ button để chỉ ra rằng nó không thể click được
                                btn.ForeColor = Color.Gray;
                            }
                            break;
                            
                        case "Đã đặt":
                            btn.BackColor = Color.Yellow;
                            btn.Enabled = true; // Có thể chọn bàn đã đặt để tạo đơn cho khách đến
                            tableToolTip.SetToolTip(btn, $"{tableName} - {location} (Đã đặt)");
                            break;
                            
                        default:
                            btn.BackColor = Color.Gray;
                            btn.Enabled = false;
                            tableToolTip.SetToolTip(btn, $"{tableName} - {location} (Không sẵn sàng)");
                            break;
                    }
                    
                    // Thêm sự kiện click
                    btn.Click += Table_Click;
                    
                    // Thêm button vào FlowLayoutPanel của tầng tương ứng
                    if (floorPanels.ContainsKey(floor))
                    {
                        floorPanels[floor].Controls.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm trích xuất thông tin tầng từ vị trí
        private string ExtractFloorFromLocation(string location)
        {
            // Trong trường hợp đơn giản, nếu location có định dạng "Tầng X - ..."
            if (location.StartsWith("Tầng"))
            {
                int dashIndex = location.IndexOf('-');
                if (dashIndex > 0)
                {
                    return location.Substring(0, dashIndex).Trim();
                }
                else
                {
                    // Trường hợp không có dấu gạch ngang, lấy toàn bộ chuỗi
                    return location.Trim();
                }
            }
            
            // Nếu không có định dạng rõ ràng, gán vào "Khác"
            return "Khác";
        }

        // Xử lý sự kiện khi nhấn vào button bàn
        private void Table_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int tableId = Convert.ToInt32(clickedButton.Tag);
            
            // Kiểm tra trạng thái bàn
            string statusQuery = "SELECT Status FROM TableFacility WHERE id = " + tableId;
            object statusObj = DataProvider.Instance.ExecuteScalar(statusQuery);
            
            if (statusObj != null && statusObj.ToString() == "Có người")
            {
                // Kiểm tra xem có hóa đơn đang mở không
                string billQuery = "SELECT id FROM Bill WHERE TableID = " + tableId + " AND Status = 0";
                object billObj = DataProvider.Instance.ExecuteScalar(billQuery);
                
                if (billObj == null)
                {
                    MessageBox.Show("Bàn này đã có khách nhưng chưa có hóa đơn. Không thể tạo đơn hàng mới.", 
                                   "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            
            // Đổi màu của button trước đó về màu bình thường
            if (selectedTableButton != null)
            {
                // Lấy trạng thái của bàn từ TableFacility để thiết lập lại màu sắc đúng
                string prevStatusQuery = "SELECT Status FROM TableFacility WHERE id = " + selectedTableButton.Tag;
                object prevStatusObj = DataProvider.Instance.ExecuteScalar(prevStatusQuery);
                
                if (prevStatusObj != null && prevStatusObj != DBNull.Value)
                {
                    string status = prevStatusObj.ToString();
                    switch (status)
                    {
                        case "Trống":
                            selectedTableButton.BackColor = Color.LightGreen;
                            break;
                        case "Có người":
                            selectedTableButton.BackColor = Color.Orange;
                            break;
                        case "Đã đặt":
                            selectedTableButton.BackColor = Color.Yellow;
                            break;
                        default:
                            selectedTableButton.BackColor = Color.Gray;
                            break;
                    }
                }
            }
            
            // Đặt button đang chọn với viền đậm hoặc màu khác
            selectedTableButton = clickedButton;
            clickedButton.BackColor = Color.LightBlue; // Màu highlight khi chọn
            
            // Lưu ID bàn và tải hóa đơn nếu có
            currentTableID = tableId;
            
            // Cập nhật label hiển thị bàn đang chọn
            lblTable.Text = "Bàn: " + clickedButton.Text;
            
            // Kiểm tra và tải hóa đơn hiện tại của bàn
            try
            {
                string getBillQuery = "SELECT id FROM Bill WHERE TableID = " + tableId + " AND Status = 0";
                DataTable billResult = DataProvider.Instance.ExecuteQuery(getBillQuery);
                
                if (billResult.Rows.Count > 0)
                {
                    currentBillID = Convert.ToInt32(billResult.Rows[0]["id"]);
                    LoadBillDetails(currentBillID.Value);
                }
                else
                {
                    currentBillID = null;
                    dgvOrderDetails.Rows.Clear();
                    lblTotal.Text = "Tổng: 0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadAllMenuItems();
                return;
            }

            try
            {
                // Tìm kiếm món theo tên
                List<MenuDTO> results = MenuDAO.Instance.GetListMenu().FindAll(
                    item => item.Name.ToLower().Contains(searchText.ToLower()));
                DisplayMenuItems(results);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvMenuItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try 
                {
                    // Đánh dấu đã chọn món
                    hasSelectedMenuItem = true;
                    
                    // Xử lý logic hiện có - sửa lại tên cột cho đúng
                    int menuItemId = Convert.ToInt32(dgvMenuItems.Rows[e.RowIndex].Cells["ID"].Value);
                    
                    // Sửa từ "Name" thành "Tên món" theo đúng tên cột trong DataGridView
                    string menuItemName = dgvMenuItems.Rows[e.RowIndex].Cells["Tên món"].Value.ToString();
                    
                    // Xử lý giá tiền từ chuỗi định dạng
                    string priceString = dgvMenuItems.Rows[e.RowIndex].Cells["Giá"].Value.ToString();
                    // Loại bỏ các ký tự không phải số và dấu thập phân
                    priceString = Regex.Replace(priceString, @"[^\d.]", "");
                    decimal price = 0;
                    if (decimal.TryParse(priceString, out price))
                    {
                        // Sử dụng MessageBox.Show với TextBox để nhập số lượng
                        string input = Microsoft.VisualBasic.Interaction.InputBox("Nhập số lượng:", "Số lượng", "1");
                        
                        if (!string.IsNullOrEmpty(input))
                        {
                            if (int.TryParse(input, out int quantity) && quantity > 0)
                            {
                                AddItemToOrder(menuItemId, menuItemName, quantity);
                            }
                            else
                            {
                                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (số nguyên dương)!", "Lỗi", 
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể đọc giá của món ăn!", "Lỗi", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn món: " + ex.Message, "Lỗi", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddItemToOrder(int menuId, string menuName, int quantity)
        {
            try
            {
                if (currentTableID == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn trước khi thêm món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đánh dấu đã chọn món
                hasSelectedMenuItem = true;
                
                // Lấy giá của món ăn
                string query = string.Format("SELECT Price FROM Menu WHERE id = {0}", menuId);
                object priceObj = DataProvider.Instance.ExecuteScalar(query);
                decimal price = Convert.ToDecimal(priceObj);
                
                // Kiểm tra xem món đã có trong đơn hàng tạm thời chưa
                TempOrderItem existingItem = tempOrderItems.FirstOrDefault(item => item.MenuID == menuId);
                
                if (existingItem != null)
                {
                    // Nếu món đã tồn tại, cập nhật số lượng
                    existingItem.Quantity += quantity;
                    MessageBox.Show($"Đã cập nhật số lượng món {menuName} lên {existingItem.Quantity}!", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Nếu món chưa tồn tại, thêm mới vào danh sách tạm thời
                    TempOrderItem newItem = new TempOrderItem
                    {
                        MenuID = menuId,
                        MenuName = menuName,
                        Quantity = quantity,
                        Price = price
                    };
                    
                    tempOrderItems.Add(newItem);
                    MessageBox.Show("Đã thêm món vào đơn hàng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                // Đánh dấu là đã có thay đổi trong đơn hàng
                hasChangedOrder = true;
                
                // Hiển thị đơn hàng tạm thời
                DisplayTempOrder();
                
                // Cập nhật danh sách hóa đơn
                LoadActiveBills();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món vào đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayTempOrder()
        {
            try
            {
                // Xóa dữ liệu cũ
                dgvOrderDetails.Rows.Clear();
                
                // Thêm dữ liệu từ đơn hàng tạm thời
                foreach (TempOrderItem item in tempOrderItems)
                {
                    int rowId = dgvOrderDetails.Rows.Add();
                    DataGridViewRow dgvRow = dgvOrderDetails.Rows[rowId];
                    
                    dgvRow.Cells[0].Value = item.MenuID;  // Sử dụng MenuID làm id tạm thời
                    dgvRow.Cells[1].Value = item.MenuName;
                    dgvRow.Cells[2].Value = item.Quantity;
                    dgvRow.Cells[3].Value = string.Format("{0:N0} VNĐ", item.Price);
                    dgvRow.Cells[4].Value = string.Format("{0:N0} VNĐ", item.Total);
                    
                    // Thông tin bàn (nếu cột này tồn tại)
                    if (dgvOrderDetails.Columns.Count > 5)
                    {
                        string tableQuery = string.Format("SELECT f.Name, f.Location FROM Facility f WHERE f.id = {0}", currentTableID);
                        DataTable tableData = DataProvider.Instance.ExecuteQuery(tableQuery);
                        
                        if (tableData.Rows.Count > 0)
                        {
                            string tableName = tableData.Rows[0]["Name"].ToString();
                            string tableLocation = tableData.Rows[0]["Location"].ToString();
                            dgvRow.Cells[5].Value = $"{tableName} - {tableLocation}";
                        }
                    }
                }
                
                // Cập nhật tổng tiền
                decimal total = tempOrderItems.Sum(item => item.Total);
                lblTotal.Text = string.Format("Tổng: {0:N0} VNĐ", total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị đơn hàng tạm thời: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetails.SelectedRows.Count > 0)
            {
                // TODO: Thêm code xóa món khỏi database
                // Ví dụ:
                // int billInfoId = Convert.ToInt32(dgvOrderDetails.SelectedRows[0].Cells[0].Value);
                // BillInfoDAO.Instance.DeleteBillInfo(billInfoId);
                
                // Xóa khỏi datagridview
                dgvOrderDetails.Rows.RemoveAt(dgvOrderDetails.SelectedRows[0].Index);
                
                // Cập nhật tổng tiền
                UpdateTotal();
                
                // Cập nhật danh sách hóa đơn
                LoadActiveBills();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // TODO: Thêm code cập nhật đơn hàng
            MessageBox.Show("Đã cập nhật đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Cập nhật danh sách hóa đơn
            LoadActiveBills();
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetails.Rows.Count == 0)
            {
                MessageBox.Show("Không có món nào để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentBillID == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (MessageBox.Show("Xác nhận thanh toán?", "Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Thanh toán và cập nhật database
                    BillDAO.Instance.CheckOut(currentBillID.Value);
                    
                    MessageBox.Show("Đã thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Bằng:
                    btnPrint_Click(this, EventArgs.Empty);
                    
                    // Làm mới giao diện
                    dgvOrderDetails.Rows.Clear();
                    lblTotal.Text = "Tổng: 0 VNĐ";
                    currentBillID = null;
                    
                    // Cập nhật lại danh sách bàn
                    LoadTables();
                    
                    // Cập nhật lại danh sách hóa đơn đang hoạt động
                    LoadActiveBills();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (currentBillID == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                // Sử dụng BillDAO thay vì truy vấn trực tiếp
                DataTable billInfo = BillDAO.Instance.GetBillInfo(currentBillID.Value);
                
                if (billInfo.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Lấy chi tiết hóa đơn từ BillDAO
                DataTable billDetails = BillDAO.Instance.GetBillDetails(currentBillID.Value);
                
                // Khởi tạo và hiển thị PrintPreviewDialog
                using (PrintDocument pd = new PrintDocument())
                {
                    pd.PrintPage += (s, ev) => PrintPage(s, ev, billInfo, billDetails);
                    
                    PrintPreviewDialog ppd = new PrintPreviewDialog();
                    ppd.Document = pd;
                    ppd.WindowState = FormWindowState.Maximized;
                    
                    if (ppd.ShowDialog() == DialogResult.OK)
                    {
                        pd.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Exception details: " + ex.ToString());
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e, DataTable billInfo, DataTable billDetails)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (billInfo == null || billInfo.Rows.Count == 0)
                {
                    e.Graphics.DrawString("Không có dữ liệu hóa đơn!", new Font("Arial", 14, FontStyle.Bold), Brushes.Red, 100, 100);
                    return;
                }
                
                if (billDetails == null || billDetails.Rows.Count == 0)
                {
                    e.Graphics.DrawString("Không có chi tiết hóa đơn!", new Font("Arial", 14, FontStyle.Bold), Brushes.Red, 100, 150);
                }

                // Lấy thông tin từ DataTable
                DataRow billRow = billInfo.Rows[0];
                
                // Định nghĩa font và bút
                Font titleFont = new Font("Arial", 12, FontStyle.Bold);
                Font subTitleFont = new Font("Arial", 10, FontStyle.Bold);
                Font headerFont = new Font("Arial", 9, FontStyle.Bold);
                Font normalFont = new Font("Arial", 9, FontStyle.Regular);
                Font totalFont = new Font("Arial", 10, FontStyle.Bold);
                Font thankYouFont = new Font("Arial", 9, FontStyle.Italic);
                
                Brush textBrush = Brushes.Black;
                Pen linePen = new Pen(Color.Black, 1);
                
                // Định vị và kích thước (căn theo tờ A5)
                float yPos = 50;
                int leftMargin = 50;
                int width = 500;
                int centerX = leftMargin + width / 2;
                
                // Vẽ thông tin quán
                e.Graphics.DrawString("Liceria & Co", titleFont, textBrush, centerX - 55, yPos);
                yPos += 20;
                
                string address = "Đường 3/2 , Xuân Khánh , Ninh Kiều, Cần Thơ";
                e.Graphics.DrawString(address, normalFont, textBrush, centerX - e.Graphics.MeasureString(address, normalFont).Width / 2, yPos);
                yPos += 15;
                
                string phone = "ĐT: 0706871283";
                e.Graphics.DrawString(phone, normalFont, textBrush, centerX - e.Graphics.MeasureString(phone, normalFont).Width / 2, yPos);
                yPos += 20;
                
                // Vẽ tiêu đề hóa đơn
                e.Graphics.DrawString("HÓA ĐƠN BÁN HÀNG", subTitleFont, textBrush, centerX - 70, yPos);
                yPos += 25;
                
                // Thông tin hóa đơn
                e.Graphics.DrawString($"Số hóa đơn: {billRow["id"]}", normalFont, textBrush, leftMargin, yPos);
                e.Graphics.DrawString($"Bàn: {billRow["TableName"]}", normalFont, textBrush, leftMargin + 200, yPos);
                yPos += 20;
                
                // Thông tin ngày giờ
                DateTime checkInDate = Convert.ToDateTime(billRow["CheckInDate"]);
                string dateStr = checkInDate.ToString("dd/MM/yyyy");
                e.Graphics.DrawString($"Ngày: {dateStr}", normalFont, textBrush, leftMargin, yPos);
                
                string timeStr = checkInDate.ToString("HH:mm");
                e.Graphics.DrawString($"Giờ vào: {timeStr}", normalFont, textBrush, leftMargin + 200, yPos);
                e.Graphics.DrawString($"Giờ ra: {DateTime.Now.ToString("HH:mm")}", normalFont, textBrush, leftMargin + 350, yPos);
                yPos += 20;
                
                // Thu ngân
                if (billRow["StaffName"] != DBNull.Value)
                    e.Graphics.DrawString($"Thu ngân: {billRow["StaffName"]}", normalFont, textBrush, leftMargin, yPos);
                yPos += 15;
                
                // Vẽ đường kẻ ngang
                e.Graphics.DrawLine(linePen, leftMargin, yPos, leftMargin + width, yPos);
                yPos += 10;
                
                // Tiêu đề bảng chi tiết
                string[] headers = new string[] { "Mặt hàng", "SL", "Giá", "Thành tiền" };
                float[] colWidths = new float[] { 0.45f, 0.15f, 0.2f, 0.2f };
                
                // Vẽ tiêu đề bảng
                float xPos = leftMargin;
                for (int i = 0; i < headers.Length; i++)
                {
                    e.Graphics.DrawString(headers[i], headerFont, textBrush, xPos, yPos);
                    xPos += width * colWidths[i];
                }
                yPos += 20;
                
                // Vẽ đường kẻ ngang dưới tiêu đề
                e.Graphics.DrawLine(linePen, leftMargin, yPos, leftMargin + width, yPos);
                yPos += 10;
                
                // Vẽ các mục trong bảng
                decimal totalAmount = 0;
                foreach (DataRow row in billDetails.Rows)
                {
                    xPos = leftMargin;
                    
                    // Tên món
                    e.Graphics.DrawString(row["MenuName"].ToString(), normalFont, textBrush, xPos, yPos);
                    
                    // Số lượng
                    xPos = leftMargin + width * colWidths[0];
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    e.Graphics.DrawString(quantity.ToString(), normalFont, textBrush, xPos, yPos);
                    
                    // Đơn giá
                    xPos = leftMargin + width * (colWidths[0] + colWidths[1]);
                    decimal price = Convert.ToDecimal(row["Price"]);
                    e.Graphics.DrawString(string.Format("{0:N0}", price), normalFont, textBrush, xPos, yPos);
                    
                    // Thành tiền
                    xPos = leftMargin + width * (colWidths[0] + colWidths[1] + colWidths[2]);
                    decimal total = Convert.ToDecimal(row["Total"]);
                    totalAmount += total;
                    e.Graphics.DrawString(string.Format("{0:N0}", total), normalFont, textBrush, xPos, yPos);
                    
                    yPos += 20;
                }
                
                // Vẽ đường kẻ ngang dưới danh sách món
                e.Graphics.DrawLine(linePen, leftMargin, yPos, leftMargin + width, yPos);
                yPos += 10;
                
                // Hiển thị tổng cộng
                float rightAlign = leftMargin + width - 80;
                e.Graphics.DrawString("Tổng:", totalFont, textBrush, rightAlign - 80, yPos);
                e.Graphics.DrawString(string.Format("{0:N0}", totalAmount), totalFont, textBrush, rightAlign, yPos);
                yPos += 20;
                
                // Hiển thị giảm giá nếu có
                if (billRow["Discount"] != DBNull.Value && Convert.ToDecimal(billRow["Discount"]) > 0)
                {
                    decimal discount = Convert.ToDecimal(billRow["Discount"]);
                    decimal discountAmount = totalAmount * discount / 100;
                    
                    e.Graphics.DrawString($"Giảm giá ({discount}%):", normalFont, textBrush, rightAlign - 80, yPos);
                    e.Graphics.DrawString(string.Format("- {0:N0}", discountAmount), normalFont, textBrush, rightAlign, yPos);
                    yPos += 20;
                    
                    // Thành tiền sau giảm giá
                    decimal finalAmount = totalAmount - discountAmount;
                    e.Graphics.DrawString("Thanh toán:", totalFont, textBrush, rightAlign - 80, yPos);
                    e.Graphics.DrawString(string.Format("{0:N0}", finalAmount), totalFont, textBrush, rightAlign, yPos);
                    yPos += 20;
                }
                
                // Vẽ đường kẻ dưới tổng cộng
                e.Graphics.DrawLine(linePen, leftMargin, yPos, leftMargin + width, yPos);
                yPos += 15;
                
                // Hiển thị lời cảm ơn
                string thankYouMsg = "Cảm ơn Quý khách. Hẹn gặp lại!";
                e.Graphics.DrawString(thankYouMsg, thankYouFont, textBrush, 
                    centerX - e.Graphics.MeasureString(thankYouMsg, thankYouFont).Width / 2, yPos);
                
                // Không còn trang khác để in
                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi vẽ trang in: " + ex.ToString());
                e.Graphics.DrawString("Đã xảy ra lỗi khi in hóa đơn: " + ex.Message, new Font("Arial", 12, FontStyle.Bold), Brushes.Red, 100, 100);
            }
        }

        private void BtnSendToKitchen_Click(object sender, EventArgs e)
        {
            if (currentBillID == null)
            {
                MessageBox.Show("Chưa có hóa đơn để gửi bếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Logic gửi bếp hiện tại
            // ...
            
            // Cập nhật danh sách hóa đơn
            LoadActiveBills();
            
            MessageBox.Show("Đã gửi món ăn tới bếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        // Thêm phương thức xử lý sự kiện Click cho lblMenuListTitle
        private void lblMenuListTitle_Click(object sender, EventArgs e)
        {
            // Để trống - chỉ để tránh lỗi
        }

        private void LoadActiveBills()
        {
            try
            {
                // Xóa dữ liệu cũ
                dgvActiveBills.Rows.Clear();

                // Truy vấn để lấy danh sách hóa đơn đang hoạt động (truy vấn mới)
                string query = @"
                    SELECT 
                        b.id AS [Mã hóa đơn],
                        f.Name AS [Bàn],
                        b.CheckInDate AS [Thời gian vào],
                        COUNT(bi.id) AS [Số món],
                        SUM(m.Price * bi.Quantity) AS [Tổng tiền],
                        CASE 
                            WHEN b.Status = 0 THEN N'Đang phục vụ'
                            WHEN b.Status = 1 THEN N'Đã thanh toán'
                            ELSE N'Trạng thái khác'
                        END AS [Trạng thái],
                        u.FullName AS [Nhân viên]
                    FROM 
                        Bill b
                    JOIN 
                        Facility f ON b.TableID = f.id
                    LEFT JOIN 
                        BillInfo bi ON b.id = bi.BillID
                    LEFT JOIN 
                        Menu m ON bi.MenuID = m.id
                    LEFT JOIN 
                        Users u ON b.UserID = u.uid
                    WHERE 
                        b.Status = 0
                        AND b.CustomerLeft = 0
                    GROUP BY 
                        b.id, f.Name, b.CheckInDate, b.Status, u.FullName
                    ORDER BY 
                        b.CheckInDate DESC";

                // In câu truy vấn để debug
                Console.WriteLine("Query: " + query);

                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                
                // In số lượng kết quả để debug
                Console.WriteLine("Số lượng hóa đơn: " + data.Rows.Count);

                // Thêm dữ liệu vào DataGridView
                foreach (DataRow row in data.Rows)
                {
                    int rowIndex = dgvActiveBills.Rows.Add();
                    DataGridViewRow gridRow = dgvActiveBills.Rows[rowIndex];

                    gridRow.Cells["ID"].Value = row["Mã hóa đơn"];
                    gridRow.Cells["TableName"].Value = row["Bàn"];
                    gridRow.Cells["CheckInTime"].Value = ((DateTime)row["Thời gian vào"]).ToString("dd/MM/yyyy HH:mm");
                    gridRow.Cells["TotalItems"].Value = row["Số món"];
                    
                    // Xử lý giá trị tổng tiền có thể là DBNull
                    object totalAmount = row["Tổng tiền"];
                    if (totalAmount != DBNull.Value)
                    {
                        gridRow.Cells["TotalAmount"].Value = string.Format("{0:N0} VNĐ", totalAmount);
                    }
                    else
                    {
                        gridRow.Cells["TotalAmount"].Value = "0 VNĐ";
                    }
                    
                    gridRow.Cells["Status"].Value = row["Trạng thái"];
                    gridRow.Cells["Staff"].Value = row["Nhân viên"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message, 
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // In lỗi để debug
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        private void DgvActiveBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    int billId = Convert.ToInt32(dgvActiveBills.Rows[e.RowIndex].Cells["ID"].Value);
                    string tableName = dgvActiveBills.Rows[e.RowIndex].Cells["TableName"].Value.ToString();
                    
                    // Xác nhận xem người dùng muốn thanh toán hay xem chi tiết
                    if (MessageBox.Show($"Bạn muốn xem chi tiết hóa đơn #{billId} - Bàn: {tableName}?", 
                                      "Tùy chọn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Tìm tableID từ tableName
                        string tableQuery = "SELECT id FROM Facility WHERE Name = @tableName";
                        object tableIdObj = DataProvider.Instance.ExecuteScalar(tableQuery, new object[] { tableName });
                        
                        if (tableIdObj != null)
                        {
                            int tableId = Convert.ToInt32(tableIdObj);
                            
                            // Đặt bàn hiện tại và billID
                            currentTableID = tableId;
                            currentBillID = billId;
                            
                            // Cập nhật UI để hiển thị chi tiết hóa đơn
                            LoadBillDetails(billId);
                            
                            // Cập nhật để thêm chọn đúng tab tầng
                            string tableLocation = ""; // Lấy thông tin location của bàn từ database
                            string floor = ExtractFloorFromLocation(tableLocation);
                            
                            // Chọn tab tầng tương ứng
                            for (int i = 0; i < tabFloors.TabPages.Count; i++)
                            {
                                if (tabFloors.TabPages[i].Text == floor)
                                {
                                    tabFloors.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xử lý hóa đơn: " + ex.Message, 
                                   "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FilterActiveBills(string keyword)
        {
            try
            {
                foreach (DataGridViewRow row in dgvActiveBills.Rows)
                {
                    bool visible = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(keyword.ToLower()))
                        {
                            visible = true;
                            break;
                        }
                    }
                    row.Visible = visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc hóa đơn: " + ex.Message, 
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckoutBillFromList(int billId)
        {
            try
            {
                string tableName = string.Empty;
                
                // Tìm tên bàn
                foreach (DataGridViewRow row in dgvActiveBills.Rows)
                {
                    if (Convert.ToInt32(row.Cells["ID"].Value) == billId)
                    {
                        tableName = row.Cells["TableName"].Value.ToString();
                        break;
                    }
                }
                
                if (MessageBox.Show($"Xác nhận thanh toán hóa đơn #{billId} - Bàn: {tableName}?", 
                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Thanh toán hóa đơn
                    if (BillDAO.Instance.CheckOut(billId))
                    {
                        MessageBox.Show("Thanh toán thành công!", "Thông báo");
                        
                        // Làm mới danh sách
                        LoadActiveBills();
                        LoadTables();
                        
                        // Nếu đang xem chi tiết hóa đơn này thì làm mới
                        if (currentBillID == billId)
                        {
                            dgvOrderDetails.Rows.Clear();
                            lblTotal.Text = "Tổng: 0 VNĐ";
                            currentBillID = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể thanh toán hóa đơn!", 
                                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán: " + ex.Message, 
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBillSearch_Enter(object sender, EventArgs e)
        {
            if (txtBillSearch.Text == "Tìm kiếm hóa đơn...")
            {
                txtBillSearch.Text = "";
                txtBillSearch.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void txtBillSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBillSearch.Text))
            {
                txtBillSearch.Text = "Tìm kiếm hóa đơn...";
                txtBillSearch.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            // Làm mới tất cả dữ liệu
            LoadCategories();
            LoadTables();
            LoadAllMenuItems();
            LoadActiveBills();
            
            // Xóa selection hiện tại nếu có
            currentTableID = null;
            currentBillID = null;
            selectedTableButton = null;
            lblTable.Text = "Bàn:";
            dgvOrderDetails.Rows.Clear();
            lblTotal.Text = "Tổng: 0 VNĐ";
            
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblOrder_Click(object sender, EventArgs e)
        {

        }

        private void lblActiveBills_Click(object sender, EventArgs e)
        {

        }

        private void SetupAutoRefresh()
        {
            // Khởi tạo timer tự động làm mới mỗi 30 giây
            autoRefreshTimer = new System.Windows.Forms.Timer();
            autoRefreshTimer.Interval = 30000; // 30 giây
            autoRefreshTimer.Tick += (s, e) => {
                // Chỉ cập nhật danh sách hóa đơn mà không làm refresh toàn bộ
                LoadActiveBills();
            };
            autoRefreshTimer.Start();
        }

        // Thêm phương thức xử lý sự kiện FormClosing
        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dừng và giải phóng timer để tránh memory leak
            if (autoRefreshTimer != null)
            {
                autoRefreshTimer.Stop();
                autoRefreshTimer.Dispose();
            }
            
            // Dừng và giải phóng timerRefresh
            if (timerRefresh != null)
            {
                timerRefresh.Stop();
                timerRefresh.Dispose();
            }
        }

        // Thêm phương thức xử lý sự kiện nút "Tạo hóa đơn"
        private void BtnCreateBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentTableID == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn trước khi tạo hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (tempOrderItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm ít nhất một món vào đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Kiểm tra xem bàn đã có hóa đơn chưa
                string checkBillQuery = string.Format("SELECT id FROM Bill WHERE TableID = {0} AND Status = 0", currentTableID);
                DataTable billCheck = DataProvider.Instance.ExecuteQuery(checkBillQuery);
                
                if (billCheck.Rows.Count > 0)
                {
                    // Nếu đã có hóa đơn, hỏi người dùng có muốn thêm vào hóa đơn hiện tại không
                    DialogResult dialogResult = MessageBox.Show(
                        "Bàn này đã có hóa đơn. Bạn có muốn thêm các món vào hóa đơn hiện tại không?", 
                        "Xác nhận", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question);
                        
                    if (dialogResult == DialogResult.Yes)
                    {
                        currentBillID = Convert.ToInt32(billCheck.Rows[0]["id"]);
                        
                        // Thêm từng món vào hóa đơn hiện tại
                        foreach (TempOrderItem item in tempOrderItems)
                        {
                            // Kiểm tra món đã tồn tại chưa
                            string checkItemQuery = string.Format(
                                "SELECT id, Quantity FROM BillInfo WHERE BillID = {0} AND MenuID = {1}", 
                                currentBillID, item.MenuID);
                            DataTable itemCheck = DataProvider.Instance.ExecuteQuery(checkItemQuery);
                            
                            if (itemCheck.Rows.Count > 0)
                            {
                                // Nếu món đã tồn tại, cập nhật số lượng
                                int existingId = Convert.ToInt32(itemCheck.Rows[0]["id"]);
                                int existingQuantity = Convert.ToInt32(itemCheck.Rows[0]["Quantity"]);
                                int newQuantity = existingQuantity + item.Quantity;
                                
                                string updateQuery = string.Format(
                                    "UPDATE BillInfo SET Quantity = {0} WHERE id = {1}", 
                                    newQuantity, existingId);
                                DataProvider.Instance.ExecuteNonQuery(updateQuery);
                            }
                            else
                            {
                                // Nếu món chưa tồn tại, thêm mới
                                string insertQuery = string.Format(
                                    "INSERT INTO BillInfo (BillID, MenuID, Quantity) VALUES ({0}, {1}, {2})", 
                                    currentBillID, item.MenuID, item.Quantity);
                                DataProvider.Instance.ExecuteNonQuery(insertQuery);
                            }
                        }
                        
                        // Xóa danh sách tạm thời sau khi đã thêm vào hóa đơn
                        tempOrderItems.Clear();
                        hasChangedOrder = false;
                        
                        // Cập nhật hiển thị
                        LoadBillDetails(currentBillID.Value);
                        RefreshTableStatus();
                        
                        MessageBox.Show("Đã thêm các món vào hóa đơn hiện tại!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Nếu chưa có hóa đơn, tạo hóa đơn mới
                    string insertBillQuery = string.Format(
                        "INSERT INTO Bill (TableID, Status, Discount) VALUES ({0}, 0, 0); SELECT SCOPE_IDENTITY();", 
                        currentTableID);
                    object result = DataProvider.Instance.ExecuteScalar(insertBillQuery);
                    
                    if (result != null)
                    {
                        currentBillID = Convert.ToInt32(result);
                        
                        // Cập nhật trạng thái bàn
                        string updateTableQuery = string.Format(
                            "UPDATE TableFacility SET Status = N'Có người' WHERE id = {0}", currentTableID);
                        DataProvider.Instance.ExecuteNonQuery(updateTableQuery);
                        
                        // Thêm từng món vào hóa đơn mới
                        foreach (TempOrderItem item in tempOrderItems)
                        {
                            string insertItemQuery = string.Format(
                                "INSERT INTO BillInfo (BillID, MenuID, Quantity) VALUES ({0}, {1}, {2})", 
                                currentBillID, item.MenuID, item.Quantity);
                            DataProvider.Instance.ExecuteNonQuery(insertItemQuery);
                        }
                        
                        // Xóa danh sách tạm thời sau khi đã thêm vào hóa đơn
                        tempOrderItems.Clear();
                        hasChangedOrder = false;
                        
                        // Cập nhật hiển thị
                        LoadBillDetails(currentBillID.Value);
                        RefreshTableStatus();
                        
                        MessageBox.Show("Đã tạo hóa đơn mới thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không thể tạo hóa đơn mới!", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
                // Cập nhật danh sách hóa đơn
                LoadActiveBills();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo hóa đơn: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm nút Tạo hóa đơn vào khởi tạo form (thêm trong constructor hoặc Load)
        private void InitializeCreateBillButton()
        {
            // Tạo nút "Tạo hóa đơn" nếu chưa có
            if (!pnlButtons.Controls.ContainsKey("btnCreateBill"))
            {
                Button btnCreateBill = new Button
                {
                    Name = "btnCreateBill",
                    Text = "Tạo hóa đơn",
                    Size = new Size(120, 30),
                    Location = new Point(670, 10),
                    TabIndex = 6
                };
                
                btnCreateBill.Click += BtnCreateBill_Click;
                pnlButtons.Controls.Add(btnCreateBill);
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            // Cập nhật danh sách hóa đơn
            LoadActiveBills();
            // Các cập nhật khác nếu cần
        }

        private void LoadBillDetails(int billId)
        {
            try
            {
                // Xóa dữ liệu cũ
                dgvOrderDetails.Rows.Clear();

                // Truy vấn để lấy chi tiết hóa đơn
                string query = @"
                    SELECT bi.id, m.id as MenuID, m.Name as MenuName, bi.Quantity, 
                           m.Price, (bi.Quantity * m.Price) as Total,
                           f.Name as TableName, f.Location as TableLocation
                    FROM BillInfo bi
                    JOIN Menu m ON bi.MenuID = m.id
                    JOIN Bill b ON bi.BillID = b.id
                    JOIN Facility f ON b.TableID = f.id
                    WHERE bi.BillID = " + billId;

                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                // Thêm dữ liệu vào DataGridView
                foreach (DataRow row in data.Rows)
                {
                    int rowIndex = dgvOrderDetails.Rows.Add();
                    DataGridViewRow gridRow = dgvOrderDetails.Rows[rowIndex];

                    gridRow.Cells[0].Value = row["MenuID"]; // ID món
                    gridRow.Cells[1].Value = row["MenuName"]; // Tên món
                    gridRow.Cells[2].Value = row["Quantity"]; // Số lượng
                    gridRow.Cells[3].Value = string.Format("{0:N0}", row["Price"]); // Đơn giá
                    gridRow.Cells[4].Value = string.Format("{0:N0}", row["Total"]); // Thành tiền
                    
                    // Thông tin bàn (nếu có cột này)
                    if (dgvOrderDetails.Columns.Count > 5)
                    {
                        gridRow.Cells[5].Value = $"{row["TableName"]} - {row["TableLocation"]}";
                    }
                }

                // Cập nhật tổng tiền
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvOrderDetails.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    string totalText = row.Cells[4].Value.ToString().Replace("VNĐ", "").Replace(".", "").Trim();
                    decimal itemTotal = 0;
                    if (decimal.TryParse(totalText, out itemTotal))
                    {
                        total += itemTotal;
                    }
                }
            }
            lblTotal.Text = string.Format("Tổng: {0:N0} VNĐ", total);
        }

        private void RefreshTableStatus()
        {
            try
            {
                LoadTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi làm mới trạng thái bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeTimers()
        {
            // Khởi tạo timer
            timerRefresh = new System.Windows.Forms.Timer();
            timerRefresh.Interval = 5000; // Cập nhật mỗi 5 giây
            timerRefresh.Tick += new EventHandler(TimerRefresh_Tick);
            timerRefresh.Start();
        }

        private void TimerRefresh_Tick(object sender, EventArgs e)
        {
            // Tải lại danh sách hóa đơn đang hoạt động
            LoadActiveBills();
        }

        private void BtnCustomerLeft_Click(object sender, EventArgs e)
        {
            if (currentBillID == null)
            {
                MessageBox.Show("Chưa có hóa đơn nào được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận việc khách hàng đã rời đi
            DialogResult result = MessageBox.Show(
                "Đánh dấu khách hàng đã rời đi? Hóa đơn sẽ vẫn giữ lại để xử lý sau.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Cập nhật trạng thái CustomerLeft trong cơ sở dữ liệu
                    string query = "UPDATE Bill SET CustomerLeft = 1 WHERE id = @billId";
                    int rowsAffected = DataProvider.Instance.ExecuteNonQuery(query, new object[] { currentBillID });

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã đánh dấu khách hàng rời đi!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Cập nhật danh sách hóa đơn
                        LoadActiveBills();
                        
                        // Reset trạng thái hiện tại
                        currentBillID = null;
                        LoadBillDetails(0); // Xóa chi tiết hóa đơn
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật trạng thái!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    public class TempOrderItem
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Price * Quantity;
    }
}