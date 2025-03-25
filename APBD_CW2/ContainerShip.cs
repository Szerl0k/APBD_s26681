using APBD_CW2.Containers;
using APBD_CW2.Exceptions;

namespace APBD_CW2;

public class ContainerShip(double maxSpeed, int maxContainerCapacity, double maxWeightOfAllContainers)
{
    private List<Container> _containers = [];
    
    public double MaxSpeed { get; set; } = maxSpeed;

    public int MaxContainerCapacity { get; set; } = maxContainerCapacity;

    public double MaxTotalWeightOfContainers { get; set; } = maxWeightOfAllContainers;

    private double TotalWeight
    {
        get
        {
            double totalWeight = 0;
            foreach (var container in _containers)
            {
                totalWeight += container.OverallMass;
            }
            return Math.Round(totalWeight, 2);
        }
    }


    public void AddContainer(Container container)
    {

        if (_containers.Count >= MaxContainerCapacity)
        {
            throw new ContainerOverflowException($"Can't add container {container.SerialNumber}. Current container capacity {_containers.Count}, max capacity {MaxContainerCapacity}");
        }
        
        if (Math.Round(TotalWeight + container.OverallMass, 2) > MaxTotalWeightOfContainers)
        {
            throw new ContainerOverflowException($"Can't add  container {container.SerialNumber}, because it's overall mass would exceed the max weight {MaxTotalWeightOfContainers}");
        }
        _containers.Add(container);
        
        
    }

    public void AddContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            try
            {
                AddContainer(container);
            }
            catch (ContainerOverflowException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public void RemoveContainer(Container container)
    {
        _containers.Remove(container);
    }

    public List<Container> Unload()
    {
        var tmpContainers = new List<Container>(_containers);
        _containers.Clear();
        return tmpContainers;
    }
    
    
    public void ReplaceContainers(Container replace, Container with)
    {

        int? index = FindContainerIndex(replace.SerialNumber);
        
        if (index == null)
        {
            Console.WriteLine($"Container {replace.SerialNumber} not found");
        }
        else
        {
            _containers[index.Value] = with;
        }
        
    }

    public void MoveContainer(Container container, ContainerShip secondShip)
    {
        int? index = FindContainerIndex(container.SerialNumber);
        
        if (index == null)
        {
            Console.WriteLine($"Container {container.SerialNumber} not found");
        }

        try
        {
            secondShip.AddContainer(container);
            _containers.RemoveAt(index.Value);
        }
        catch (ContainerOverflowException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine($"Failed to move {container.SerialNumber}");
        }
        
        
        
    }
    

    public override string ToString()
    {
        return $"""
                Container ship: [{ContainersToString()}] 
                Containers: {_containers.Count}
                Total weight : {TotalWeight}
                Max total weight of containers : {MaxTotalWeightOfContainers}
                Max capacity : {MaxContainerCapacity}
                
                """;
    }
    
    
    private int? FindContainerIndex(string serialNumber)
    {
        for (var i = 0; i < _containers.Count; i++)
        {
            if (_containers[i].SerialNumber == serialNumber)
            {
                return i;
            }
        }

        return null;
    }

    private string ContainersToString()
    {
        string result = "";
        foreach (var container in _containers)
        {
            result += container.SerialNumber + ", ";
        }

        if (_containers.Count != 0)
        {
            result = result.Remove(result.Length - 2, 2);
        }
        
        return result;
    }
    
}