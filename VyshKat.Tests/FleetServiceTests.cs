using VyshKat.App.Models;
using VyshKat.App.Services;
namespace VyshKat.Tests;

public class FleetServiceTests
{
    [Fact]
    public void AddTransport_WhenInspectionPassed_AddsTransport()
    {
        var service = new FleetService(new FakeServiceCenter(true));
        var scooter = new Scooter("S001", 7, 2.5);

        bool result = service.AddTransport(scooter);

        Assert.True(result);
        Assert.Equal(1, service.GetTransportCount());
    }

    [Fact]
    public void AddTransport_WhenInspectionFailed_DoesNotAdd()
    {
        var service = new FleetService(new FakeServiceCenter(false));
        var scooter = new Scooter("S001", 7, 2.5);

        bool result = service.AddTransport(scooter);

        Assert.False(result);
        Assert.Equal(0, service.GetTransportCount());
    }

    [Fact]
    public void AddTransport_WithDuplicateNumber_DoesNotAdd()
    {
        var service = new FleetService(new FakeServiceCenter(true));

        service.AddTransport(new Scooter("S001", 7, 2.5));
        bool result = service.AddTransport(new Bicycle("S001", 8));

        Assert.False(result);
        Assert.Equal(1, service.GetItemCount());
    }

    [Fact]
    public void GetTotalEnergyConsumption_ReturnsCorrectSum()
    {
        var service = new FleetService(new FakeServiceCenter(true));

        service.AddTransport(new Scooter("S001", 7, 2.5));
        service.AddTransport(new ElectricBicycle("E001", 8, 1.5));
        service.AddTransport(new Bicycle("B001", 6));
        service.AddItem(new ChargingStation("C001", 3));
        service.AddItem(new Helmet("H001"));

        double result = service.GetTotalEnergyConsumption();

        Assert.Equal(7, result);
    }

    [Fact]
    public void GetTransportForBeginners_ReturnsOnlySuitableTransport()
    {
        var service = new FleetService(new FakeServiceCenter(true));

        service.AddTransport(new Scooter("S001", 5, 2));
        service.AddTransport(new Bicycle("B001", 6));
        service.AddTransport(new ElectricBicycle("E001", 10, 1.5));

        var result = service.GetTransportForBeginners();

        Assert.Equal(2, result.Count);
        Assert.DoesNotContain(result, x => x.InventoryNumber == "S001");
    }

    [Fact]
    public void GetReports_WhenFleetIsEmpty_ReturnsZero()
    {
        var service = new FleetService(new FakeServiceCenter(true));

        Assert.Equal(0, service.GetTransportCount());
        Assert.Equal(0, service.GetItemCount());
        Assert.Equal(0, service.GetTotalEnergyConsumption());
        Assert.Empty(service.GetTransportForBeginners());
    }

    [Fact]
    public void AddItem_WithDuplicateNumber_DoesNotAdd()
    {
        var service = new FleetService(new FakeServiceCenter(true));

        service.AddItem(new Helmet("H001"));
        bool result = service.AddItem(new ChargingStation("H001", 2));

        Assert.False(result);
        Assert.Equal(1, service.GetItemCount());
    }

    [Fact]
    public void AddTransport_WithNull_DoesNotAdd()
    {
        var service = new FleetService(new FakeServiceCenter(true));

        bool result = service.AddTransport(null!);

        Assert.False(result);
        Assert.Equal(0, service.GetTransportCount());
    }
}
