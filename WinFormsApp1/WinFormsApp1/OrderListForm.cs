using System;
using System.Linq;
using System.Windows.Forms;
using ElectronicsStore.Repositories;

namespace ElectronicsStore
{
    public partial class OrderListForm : Form
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly ICustomerRepository _customerRepo;

        public OrderListForm(IOrderRepository orderRepo, IProductRepository productRepo, ICustomerRepository customerRepo)
        {
            InitializeComponent();
            _orderRepo = orderRepo; _productRepo = productRepo; _customerRepo = customerRepo;
            LoadData();
        }

        private void LoadData()
        {
            var orders = _orderRepo.GetAll().Select(o => new {
                Id = o.Id,
                Date = o.OrderDate.ToString("g"),
                CustomerId = o.CustomerId,
                Status = o.Status.ToString(),
                Delivery = o.Delivery.ToString(),
                ItemsCount = o.Items.Count
            }).ToList();
            _grid.DataSource = orders;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new OrderCreateForm(_orderRepo, _productRepo, _customerRepo))
            {
                if (form.ShowDialog() == DialogResult.OK) LoadData();
            }
        }
    }
}