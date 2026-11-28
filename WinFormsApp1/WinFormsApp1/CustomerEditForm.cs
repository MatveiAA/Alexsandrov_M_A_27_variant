using ElectronicsStore.Entities;
using ElectronicsStore.Repositories;

namespace ElectronicsStore;

public partial class CustomerEditForm : Form
{
    public CustomerEditForm(ICustomerRepository repository, Customer? existing = null)
    {
        InitializeComponent();
        Text = existing is null ? "Новый клиент" : "Редактирование клиента";
        ClientSize = new Size(380, 140);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        var name = new TextBox { Left = 120, Top = 16, Width = 240, MaxLength = 200, Text = existing?.FullName ?? "" };
        var phone = new TextBox { Left = 120, Top = 52, Width = 240, MaxLength = 40, Text = existing?.Phone ?? "" };
        var save = new Button { Text = "Сохранить", Left = 120, Top = 91, Width = 105 };
        save.Click += (_, _) => StoreUi.Run(this, () =>
        {
            var customer = Customer.Create(existing?.Id ?? 0, name.Text, phone.Text);
            if (existing is null) repository.Add(customer); else repository.Update(customer);
            DialogResult = DialogResult.OK;
        });
        Controls.AddRange([
            new Label { Text = "ФИО:", Left = 16, Top = 20, AutoSize = true }, name,
            new Label { Text = "Телефон:", Left = 16, Top = 55, AutoSize = true }, phone, save
        ]);
        AcceptButton = save;
    }
}
