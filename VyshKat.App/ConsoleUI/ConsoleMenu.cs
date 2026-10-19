using System.Globalization;
using VyshKat.App.Models;
using VyshKat.App.Services;
namespace VyshKat.App.ConsoleUI;

public class ConsoleMenu
{
    private readonly FleetService _fleet;
    public ConsoleMenu(FleetService fleet)
    {
        _fleet = fleet;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nВЫШКАТ");
            Console.WriteLine("1. Добавить транспорт");
            Console.WriteLine("2. Добавить имущество");
            Console.WriteLine("3. Отчет по парку");
            Console.WriteLine("4. Транспорт для новичков");
            Console.WriteLine("5. Список всего имущества");
            Console.WriteLine("0. Выход");

            int? choice = ReadInt("Выберите пункт: ", 0, 5);

            if (choice == null || choice == 0)
            {
                Console.WriteLine("До свидания!");
                return;
            }

            switch (choice)
            {
                case 1:
                    AddTransport();
                    break;
                case 2:
                    AddProperty();
                    break;
                case 3:
                    ShowReport();
                    break;
                case 4:
                    ShowBeginners();
                    break;
                case 5:
                    ShowAllItems();
                    break;
            }
        }
    }

    private void AddTransport()
    {
        Console.WriteLine("\nВыберите транспорт:");
        Console.WriteLine("1. Электросамокат");
        Console.WriteLine("2. Велосипед");
        Console.WriteLine("3. Электровелосипед");

        int? type = ReadInt("Тип: ", 1, 3);
        if (type == null)
            return;

        string? number = ReadText("Инвентарный номер: ");
        if (number == null)
            return;

        int? ease = ReadInt("Простота управления (1-10): ", 1, 10);
        if (ease == null)
            return;

        double energy = 0;

        if (type == 1 || type == 3)
        {
            double? value = ReadDouble("Расход энергии в квт/ч: ");
            if (value == null)
                return;

            energy = value.Value;
        }

        int? working = ReadInt(
            "транспорт исправен? (1 - да, 0 - нет): ", 0, 1);

        if (working == null)
            return;

        bool isWorking = working == 1;
        Transport transport;

        switch (type.Value)
        {
            case 1:
                transport = new Scooter(number, ease.Value, energy, isWorking);
                break;
            case 2:
                transport = new Bicycle(number, ease.Value, isWorking);
                break;
            default:
                transport = new ElectricBicycle(
                    number, ease.Value, energy, isWorking);
                break;
        }

        if (_fleet.AddTransport(transport))
            Console.WriteLine("Транспорт принят в парк");
        else
            Console.WriteLine("Транспорт не добавлен, проверьте номер или результат техосмотра.");
    }

    private void AddProperty()
    {
        Console.WriteLine("\nВыберите имущество:");
        Console.WriteLine("1. Шлем");
        Console.WriteLine("2. Зарядная станция");

        int? type = ReadInt("Тип: ", 1, 2);
        if (type == null)
            return;

        string? number = ReadText("Инвентарный номер: ");
        if (number == null)
            return;

        if (type == 1)
        {
            bool result = _fleet.AddItem(new Helmet(number));
            Console.WriteLine(result
                ? "Шлем добавлен"
                : "не удалось добавить шлем");
        }
        else
        {
            double? energy = ReadDouble("Расход энергии в квт/ч: ");
            if (energy == null)
                return;

            bool result = _fleet.AddItem(
                new ChargingStation(number, energy.Value));

            Console.WriteLine(result
                ? "Зарядная станция добавлена"
                : "Не удалось добавить зарядную станцию");
        }
    }

    private void ShowReport()
    {
        Console.WriteLine("\nОТЧЕТ");
        Console.WriteLine($"Транспорта: {_fleet.GetTransportCount()}");
        Console.WriteLine($"Всего объектов: {_fleet.GetItemCount()}");
        Console.WriteLine($"Суточное энергопотребление: {_fleet.GetTotalEnergyConsumption():F2} квт/ч");
    }

    private void ShowBeginners()
    {
        var transports = _fleet.GetTransportForBeginners();

        Console.WriteLine("\nДЛЯ НОВИЧКОВ");

        if (transports.Count == 0)
        {
            Console.WriteLine("Подходящего транспорта нет");
            return;
        }

        foreach (var transport in transports)
        {
            Console.WriteLine($"{transport.Name} | {transport.InventoryNumber} | Простота: {transport.EaseOfUse}");
        }
    }

    private void ShowAllItems()
    {
        var items = _fleet.GetAllItems();
        Console.WriteLine("\nИМУЩЕСТВО НА БАЛАНСЕ");

        if (items.Count == 0)
        {
            Console.WriteLine("Парк пока пуст.");
            return;
        }
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name} | {item.InventoryNumber}");
        }
    }

    private static string? ReadText(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (input == null)
                return null;

            input = input.Trim();

            if (input.Length > 0 && input.Length <= 100)
                return input;

            Console.WriteLine("Введите от 1 до 100 символов");
        }
    }

    private static int? ReadInt(string message, int min, int max)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (input == null)
                return null;

            if (int.TryParse(input, out int value) &&
                value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"введите целое число от {min} до {max}");
        }
    }

    private static double? ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (input == null)
                return null;

            bool parsed = double.TryParse(
                input,
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out double value);

            if (parsed &&
                !double.IsNaN(value) &&
                !double.IsInfinity(value) &&
                value >= 0 &&
                value <= 1000000)
            {
                return value;
            }
            Console.WriteLine("Введите расход от 0 до 1000000 квт/ч");
        }
    }
}
