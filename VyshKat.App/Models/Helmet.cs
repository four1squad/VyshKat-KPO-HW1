using VyshKat.App.Interfaces;

namespace VyshKat.App.Models;

public class Helmet : IInventoryItem
{
    public string InventoryNumber { get; }
    public string Name => "Шлем";

    public Helmet(string inventoryNumber)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер не может быть пустым");

        InventoryNumber = inventoryNumber.Trim();
    }
}
