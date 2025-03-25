using APBD_CW2.Containers;
using APBD_CW2.Exceptions;

namespace APBD_CW2.Utils;

public class Menu
{
    
    private static List<ContainerShip> _ships = [];
    
    private static List<Container> _containers = [];
    

    public void Start()
    {
        bool exit = false;
        while (!exit)
        {
            Show();

            switch (ReadOption())
            {
                case null:
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                    
                case 1:
                    CreateShip();
                    Console.WriteLine("Statek utworzony");
                    break;
                    
                case 2:
                    DeleteShip();
                    break;
                
                case 3:
                    CreateContainer();
                    break;
                
                case 4:
                    DeleteContainer();
                    break;
                
                case 5:
                    LoadContainers();
                    break;
                
                case 0:
                    exit = true;
                    break;
            }
            
            
            DisplayPressAnyKey();
        }
    }

    private void Show()
    {
        string mainMenu = $"""
                           ==========================================================
                           Interfejs konsolowy dla zarządzania załadunkiem kontenerów

                           Lista kontenerowców:

                           {string.Join("\n", _ships)}

                           Lista kontenerów:

                           {string.Join("\n", _containers)}


                           Możliwe akcje:
                           [1] Dodaj kontenerowiec
                           [2] Usuń kontenerowiec
                           [3] Dodaj kontener
                           [4] Usuń kontener
                           [5] Załaduj kontenery
                           [0] Wyjdź
                           """;
        Console.Clear();
        Console.WriteLine(mainMenu);
    }

    private void DisplayPressAnyKey()
    {
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    
    private static int? ReadOption()
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
    
    private static void CreateShip()
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
        
        _ships.Add(new ContainerShip(maxSpeed, maxContainerCapacity, maxWeightOfAllContainers));
        
    }

    private static int ShowShipsCompressed()
    {
        Console.WriteLine("\nWybierz kontenerowiec:");
        for (var i = 0 ; i < _ships.Count; i++)
        {
            Console.WriteLine($"[{i+1}]\n{_ships[i].ToString()}");
        }

        Console.WriteLine();
        return int.Parse(Console.ReadLine());
    }

    private static int ShowContainersCompressed()
    {
        Console.WriteLine("\nWybierz kontener:");

        for (var i = 0 ; i < _containers.Count; i++)
        {
            Console.WriteLine($"[{i+1}] {_containers[i].SerialNumber}");
        }

        Console.WriteLine();
        return int.Parse(Console.ReadLine());
    }
    
    
    

    private static void DeleteShip()
    {
        if (_ships.Count == 0)
        {
            Console.WriteLine("Brak kontenerowców do usunięcia");
            return;
        }
        
        int input = ShowShipsCompressed();
        

        if (input > _ships.Count)
        {
            Console.WriteLine($"Kontenerowiec o numerze {input} nie istnieje");
            return;
        }
        
        var ship = _ships[input - 1];
        _ships.RemoveAt(input - 1);
        Console.WriteLine(ship.ToString());
        Console.WriteLine("Kontenerowiec usunięty");
    }

    private static void CreateContainer()
    {
        double height, weight, depth, maxLoad;
        char c;
        
        while (true)
        {
            try
            {
                Console.WriteLine("\nPodaj rodzaj kontenera [G, L, C]:");
                c = char.Parse(Console.ReadLine());
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Spróbuj ponownie");
            }
        }

        if (!Container.GetContainerTypes().Contains(c))
        {
            Console.WriteLine("Niepoprawny typ kontenera");
            return;
        }

        Console.WriteLine("Podaj wysokość");
        height = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj wagę własną");
        weight = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj głębokość");
        depth = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną ładowność");
        maxLoad = double.Parse(Console.ReadLine());

        switch (c)
        {
            case 'G':
                Console.WriteLine("Podaj ciśnienie");
                double pressure = double.Parse(Console.ReadLine());
                _containers.Add(new GasContainer(height, weight, depth, maxLoad, pressure));
                break;
            
            case 'L':
                Console.WriteLine("Podaj czy kontener przewozi ładunek niebezpieczny True/False");
                
                bool stop = true;
                
                while (stop) {
                    string input = Console.ReadLine();
                    

                    if (bool.TryParse(input, out bool result))
                    {
                        _containers.Add(new LiquidContainer(height, weight, depth, maxLoad, result));
                        stop = false;
                    }
                    else
                    {
                        Console.WriteLine("Niepoprawny input. Podaj True lub False");
                    }
                }
                break;
            
            case 'C':
                Console.WriteLine("Podaj rodzaj produktu");

                string? productType;
                stop = true;

                while (stop)
                {
                    productType = Console.ReadLine();
                    
                    if (productType == null)
                    {
                        Console.WriteLine("Niepoprawny input, podaj rodzaj produktu ponownie");
                    }
                    else
                    {
                        try
                        {
                            _containers.Add(new ColdContainer(height, weight, depth, maxLoad, productType));
                            stop = false;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Niepoprawny input, podaj rodzaj produktu ponownie");
                        }
                    }
                }
                
                break;
            
            default:
                Console.WriteLine("Niepoprawny rodzaj produktu");
                break;
        }

        Console.WriteLine("Kontener dodany");
        Console.WriteLine(_containers.Last().ToString());
    }

    private static void DeleteContainer()
    {
        if (_containers.Count == 0)
        {
            Console.WriteLine("Brak kontenerowców do usunięcia");
            return;
        }
        
        int input = ShowContainersCompressed();

        if (input > _containers.Count)
        {
            Console.WriteLine($"Kontener o numerze {input} nie istnieje");
            return;
        }
        
        var container = _containers[input - 1];
        _containers.RemoveAt(input - 1);
        Console.WriteLine(container.ToString());
        Console.WriteLine("Kontener usunięty");
    }

    private static void LoadContainers()
    {
        int inputShip = ShowShipsCompressed() - 1;

        // Nie używam ShowContainersCompressed(), bo chcemy zapytać użytkownika o listę
        
        Console.WriteLine("\nWybierz kontener(y):");

        for (var i = 0 ; i < _containers.Count; i++)
        {
            Console.WriteLine($"[{i+1}] {_containers[i].SerialNumber}");
        }

        Console.WriteLine();
        
        string[] input = Console.ReadLine().Split();
        
        int[] numbers = new int[input.Length];
        
        // Zamiana inputu na inty. -1 bo cyfry w inpucie są większe o 1 od indeksów
        for (int i = 0; i < input.Length; i++)
        {
            numbers[i] = int.Parse(input[i]) - 1;
        }

        List<Container> containersToBeAdded = [];

        foreach (int i in numbers)
        {
            containersToBeAdded.Add(_containers[i]);
        }

        try
        {
            
            List<Container> containersToBeDeleted = _ships[inputShip].AddContainers(containersToBeAdded);

            Console.WriteLine(_ships[inputShip].ContainersToString());

            foreach (var container in containersToBeDeleted)
            {
                _containers.Remove(container);
            }
            Console.WriteLine("Kontenery dodane poprawnie");

        }
        catch (ContainerOverflowException e)
        {
            Console.WriteLine(e.Message);
        }
        
        
        
        
        
    }
    
}