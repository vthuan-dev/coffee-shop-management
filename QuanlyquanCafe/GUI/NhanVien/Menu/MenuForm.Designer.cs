namespace QuanlyquanCafe.GUI.NhanVien.Menu
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSendToKitchen = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlOrderDetails = new System.Windows.Forms.Panel();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.lblOrderDetails = new System.Windows.Forms.Label();
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.cbxTable = new System.Windows.Forms.ComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblOrder = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlCategories = new System.Windows.Forms.Panel();
            this.flpCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this.pnlMenuList = new System.Windows.Forms.Panel();
            this.dgvMenuItems = new System.Windows.Forms.DataGridView();
            this.lblMenuListTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTableDetails = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.pnlOrder.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlCategories.SuspendLayout();
            this.pnlMenuList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuItems)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.pnlButtons);
            this.mainPanel.Controls.Add(this.pnlOrderDetails);
            this.mainPanel.Controls.Add(this.pnlOrder);
            this.mainPanel.Controls.Add(this.pnlSearch);
            this.mainPanel.Controls.Add(this.splitContainer1);
            this.mainPanel.Controls.Add(this.lblTitle);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(10);
            this.mainPanel.Size = new System.Drawing.Size(982, 653);
            this.mainPanel.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnSendToKitchen);
            this.pnlButtons.Controls.Add(this.btnCheckout);
            this.pnlButtons.Controls.Add(this.btnUpdate);
            this.pnlButtons.Controls.Add(this.btnDeleteItem);
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(10, 740);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(962, 50);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnSendToKitchen
            // 
            this.btnSendToKitchen.Location = new System.Drawing.Point(400, 10);
            this.btnSendToKitchen.Name = "btnSendToKitchen";
            this.btnSendToKitchen.Size = new System.Drawing.Size(120, 30);
            this.btnSendToKitchen.TabIndex = 0;
            this.btnSendToKitchen.Text = "Chuyển bếp";
            // 
            // btnCheckout
            // 
            this.btnCheckout.Location = new System.Drawing.Point(270, 10);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(120, 30);
            this.btnCheckout.TabIndex = 1;
            this.btnCheckout.Text = "Thanh toán";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(0, 0);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(140, 10);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteItem.TabIndex = 3;
            this.btnDeleteItem.Text = "Cập nhật";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(530, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 30);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "In hóa đơn";
            // 
            // pnlOrderDetails
            // 
            this.pnlOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrderDetails.Controls.Add(this.dgvOrderDetails);
            this.pnlOrderDetails.Controls.Add(this.lblOrderDetails);
            this.pnlOrderDetails.Controls.Add(this.lblTableDetails);
            this.pnlOrderDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderDetails.Location = new System.Drawing.Point(10, 540);
            this.pnlOrderDetails.Name = "pnlOrderDetails";
            this.pnlOrderDetails.Size = new System.Drawing.Size(962, 200);
            this.pnlOrderDetails.TabIndex = 1;
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.AllowUserToAddRows = false;
            this.dgvOrderDetails.AllowUserToDeleteRows = false;
            this.dgvOrderDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetails.Location = new System.Drawing.Point(0, 50);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.ReadOnly = true;
            this.dgvOrderDetails.RowHeadersVisible = false;
            this.dgvOrderDetails.RowHeadersWidth = 51;
            this.dgvOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderDetails.Size = new System.Drawing.Size(960, 148);
            this.dgvOrderDetails.TabIndex = 0;
            // 
            // lblOrderDetails
            // 
            this.lblOrderDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderDetails.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblOrderDetails.Location = new System.Drawing.Point(0, 0);
            this.lblOrderDetails.Name = "lblOrderDetails";
            this.lblOrderDetails.Size = new System.Drawing.Size(960, 30);
            this.lblOrderDetails.TabIndex = 1;
            this.lblOrderDetails.Text = "Chi tiết đơn hàng";
            this.lblOrderDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTableDetails
            // 
            this.lblTableDetails.AutoSize = true;
            this.lblTableDetails.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblTableDetails.Location = new System.Drawing.Point(10, 30);
            this.lblTableDetails.Name = "lblTableDetails";
            this.lblTableDetails.Size = new System.Drawing.Size(150, 20);
            this.lblTableDetails.TabIndex = 2;
            this.lblTableDetails.Text = "Thông tin bàn: ";
            // 
            // pnlOrder
            // 
            this.pnlOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrder.Controls.Add(this.lblTotal);
            this.pnlOrder.Controls.Add(this.cbxTable);
            this.pnlOrder.Controls.Add(this.lblTable);
            this.pnlOrder.Controls.Add(this.lblOrder);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrder.Location = new System.Drawing.Point(10, 380);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new System.Drawing.Size(962, 160);
            this.pnlOrder.TabIndex = 2;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(700, 130);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(130, 24);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Tổng: 0 VNĐ";
            // 
            // cbxTable
            // 
            this.cbxTable.Location = new System.Drawing.Point(70, 40);
            this.cbxTable.Name = "cbxTable";
            this.cbxTable.Size = new System.Drawing.Size(200, 24);
            this.cbxTable.TabIndex = 1;
            this.cbxTable.Visible = false;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(170, 10);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(34, 16);
            this.lblTable.TabIndex = 2;
            this.lblTable.Text = "Bàn:";
            // 
            // lblOrder
            // 
            this.lblOrder.AutoSize = true;
            this.lblOrder.Location = new System.Drawing.Point(10, 10);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(115, 16);
            this.lblOrder.TabIndex = 3;
            this.lblOrder.Text = "Đơn hàng hiện tại: ";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(10, 340);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(962, 40);
            this.pnlSearch.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(400, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 27);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Tìm";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(90, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 22);
            this.txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(10, 10);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(65, 16);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(10, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlCategories);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlMenuList);
            this.splitContainer1.Size = new System.Drawing.Size(962, 300);
            this.splitContainer1.SplitterDistance = 294;
            this.splitContainer1.TabIndex = 1;
            // 
            // pnlCategories
            // 
            this.pnlCategories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCategories.Controls.Add(this.flpCategories);
            this.pnlCategories.Controls.Add(this.lblCategoryTitle);
            this.pnlCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategories.Location = new System.Drawing.Point(0, 0);
            this.pnlCategories.Name = "pnlCategories";
            this.pnlCategories.Size = new System.Drawing.Size(294, 300);
            this.pnlCategories.TabIndex = 0;
            // 
            // flpCategories
            // 
            this.flpCategories.AutoScroll = true;
            this.flpCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCategories.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCategories.Location = new System.Drawing.Point(0, 30);
            this.flpCategories.Name = "flpCategories";
            this.flpCategories.Size = new System.Drawing.Size(292, 268);
            this.flpCategories.TabIndex = 0;
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblCategoryTitle.Location = new System.Drawing.Point(0, 0);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(292, 30);
            this.lblCategoryTitle.TabIndex = 1;
            this.lblCategoryTitle.Text = "Danh mục";
            this.lblCategoryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMenuList
            // 
            this.pnlMenuList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenuList.Controls.Add(this.dgvMenuItems);
            this.pnlMenuList.Controls.Add(this.lblMenuListTitle);
            this.pnlMenuList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenuList.Location = new System.Drawing.Point(0, 0);
            this.pnlMenuList.Name = "pnlMenuList";
            this.pnlMenuList.Size = new System.Drawing.Size(664, 300);
            this.pnlMenuList.TabIndex = 0;
            // 
            // dgvMenuItems
            // 
            this.dgvMenuItems.AllowUserToAddRows = false;
            this.dgvMenuItems.AllowUserToDeleteRows = false;
            this.dgvMenuItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMenuItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenuItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMenuItems.Location = new System.Drawing.Point(0, 30);
            this.dgvMenuItems.Name = "dgvMenuItems";
            this.dgvMenuItems.ReadOnly = true;
            this.dgvMenuItems.RowHeadersVisible = false;
            this.dgvMenuItems.RowHeadersWidth = 51;
            this.dgvMenuItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMenuItems.Size = new System.Drawing.Size(662, 268);
            this.dgvMenuItems.TabIndex = 0;
            // 
            // lblMenuListTitle
            // 
            this.lblMenuListTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenuListTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenuListTitle.Location = new System.Drawing.Point(0, 0);
            this.lblMenuListTitle.Name = "lblMenuListTitle";
            this.lblMenuListTitle.Size = new System.Drawing.Size(662, 30);
            this.lblMenuListTitle.TabIndex = 1;
            this.lblMenuListTitle.Text = "Danh sách món";
            this.lblMenuListTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMenuListTitle.Click += new System.EventHandler(this.lblMenuListTitle_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(962, 30);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "MENU";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // MenuForm
            // 
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.mainPanel);
            this.Name = "MenuForm";
            this.Text = "Menu";
            this.mainPanel.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlOrderDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.pnlOrder.ResumeLayout(false);
            this.pnlOrder.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlCategories.ResumeLayout(false);
            this.pnlMenuList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlCategories;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.FlowLayoutPanel flpCategories;
        private System.Windows.Forms.Panel pnlMenuList;
        private System.Windows.Forms.Label lblMenuListTitle;
        private System.Windows.Forms.DataGridView dgvMenuItems;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlOrder;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.ComboBox cbxTable;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlOrderDetails;
        private System.Windows.Forms.Label lblOrderDetails;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnSendToKitchen;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblTableDetails;
    }
}