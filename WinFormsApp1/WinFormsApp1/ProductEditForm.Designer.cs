using ElectronicsStore.Enums;

namespace ElectronicsStore
{
    partial class ProductEditForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this._txtName = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this._numPrice = new System.Windows.Forms.NumericUpDown();
            this.lblStock = new System.Windows.Forms.Label();
            this._numStock = new System.Windows.Forms.NumericUpDown();
            this.lblCategory = new System.Windows.Forms.Label();
            this._cmbCategory = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this._numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numStock)).BeginInit();
            this.SuspendLayout();

            // lblName
            this.lblName.Text = "Название:"; this.lblName.Location = new System.Drawing.Point(20, 20); this.lblName.Width = 100;
            // _txtName
            this._txtName.Location = new System.Drawing.Point(130, 20); this._txtName.Width = 200;
            // lblPrice
            this.lblPrice.Text = "Цена:"; this.lblPrice.Location = new System.Drawing.Point(20, 60); this.lblPrice.Width = 100;
            // _numPrice
            this._numPrice.Location = new System.Drawing.Point(130, 60); this._numPrice.Width = 200; this._numPrice.DecimalPlaces = 2; this._numPrice.Maximum = 1000000;
            // lblStock
            this.lblStock.Text = "Кол-во:"; this.lblStock.Location = new System.Drawing.Point(20, 100); this.lblStock.Width = 100;
            // _numStock
            this._numStock.Location = new System.Drawing.Point(130, 100); this._numStock.Width = 200; this._numStock.Maximum = 10000;
            // lblCategory
            this.lblCategory.Text = "Категория:"; this.lblCategory.Location = new System.Drawing.Point(20, 140); this.lblCategory.Width = 100;
            // _cmbCategory
            this._cmbCategory.Location = new System.Drawing.Point(130, 140); this._cmbCategory.Width = 200; this._cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbCategory.DataSource = System.Enum.GetValues(typeof(ProductCategory));
            // btnSave
            this.btnSave.Text = "Сохранить"; this.btnSave.Location = new System.Drawing.Point(130, 190); this.btnSave.Width = 100;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            this.Controls.Add(this.lblName); this.Controls.Add(this._txtName);
            this.Controls.Add(this.lblPrice); this.Controls.Add(this._numPrice);
            this.Controls.Add(this.lblStock); this.Controls.Add(this._numStock);
            this.Controls.Add(this.lblCategory); this.Controls.Add(this._cmbCategory);
            this.Controls.Add(this.btnSave);

            this.ClientSize = new System.Drawing.Size(400, 250);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Text = "Товар";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            ((System.ComponentModel.ISupportInitialize)(this._numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numStock)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblName, lblPrice, lblStock, lblCategory;
        private System.Windows.Forms.TextBox _txtName;
        private System.Windows.Forms.NumericUpDown _numPrice, _numStock;
        private System.Windows.Forms.ComboBox _cmbCategory;
        private System.Windows.Forms.Button btnSave;
    }
}