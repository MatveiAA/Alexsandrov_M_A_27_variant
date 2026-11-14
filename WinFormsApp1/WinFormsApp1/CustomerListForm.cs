using System;
using System.Linq;
using System.Windows.Forms;
using ElectronicsStore.Entities;
using ElectronicsStore.Repositories;

namespace ElectronicsStore
{
    public partial class CustomerListForm : Form
    {
        private readonly ICustomerRepository _repository;
        private DataGridView _grid;

        public CustomerListForm(ICustomerRepository repository) { InitializeComponent(); _repository = repository; SetupForm(); LoadData(); }

        private void SetupForm()
        {
            this.Text = "Список клиентов";
            this.Size = new System.Drawing.Size(600, 400);

            _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AllowUserToAddRows = false };
            this.Controls.Add(_grid);

            var panel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 50 };
            var btnAdd = new Button { Text = "Добавить", Width = 100 };
            btnAdd.Click += (s, e) => { using (var form = new CustomerEditForm(_repository)) { if (form.ShowDialog() == DialogResult.OK) LoadData(); } };

            var btnEdit = new Button { Text = "Изменить", Width = 100 };
            btnEdit.Click += (s, e) => {
                if (_grid.SelectedRows.Count == 0) return;
                var cust = (Customer)_grid.SelectedRows[0].DataBoundItem;
                using (var form = new CustomerEditForm(_repository, cust)) { if (form.ShowDialog() == DialogResult.OK) LoadData(); }
            };

            var btnDelete = new Button { Text = "Удалить", Width = 100 };
            btnDelete.Click += (s, e) => {
                if (_grid.SelectedRows.Count == 0) return;
                var cust = (Customer)_grid.SelectedRows[0].DataBoundItem;
                if (MessageBox.Show($"Удалить {cust.FullName}?", "Вопрос", MessageBoxButtons.YesNo) == DialogResult.Yes) { _repository.Delete(cust.Id); LoadData(); }
            };

            panel.Controls.Add(btnAdd); panel.Controls.Add(btnEdit); panel.Controls.Add(btnDelete);
            this.Controls.Add(panel);
        }
        private void LoadData() => _grid.DataSource = _repository.GetAll().ToList();
    }
}