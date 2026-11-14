using System;
using System.Linq;
using System.Windows.Forms;
using ElectronicsStore.Entities;
using ElectronicsStore.Repositories;

namespace ElectronicsStore
{
    public partial class ProductListForm : Form
    {
        private readonly IProductRepository _repository;
        private DataGridView _grid;

        public ProductListForm(IProductRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            SetupForm();
            LoadData();
        }

        private void SetupForm()
        {
            this.Text = "Список товаров";
            this.Size = new System.Drawing.Size(800, 500);

            _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AllowUserToAddRows = false };
            this.Controls.Add(_grid);

            var panel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 50 };
            var btnAdd = new Button { Text = "Добавить", Width = 100 };
            btnAdd.Click += (s, e) => { using (var form = new ProductEditForm(_repository)) { if (form.ShowDialog() == DialogResult.OK) LoadData(); } };

            var btnEdit = new Button { Text = "Изменить", Width = 100 };
            btnEdit.Click += (s, e) => {
                if (_grid.SelectedRows.Count == 0) return;
                var prod = (Product)_grid.SelectedRows[0].DataBoundItem;
                using (var form = new ProductEditForm(_repository, prod)) { if (form.ShowDialog() == DialogResult.OK) LoadData(); }
            };

            var btnDelete = new Button { Text = "Удалить", Width = 100 };
            btnDelete.Click += (s, e) => {
                if (_grid.SelectedRows.Count == 0) return;
                var prod = (Product)_grid.SelectedRows[0].DataBoundItem;
                if (MessageBox.Show($"Удалить {prod.Name}?", "Вопрос", MessageBoxButtons.YesNo) == DialogResult.Yes) { _repository.Delete(prod.Id); LoadData(); }
            };

            panel.Controls.Add(btnAdd); panel.Controls.Add(btnEdit); panel.Controls.Add(btnDelete);
            this.Controls.Add(panel);
        }

        private void LoadData() => _grid.DataSource = _repository.GetAll().ToList();
    }
}