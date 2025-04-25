using Microsoft.AspNetCore.Mvc;
using Vet_REST_API.Models;

namespace Vet_REST_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetAnimals([FromQuery] string? name)
    {
        var animals = Database.GetAnimals();

        if (name != null)
        {
            animals = animals.Where(a => a.Name.Equals(name)).ToList();
        }
        
        return Ok(animals);
    }

    [HttpGet]
    [Route("{animalId:int}")]
    public IActionResult GetAnimal([FromRoute] int animalId)
    {
        var animal = GetAnimalFromDB(animalId);

        if (animal == null) return NotFound(AnimalCannotBeFoundMsg(animalId));
        
        return Ok(animal);
        
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] Animal animal)
    {
        animal.Id = Database.GetAnimals().Count + 1;
        Database.AddAnimal(animal);
        
        return Created($"/Animals/{animal.Id}", animal);
    }

    [HttpPut]
    [Route("{animalId:int}")]
    public IActionResult UpdateAnimal([FromRoute] int animalId,  [FromBody] Animal animal)
    {
        var animalToUpdate = GetAnimalFromDB(animalId);
        if (animalToUpdate == null) return NotFound(AnimalCannotBeFoundMsg(animalId));
        
        animalToUpdate.Name = animal.Name;
        animalToUpdate.Breed = animal.Breed;
        animalToUpdate.FurColour = animal.FurColour;
        animalToUpdate.Weight = animal.Weight;

        return Ok(animalToUpdate);

    }
    
    
    [HttpDelete]
    [Route("{animalId:int}")]
    public IActionResult DeleteAnimal([FromRoute] int animalId)
    {
        var animal = GetAnimalFromDB(animalId);
        if (animal == null) return NotFound(AnimalCannotBeFoundMsg(animalId));
        
        Database.RemoveAnimal(animal);
        
        return NoContent();
    }



    [HttpGet]
    [Route("visits/{animalId:int}")]
    public IActionResult GetAnimalVisits([FromRoute] int animalId)
    {
        var animal = GetAnimalFromDB(animalId);
        
        if (animal == null) return NotFound(AnimalCannotBeFoundMsg(animalId));

        var visits = Database.GetVisits().Where(v => v.Animal.Id == animalId).ToList();
        
        return Ok(visits);
    }


    [HttpPost]
    [Route("visits")]
    public IActionResult CreateVisit([FromBody] Visit visit)
    {
        Database.AddVisit(visit);
        
        return Created($"/Animals/{visit.Animal.Id}", visit);
    }
    


    private Animal? GetAnimalFromDB(int id)
    {
        return Database.GetAnimals().FirstOrDefault(x => x.Id == id);
    }

    private string AnimalCannotBeFoundMsg(int id)
    {
        return $"Animal with id {id} cannot be found";
    }
    
}