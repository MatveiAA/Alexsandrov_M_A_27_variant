using System;
using System.Windows.Forms;
using ElectronicsStore.Repositories;

namespace ElectronicsStore
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MenuProducts_Click(object sender, EventArgs e)
        {
            var repo = Program.ServiceProvider.GetService(typeof(IProductRepository)) as IProductRepository;
            new ProductListForm(repo).ShowDialog();
        }

        private void MenuCustomers_Click(object sender, EventArgs e)
        {
            var repo = Program.ServiceProvider.GetService(typeof(ICustomerRepository)) as ICustomerRepository;
            new CustomerListForm(repo).ShowDialog();
        }

        private void MenuOrders_Click(object sender, EventArgs e)
        {
            var oRepo = Program.ServiceProvider.GetService(typeof(IOrderRepository)) as IOrderRepository;
            var pRepo = Program.ServiceProvider.GetService(typeof(IProductRepository)) as IProductRepository;
            var cRepo = Program.ServiceProvider.GetService(typeof(ICustomerRepository)) as ICustomerRepository;
            new OrderListForm(oRepo, pRepo, cRepo).ShowDialog();
        }
    }
}