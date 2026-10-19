using VyshKat.App.Interfaces;
namespace VyshKat.App.Models;

public abstract class Transport : IInventoryItem
{
    public string InventoryNumber { get; }
    public string Name { get; }
    public int EaseOfUse { get; }
    public bool IsWorking { get; }

    protected Transport(
        string inventoryNumber,
        string name,
        int easeOfUse,
        bool isWorking)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер не может быть пустым");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название не может быть пустым");

        if (easeOfUse < 1 || easeOfUse > 10)
            throw new ArgumentOutOfRangeException(nameof(easeOfUse), "Простота управления должна быть от 1 до 10");

        InventoryNumber = inventoryNumber.Trim();
        Name = name.Trim();
        EaseOfUse = easeOfUse;
        IsWorking = isWorking;
    }
}
