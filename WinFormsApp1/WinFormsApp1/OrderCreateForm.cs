using System;
using System.Windows.Forms;
using System.Linq;
using ElectronicsStore.Entities;
using ElectronicsStore.Enums;
using ElectronicsStore.Interfaces;

namespace ElectronicsStore
{
    public partial class OrderCreateForm : Form
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly ICustomerRepository _customerRepo;
        private Order _currentOrder;

        public OrderCreateForm(IOrderRepository orderRepo, IProductRepository productRepo, ICustomerRepository customerRepo)
        {
            InitializeComponent();
            _orderRepo = orderRepo; _productRepo = productRepo; _customerRepo = customerRepo;
            _currentOrder = Order.Create(new Random().Next(1, 9999), 1, DeliveryOptions.Courier);

            _cmbCustomer.DataSource = _customerRepo.GetAll().ToList();
            _cmbCustomer.DisplayMember = "FullName";
            _cmbCustomer.ValueMember = "Id";

            _cmbDelivery.DataSource = Enum.GetValues(typeof(DeliveryOptions));

            _cmbProducts.DataSource = _productRepo.GetAll().ToList();
            _cmbProducts.DisplayMember = "Name";
            _cmbProducts.ValueMember = "Id";
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (_cmbProducts.SelectedItem is Product prod)
            {
                _currentOrder.AddItem(prod, 1);
                _lstCart.Items.Add($"{prod.Name} - {prod.Price}р.");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_cmbCustomer.SelectedItem == null) { MessageBox.Show("Выберите клиента!"); return; }
            if (_currentOrder.Items.Count == 0) { MessageBox.Show("Добавьте товары!"); return; }

            var cust = (Customer)_cmbCustomer.SelectedItem;
            var deliv = (DeliveryOptions)_cmbDelivery.SelectedItem;
            var final = Order.Create(new Random().Next(1, 9999), cust.Id, deliv);
            foreach (var item in _currentOrder.Items) final.AddItem(item.Product, item.Quantity);

            _orderRepo.Add(final);
            this.DialogResult = DialogResult.OK; this.Close();
        }
    }
}