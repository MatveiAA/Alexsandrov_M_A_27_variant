namespace ElectronicsStore
{
    partial class OrderListForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this._grid = new System.Windows.Forms.DataGridView();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();

            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.ReadOnly = true;
            this._grid.AllowUserToAddRows = false;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Height = 50;

            this.btnAdd.Text = "Создать заказ"; this.btnAdd.Width = 120;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            this.panel.Controls.Add(this.btnAdd);

            this.Controls.Add(this._grid);
            this.Controls.Add(this.panel);

            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Text = "Список заказов";
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.Button btnAdd;
    }
}