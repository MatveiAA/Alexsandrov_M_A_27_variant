using ElectronicsStore.Entities;
using ElectronicsStore.Repositories;

namespace ElectronicsStore;

public partial class ProductListForm : Form
{
    public ProductListForm(IProductRepository repository)
    {
        InitializeComponent();
        Text = "Каталог товаров";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(840, 460);
        var grid = new StoreGrid<Product>(("№", p => p.Id), ("Модель", p => p.Name),
            ("Категория", p => StoreUi.Name(p.Category)), ("Характеристики", p => p.Specifications),
            ("Цена, руб.", p => p.Price.ToString("N2")), ("Остаток, шт.", p => p.StockQuantity));
        grid.Columns[3].FillWeight = 200;
        void RefreshGrid() => grid.SetItems(repository.GetAll());
        void Edit(Product? product) => StoreUi.Run(this, () =>
        {
            using var dialog = new ProductEditForm(repository, product);
            if (dialog.ShowDialog(this) == DialogResult.OK) RefreshGrid();
        });
        var toolbar = StoreGrid<Product>.Toolbar();
        toolbar.Controls.AddRange([
            StoreGrid<Product>.Action("Добавить", (_, _) => Edit(null)),
            StoreGrid<Product>.Action("Изменить", (_, _) => { if (grid.SelectedItem is { } product) Edit(product); }),
            StoreGrid<Product>.Action("Удалить", (_, _) => StoreUi.Run(this, () =>
            {
                if (grid.SelectedItem is not { } product || MessageBox.Show(this, $"Удалить товар «{product.Name}» из каталога?",
                        Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                repository.Delete(product.Id);
                RefreshGrid();
            }))
        ]);
        Controls.Add(grid);
        Controls.Add(toolbar);
        Shown += (_, _) => StoreUi.Run(this, RefreshGrid);
    }
}
