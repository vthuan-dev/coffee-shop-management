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
            this.pnlActiveBills = new System.Windows.Forms.Panel();
            this.dgvActiveBills = new System.Windows.Forms.DataGridView();
            this.lblActiveBills = new System.Windows.Forms.Label();
            this.txtBillSearch = new System.Windows.Forms.TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSendToKitchen = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCustomerLeft = new System.Windows.Forms.Button();
            this.pnlOrderDetails = new System.Windows.Forms.Panel();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.lblOrderDetails = new System.Windows.Forms.Label();
            this.lblTableDetails = new System.Windows.Forms.Label();
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.tabFloors = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flpTables1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flpTables2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.flpTables3 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.flpTables4 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblOrder = new System.Windows.Forms.Label();
            this.cbxTable = new System.Windows.Forms.ComboBox();
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
            this.mainPanel.SuspendLayout();
            this.pnlActiveBills.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveBills)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.pnlOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.pnlOrder.SuspendLayout();
            this.tabFloors.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.flpTables4.SuspendLayout();
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
            this.mainPanel.Controls.Add(this.pnlActiveBills);
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
            this.mainPanel.Size = new System.Drawing.Size(1305, 653);
            this.mainPanel.TabIndex = 0;
            // 
            // pnlActiveBills
            // 
            this.pnlActiveBills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActiveBills.Controls.Add(this.dgvActiveBills);
            this.pnlActiveBills.Controls.Add(this.lblActiveBills);
            this.pnlActiveBills.Controls.Add(this.txtBillSearch);
            this.pnlActiveBills.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActiveBills.Location = new System.Drawing.Point(10, 427);
            this.pnlActiveBills.Name = "pnlActiveBills";
            this.pnlActiveBills.Size = new System.Drawing.Size(1285, 166);
            this.pnlActiveBills.TabIndex = 5;
            // 
            // dgvActiveBills
            // 
            this.dgvActiveBills.AllowUserToAddRows = false;
            this.dgvActiveBills.AllowUserToDeleteRows = false;
            this.dgvActiveBills.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActiveBills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActiveBills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActiveBills.Location = new System.Drawing.Point(0, 30);
            this.dgvActiveBills.Name = "dgvActiveBills";
            this.dgvActiveBills.ReadOnly = true;
            this.dgvActiveBills.RowHeadersVisible = false;
            this.dgvActiveBills.RowHeadersWidth = 51;
            this.dgvActiveBills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActiveBills.Size = new System.Drawing.Size(1283, 134);
            this.dgvActiveBills.TabIndex = 0;
            // 
            // lblActiveBills
            // 
            this.lblActiveBills.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblActiveBills.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblActiveBills.Location = new System.Drawing.Point(0, 0);
            this.lblActiveBills.Name = "lblActiveBills";
            this.lblActiveBills.Size = new System.Drawing.Size(1283, 30);
            this.lblActiveBills.TabIndex = 1;
            this.lblActiveBills.Text = "Danh sách hóa đơn đang hoạt động";
            this.lblActiveBills.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblActiveBills.Click += new System.EventHandler(this.lblActiveBills_Click);
            // 
            // txtBillSearch
            // 
            this.txtBillSearch.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtBillSearch.Location = new System.Drawing.Point(700, 5);
            this.txtBillSearch.Name = "txtBillSearch";
            this.txtBillSearch.Size = new System.Drawing.Size(200, 22);
            this.txtBillSearch.TabIndex = 2;
            this.txtBillSearch.Text = "Tìm kiếm hóa đơn...";
            this.txtBillSearch.Enter += new System.EventHandler(this.txtBillSearch_Enter);
            this.txtBillSearch.Leave += new System.EventHandler(this.txtBillSearch_Leave);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnSendToKitchen);
            this.pnlButtons.Controls.Add(this.btnCheckout);
            this.pnlButtons.Controls.Add(this.btnUpdate);
            this.pnlButtons.Controls.Add(this.btnDeleteItem);
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Controls.Add(this.btnCustomerLeft);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(10, 593);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1285, 50);
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
            this.btnUpdate.Location = new System.Drawing.Point(38, 10);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(96, 30);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Làm mới";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click_1);
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
            this.btnPrint.Location = new System.Drawing.Point(526, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(136, 30);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "In hóa đơn";
            // 
            // btnCustomerLeft
            // 
            this.btnCustomerLeft.Location = new System.Drawing.Point(833, 7);
            this.btnCustomerLeft.Name = "btnCustomerLeft";
            this.btnCustomerLeft.Size = new System.Drawing.Size(169, 30);
            this.btnCustomerLeft.TabIndex = 5;
            this.btnCustomerLeft.Text = "Khách đã rời";
            this.btnCustomerLeft.Click += new System.EventHandler(this.BtnCustomerLeft_Click);
            // 
            // pnlOrderDetails
            // 
            this.pnlOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrderDetails.Controls.Add(this.dgvOrderDetails);
            this.pnlOrderDetails.Controls.Add(this.lblOrderDetails);
            this.pnlOrderDetails.Controls.Add(this.lblTableDetails);
            this.pnlOrderDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderDetails.Location = new System.Drawing.Point(10, 580);
            this.pnlOrderDetails.Name = "pnlOrderDetails";
            this.pnlOrderDetails.Size = new System.Drawing.Size(1285, 200);
            this.pnlOrderDetails.TabIndex = 1;
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.AllowUserToAddRows = false;
            this.dgvOrderDetails.AllowUserToDeleteRows = false;
            this.dgvOrderDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetails.Location = new System.Drawing.Point(0, 30);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.ReadOnly = true;
            this.dgvOrderDetails.RowHeadersVisible = false;
            this.dgvOrderDetails.RowHeadersWidth = 51;
            this.dgvOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderDetails.Size = new System.Drawing.Size(1283, 168);
            this.dgvOrderDetails.TabIndex = 0;
            // 
            // lblOrderDetails
            // 
            this.lblOrderDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderDetails.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblOrderDetails.Location = new System.Drawing.Point(0, 0);
            this.lblOrderDetails.Name = "lblOrderDetails";
            this.lblOrderDetails.Size = new System.Drawing.Size(1283, 30);
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
            this.lblTableDetails.Size = new System.Drawing.Size(129, 19);
            this.lblTableDetails.TabIndex = 2;
            this.lblTableDetails.Text = "Thông tin bàn: ";
            // 
            // pnlOrder
            // 
            this.pnlOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrder.Controls.Add(this.lblTotal);
            this.pnlOrder.Controls.Add(this.tabFloors);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrder.Location = new System.Drawing.Point(10, 380);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new System.Drawing.Size(1285, 200);
            this.pnlOrder.TabIndex = 2;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(700, 170);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(130, 24);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Tổng: 0 VNĐ";
            // 
            // tabFloors
            // 
            this.tabFloors.Controls.Add(this.tabPage1);
            this.tabFloors.Controls.Add(this.tabPage2);
            this.tabFloors.Controls.Add(this.tabPage3);
            this.tabFloors.Controls.Add(this.tabPage4);
            this.tabFloors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFloors.Location = new System.Drawing.Point(0, 0);
            this.tabFloors.Name = "tabFloors";
            this.tabFloors.SelectedIndex = 0;
            this.tabFloors.Size = new System.Drawing.Size(1283, 198);
            this.tabFloors.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flpTables1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1275, 169);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tầng 1";
            // 
            // flpTables1
            // 
            this.flpTables1.AutoScroll = true;
            this.flpTables1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTables1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTables1.Location = new System.Drawing.Point(0, 0);
            this.flpTables1.Name = "flpTables1";
            this.flpTables1.Size = new System.Drawing.Size(1275, 169);
            this.flpTables1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flpTables2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(952, 169);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tầng 2";
            // 
            // flpTables2
            // 
            this.flpTables2.AutoScroll = true;
            this.flpTables2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTables2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTables2.Location = new System.Drawing.Point(0, 0);
            this.flpTables2.Name = "flpTables2";
            this.flpTables2.Size = new System.Drawing.Size(952, 169);
            this.flpTables2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.flpTables3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(952, 169);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tầng 3";
            // 
            // flpTables3
            // 
            this.flpTables3.AutoScroll = true;
            this.flpTables3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTables3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTables3.Location = new System.Drawing.Point(0, 0);
            this.flpTables3.Name = "flpTables3";
            this.flpTables3.Size = new System.Drawing.Size(952, 169);
            this.flpTables3.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.flpTables4);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(952, 169);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tầng 4";
            // 
            // flpTables4
            // 
            this.flpTables4.AutoScroll = true;
            this.flpTables4.Controls.Add(this.lblTable);
            this.flpTables4.Controls.Add(this.lblOrder);
            this.flpTables4.Controls.Add(this.cbxTable);
            this.flpTables4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTables4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTables4.Location = new System.Drawing.Point(0, 0);
            this.flpTables4.Name = "flpTables4";
            this.flpTables4.Size = new System.Drawing.Size(952, 169);
            this.flpTables4.TabIndex = 0;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(3, 0);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(34, 16);
            this.lblTable.TabIndex = 2;
            this.lblTable.Text = "Bàn:";
            // 
            // lblOrder
            // 
            this.lblOrder.AutoSize = true;
            this.lblOrder.Location = new System.Drawing.Point(3, 16);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(115, 16);
            this.lblOrder.TabIndex = 3;
            this.lblOrder.Text = "Đơn hàng hiện tại: ";
            this.lblOrder.Click += new System.EventHandler(this.lblOrder_Click);
            // 
            // cbxTable
            // 
            this.cbxTable.Location = new System.Drawing.Point(3, 35);
            this.cbxTable.Name = "cbxTable";
            this.cbxTable.Size = new System.Drawing.Size(200, 24);
            this.cbxTable.TabIndex = 1;
            this.cbxTable.Visible = false;
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
            this.pnlSearch.Size = new System.Drawing.Size(1285, 40);
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
            this.splitContainer1.Size = new System.Drawing.Size(1285, 300);
            this.splitContainer1.SplitterDistance = 392;
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
            this.pnlCategories.Size = new System.Drawing.Size(392, 300);
            this.pnlCategories.TabIndex = 0;
            // 
            // flpCategories
            // 
            this.flpCategories.AutoScroll = true;
            this.flpCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCategories.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCategories.Location = new System.Drawing.Point(0, 30);
            this.flpCategories.Name = "flpCategories";
            this.flpCategories.Size = new System.Drawing.Size(390, 268);
            this.flpCategories.TabIndex = 0;
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblCategoryTitle.Location = new System.Drawing.Point(0, 0);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(390, 30);
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
            this.pnlMenuList.Size = new System.Drawing.Size(889, 300);
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
            this.dgvMenuItems.Size = new System.Drawing.Size(887, 268);
            this.dgvMenuItems.TabIndex = 0;
            // 
            // lblMenuListTitle
            // 
            this.lblMenuListTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenuListTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenuListTitle.Location = new System.Drawing.Point(0, 0);
            this.lblMenuListTitle.Name = "lblMenuListTitle";
            this.lblMenuListTitle.Size = new System.Drawing.Size(887, 30);
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
            this.lblTitle.Size = new System.Drawing.Size(1285, 30);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "MENU";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // MenuForm
            // 
            this.ClientSize = new System.Drawing.Size(1305, 653);
            this.Controls.Add(this.mainPanel);
            this.Name = "MenuForm";
            this.mainPanel.ResumeLayout(false);
            this.pnlActiveBills.ResumeLayout(false);
            this.pnlActiveBills.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveBills)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.pnlOrderDetails.ResumeLayout(false);
            this.pnlOrderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.pnlOrder.ResumeLayout(false);
            this.pnlOrder.PerformLayout();
            this.tabFloors.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.flpTables4.ResumeLayout(false);
            this.flpTables4.PerformLayout();
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
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnSendToKitchen;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblTableDetails;
        private System.Windows.Forms.Panel pnlActiveBills;
        private System.Windows.Forms.DataGridView dgvActiveBills;
        private System.Windows.Forms.Label lblActiveBills;
        private System.Windows.Forms.TabControl tabFloors;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.FlowLayoutPanel flpTables1;
        private System.Windows.Forms.FlowLayoutPanel flpTables2;
        private System.Windows.Forms.FlowLayoutPanel flpTables3;
        private System.Windows.Forms.FlowLayoutPanel flpTables4;
        private System.Windows.Forms.TextBox txtBillSearch;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCustomerLeft;
    }
}