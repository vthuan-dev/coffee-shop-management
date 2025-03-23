using QuanlyquanCafe.Admin.DAO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanlyquanCafe.GUI.NhanVien.DonHang
{
    public partial class DonHangListForm : Form
    {
        private DataGridView dgvActiveBills;
        private Button btnRefresh;
        private Button btnViewDetails;
        private Button btnCheckout;
        private Label lblTitle;
        private Panel mainPanel;

        public DonHangListForm()
        {
            InitializeComponent();
            SetupUI();
            LoadActiveBills();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DonHangListForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "DonHangListForm";
            this.Text = "Danh sách đơn hàng";
            this.Load += new System.EventHandler(this.DonHangListForm_Load);
            this.ResumeLayout(false);
        }

        private void SetupUI()
        {
            // Thiết lập main panel
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(mainPanel);

            // Thêm tiêu đề
            lblTitle = new Label
            {
                Text = "DANH SÁCH HÓA ĐƠN ĐANG HOẠT ĐỘNG",
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };
            mainPanel.Controls.Add(lblTitle);

            // Thêm DataGridView hiển thị hóa đơn
            dgvActiveBills = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };

            // Thêm các cột cần thiết
            dgvActiveBills.Columns.Add("ID", "Mã hóa đơn");
            dgvActiveBills.Columns.Add("TableName", "Bàn");
            dgvActiveBills.Columns.Add("CheckInTime", "Thời gian vào");
            dgvActiveBills.Columns.Add("TotalItems", "Số món");
            dgvActiveBills.Columns.Add("TotalAmount", "Tổng tiền");
            dgvActiveBills.Columns.Add("Status", "Trạng thái");
            dgvActiveBills.Columns.Add("Staff", "Nhân viên");

            mainPanel.Controls.Add(dgvActiveBills);

            // Panel chứa buttons
            Panel buttonPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom
            };
            mainPanel.Controls.Add(buttonPanel);

            // Thêm các buttons
            btnRefresh = new Button
            {
                Text = "Làm mới",
                Width = 120,
                Height = 40,
                Location = new Point(20, 10)
            };
            btnRefresh.Click += BtnRefresh_Click;
            buttonPanel.Controls.Add(btnRefresh);

            btnViewDetails = new Button
            {
                Text = "Xem chi tiết",
                Width = 120,
                Height = 40,
                Location = new Point(150, 10)
            };
            btnViewDetails.Click += BtnViewDetails_Click;
            buttonPanel.Controls.Add(btnViewDetails);

            btnCheckout = new Button
            {
                Text = "Thanh toán",
                Width = 120,
                Height = 40,
                Location = new Point(280, 10)
            };
            btnCheckout.Click += BtnCheckout_Click;
            buttonPanel.Controls.Add(btnCheckout);
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

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadActiveBills();
        }

        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvActiveBills.SelectedRows.Count > 0)
            {
                int billId = Convert.ToInt32(dgvActiveBills.SelectedRows[0].Cells["ID"].Value);
                string tableName = dgvActiveBills.SelectedRows[0].Cells["TableName"].Value.ToString();
                
                MessageBox.Show($"Xem chi tiết hóa đơn #{billId} - Bàn: {tableName}", "Thông báo");
                // TODO: Mở form chi tiết hóa đơn hoặc hiển thị thông tin chi tiết
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết", 
                               "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (dgvActiveBills.SelectedRows.Count > 0)
            {
                int billId = Convert.ToInt32(dgvActiveBills.SelectedRows[0].Cells["ID"].Value);
                string tableName = dgvActiveBills.SelectedRows[0].Cells["TableName"].Value.ToString();
                
                if (MessageBox.Show($"Xác nhận thanh toán hóa đơn #{billId} - Bàn: {tableName}?", 
                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Thanh toán hóa đơn
                        if (BillDAO.Instance.CheckOut(billId))
                        {
                            MessageBox.Show("Thanh toán thành công!", "Thông báo");
                            LoadActiveBills(); // Làm mới danh sách
                        }
                        else
                        {
                            MessageBox.Show("Không thể thanh toán hóa đơn!", 
                                           "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thanh toán: " + ex.Message, 
                                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để thanh toán", 
                               "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DonHangListForm_Load(object sender, EventArgs e)
        {
            LoadActiveBills();
        }
    }
} 