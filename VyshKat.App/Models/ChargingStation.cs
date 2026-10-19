using VyshKat.App.Interfaces;

namespace VyshKat.App.Models;

public class ChargingStation : IInventoryItem, IEnergyConsumer
{
    public string InventoryNumber { get; }
    public string Name => "Зарядная станция";
    public double DailyEnergyConsumption { get; }

    public ChargingStation(string inventoryNumber, double dailyEnergyConsumption)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер не может быть пустым");

        if (double.IsNaN(dailyEnergyConsumption) ||
            double.IsInfinity(dailyEnergyConsumption) ||
            dailyEnergyConsumption < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyConsumption),
                "Энергопотребление должно быть неотрицательным конеяным числом");
        }

        InventoryNumber = inventoryNumber.Trim();
        DailyEnergyConsumption = dailyEnergyConsumption;
    }
}
