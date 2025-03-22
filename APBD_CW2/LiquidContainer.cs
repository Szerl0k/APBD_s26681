namespace APBD_CW2;

public class LiquidContainer : Container, IHazardNotifier
{
    
    public bool IsLoadHazardous { get; set; }
    
    public LiquidContainer(double cargoMass, double height, double weight, double depth, double maxLoad, bool isLoadHazardous) : base(cargoMass, height, weight, depth, maxLoad, 'L')
    {
        IsLoadHazardous = isLoadHazardous;
    }
    
    public void SendTextNotification()
    {
        Console.WriteLine($"A hazardous situation has happened! Container: {SerialNumber}");
    }

    public override void Load(double loadMass)
    {
        
        
        double allowedMaxLoad = CalculateMaxAllowedLoad(CalculateMaxLoadModifier());
        
        if (allowedMaxLoad - loadMass < 0)
        {
            SendTextNotification();
        } 
        else
        {
            CargoMass = loadMass;
        }
        
    }
    
    private double CalculateMaxLoadModifier()
    {
        return IsLoadHazardous ? 0.5 : 0.9;
    }

    private double CalculateMaxAllowedLoad(double maxLoadModifier)
    {
        return Math.Round(MaxLoad * maxLoadModifier, 2);
    }
    
}