using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_CW5.Models;

[Table(nameof(Medicament))]
public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }

    [Required] 
    [MaxLength(100)] 
    public string Name { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = null!;

    [Required] 
    [MaxLength(100)] 
    public string Type { get; set; } = null!;
    
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;
}