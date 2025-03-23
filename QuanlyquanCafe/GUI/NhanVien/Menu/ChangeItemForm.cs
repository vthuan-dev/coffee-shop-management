using System;
using System.Data;
using System.Windows.Forms;
using QuanlyquanCafe.Admin.DAO;

namespace QuanlyquanCafe.GUI.NhanVien.Menu
{
    public partial class ChangeItemForm : Form
    {
        private int billId;
        private int billDetailId;
        private string currentItemName;
        private int currentQuantity;
        private TextBox txtCurrentItem;
        private ComboBox cbxNewMenuItem;
        private NumericUpDown numNewQuantity;
        private Button btnSave;
        private Button btnCancel;
        private Label lblCurrentItem;
        private Label lblNewItem;
        private Label lblQuantity;
        
        public ChangeItemForm(int billId, int billDetailId, string currentItemName, int currentQuantity)
        {
            InitializeComponent();
            this.billId = billId;
            this.billDetailId = billDetailId;
            this.currentItemName = currentItemName;
            this.currentQuantity = currentQuantity;
            
            this.Load += ChangeItemForm_Load;
        }
        
        private void InitializeComponent()
        {
            this.txtCurrentItem = new System.Windows.Forms.TextBox();
            this.cbxNewMenuItem = new System.Windows.Forms.ComboBox();
            this.numNewQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCurrentItem = new System.Windows.Forms.Label();
            this.lblNewItem = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numNewQuantity)).BeginInit();
            this.SuspendLayout();
            
            // lblCurrentItem
            this.lblCurrentItem.AutoSize = true;
            this.lblCurrentItem.Location = new System.Drawing.Point(12, 15);
            this.lblCurrentItem.Name = "lblCurrentItem";
            this.lblCurrentItem.Size = new System.Drawing.Size(70, 13);
            this.lblCurrentItem.Text = "Món hiện tại:";
            
            // txtCurrentItem
            this.txtCurrentItem.Location = new System.Drawing.Point(120, 12);
            this.txtCurrentItem.Name = "txtCurrentItem";
            this.txtCurrentItem.ReadOnly = true;
            this.txtCurrentItem.Size = new System.Drawing.Size(250, 20);
            
            // lblNewItem
            this.lblNewItem.AutoSize = true;
            this.lblNewItem.Location = new System.Drawing.Point(12, 45);
            this.lblNewItem.Name = "lblNewItem";
            this.lblNewItem.Size = new System.Drawing.Size(56, 13);
            this.lblNewItem.Text = "Món mới:";
            
            // cbxNewMenuItem
            this.cbxNewMenuItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNewMenuItem.FormattingEnabled = true;
            this.cbxNewMenuItem.Location = new System.Drawing.Point(120, 42);
            this.cbxNewMenuItem.Name = "cbxNewMenuItem";
            this.cbxNewMenuItem.Size = new System.Drawing.Size(250, 21);
            
            // lblQuantity
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(12, 75);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(53, 13);
            this.lblQuantity.Text = "Số lượng:";
            
            // numNewQuantity
            this.numNewQuantity.Location = new System.Drawing.Point(120, 73);
            this.numNewQuantity.Name = "numNewQuantity";
            this.numNewQuantity.Size = new System.Drawing.Size(120, 20);
            this.numNewQuantity.Minimum = 1;
            this.numNewQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            
            // btnSave
            this.btnSave.Location = new System.Drawing.Point(120, 110);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(210, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // ChangeItemForm
            this.ClientSize = new System.Drawing.Size(384, 150);
            this.Controls.Add(this.lblCurrentItem);
            this.Controls.Add(this.txtCurrentItem);
            this.Controls.Add(this.lblNewItem);
            this.Controls.Add(this.cbxNewMenuItem);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.numNewQuantity);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi món";
            ((System.ComponentModel.ISupportInitialize)(this.numNewQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private void ChangeItemForm_Load(object sender, EventArgs e)
        {
            // Hiển thị tên món hiện tại
            txtCurrentItem.Text = currentItemName;
            numNewQuantity.Value = currentQuantity;
            
            // Load danh sách món ăn từ database - Sử dụng bảng Menu theo CSDL đã cung cấp
            string query = "SELECT id, Name FROM Menu";
            DataTable menuItems = DataProvider.Instance.ExecuteQuery(query);
            
            cbxNewMenuItem.DataSource = menuItems;
            cbxNewMenuItem.DisplayMember = "Name";
            cbxNewMenuItem.ValueMember = "id";
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin món mới
                int newMenuId = Convert.ToInt32(cbxNewMenuItem.SelectedValue);
                string newMenuName = cbxNewMenuItem.Text;
                int newQuantity = Convert.ToInt32(numNewQuantity.Value);
                
                // Cập nhật chi tiết hóa đơn với món mới - Sử dụng tên cột MenuID theo CSDL
                string updateQuery = string.Format(
                    "UPDATE BillInfo SET MenuID = {0}, Quantity = {1} WHERE id = {2}",
                    newMenuId, newQuantity, billDetailId);
                
                // Thực hiện câu truy vấn
                int affected = DataProvider.Instance.ExecuteNonQuery(updateQuery);
                
                if (affected > 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật món!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
} 