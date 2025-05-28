namespace APBD_CW5.DTOs;

public class MedicamentGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public int? Dose { get; set; }
    
}