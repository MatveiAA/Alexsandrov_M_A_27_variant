using Microsoft.Extensions.DependencyInjection;

namespace ElectronicsStore;

public partial class MainForm : Form
{
    private readonly IServiceProvider? _services;

    public MainForm()
    {
        InitializeComponent();
        Text = "Магазин электроники";
        StartPosition = FormStartPosition.CenterScreen;
        var menu = new MenuStrip();
        var directories = new ToolStripMenuItem("Справочники");
        directories.DropDownItems.Add("Товары", null, (_, _) => ShowForm<ProductListForm>());
        directories.DropDownItems.Add("Клиенты", null, (_, _) => ShowForm<CustomerListForm>());
        var operations = new ToolStripMenuItem("Операции");
        operations.DropDownItems.Add("Заказы", null, (_, _) => ShowForm<OrderListForm>());
        menu.Items.AddRange([directories, operations]);
        MainMenuStrip = menu;
        Controls.Add(menu);
    }

    public MainForm(IServiceProvider services) : this() => _services = services;

    private void ShowForm<T>() where T : Form
    {
        if (_services is null) return;
        StoreUi.Run(this, () =>
        {
            using var form = ActivatorUtilities.CreateInstance<T>(_services);
            form.ShowDialog(this);
        });
    }
}
