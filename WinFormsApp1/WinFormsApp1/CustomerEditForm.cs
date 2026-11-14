using System;
using System.Windows.Forms;
using ElectronicsStore.Entities;
using ElectronicsStore.Interfaces;

namespace ElectronicsStore
{
    public partial class CustomerEditForm : Form
    {
        private readonly ICustomerRepository _repository;
        private readonly Customer _existing;

        public CustomerEditForm(ICustomerRepository repository) { InitializeComponent(); _repository = repository; }
        public CustomerEditForm(ICustomerRepository repository, Customer customer)
        {
            InitializeComponent(); _repository = repository; _existing = customer;
            _txtName.Text = customer.FullName; _txtPhone.Text = customer.Phone;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_txtName.Text)) throw new Exception("Введите ФИО!");
                if (_existing == null) _repository.Add(Customer.Create(new Random().Next(1, 9999), _txtName.Text, _txtPhone.Text));
                else _repository.Update(Customer.Create(_existing.Id, _txtName.Text, _txtPhone.Text));
                this.DialogResult = DialogResult.OK; this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}