using APBD_CW5.Models;

namespace APBD_CW5.DTOs;

public class PrescriptionGetDto
{
    public int IdPrescription { get; set; }
    public PatientGetDto Patient { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime DueTo { get; set; }
    public ICollection<MedicamentGetDto> Medicaments { get; set; } = null!;
    public DoctorGetDto Doctor { get; set; } = null!;
}

