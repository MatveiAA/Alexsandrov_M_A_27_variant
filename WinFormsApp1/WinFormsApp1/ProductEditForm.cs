using System;
using System.Windows.Forms;
using ElectronicsStore.Entities;
using ElectronicsStore.Enums;
using ElectronicsStore.Interfaces;

namespace ElectronicsStore
{
    public partial class ProductEditForm : Form
    {
        private readonly IProductRepository _repository;
        private readonly Product _existingProduct;

        public ProductEditForm(IProductRepository repository) { InitializeComponent(); _repository = repository; }
        public ProductEditForm(IProductRepository repository, Product product)
        {
            InitializeComponent(); _repository = repository; _existingProduct = product;
            _txtName.Text = product.Name; _numPrice.Value = product.Price; _numStock.Value = product.StockQuantity; _cmbCategory.SelectedItem = product.Category;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_txtName.Text)) throw new Exception("Введите название!");
                var cat = (ProductCategory)_cmbCategory.SelectedItem;

                if (_existingProduct == null)
                    _repository.Add(Product.Create(new Random().Next(1, 9999), _txtName.Text, _numPrice.Value, (int)_numStock.Value, cat));
                else
                    _repository.Update(Product.Create(_existingProduct.Id, _txtName.Text, _numPrice.Value, (int)_numStock.Value, cat));

                this.DialogResult = DialogResult.OK; this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}