using System;
using System.Windows.Forms;
using ElectronicsStore.Repositories;

namespace ElectronicsStore
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            // Убрали InitializeComponent(), так как файл дизайнера мы удалили
            SetupMenu();
        }

        private void SetupMenu()
        {
            this.Text = "Главное меню - Магазин электроники";
            this.Size = new System.Drawing.Size(800, 600);

            MenuStrip mainMenu = new MenuStrip();
            ToolStripMenuItem directoriesMenu = new ToolStripMenuItem("Справочники");

            ToolStripMenuItem productsItem = new ToolStripMenuItem("Товары");
            productsItem.Click += (s, e) => {
                var repo = Program.ServiceProvider.GetService(typeof(IProductRepository)) as IProductRepository;
                new ProductListForm(repo).ShowDialog();
            };

            ToolStripMenuItem customersItem = new ToolStripMenuItem("Клиенты");
            customersItem.Click += (s, e) => {
                var repo = Program.ServiceProvider.GetService(typeof(ICustomerRepository)) as ICustomerRepository;
                new CustomerListForm(repo).ShowDialog();
            };

            directoriesMenu.DropDownItems.Add(productsItem);
            directoriesMenu.DropDownItems.Add(customersItem);

            ToolStripMenuItem operationsMenu = new ToolStripMenuItem("Операции");
            ToolStripMenuItem ordersItem = new ToolStripMenuItem("Заказы");
            ordersItem.Click += (s, e) => {
                var oRepo = Program.ServiceProvider.GetService(typeof(IOrderRepository)) as IOrderRepository;
                var pRepo = Program.ServiceProvider.GetService(typeof(IProductRepository)) as IProductRepository;
                var cRepo = Program.ServiceProvider.GetService(typeof(ICustomerRepository)) as ICustomerRepository;
                new OrderListForm(oRepo, pRepo, cRepo).ShowDialog();
            };
            operationsMenu.DropDownItems.Add(ordersItem);

            ToolStripMenuItem reportsMenu = new ToolStripMenuItem("Отчеты");

            mainMenu.Items.Add(directoriesMenu);
            mainMenu.Items.Add(operationsMenu);
            mainMenu.Items.Add(reportsMenu);

            this.MainMenuStrip = mainMenu;
            this.Controls.Add(mainMenu);
        }
    }
}