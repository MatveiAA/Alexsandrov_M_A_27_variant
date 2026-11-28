using ElectronicsStore.Entities;
using ElectronicsStore.Repositories;

namespace ElectronicsStore;

public partial class CustomerListForm : Form
{
    public CustomerListForm(ICustomerRepository repository)
    {
        InitializeComponent();
        Text = "Клиенты";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(640, 400);
        var grid = new StoreGrid<Customer>(("№", c => c.Id), ("ФИО", c => c.FullName), ("Телефон", c => c.Phone));
        void RefreshGrid() => grid.SetItems(repository.GetAll());
        void Edit(Customer? customer) => StoreUi.Run(this, () =>
        {
            using var dialog = new CustomerEditForm(repository, customer);
            if (dialog.ShowDialog(this) == DialogResult.OK) RefreshGrid();
        });
        var toolbar = StoreGrid<Customer>.Toolbar();
        toolbar.Controls.AddRange([
            StoreGrid<Customer>.Action("Добавить", (_, _) => Edit(null)),
            StoreGrid<Customer>.Action("Изменить", (_, _) => { if (grid.SelectedItem is { } customer) Edit(customer); }),
            StoreGrid<Customer>.Action("Удалить", (_, _) => StoreUi.Run(this, () =>
            {
                if (grid.SelectedItem is not { } customer || MessageBox.Show(this, $"Удалить клиента «{customer.FullName}» из справочника?",
                        Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                repository.Delete(customer.Id);
                RefreshGrid();
            }))
        ]);
        Controls.Add(grid);
        Controls.Add(toolbar);
        Shown += (_, _) => StoreUi.Run(this, RefreshGrid);
    }
}
