namespace ElectronicsStore
{
    partial class OrderCreateForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblCustomer = new System.Windows.Forms.Label();
            this._cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblDelivery = new System.Windows.Forms.Label();
            this._cmbDelivery = new System.Windows.Forms.ComboBox();
            this.lblProducts = new System.Windows.Forms.Label();
            this._cmbProducts = new System.Windows.Forms.ComboBox();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this._lstCart = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();

            this.lblCustomer.Text = "Клиент:"; this.lblCustomer.Location = new System.Drawing.Point(20, 20); this.lblCustomer.Width = 100;
            this._cmbCustomer.Location = new System.Drawing.Point(130, 20); this._cmbCustomer.Width = 300; this._cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblDelivery.Text = "Доставка:"; this.lblDelivery.Location = new System.Drawing.Point(20, 60); this.lblDelivery.Width = 100;
            this._cmbDelivery.Location = new System.Drawing.Point(130, 60); this._cmbDelivery.Width = 300; this._cmbDelivery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblProducts.Text = "Товар:"; this.lblProducts.Location = new System.Drawing.Point(20, 110); this.lblProducts.Width = 100;
            this._cmbProducts.Location = new System.Drawing.Point(130, 110); this._cmbProducts.Width = 200; this._cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnAddToCart.Text = "Добавить"; this.btnAddToCart.Location = new System.Drawing.Point(340, 108); this.btnAddToCart.Width = 90;
            this.btnAddToCart.Click += new System.EventHandler(this.BtnAddToCart_Click);

            this._lstCart.Location = new System.Drawing.Point(130, 150); this._lstCart.Width = 300; this._lstCart.Height = 100;

            this.btnSave.Text = "Оформить"; this.btnSave.Location = new System.Drawing.Point(130, 280); this.btnSave.Width = 100;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            this.Controls.Add(this.lblCustomer); this.Controls.Add(this._cmbCustomer);
            this.Controls.Add(this.lblDelivery); this.Controls.Add(this._cmbDelivery);
            this.Controls.Add(this.lblProducts); this.Controls.Add(this._cmbProducts);
            this.Controls.Add(this.btnAddToCart); this.Controls.Add(this._lstCart);
            this.Controls.Add(this.btnSave);

            this.ClientSize = new System.Drawing.Size(500, 350);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Text = "Оформление заказа";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        private System.Windows.Forms.Label lblCustomer, lblDelivery, lblProducts;
        private System.Windows.Forms.ComboBox _cmbCustomer, _cmbDelivery, _cmbProducts;
        private System.Windows.Forms.Button btnAddToCart, btnSave;
        private System.Windows.Forms.ListBox _lstCart;
    }
}