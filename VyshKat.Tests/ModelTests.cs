using VyshKat.App.Models;
namespace VyshKat.Tests;

public class ModelTests
{
    [Fact]
    public void Scooter_WithValidData_Created()
    {
        var scooter = new Scooter("S001", 7, 2.5);

        Assert.Equal("S001", scooter.InventoryNumber);
        Assert.Equal(7, scooter.EaseOfUse);
        Assert.Equal(2.5, scooter.DailyEnergyConsumption);
    }

    [Fact]
    public void Scooter_WithInvalidEaseOfUse_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Scooter("S001", 11, 2.5));
    }

    [Fact]
    public void Bicycle_WithEmptyInventoryNumber_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Bicycle("", 5));
    }

    [Fact]
    public void ChargingStation_WithNegativeEnergy_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new ChargingStation("C001", -5));
    }

    [Fact]
    public void Helmet_WithValidNumber_Created()
    {
        var helmet = new Helmet("H001");

        Assert.Equal("H001", helmet.InventoryNumber);
        Assert.Equal("Шлем", helmet.Name);
    }
}
