using APBD_CW2.Containers;
using APBD_CW2.Exceptions;

namespace APBD_CW2.Utils;

public class Menu
{
    
    private static List<ContainerShip> _ships = [];
    
    private static List<Container> _containers = [];
    

    public void Start()
    {
        LoadTestData();
        
        bool exit = false;
        while (!exit)
        {
            
            ShowMainMenuOptions();

            switch (ReadOption())
            {
                case null:
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;
                    
                case 1:
                    CreateShip();
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
                    LoadShip();
                    break;
                
                case 6:
                    RemoveContainer();
                    break;
                
                case 7:
                    UnloadShip();
                    break;
                
                case 8:
                    ReplaceContainers();
                    break;
                
                case 9:
                    MoveContainers();
                    break;
                
                case 10:
                    LoadContainer();
                    break;
                
                case 11:
                    UnloadContainer();
                    break;
                
                case 0:
                    exit = true;
                    break;
            }
            
            
            DisplayPressAnyKey();
        }
    }

    private void LoadTestData()
    {
        _ships.AddRange([
                new ContainerShip(10, 10, 10000),
                new ContainerShip(10, 3, 10000)
            ]);
        
        _containers.AddRange([
            new GasContainer(10, 10, 10, 1000, 1),
            new GasContainer(10, 10, 10, 2000, 2),
            new LiquidContainer(10, 10, 10, 1000, true),
            new LiquidContainer(10, 10, 10, 1000, false),
            new ColdContainer(10, 10, 10, 1000, "Frozen Pizza"),
            new ColdContainer(10, 10, 10, 1000, "Butter")
            ]);
    }
    private void ShowMainMenuOptions()
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
                           [5] Załaduj kontenery na statek
                           [6] Usuń kontener ze statku
                           [7] Rozładuj kontenerowiec
                           [8] Zastąp kontener na statku z innym kontenerem
                           [9] Przenieś kontener między dwoma statkami
                           [10] Załaduj kontener towarem
                           [11] Rozładuj kontener
                           [0] Wyjdź
                           """;
        Console.Clear();
        Console.WriteLine(mainMenu);
    }

    private void DisplayPressAnyKey()
    {
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
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
        
        Console.WriteLine("Statek utworzony");
        
    }
    
    private static int DisplayCompactShipList()
    // Returns index of ship chosen by user
    {
        Console.WriteLine("\nWybierz kontenerowiec:");
        for (var i = 0 ; i < _ships.Count; i++)
        {
            Console.WriteLine($"[{i+1}]\n{_ships[i].ToString()}");
        }

        Console.WriteLine();
        return int.Parse(Console.ReadLine()) - 1;
    }

    private static int DisplayCompactContainerList()
    // Returns index of container chosen from _containers
    {
        Console.WriteLine("\nWybierz kontener:");

        for (var i = 0 ; i < _containers.Count; i++)
        {
            Console.WriteLine($"[{i+1}] {_containers[i].SerialNumber}");
        }

        Console.WriteLine();
        return int.Parse(Console.ReadLine()) - 1;
    }
    
    
    

    private static void DeleteShip()
    {
        if (_ships.Count == 0)
        {
            Console.WriteLine("Brak kontenerowców do usunięcia");
            return;
        }
        
        int input = DisplayCompactShipList();
        

        if (input > _ships.Count - 1)
        {
            Console.WriteLine($"Kontenerowiec o numerze {input} nie istnieje");
            return;
        }
        
        var ship = _ships[input];
        _ships.RemoveAt(input);
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
        Console.WriteLine("Dla kontenera typu 'L' zostanie ona przeskalowana zgodnie z wytycznymi ładunku niebezpiecznego");
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
            Console.WriteLine("Brak kontenerów do usunięcia");
            return;
        }
        
        int input = DisplayCompactContainerList();

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

    private static void LoadShip()
    {
        int inputShip = DisplayCompactShipList();

        // Nie używamy ShowContainersCompressed(), bo chcemy zapytać użytkownika o listę
        
        Console.WriteLine("\nWybierz kontener(y):");

        for (var i = 0 ; i < _containers.Count; i++)
        {
            Console.WriteLine($"[{i+1}] {_containers[i].SerialNumber}");
        }

        Console.WriteLine();
        
        string[] input = Console.ReadLine().Split();
        
        
        int[] numbers = new int[input.Length];
        
        // Zamiana na inty -1, bo cyfry w inpucie są większe o 1 od indeksów
        for (int i = 0; i < input.Length; i++)
        {
            numbers[i] = int.Parse(input[i]) - 1;
        }

        List<Container> containersToBeAdded = [];

        try
        {
            foreach (int i in numbers)
            {
                containersToBeAdded.Add(_containers[i]);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Jeden z podanych numerów jest nieprawidłowy");
            return;
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
        // Tutaj łapiemy sytuację, gdyby użytkownik wybrał statek, który nie istnieje
        catch (Exception e)
        {
            Console.WriteLine("Niepoprawny numer statku");
        }
        
        
    }

    private void RemoveContainer()
    {
        int inputShip = DisplayCompactShipList();

        Console.WriteLine("Kontenerowiec");
        Console.WriteLine(_ships[inputShip].ContainersToString());
        Console.WriteLine("Podaj nazwę kontenera do usunięcia");
        
        string? serialNumber = Console.ReadLine();

        if (IsSerialNumberNull(serialNumber)) return;

        try
        {
            var removedContainer = _ships[inputShip].RemoveContainer(serialNumber);
            Console.WriteLine("Kontener usunięty ze statku");

            _containers.Add(removedContainer);

        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
            
        }

        Console.WriteLine(_ships[inputShip].ContainersToString());

        
    }

    private void UnloadShip()
    {
        int inputShip = DisplayCompactShipList();
        
        var containers = _ships[inputShip].Unload();
        
        _containers.AddRange(containers);

        Console.WriteLine("Kontenerowiec wyładowany");
        
    }

    private void ReplaceContainers()
    {
        int inputContainer = DisplayCompactContainerList();
        
        var with = _containers[inputContainer];
        
        
        int inputShip = DisplayCompactShipList();

        Console.WriteLine("Kontenerowiec");
        Console.WriteLine(_ships[inputShip].ContainersToString());
        Console.WriteLine("\nPodaj nazwę kontenera do usunięcia");
        
        string? replaceSerialNumber = Console.ReadLine();

        if (IsSerialNumberNull(replaceSerialNumber)) return;
        
        
        _ships[inputShip].ReplaceContainers(replaceSerialNumber, with);

        Console.WriteLine(_ships[inputShip].ContainersToString());

        Console.WriteLine("Kontenery zastąpione");


    }

    private void MoveContainers()
    {
        int inputFirstShip = DisplayCompactShipList();

        Console.WriteLine("Kontenerowiec");
        Console.WriteLine(_ships[inputFirstShip].ContainersToString());
        Console.WriteLine("\nPodaj nazwę kontenera do przeniesienia");
        
        string? serialNumber = Console.ReadLine();

        if (IsSerialNumberNull(serialNumber)) return;

        Console.WriteLine($"Wybierz kontenerowiec na który kontener {serialNumber} na zostać przeniesiony");
        
        int inputSecondShip = DisplayCompactShipList();

        try
        {
            _ships[inputFirstShip].MoveContainer(serialNumber, _ships[inputSecondShip]);
            Console.WriteLine("Kontener przeniesiony");
        }
        catch (ContainerOverflowException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine($"Failed to move {serialNumber}");
        }
        
    }
    
    private void LoadContainer()
    {
        int inputContainer = DisplayCompactContainerList();

        Console.WriteLine("Podaj masę ładunku");
        
        int loadMass = int.Parse(Console.ReadLine());

        try
        {
            _containers[inputContainer].Load(loadMass);
            Console.WriteLine($"Kontener {_containers[inputContainer].SerialNumber} załadowany");
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e.Message);
        }
        
        
    }

    private bool IsSerialNumberNull(string? sN)
    {
        if (sN == null)
        {
            Console.WriteLine("Nieprawidłowy input");
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void UnloadContainer()
    {
        int inputContainer = DisplayCompactContainerList();
        
        _containers[inputContainer].Empty();

        Console.WriteLine($"Kontener {_containers[inputContainer].SerialNumber} rozładowany");
    }
    
}