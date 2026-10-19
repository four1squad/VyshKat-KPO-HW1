using VyshKat.App.Models;
using VyshKat.App.Services;
namespace VyshKat.Tests;

public class ServiceCenterTests
{
    [Fact]
    public void CheckTransport_WhenWorking_ReturnsTrue()
    {
        var center = new ServiceCenter();
        var scooter = new Scooter("S001", 7, 2.5, true);

        Assert.True(center.CheckTransport(scooter));
    }

    [Fact]
    public void CheckTransport_WhenBroken_ReturnsFalse()
    {
        var center = new ServiceCenter();
        var scooter = new Scooter("S001", 7, 2.5, false);

        Assert.False(center.CheckTransport(scooter));
    }

    [Fact]
    public void AddTransport_WhenBroken_DoesNotAdd()
    {
        var fleet = new FleetService(new ServiceCenter());
        var bicycle = new Bicycle("B001", 8, false);

        bool result = fleet.AddTransport(bicycle);

        Assert.False(result);
        Assert.Equal(0, fleet.GetTransportCount());
    }
}
