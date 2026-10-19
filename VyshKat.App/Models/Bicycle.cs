namespace VyshKat.App.Models;

public class Bicycle : Transport {
    
    public Bicycle(string inventoryNumber, int easeOfUse, bool isWorking = true)
        : base(inventoryNumber, "Велосипед", easeOfUse, isWorking)
    {
    }

}
