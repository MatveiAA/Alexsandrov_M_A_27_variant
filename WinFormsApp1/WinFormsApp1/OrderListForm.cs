using ElectronicsStore.Entities;
using ElectronicsStore.Enums;
using ElectronicsStore.Repositories;

namespace ElectronicsStore;

public partial class OrderListForm : Form
{
    public OrderListForm(IOrderRepository orders, IProductRepository products, ICustomerRepository customers)
    {
        InitializeComponent();
        Text = "Заказы";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(960, 490);
        var grid = new StoreGrid<Order>(("№", o => o.Id), ("Оформлен", o => o.OrderDate.ToLocalTime().ToString("g")),
            ("Клиент", o => o.CustomerName), ("Доставка", o => StoreUi.Name(o.Delivery)),
            ("Статус", o => StoreUi.Name(o.Status)), ("Товаров, шт.", o => o.Items.Sum(i => (long)i.Quantity)),
            ("Сумма, руб.", o => o.TotalAmount.ToString("N2")));
        grid.Columns[0].FillWeight = 40;
        var incomplete = new CheckBox { Text = "Только невыполненные", AutoSize = true, Margin = new Padding(12, 8, 3, 3) };
        void RefreshGrid() => grid.SetItems(orders.GetAll().Where(o => !incomplete.Checked || o.Status != OrderStatus.Delivered));
        var add = StoreGrid<Order>.Action("Создать заказ", (_, _) => StoreUi.Run(this, () =>
        {
            using var dialog = new OrderCreateForm(orders, products, customers);
            if (dialog.ShowDialog(this) == DialogResult.OK) RefreshGrid();
        }));
        void ShowDetails()
        {
            if (grid.SelectedItem is not { } order) return;
            StoreUi.Run(this, () =>
            {
                using var dialog = new Form { Text = $"Позиции заказа №{order.Id}", ClientSize = new Size(740, 350),
                    StartPosition = FormStartPosition.CenterParent, ShowInTaskbar = false };
                var lines = new StoreGrid<OrderItem>(("Модель", i => i.ProductName), ("Категория", i => StoreUi.Name(i.Category)),
                    ("Количество", i => i.Quantity), ("Цена, руб.", i => i.UnitPrice.ToString("N2")), ("Сумма, руб.", i => i.LineTotal.ToString("N2")));
                lines.SetItems(order.Items);
                dialog.Controls.Add(lines);
                dialog.ShowDialog(this);
            });
        }
        var details = StoreGrid<Order>.Action("Позиции", (_, _) => ShowDetails());
        var advance = StoreGrid<Order>.Action("Следующий статус", (_, _) => StoreUi.Run(this, () =>
        {
            if (grid.SelectedItem is not { } order || Next(order.Status) is not { } status) return;
            if (MessageBox.Show(this, $"Заказ №{order.Id}: установить статус «{StoreUi.Name(status)}»?",
                    Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            orders.UpdateStatus(order.Id, status);
            RefreshGrid();
        }));
        void UpdateActions()
        {
            details.Enabled = grid.SelectedItem is not null;
            advance.Enabled = grid.SelectedItem is { } order && Next(order.Status) is not null;
        }
        var toolbar = StoreGrid<Order>.Toolbar();
        toolbar.Controls.AddRange([add, details, advance, incomplete]);
        Controls.Add(grid);
        Controls.Add(toolbar);
        grid.SelectionChanged += (_, _) => UpdateActions();
        grid.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) ShowDetails(); };
        incomplete.CheckedChanged += (_, _) => StoreUi.Run(this, RefreshGrid);
        Shown += (_, _) => StoreUi.Run(this, RefreshGrid);
        UpdateActions();
    }

    private static OrderStatus? Next(OrderStatus status) => status switch
    {
        OrderStatus.Processing => OrderStatus.Shipped,
        OrderStatus.Shipped => OrderStatus.Delivered,
        _ => null
    };
}
