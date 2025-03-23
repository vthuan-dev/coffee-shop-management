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

        public MenuForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            
            // Đăng ký các sự kiện
            this.Load += MenuForm_Load;
            this.btnSearch.Click += BtnSearch_Click;
            this.dgvMenuItems.CellClick += DgvMenuItems_CellClick;
            this.cbxTable.SelectedIndexChanged += CbxTable_SelectedIndexChanged;
            this.btnDeleteItem.Click += BtnDeleteItem_Click;
            this.btnUpdate.Click += BtnUpdate_Click;
            this.btnCheckout.Click += BtnCheckout_Click;
            this.btnSendToKitchen.Click += BtnSendToKitchen_Click;
            this.btnPrint.Click += BtnPrint_Click;
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            // Đầu tiên, thiết lập cấu trúc bảng
            SetupMenuTable();
            SetupOrderDetailsTable();
            
            // Sau đó mới tải dữ liệu
            LoadCategories();
            LoadTables();
            LoadAllMenuItems();
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

        private void LoadTables()
        {
            // Xóa danh sách cũ
            cbxTable.Items.Clear();
            
            // Thêm mục mặc định
            cbxTable.Items.Add("-- Chọn bàn --");
            
            try
            {
                // TODO: Thay bằng code lấy danh sách bàn từ database
                // Ví dụ: List<TableDTO> tables = TableDAO.Instance.GetTableList();
                
                // Tạm thời thêm một số bàn để demo
                for (int i = 1; i <= 10; i++)
                {
                    cbxTable.Items.Add($"Bàn {i}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            // Chọn mục mặc định
            cbxTable.SelectedIndex = 0;
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
                if (cbxTable.SelectedIndex <= 0)
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
                // Lấy dữ liệu chi tiết hóa đơn
                DataTable data = BillInfoDAO.Instance.GetBillDetailsByBillID(billID);
                
                // Xóa dữ liệu cũ
                dgvOrderDetails.Rows.Clear();
                
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

        private void CbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTable.SelectedIndex <= 0)
            {
                currentTableID = null;
                dgvOrderDetails.Rows.Clear();
                lblTotal.Text = "Tổng: 0 VNĐ";
                return;
            }

            currentTableID = cbxTable.SelectedIndex; // Giả lập ID bàn
            
            // TODO: Thêm code để lấy đơn hàng hiện tại của bàn
            // Ví dụ:
            // currentBillID = BillDAO.Instance.GetUncheckBillIDByTableID(currentTableID.Value);
            // if (currentBillID != null)
            // {
            //     LoadBillDetails();
            // }
            // else
            // {
            //     dgvOrderDetails.Rows.Clear();
            //     lblTotal.Text = "Tổng: 0 VNĐ";
            // }
            
            // Tạm thời xóa dữ liệu để demo
            dgvOrderDetails.Rows.Clear();
            lblTotal.Text = "Tổng: 0 VNĐ";
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
    }
}