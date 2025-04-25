namespace Vet_REST_API.Models;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }
    public float Weight { get; set; }
    public string FurColour { get; set; }

    public Animal(int id, string name, string breed, float weight, string furColour)
    {
        Id = id;
        Name = name;
        Breed = breed;
        Weight = weight;
        FurColour = furColour;
        
    }
    
}