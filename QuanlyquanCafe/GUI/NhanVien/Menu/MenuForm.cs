using QuanlyquanCafe.Admin.DAO;
using QuanlyquanCafe.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace QuanlyquanCafe.GUI.NhanVien.Menu
{
    public partial class MenuForm : Form
    {
        private int? currentTableID = null;
        private int? currentBillID = null;
        //private FlowLayoutPanel flpTables;
        private Button selectedTableButton = null;
        private Dictionary<string, FlowLayoutPanel> floorPanels;

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
            this.btnPrint.Click += BtnPrint_Click;
            this.btnRefresh.Click += BtnRefresh_Click;
            SetupTableView();
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
            // Xử lý khi nhấn vào nút +
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvMenuItems.Columns.Count - 1)
            {
                if (currentTableID == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int menuId = Convert.ToInt32(dgvMenuItems.Rows[e.RowIndex].Cells[0].Value);
                string menuName = dgvMenuItems.Rows[e.RowIndex].Cells[1].Value.ToString();
                
                // Hiển thị dialog nhập số lượng
                using (Form quantityForm = new Form())
                {
                    quantityForm.Text = "Nhập số lượng";
                    quantityForm.Size = new Size(300, 150);
                    quantityForm.StartPosition = FormStartPosition.CenterParent;
                    quantityForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    quantityForm.MaximizeBox = false;
                    quantityForm.MinimizeBox = false;
                    
                    Label lblPrompt = new Label
                    {
                        Text = $"Số lượng cho món {menuName}:",
                        Location = new Point(20, 20),
                        AutoSize = true
                    };
                    
                    NumericUpDown numQuantity = new NumericUpDown
                    {
                        Location = new Point(20, 50),
                        Minimum = 1,
                        Maximum = 100,
                        Value = 1,
                        Width = 100
                    };
                    
                    Button btnOK = new Button
                    {
                        Text = "OK",
                        DialogResult = DialogResult.OK,
                        Location = new Point(150, 50),
                        Width = 75
                    };
                    
                    quantityForm.Controls.Add(lblPrompt);
                    quantityForm.Controls.Add(numQuantity);
                    quantityForm.Controls.Add(btnOK);
                    
                    if (quantityForm.ShowDialog() == DialogResult.OK)
                    {
                        int quantity = (int)numQuantity.Value;
                        AddItemToOrder(menuId, menuName, quantity);
                    }
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
                
                // Kiểm tra và tạo hóa đơn nếu chưa có
                if (currentBillID == null)
                {
                    // Giả sử userID = 1, trong thực tế lấy từ đăng nhập
                    int userID = 1; 
                    currentBillID = BillDAO.Instance.CreateBill(currentTableID.Value, userID);
                    
                    if (currentBillID <= 0)
                    {
                        MessageBox.Show("Lỗi tạo hóa đơn mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                
                // Thêm món vào hóa đơn
                bool result = BillInfoDAO.Instance.InsertBillInfo(currentBillID.Value, menuId, quantity);
                
                if (result)
                {
                    // Cập nhật hiển thị chi tiết hóa đơn
                    LoadBillDetails(currentBillID.Value);
                    
                    // Cập nhật tổng tiền
                    UpdateTotal();
                }
                else
                {
                    MessageBox.Show("Lỗi thêm món vào hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBillDetails(int billID)
        {
            try
            {
                // Lấy dữ liệu chi tiết hóa đơn và thông tin bàn
                string query = @"
                    SELECT bi.id, m.Name AS 'Món', bi.Quantity AS 'Số lượng', 
                          m.Price AS 'Giá', (m.Price * bi.Quantity) AS 'Thành tiền',
                          f.Name AS 'Tên bàn', f.Location AS 'Vị trí', tf.Status AS 'Trạng thái bàn'
                    FROM BillInfo bi
                    INNER JOIN Menu m ON bi.MenuID = m.id
                    INNER JOIN Bill b ON bi.BillID = b.id
                    INNER JOIN Facility f ON b.TableID = f.id
                    INNER JOIN TableFacility tf ON f.id = tf.id
                    WHERE bi.BillID = @billID";
                
                DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { billID });
                
                // Xóa dữ liệu cũ
                dgvOrderDetails.Rows.Clear();
                
                // Thêm cột thông tin bàn nếu chưa có
                if (dgvOrderDetails.Columns.Count < 6)
                {
                    // Kiểm tra xem cột đã tồn tại chưa trước khi thêm
                    if (!dgvOrderDetails.Columns.Contains("TableInfo"))
                    {
                        dgvOrderDetails.Columns.Add("TableInfo", "Thông tin bàn");
                    }
                }
                
                // Thêm dữ liệu mới
                foreach (DataRow row in data.Rows)
                {
                    int rowId = dgvOrderDetails.Rows.Add();
                    DataGridViewRow dgvRow = dgvOrderDetails.Rows[rowId];
                    
                    dgvRow.Cells[0].Value = row["id"];
                    dgvRow.Cells[1].Value = row["Món"];
                    dgvRow.Cells[2].Value = row["Số lượng"];
                    dgvRow.Cells[3].Value = string.Format("{0:N0} VNĐ", Convert.ToDecimal(row["Giá"]));
                    dgvRow.Cells[4].Value = string.Format("{0:N0} VNĐ", Convert.ToDecimal(row["Thành tiền"]));
                    
                    // Thông tin bàn
                    string tableInfo = $"{row["Tên bàn"]} - {row["Vị trí"]} ({row["Trạng thái bàn"]})";
                    dgvRow.Cells[5].Value = tableInfo;
                }
                
                // Hiển thị thông tin bàn ở phần trên của form
                if (data.Rows.Count > 0)
                {
                    string tableName = data.Rows[0]["Tên bàn"].ToString();
                    string tableLocation = data.Rows[0]["Vị trí"].ToString();
                    string tableStatus = data.Rows[0]["Trạng thái bàn"].ToString();
                    
                    lblTable.Text = $"Bàn: {tableName} - {tableLocation} ({tableStatus})";
                }
                
                // Cập nhật tổng tiền
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotal()
        {
            try
            {
                if (currentBillID != null)
                {
                    // Cập nhật tổng tiền trong database
                    BillDAO.Instance.UpdateTotalPrice(currentBillID.Value);
                    
                    // Lấy tổng tiền mới
                    string query = "SELECT TotalPrice FROM Bill WHERE id = @billID";
                    object result = DataProvider.Instance.ExecuteScalar(query, new object[] { currentBillID.Value });
                    
                    if (result != null)
                    {
                        decimal totalPrice = Convert.ToDecimal(result);
                        lblTotal.Text = $"Tổng: {totalPrice:N0} VNĐ";
                    }
                }
                else
                {
                    lblTotal.Text = "Tổng: 0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật tổng tiền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    
                    // Hỏi người dùng có muốn in hóa đơn không
                    if (MessageBox.Show("Bạn có muốn in hóa đơn không?", "In hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PrintBill(currentBillID.Value);
                    }
                    
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

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (currentBillID == null)
            {
                MessageBox.Show("Không có hóa đơn để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            PrintBill(currentBillID.Value);
        }

        private void PrintBill(int billID)
        {
            try
            {
                // Lấy thông tin bill
                DataTable billInfo = BillDAO.Instance.GetBillInfo(billID);
                if (billInfo.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Lấy chi tiết bill
                DataTable billDetails = BillDAO.Instance.GetBillDetails(billID);
                
                // Tạo form in hóa đơn hoặc sử dụng PrintDocument
                // Ví dụ đơn giản: hiển thị thông tin trong MessageBox
                string billText = "HÓA ĐƠN THANH TOÁN\n";
                billText += "========================\n";
                billText += $"Số hóa đơn: {billInfo.Rows[0]["id"]}\n";
                billText += $"Nhân viên: {billInfo.Rows[0]["Nhân viên"]}\n";
                billText += $"Bàn: {billInfo.Rows[0]["Bàn"]}\n";
                billText += $"Ngày: {Convert.ToDateTime(billInfo.Rows[0]["Ngày"]).ToString("dd/MM/yyyy HH:mm")}\n";
                billText += "========================\n";
                billText += "Món        SL   Đơn giá   Thành tiền\n";
                
                foreach (DataRow row in billDetails.Rows)
                {
                    billText += $"{row["Tên món"]}   {row["Số lượng"]}   {Convert.ToDecimal(row["Đơn giá"]):N0}   {Convert.ToDecimal(row["Thành tiền"]):N0}\n";
                }
                
                billText += "========================\n";
                billText += $"Tổng tiền: {Convert.ToDecimal(billInfo.Rows[0]["Tổng tiền"]):N0} VNĐ\n";
                billText += $"Giảm giá: {Convert.ToDecimal(billInfo.Rows[0]["Giảm giá"]):N0}%\n";
                billText += $"Thanh toán: {Convert.ToDecimal(billInfo.Rows[0]["Thanh toán"]):N0} VNĐ\n";
                billText += "========================\n";
                billText += "Cảm ơn quý khách đã sử dụng dịch vụ!";
                
                // Trong một ứng dụng thực tế, bạn sẽ sử dụng PrintDocument để in ra máy in
                MessageBox.Show(billText, "In hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSendToKitchen_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetails.Rows.Count == 0)
            {
                MessageBox.Show("Không có món nào để chuyển bếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Thêm code xử lý chuyển bếp
            MessageBox.Show("Đã chuyển đơn hàng đến bếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Truy vấn để lấy danh sách hóa đơn đang hoạt động
                string query = @"
                    SELECT b.id, f.Name AS TableName, b.CheckInDate, 
                           (SELECT COUNT(*) FROM BillInfo WHERE BillID = b.id) AS TotalItems,
                           b.TotalPrice, 
                           CASE WHEN b.Status = 0 THEN N'Đang phục vụ' ELSE N'Đã thanh toán' END AS Status,
                           u.FullName AS StaffName
                    FROM Bill b
                    JOIN Facility f ON b.TableID = f.id
                    JOIN Users u ON b.UserID = u.uid
                    WHERE b.Status = 0
                    ORDER BY b.CheckInDate DESC";

                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                // Thêm dữ liệu vào DataGridView
                foreach (DataRow row in data.Rows)
                {
                    int rowIndex = dgvActiveBills.Rows.Add();
                    DataGridViewRow gridRow = dgvActiveBills.Rows[rowIndex];

                    gridRow.Cells["ID"].Value = row["id"];
                    gridRow.Cells["TableName"].Value = row["TableName"];
                    gridRow.Cells["CheckInTime"].Value = ((DateTime)row["CheckInDate"]).ToString("dd/MM/yyyy HH:mm");
                    gridRow.Cells["TotalItems"].Value = row["TotalItems"];
                    gridRow.Cells["TotalAmount"].Value = string.Format("{0:N0} VNĐ", row["TotalPrice"]);
                    gridRow.Cells["Status"].Value = row["Status"];
                    gridRow.Cells["Staff"].Value = row["StaffName"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message, 
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}