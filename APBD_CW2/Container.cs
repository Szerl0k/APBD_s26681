using System.Collections.Frozen;

namespace APBD_CW2;

public class Container
{
    // It will be increased to 1 in first call of GenerateSerialNumber()
    private static int _serialNumberCounter = 0;
    private static HashSet<string> _serialNumbers = [];
    
    private static HashSet<char> _containerTypes = ['L', 'G', 'C'];
    public double CargoMass { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public double Depth { get; set; }
    public string SerialNumber { get; private set; }
    
    public double MaxLoad { get; set; }

    public Container(double cargoMass, double height, double weight, double depth, double maxLoad, char type)
    {

        if (!_containerTypes.Contains(type))
        {
            throw new ArgumentException($"Invalid container type: {type}");
        }
        
        CargoMass = cargoMass;
        Height = height;
        Weight = weight;
        Depth = depth;
        MaxLoad = maxLoad;

        SerialNumber = GenerateSerialNumber(type);

    }

    public virtual void Empty()
    {
        CargoMass = 0.0;
    }

    public virtual void Load(double loadMass)
    {
        if (loadMass > MaxLoad)
        {
            throw new OverfillException($"Load mass exceeds MaxLoad: {MaxLoad}");
        }
        
        CargoMass = loadMass;
        
    }


    protected string GenerateSerialNumber(char type)
    {
        var newSerialNumber = "";

        do
        {
            newSerialNumber = $"KON-{type}-{++_serialNumberCounter}";
        } while (_serialNumbers.Contains(newSerialNumber));
        
        _serialNumbers.Add(newSerialNumber);
        return newSerialNumber;
    }

    public override string ToString()
    {
        return $"Container: {SerialNumber}, height: {Height}, weight: {Weight}, depth : {Depth}, max load: {MaxLoad}";
    }
    
    
    
}
