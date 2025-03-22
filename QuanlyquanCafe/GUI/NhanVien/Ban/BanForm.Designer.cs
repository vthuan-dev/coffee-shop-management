namespace QuanlyquanCafe.GUI.NhanVien.Ban
{
    partial class BanForm
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.datbanBtn = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button9 = new System.Windows.Forms.Button();
            this.panelButtons.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelButtons.Controls.Add(this.button8);
            this.panelButtons.Controls.Add(this.button7);
            this.panelButtons.Controls.Add(this.button4);
            this.panelButtons.Controls.Add(this.datbanBtn);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(136, 450);
            this.panelButtons.TabIndex = 0;
            this.panelButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // datbanBtn
            // 
            this.datbanBtn.Location = new System.Drawing.Point(15, 26);
            this.datbanBtn.Name = "datbanBtn";
            this.datbanBtn.Size = new System.Drawing.Size(100, 72);
            this.datbanBtn.TabIndex = 0;
            this.datbanBtn.Text = "Đặt bàn";
            this.datbanBtn.UseVisualStyleBackColor = true;
            this.datbanBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 130);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 72);
            this.button4.TabIndex = 0;
            this.button4.Text = "Gợp bàn";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 227);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(100, 72);
            this.button7.TabIndex = 0;
            this.button7.Text = "Đặt bàn";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button4_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(15, 328);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(100, 72);
            this.button8.TabIndex = 0;
            this.button8.Text = "Thanh toán";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button4_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button9);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(136, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(664, 450);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // button9
            // 
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.Image = global::QuanlyquanCafe.Properties.Resources._7113274;
            this.button9.Location = new System.Drawing.Point(13, 13);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(179, 101);
            this.button9.TabIndex = 0;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // BanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelButtons);
            this.Name = "BanForm";
            this.panelButtons.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button datbanBtn;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button9;
    }
}