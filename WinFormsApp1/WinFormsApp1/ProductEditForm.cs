using ElectronicsStore.Entities;
using ElectronicsStore.Enums;
using ElectronicsStore.Repositories;

namespace ElectronicsStore;

public partial class ProductEditForm : Form
{
    public ProductEditForm(IProductRepository repository, Product? existing = null)
    {
        InitializeComponent();
        Text = existing is null ? "Новый товар" : "Редактирование товара";
        ClientSize = new Size(460, 286);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        var name = new TextBox { Width = 270, MaxLength = 200, Text = existing?.Name ?? "" };
        var price = new NumericUpDown { Width = 150, DecimalPlaces = 2, Maximum = 9_999_999_999.99M, Value = existing?.Price ?? 0 };
        var stock = new NumericUpDown { Width = 150, Maximum = int.MaxValue, Value = existing?.StockQuantity ?? 0 };
        var category = new ComboBox { Width = 270, DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "Name", ValueMember = "Value",
            DataSource = Enum.GetValues<ProductCategory>().Select(value => new { Value = value, Name = StoreUi.Name(value) }).ToList() };
        if (existing is not null) category.SelectedValue = existing.Category;
        var specifications = new TextBox { Width = 270, Height = 65, Multiline = true, ScrollBars = ScrollBars.Vertical,
            MaxLength = 1000, Text = existing?.Specifications ?? "" };
        AddField("Название / модель:", name, 16);
        AddField("Цена, руб.:", price, 52);
        AddField("Остаток, шт.:", stock, 88);
        AddField("Категория:", category, 124);
        AddField("Характеристики:", specifications, 160);
        var save = new Button { Text = "Сохранить", Left = 164, Top = 240, Width = 105 };
        save.Click += (_, _) => StoreUi.Run(this, () =>
        {
            if (category.SelectedValue is not ProductCategory selected) throw new InvalidOperationException("Выберите категорию.");
            var product = Product.Create(existing?.Id ?? 0, name.Text, price.Value, (int)stock.Value, selected, specifications.Text);
            if (existing is null) repository.Add(product); else repository.Update(product, existing.StockQuantity);
            DialogResult = DialogResult.OK;
        });
        Controls.Add(save);
        AcceptButton = save;
    }

    private void AddField(string title, Control field, int top)
    {
        Controls.Add(new Label { Text = title, Left = 16, Top = top + 4, AutoSize = true });
        field.SetBounds(164, top, field.Width, field.Height);
        Controls.Add(field);
    }
}
