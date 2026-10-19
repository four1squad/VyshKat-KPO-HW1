using VyshKat.App.Interfaces;
using VyshKat.App.Models;
namespace VyshKat.App.Services;

public class FleetService
{
    private readonly IServiceCenter _serviceCenter;
    private readonly List<IInventoryItem> _items = new();

    public FleetService(IServiceCenter serviceCenter)
    {
        _serviceCenter = serviceCenter;
    }

    public bool AddTransport(Transport transport)
    {
        if (transport == null)
            return false;

        if (HasInventoryNumber(transport.InventoryNumber))
            return false;

        if (!_serviceCenter.CheckTransport(transport))
            return false;

        _items.Add(transport);
        return true;
    }

    public bool AddItem(IInventoryItem item)
    {
        if (item == null || item is Transport)
            return false;

        if (string.IsNullOrWhiteSpace(item.InventoryNumber))
            return false;

        if (HasInventoryNumber(item.InventoryNumber))
            return false;

        _items.Add(item);
        return true;
    }

    private bool HasInventoryNumber(string inventoryNumber)
    {
        return _items.Any(item =>
            string.Equals(item.InventoryNumber, inventoryNumber, StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlyList<IInventoryItem> GetAllItems()
    {
        return _items.AsReadOnly();
    }

    public int GetTransportCount()
    {
        return _items.OfType<Transport>().Count();
    }

    public int GetItemCount()
    {
        return _items.Count;
    }

    public double GetTotalEnergyConsumption()
    {
        return _items.OfType<IEnergyConsumer>()
            .Sum(item => item.DailyEnergyConsumption);
    }

    public List<Transport> GetTransportForBeginners()
    {
        return _items.OfType<Transport>()
            .Where(transport => transport.EaseOfUse >= 6)
            .ToList();
    }
}
