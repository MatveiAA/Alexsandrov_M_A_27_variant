using System;
using System.Windows.Forms;
using ElectronicsStore.Entities;
using ElectronicsStore.Repositories;
using ElectronicsStore.Enums;

namespace ElectronicsStore
{
    public partial class ProductEditForm : Form
    {
        private readonly IProductRepository _repository;
        private readonly Product _existingProduct;
        private TextBox _txtName;
        private NumericUpDown _numPrice, _numStock;
        private ComboBox _cmbCategory;

        public ProductEditForm(IProductRepository repository) { InitializeComponent(); _repository = repository; SetupForm(); }
        public ProductEditForm(IProductRepository repository, Product product) { InitializeComponent(); _repository = repository; _existingProduct = product; SetupForm(); FillForm(); }

        private void SetupForm()
        {
            this.Text = _existingProduct == null ? "Добавление товара" : "Редактирование";
            this.Size = new System.Drawing.Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            this.Controls.Add(new Label { Text = "Название:", Location = new System.Drawing.Point(20, 20), Width = 100 });
            _txtName = new TextBox { Location = new System.Drawing.Point(130, 20), Width = 200 };

            this.Controls.Add(new Label { Text = "Цена:", Location = new System.Drawing.Point(20, 60), Width = 100 });
            _numPrice = new NumericUpDown { Location = new System.Drawing.Point(130, 60), Width = 200, DecimalPlaces = 2, Maximum = 1000000 };

            this.Controls.Add(new Label { Text = "Кол-во:", Location = new System.Drawing.Point(20, 100), Width = 100 });
            _numStock = new NumericUpDown { Location = new System.Drawing.Point(130, 100), Width = 200, Maximum = 10000 };

            this.Controls.Add(new Label { Text = "Категория:", Location = new System.Drawing.Point(20, 140), Width = 100 });
            _cmbCategory = new ComboBox { Location = new System.Drawing.Point(130, 140), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            _cmbCategory.DataSource = Enum.GetValues(typeof(ProductCategory));

            var btnSave = new Button { Text = "Сохранить", Location = new System.Drawing.Point(130, 190), Width = 100 };
            btnSave.Click += BtnSave_Click;

            this.Controls.Add(_txtName); this.Controls.Add(_numPrice); this.Controls.Add(_numStock); this.Controls.Add(_cmbCategory); this.Controls.Add(btnSave);
        }

        private void FillForm() { _txtName.Text = _existingProduct.Name; _numPrice.Value = _existingProduct.Price; _numStock.Value = _existingProduct.StockQuantity; _cmbCategory.SelectedItem = _existingProduct.Category; }

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