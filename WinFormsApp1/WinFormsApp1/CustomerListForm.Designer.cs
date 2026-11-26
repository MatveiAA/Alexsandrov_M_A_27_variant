namespace ElectronicsStore
{
    partial class CustomerListForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            _grid = new DataGridView();
            panel = new FlowLayoutPanel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // _grid
            // 
            _grid.AllowUserToAddRows = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.ColumnHeadersHeight = 29;
            _grid.Dock = DockStyle.Fill;
            _grid.Location = new Point(0, 0);
            _grid.Name = "_grid";
            _grid.ReadOnly = true;
            _grid.RowHeadersWidth = 51;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.Size = new Size(600, 374);
            _grid.TabIndex = 0;
            // 
            // panel
            // 
            panel.Controls.Add(btnAdd);
            panel.Controls.Add(btnEdit);
            panel.Controls.Add(btnDelete);
            panel.Dock = DockStyle.Bottom;
            panel.Location = new Point(0, 374);
            panel.Name = "panel";
            panel.Size = new Size(600, 50);
            panel.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(3, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 23);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Добавить";
            btnAdd.Click += BtnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(109, 3);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 23);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Изменить";
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(215, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 23);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Удалить";
            btnDelete.Click += BtnDelete_Click;
            // 
            // CustomerListForm
            // 
            ClientSize = new Size(600, 424);
            Controls.Add(_grid);
            Controls.Add(panel);
            Name = "CustomerListForm";
            Text = "Список клиентов";
            ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
            panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnDelete;
    }
}