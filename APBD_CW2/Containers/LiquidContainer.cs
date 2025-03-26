using APBD_CW2.Interfaces;

namespace APBD_CW2.Containers;

public class LiquidContainer: Container, IHazardNotifier
{

    public LiquidContainer(double height, double weight, double depth, double maxLoad, bool isLoadHazardous) : base(height, weight, depth, maxLoad, 'L')
    {
        IsLoadHazardous = isLoadHazardous;
        MaxLoad = CalculateMaxAllowedLoad(CalculateMaxLoadModifier());

        // (height, weight, depth, maxLoad, 'L')
    }
    public bool IsLoadHazardous { get; }

    public void SendTextNotification()
    {
        Console.WriteLine($"A hazardous situation has happened! Container: {SerialNumber}");
    }

    public override void Load(double loadMass)
    {
        
        if (MaxLoad - loadMass < 0)
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

    public override string ToString()
    {
        return base.ToString() + $"  is load hazardous: {IsLoadHazardous}]";
    }
}