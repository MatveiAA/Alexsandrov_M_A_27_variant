namespace ElectronicsStore
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.directoriesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.productsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsMenu = new System.Windows.Forms.ToolStripMenuItem();

            // Инициализация меню
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.directoriesMenu, this.operationsMenu, this.reportsMenu });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.Controls.Add(this.menuStrip1);

            this.directoriesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.productsItem, this.customersItem });
            this.directoriesMenu.Text = "Справочники";

            this.productsItem.Text = "Товары";
            this.productsItem.Click += new System.EventHandler(this.MenuProducts_Click);
            this.customersItem.Text = "Клиенты";
            this.customersItem.Click += new System.EventHandler(this.MenuCustomers_Click);

            this.operationsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.ordersItem });
            this.operationsMenu.Text = "Операции";
            this.ordersItem.Text = "Заказы";
            this.ordersItem.Click += new System.EventHandler(this.MenuOrders_Click);

            this.reportsMenu.Text = "Отчеты";

            this.MainMenuStrip = this.menuStrip1;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Главное меню - Магазин электроники";
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem directoriesMenu;
        private System.Windows.Forms.ToolStripMenuItem productsItem;
        private System.Windows.Forms.ToolStripMenuItem customersItem;
        private System.Windows.Forms.ToolStripMenuItem operationsMenu;
        private System.Windows.Forms.ToolStripMenuItem ordersItem;
        private System.Windows.Forms.ToolStripMenuItem reportsMenu;
    }
}