namespace ElectronicsStore
{
    partial class ProductListForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this._grid = new System.Windows.Forms.DataGridView();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();

            // _grid
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.ReadOnly = true;
            this._grid.AllowUserToAddRows = false;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // panel
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Height = 50;

            // Кнопки
            this.btnAdd.Text = "Добавить"; this.btnAdd.Width = 100;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            this.btnEdit.Text = "Изменить"; this.btnEdit.Width = 100;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            this.btnDelete.Text = "Удалить"; this.btnDelete.Width = 100;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            this.panel.Controls.Add(this.btnAdd); this.panel.Controls.Add(this.btnEdit); this.panel.Controls.Add(this.btnDelete);

            this.Controls.Add(this._grid);
            this.Controls.Add(this.panel);

            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Text = "Список товаров";
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnDelete;
    }
}