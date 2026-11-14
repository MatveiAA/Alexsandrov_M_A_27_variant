using System;
using System.Windows.Forms;
using ElectronicsStore.Entities;
using ElectronicsStore.Repositories;

namespace ElectronicsStore
{
    public partial class CustomerEditForm : Form
    {
        private readonly ICustomerRepository _repository;
        private readonly Customer _existing;
        private TextBox _txtName, _txtPhone;

        public CustomerEditForm(ICustomerRepository repository) { InitializeComponent(); _repository = repository; SetupForm(); }
        public CustomerEditForm(ICustomerRepository repository, Customer customer) { InitializeComponent(); _repository = repository; _existing = customer; SetupForm(); _txtName.Text = customer.FullName; _txtPhone.Text = customer.Phone; }

        private void SetupForm()
        {
            this.Text = _existing == null ? "Новый клиент" : "Редактирование";
            this.Size = new System.Drawing.Size(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            this.Controls.Add(new Label { Text = "ФИО:", Location = new System.Drawing.Point(20, 20), Width = 100 });
            _txtName = new TextBox { Location = new System.Drawing.Point(130, 20), Width = 200 };

            this.Controls.Add(new Label { Text = "Телефон:", Location = new System.Drawing.Point(20, 60), Width = 100 });
            _txtPhone = new TextBox { Location = new System.Drawing.Point(130, 60), Width = 200 };

            var btnSave = new Button { Text = "Сохранить", Location = new System.Drawing.Point(130, 110), Width = 100 };
            btnSave.Click += (s, e) => {
                try
                {
                    if (string.IsNullOrWhiteSpace(_txtName.Text)) throw new Exception("Введите ФИО!");
                    if (_existing == null) _repository.Add(Customer.Create(new Random().Next(1, 9999), _txtName.Text, _txtPhone.Text));
                    else _repository.Update(Customer.Create(_existing.Id, _txtName.Text, _txtPhone.Text));
                    this.DialogResult = DialogResult.OK; this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            };

            this.Controls.Add(_txtName); this.Controls.Add(_txtPhone); this.Controls.Add(btnSave);
        }
    }
}