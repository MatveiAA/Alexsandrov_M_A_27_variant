namespace ElectronicsStore
{
    partial class CustomerEditForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this._txtName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this._txtPhone = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();

            this.lblName.Text = "ФИО:"; this.lblName.Location = new System.Drawing.Point(20, 20); this.lblName.Width = 100;
            this._txtName.Location = new System.Drawing.Point(130, 20); this._txtName.Width = 200;

            this.lblPhone.Text = "Телефон:"; this.lblPhone.Location = new System.Drawing.Point(20, 60); this.lblPhone.Width = 100;
            this._txtPhone.Location = new System.Drawing.Point(130, 60); this._txtPhone.Width = 200;

            this.btnSave.Text = "Сохранить"; this.btnSave.Location = new System.Drawing.Point(130, 110); this.btnSave.Width = 100;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            this.Controls.Add(this.lblName); this.Controls.Add(this._txtName);
            this.Controls.Add(this.lblPhone); this.Controls.Add(this._txtPhone);
            this.Controls.Add(this.btnSave);

            this.ClientSize = new System.Drawing.Size(380, 180);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Text = "Клиент";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        private System.Windows.Forms.Label lblName, lblPhone;
        private System.Windows.Forms.TextBox _txtName, _txtPhone;
        private System.Windows.Forms.Button btnSave;
    }
}