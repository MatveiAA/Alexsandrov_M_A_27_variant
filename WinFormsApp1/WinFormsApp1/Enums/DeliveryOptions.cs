using System;

namespace ElectronicsStore.Enums
{
    [Flags]
    public enum DeliveryOptions
    {
        None = 0,
        Courier = 1,
        Pickup = 2,
        Express = 4
    }
}