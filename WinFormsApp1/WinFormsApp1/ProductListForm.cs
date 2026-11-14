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

        public ProductListForm(IProductRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            LoadData();
        }

        private void LoadData() => _grid.DataSource = _repository.GetAll().ToList();

        private void BtnAdd_Click(object sender, EventArgs e) { using (var form = new ProductEditForm(_repository)) { if (form.ShowDialog() == DialogResult.OK) LoadData(); } }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_grid.SelectedRows.Count == 0) return;
            var prod = (Product)_grid.SelectedRows[0].DataBoundItem;
            using (var form = new ProductEditForm(_repository, prod)) { if (form.ShowDialog() == DialogResult.OK) LoadData(); }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_grid.SelectedRows.Count == 0) return;
            var prod = (Product)_grid.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show($"Удалить {prod.Name}?", "Вопрос", MessageBoxButtons.YesNo) == DialogResult.Yes) { _repository.Delete(prod.Id); LoadData(); }
        }
    }
}