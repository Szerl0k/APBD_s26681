using APBD_CW2.Interfaces;

namespace APBD_CW2.Containers;

public class GasContainer : Container, IHazardNotifier
{
    
    public double Pressure { get; set; }
    
    public GasContainer(double height, double weight, double depth, double maxLoad, double pressure) : base(height, weight, depth, maxLoad, 'G')
    {
        Pressure = pressure;
        Empty();
    }

    public override void Empty()
    {
        // Zakładam, że "pozostawiamy 5% jego ładunku" oznacza 5% maksymalnie dozwolonej masy
        CargoMass = Math.Round(MaxLoad * 0.05, 2);
    }
    
    public void SendTextNotification()
    {
        Console.WriteLine($"A hazardous situation has happened! Container: {SerialNumber}");
    }

    public override string ToString()
    {
        return base.ToString() + $"  pressure: {Pressure}]";
    }
}