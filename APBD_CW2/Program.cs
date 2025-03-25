using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using APBD_CW2.Containers;
using APBD_CW2.Exceptions;

namespace APBD_CW2;

class Program
{
    static void Main(string[] args)
    {

        var liquid = new LiquidContainer(50.0, 5.0, 5.0, 10.0, 200.0, false);

        var gas = new GasContainer(600.0, 5.0, 5.0, 10.0, 200.0, 4.0);

        var cold = new ColdContainer(700.0, 5.0, 5.0, 10.0, 200.0, "Frozen Pizza");
        
        var cold2 = new ColdContainer(120.0, 5.0, 5.0, 10.0, 200.0, "Chocolate");

        var ship = new ContainerShip(12.0, 5, 1000.0);

        var ship2 = new ContainerShip(14.0, 10, 1200.0);

        try
        {
            ship.AddContainer(cold2);
        }
        catch (ContainerOverflowException e)
        {
            Console.WriteLine(e.Message);
        }
        
        List<ContainerShip> ships = new List<ContainerShip>();
        
        List<Container> containers = new List<Container>();
        
        containers.Add(cold);
        containers.Add(gas);
        containers.Add(liquid);

        ships.Add(ship);
        ships.Add(ship2);
        

        int? option;

        while (true)
        {

            DisplayMainMenu(ships, containers);

            switch (ReadAnOption())
            {
                case null:
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                    
                case 1:
                    ships.Add(ConsoleCreateShip());
                    Console.WriteLine("Statek utworzony");
                    break;
                    
                        
            }
            
            
            DisplayPressAnyKey();
        }

    }

    static void DisplayMainMenu(List<ContainerShip> ships, List<Container> containers)
    {
        string mainMenu = $"""
                           ==========================================================
                           Interfejs konsolowy dla zarządzania załadunkiem kontenerów

                           Lista kontenerowców:

                           {string.Join("\n", ships)}

                           Lista kontenerów:

                           {string.Join("\n", containers)}


                           Możliwe akcje:
                           1. Dodaj kontenerowiec
                           2. Usuń kontenerowiec
                           3. Dodaj kontener
                           4. Usuń kontener
                           """;
        Console.Clear();
        Console.WriteLine(mainMenu);
    }

    static void DisplayPressAnyKey()
    {
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    static int? ReadAnOption()
    {
        try
        {
            int option = int.Parse(Console.ReadLine());

            if (option < 0)
            {
                throw new FormatException();
            }
            
            return option;

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message + " Use integer number from 0 to 100");
        }

        return null;
    }

    static ContainerShip ConsoleCreateShip()
    {
        double maxSpeed, maxWeightOfAllContainers;
        int maxContainerCapacity;
        
        Console.WriteLine("\nPodaj dane dla nowego kontenerowca:");
        Console.WriteLine("Maksymalna prędkość statku:");
        maxSpeed = double.Parse(Console.ReadLine());

        Console.WriteLine("Maksymalna liczba kontenerów, które mogą być przewożone");
        maxContainerCapacity = int.Parse(Console.ReadLine());

        Console.WriteLine("Maksymalna waga wszystkich kontenerów");
        maxWeightOfAllContainers = double.Parse(Console.ReadLine());
        
        return new ContainerShip(maxSpeed, maxContainerCapacity, maxWeightOfAllContainers);
        
    }
    
}