using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW5.Models;

[Table("Prescription")]
[PrimaryKey(nameof(IdPrescription))]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    [Column("IdDoctor")]
    [Required]
    public int IdDoctor { get; set; }

    [Column("IdPatient")]
    [Required]
    public int IdPatient { get; set; }

    [ForeignKey(nameof(IdDoctor))] 
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey(nameof(IdPatient))] 
    public virtual Patient Patient { get; set; } = null!;
}
