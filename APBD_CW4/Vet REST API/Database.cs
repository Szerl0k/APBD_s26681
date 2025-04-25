using Vet_REST_API.Models;

namespace Vet_REST_API;

public static class Database
{
    private static List<Animal> _animals =
    [
        new(0, "Psiak", "Shiba", 10.5f, "Ginger"),
        new(1, "Kitku", "Maine Coon", 5.0f, "Grey"),
        new(2, "AAA", "Arctic Fox", 2.3f, "White")
    ];

    private static DateTime defaultDate
    {
        get
        {
            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
        }
    }

    private static List<Visit> _visits = 
    [
        new Visit(defaultDate, _animals[0], "?", 120.0),
        new Visit(defaultDate.AddYears(1).AddDays(12), _animals[1], "?", 111.1),
        new Visit(defaultDate.AddMonths(-2), _animals[2], "?", 122.2),
    ];
    
    public static List<Animal> GetAnimals() => new List<Animal>(_animals);

    public static void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }
    public static void RemoveAnimal(Animal animal) => _animals.Remove(animal);
    
    public static List<Visit> GetVisits() => new List<Visit>(_visits);
    public static void AddVisit(Visit visit) => _visits.Add(visit);
    public static void RemoveVisit(Visit visit) => _visits.Remove(visit);
}