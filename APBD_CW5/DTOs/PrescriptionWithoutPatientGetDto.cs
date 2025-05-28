namespace APBD_CW5.DTOs;

public class PrescriptionWithoutPatientGetDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentGetDto> Medicaments { get; set; } = null!;
    public DoctorGetDto Doctor { get; set; } = null!;
    
}