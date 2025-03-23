using System;
using System.Data;
using System.Windows.Forms;
using QuanlyquanCafe.Admin.DAO;

namespace QuanlyquanCafe.GUI.NhanVien.Menu
{
    public partial class ChangeTableForm : Form
    {
        private int currentTableId;
        private int billId;
        public int NewTableId { get; private set; }
        public string NewTableName { get; private set; }
        
        public ChangeTableForm(DataTable availableTables, int currentTableId, int billId)
        {
            InitializeComponent();
            this.currentTableId = currentTableId;
            this.billId = billId;
            
            // Load danh sách bàn trống vào combobox
            cbxNewTable.DataSource = availableTables;
            cbxNewTable.DisplayMember = "Name";
            cbxNewTable.ValueMember = "id";
            
            this.Load += ChangeTableForm_Load;
        }
        
        private void InitializeComponent()
        {
            this.txtCurrentTable = new System.Windows.Forms.TextBox();
            this.cbxNewTable = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCurrentTable = new System.Windows.Forms.Label();
            this.lblNewTable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // lblCurrentTable
            this.lblCurrentTable.AutoSize = true;
            this.lblCurrentTable.Location = new System.Drawing.Point(12, 15);
            this.lblCurrentTable.Name = "lblCurrentTable";
            this.lblCurrentTable.Size = new System.Drawing.Size(72, 13);
            this.lblCurrentTable.Text = "Bàn hiện tại:";
            
            // txtCurrentTable
            this.txtCurrentTable.Location = new System.Drawing.Point(120, 12);
            this.txtCurrentTable.Name = "txtCurrentTable";
            this.txtCurrentTable.ReadOnly = true;
            this.txtCurrentTable.Size = new System.Drawing.Size(250, 20);
            
            // lblNewTable
            this.lblNewTable.AutoSize = true;
            this.lblNewTable.Location = new System.Drawing.Point(12, 45);
            this.lblNewTable.Name = "lblNewTable";
            this.lblNewTable.Size = new System.Drawing.Size(53, 13);
            this.lblNewTable.Text = "Bàn mới:";
            
            // cbxNewTable
            this.cbxNewTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNewTable.FormattingEnabled = true;
            this.cbxNewTable.Location = new System.Drawing.Point(120, 42);
            this.cbxNewTable.Name = "cbxNewTable";
            this.cbxNewTable.Size = new System.Drawing.Size(250, 21);
            
            // btnSave
            this.btnSave.Location = new System.Drawing.Point(120, 80);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(210, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // ChangeTableForm
            this.ClientSize = new System.Drawing.Size(384, 120);
            this.Controls.Add(this.lblCurrentTable);
            this.Controls.Add(this.txtCurrentTable);
            this.Controls.Add(this.lblNewTable);
            this.Controls.Add(this.cbxNewTable);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi bàn";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private System.Windows.Forms.TextBox txtCurrentTable;
        private System.Windows.Forms.ComboBox cbxNewTable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCurrentTable;
        private System.Windows.Forms.Label lblNewTable;
        
        private void ChangeTableForm_Load(object sender, EventArgs e)
        {
            // Hiển thị tên bàn hiện tại
            string query = "SELECT Name FROM Facility WHERE id = " + currentTableId;
            object result = DataProvider.Instance.ExecuteScalar(query);
            if (result != null)
            {
                txtCurrentTable.Text = result.ToString();
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                NewTableId = Convert.ToInt32(cbxNewTable.SelectedValue);
                NewTableName = cbxNewTable.Text;
                
                // Cập nhật hóa đơn với bàn mới
                string updateBillQuery = string.Format(
                    "UPDATE Bill SET TableID = {0} WHERE id = {1}",
                    NewTableId, billId);
                
                // Cập nhật trạng thái bàn mới
                string updateNewTableQuery = string.Format(
                    "UPDATE TableFacility SET Status = N'Có người' WHERE id = {0}",
                    NewTableId);
                
                // Cập nhật trạng thái bàn cũ
                string updateOldTableQuery = string.Format(
                    "UPDATE TableFacility SET Status = N'Trống' WHERE id = {0}",
                    currentTableId);
                
                // Thực hiện các câu truy vấn
                DataProvider.Instance.ExecuteNonQuery(updateBillQuery);
                DataProvider.Instance.ExecuteNonQuery(updateNewTableQuery);
                DataProvider.Instance.ExecuteNonQuery(updateOldTableQuery);
                
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đổi bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
} 