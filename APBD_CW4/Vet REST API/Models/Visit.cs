namespace Vet_REST_API.Models;

public class Visit
{
    public DateTime Date { get; set; }
    public Animal Animal { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

    public Visit(DateTime date, Animal animal, string description, double price)
    {
        this.Date = date;
        this.Animal = animal;
        this.Description = description;
        this.Price = price;
    }
    
}