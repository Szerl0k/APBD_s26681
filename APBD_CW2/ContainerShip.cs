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


    private void AddContainer(Container container)
    {

        if (_containers.Count >= MaxContainerCapacity)
        {
            throw new ContainerOverflowException($"Can't add container {container.SerialNumber}. Current container capacity {_containers.Count}, max capacity {MaxContainerCapacity}");
        }
        
        if (Math.Round(TotalWeight + container.OverallMass, 2) > MaxTotalWeightOfContainers)
        {
            throw new ContainerOverflowException($"Can't add container {container.SerialNumber}, because it's overall mass would exceed the max weight {MaxTotalWeightOfContainers}");
        }
        _containers.Add(container);
        
        
    }

    public List<Container> AddContainers(List<Container> containers)
    {
        var containersToBeDeleted = new List<Container>();
        foreach (var container in containers)
        {
            try
            {
                AddContainer(container);
                containersToBeDeleted.Add(container);
            }
            catch (ContainerOverflowException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return containersToBeDeleted;
    }

    public Container RemoveContainer(string serialNumber)
    {
        var container = _containers.Find(c => c.SerialNumber == serialNumber);

        if (container == null)
        {
            throw new ArgumentException($"Kontener o numerze seryjnym {serialNumber} nie istnieje w tym kontenerowcu");
        }
        
        _containers.Remove(container);

        return container;

    }

    public List<Container> Unload()
    {
        var tmpContainers = new List<Container>(_containers);
        _containers.Clear();
        return tmpContainers;
    }
    
    
    public void ReplaceContainers(string replaceSerialNumber, Container with)
    {

        int? index = FindContainerIndex(replaceSerialNumber);
        
        if (index == null)
        {
            Console.WriteLine($"Container {replaceSerialNumber} not found");
        }
        else
        {
            _containers[index.Value] = with;
        }
        
    }

    public void MoveContainer(string serialNumber, ContainerShip secondShip)
    {
        int? index = FindContainerIndex(serialNumber);
        
        if (index == null)
        {
            Console.WriteLine($"Container {serialNumber} not found");
        }

        try
        {
            secondShip.AddContainer(_containers[index.Value]);
            _containers.RemoveAt(index.Value);
        }
        catch (ContainerOverflowException e)
        {
            // Obsługa przeniesiona do miejsca wywołania MoveContainer()
            throw new ContainerOverflowException(e.Message);
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
    
    public string ContainersToString()
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
    
    
}