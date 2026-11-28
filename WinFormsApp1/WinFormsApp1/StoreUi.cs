using ElectronicsStore.Enums;
using Serilog;

namespace ElectronicsStore;

internal static class StoreUi
{
    public static void Run(Form owner, Action action)
    {
        try { action(); }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or KeyNotFoundException)
        {
            Log.Warning(ex, "Операция отклонена в форме {Form}", owner.GetType().Name);
            MessageBox.Show(owner, ex.Message, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Ошибка в форме {Form}", owner.GetType().Name);
            MessageBox.Show(owner, "Операция не выполнена. Подробности в журнале приложения.",
                owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static string Name(DeliveryOptions value) => value switch
    {
        DeliveryOptions.Courier => "Курьер",
        DeliveryOptions.Pickup => "Самовывоз",
        _ => value.ToString()
    };

    public static string Name(OrderStatus value) => value switch
    {
        OrderStatus.Processing => "В обработке",
        OrderStatus.Shipped => "Отправлен",
        OrderStatus.Delivered => "Доставлен",
        _ => value.ToString()
    };

    public static string Name(ProductCategory value) => value switch
    {
        ProductCategory.Smartphone => "Смартфоны",
        ProductCategory.Laptop => "Ноутбуки",
        ProductCategory.Tablet => "Планшеты",
        ProductCategory.Accessory => "Аксессуары",
        _ => value.ToString()
    };
}
