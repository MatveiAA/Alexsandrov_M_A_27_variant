using System;
using System.Windows.Forms;
using System.Linq;
using ElectronicsStore.Entities;
using ElectronicsStore.Repositories;
using ElectronicsStore.Enums;

namespace ElectronicsStore
{
    public partial class OrderCreateForm : Form
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly ICustomerRepository _customerRepo;

        private ComboBox _cmbCustomer, _cmbDelivery, _cmbProducts;
        private ListBox _lstCart;
        private Order _currentOrder;

        public OrderCreateForm(IOrderRepository orderRepo, IProductRepository productRepo, ICustomerRepository customerRepo)
        {
            InitializeComponent();
            _orderRepo = orderRepo; _productRepo = productRepo; _customerRepo = customerRepo;
            _currentOrder = Order.Create(new Random().Next(1, 9999), 1, DeliveryOptions.Courier);
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Оформление заказа";
            this.Size = new System.Drawing.Size(500, 400);

            this.Controls.Add(new Label { Text = "Клиент:", Location = new System.Drawing.Point(20, 20), Width = 100 });
            _cmbCustomer = new ComboBox { Location = new System.Drawing.Point(130, 20), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList, DataSource = _customerRepo.GetAll().ToList(), DisplayMember = "FullName", ValueMember = "Id" };

            this.Controls.Add(new Label { Text = "Доставка:", Location = new System.Drawing.Point(20, 60), Width = 100 });
            _cmbDelivery = new ComboBox { Location = new System.Drawing.Point(130, 60), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList, DataSource = Enum.GetValues(typeof(DeliveryOptions)) };

            this.Controls.Add(new Label { Text = "Товар:", Location = new System.Drawing.Point(20, 110), Width = 100 });
            _cmbProducts = new ComboBox { Location = new System.Drawing.Point(130, 110), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, DataSource = _productRepo.GetAll().ToList(), DisplayMember = "Name", ValueMember = "Id" };

            var btnAddToCart = new Button { Text = "Добавить", Location = new System.Drawing.Point(340, 108), Width = 90 };
            btnAddToCart.Click += (s, e) => {
                if (_cmbProducts.SelectedItem is Product prod)
                {
                    _currentOrder.AddItem(prod, 1);
                    _lstCart.Items.Add($"{prod.Name} - {prod.Price}р.");
                }
            };

            _lstCart = new ListBox { Location = new System.Drawing.Point(130, 150), Width = 300, Height = 100 };

            var btnSave = new Button { Text = "Оформить", Location = new System.Drawing.Point(130, 280), Width = 100 };
            btnSave.Click += (s, e) => {
                if (_cmbCustomer.SelectedItem == null) { MessageBox.Show("Выберите клиента!"); return; }
                if (_currentOrder.Items.Count == 0) { MessageBox.Show("Добавьте товары!"); return; }

                var cust = (Customer)_cmbCustomer.SelectedItem;
                var deliv = (DeliveryOptions)_cmbDelivery.SelectedItem;
                var final = Order.Create(new Random().Next(1, 9999), cust.Id, deliv);
                foreach (var item in _currentOrder.Items) final.AddItem(item.Product, item.Quantity);

                _orderRepo.Add(final);
                this.DialogResult = DialogResult.OK; this.Close();
            };

            this.Controls.Add(_cmbCustomer); this.Controls.Add(_cmbDelivery); this.Controls.Add(_cmbProducts);
            this.Controls.Add(btnAddToCart); this.Controls.Add(_lstCart); this.Controls.Add(btnSave);
        }
    }
}