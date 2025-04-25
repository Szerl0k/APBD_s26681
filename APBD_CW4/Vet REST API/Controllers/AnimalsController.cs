using Microsoft.AspNetCore.Mvc;
using Vet_REST_API.Models;

namespace Vet_REST_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetAllAnimals()
    {
        return Ok(Database.GetAnimals());
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



    [HttpGet]
    [Route("visits/{animalId:int}")]
    public IActionResult GetAnimalVisits([FromRoute] int animalId)
    {
        var animal = GetAnimalFromDB(animalId);
        
        if (animal == null) return NotFound(AnimalCannotBeFoundMsg(animalId));

        var visits = Database.GetVisits().Where(v => v.Animal.Id == animalId).ToList();
        
        return Ok(visits);
    }


    private Animal? GetAnimalFromDB(int id)
    {
        return Database.GetAnimals().FirstOrDefault(x => x.Id == id);
    }

    private string AnimalCannotBeFoundMsg(int id)
    {
        return $"$Animal with id {id} cannot be found";
    }
    
}