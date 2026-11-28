using ElectronicsStore.Entities;
using ElectronicsStore.Enums;
using ElectronicsStore.Repositories;

namespace ElectronicsStore;

public partial class OrderCreateForm : Form
{
    public OrderCreateForm(IOrderRepository orders, IProductRepository products, ICustomerRepository customers)
    {
        InitializeComponent();
        Text = "Оформление заказа";
        ClientSize = new Size(580, 430);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        var customerCombo = NewCombo("Клиент:", 16, "FullName", customers.GetAll().ToList());
        var deliveryCombo = NewCombo("Доставка:", 53, "Name", new[] { DeliveryOptions.Courier, DeliveryOptions.Pickup }
            .Select(value => new { Value = value, Name = StoreUi.Name(value) }).ToList());
        var productCombo = NewCombo("Товар:", 90, "Label", products.GetAll().Where(p => p.StockQuantity > 0)
            .Select(p => new ProductChoice(p, $"{p.Name} — {p.Price:N2} руб. (ост. {p.StockQuantity})")).ToList());
        var quantity = new NumericUpDown { Left = 120, Top = 127, Minimum = 1, Maximum = int.MaxValue, Value = 1, Width = 90 };
        Controls.AddRange([new Label { Text = "Количество:", Left = 16, Top = 131, AutoSize = true }, quantity]);
        var add = new Button { Text = "В корзину", Left = 222, Top = 125, AutoSize = true };
        var remove = new Button { Text = "Убрать позицию", Left = 330, Top = 125, AutoSize = true };
        var list = new ListBox { Left = 16, Top = 165, Width = 548, Height = 187 };
        var total = new Label { Left = 16, Top = 370, AutoSize = true };
        var save = new Button { Text = "Оформить", Left = 455, Top = 367, Width = 109,
            Enabled = customerCombo.Items.Count > 0 && productCombo.Items.Count > 0 };
        var cart = new Dictionary<int, CartLine>();
        void RefreshCart()
        {
            list.DataSource = cart.Values.ToList();
            list.DisplayMember = nameof(CartLine.Label);
            total.Text = $"Итого: {cart.Values.Sum(line => line.Product.Price * line.Quantity):N2} руб.";
        }
        add.Click += (_, _) => StoreUi.Run(this, () =>
        {
            if (productCombo.SelectedItem is not ProductChoice choice) throw new InvalidOperationException("Выберите товар в наличии.");
            var product = choice.Product;
            var already = cart.GetValueOrDefault(product.Id)?.Quantity ?? 0;
            var toAdd = (int)quantity.Value;
            if (toAdd > product.StockQuantity - already) throw new InvalidOperationException("Столько товара нет на складе.");
            cart[product.Id] = new CartLine(product, already + toAdd);
            RefreshCart();
        });
        remove.Click += (_, _) =>
        {
            if (list.SelectedItem is not CartLine line) return;
            cart.Remove(line.Product.Id);
            RefreshCart();
        };
        save.Click += (_, _) => StoreUi.Run(this, () =>
        {
            if (customerCombo.SelectedItem is not Customer customer) throw new InvalidOperationException("Выберите клиента.");
            if (cart.Count == 0) throw new InvalidOperationException("Добавьте хотя бы один товар.");
            if (deliveryCombo.SelectedItem is null || deliveryCombo.SelectedValue is not DeliveryOptions delivery)
                throw new InvalidOperationException("Выберите способ доставки.");
            var order = Order.Create(0, customer.Id, delivery);
            foreach (var line in cart.Values) order.AddItem(line.Product, line.Quantity);
            orders.Add(order);
            DialogResult = DialogResult.OK;
        });
        deliveryCombo.ValueMember = "Value";
        Controls.AddRange([add, remove, list, total, save]);
        RefreshCart();
        if (!save.Enabled) total.Text = "Для заказа нужен клиент и товар в наличии.";
    }

    private ComboBox NewCombo(string label, int top, string displayMember, object data)
    {
        var combo = new ComboBox { Left = 120, Top = top, Width = 444, DropDownStyle = ComboBoxStyle.DropDownList,
            DisplayMember = displayMember, DataSource = data };
        Controls.AddRange([new Label { Text = label, Left = 16, Top = top + 3, AutoSize = true }, combo]);
        return combo;
    }

    private sealed record ProductChoice(Product Product, string Label);
    private sealed record CartLine(Product Product, int Quantity)
    {
        public string Label => $"{Product.Name} × {Quantity} — {Product.Price * Quantity:N2} руб.";
    }
}
