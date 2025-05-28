namespace APBD_CW5.DTOs;

public class PrescriptionPutDto
{
    public PatientPutDto Patient { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime DueTo { get; set; }
    public ICollection<MedicamentGetDto> Medicaments { get; set; } = null!;
    
    public string Details { get; set; } = null!;
    public int IdDoctor { get; set; }
    
}
