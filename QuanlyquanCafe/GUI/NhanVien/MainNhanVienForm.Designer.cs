namespace QuanlyquanCafe.GUI.NhanVien
{
    partial class MainNhanVienForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainNhanVienForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainNhanVienForm";
            this.Text = "Quản lý quán cafe - Nhân viên";
            this.Load += new System.EventHandler(this.MainNhanVienForm_Load);
            this.ResumeLayout(false);

        }
    }
} 