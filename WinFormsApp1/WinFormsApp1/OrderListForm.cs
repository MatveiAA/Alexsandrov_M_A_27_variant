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
        private DataGridView _grid;

        public OrderListForm(IOrderRepository orderRepo, IProductRepository productRepo, ICustomerRepository customerRepo)
        {
            InitializeComponent();
            _orderRepo = orderRepo; _productRepo = productRepo; _customerRepo = customerRepo;
            SetupForm(); LoadData();
        }

        private void SetupForm()
        {
            this.Text = "Список заказов";
            this.Size = new System.Drawing.Size(800, 500);

            _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AllowUserToAddRows = false };
            this.Controls.Add(_grid);

            var panel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 50 };
            var btnAdd = new Button { Text = "Создать заказ", Width = 120 };
            btnAdd.Click += (s, e) => { using (var form = new OrderCreateForm(_orderRepo, _productRepo, _customerRepo)) { if (form.ShowDialog() == DialogResult.OK) LoadData(); } };
            panel.Controls.Add(btnAdd);
            this.Controls.Add(panel);
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
    }
}