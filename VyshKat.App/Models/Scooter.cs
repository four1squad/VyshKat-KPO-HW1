using VyshKat.App.Interfaces;
namespace VyshKat.App.Models;

public class Scooter : Transport, IEnergyConsumer
{
    public double DailyEnergyConsumption { get; }

    public Scooter(
        string inventoryNumber,
        int easeOfUse,
        double dailyEnergyConsumption,
        bool isWorking = true)
        : base(inventoryNumber, "Электросамокат", easeOfUse, isWorking)
    {
        if (double.IsNaN(dailyEnergyConsumption) ||
            double.IsInfinity(dailyEnergyConsumption) ||
            dailyEnergyConsumption < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(dailyEnergyConsumption),
                "Энергопотребление должно быть неотрицательным конечным числом");
        }

        DailyEnergyConsumption = dailyEnergyConsumption;
    }

}
