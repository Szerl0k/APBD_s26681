using APBD_CW5.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW5.DAL;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var doctors = new List<Doctor>
        {
            new()
            {
                IdDoctor = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@gmail.com",
            },
            new()
            {
                IdDoctor = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@gmail.com",
            }
        };

        var patients = new List<Patient>
        {
            new()
            {
                IdPatient = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                BirthDate = new DateTime(1980, 1, 1),
            },
            new()
            {
                IdPatient = 2,
                FirstName = "Elżbieta",
                LastName = "Windsor",
                BirthDate = new DateTime(1926, 4, 21),
            },
            new ()
            {
                IdPatient = 3,
                FirstName = "Maciej",
                LastName = "M",
            }
        };
        
        var prescriptions = new List<Prescription>
        {
            new()
            {
                IdPrescription = 1,
                Date = new DateTime(2025, 10, 10),
                DueDate = new DateTime(2025, 11, 10),
                IdPatient = 1,
                IdDoctor = 1,
            },
            new ()
            {
                IdPrescription = 2,
                Date = new DateTime(2025, 10, 11),
                DueDate = new DateTime(2025, 11, 12),
                IdPatient = 2,
                IdDoctor = 1,
            },
            new ()
            {
                IdPrescription = 3,
                Date = new DateTime(2025, 11, 13),
                DueDate = new DateTime(2025, 11, 13),
                IdPatient = 3,
                IdDoctor = 2,
            }
        };

        var medicaments = new List<Medicament>
        {
            new()
            {
                IdMedicament = 1,
                Name = "Paracetamol",
                Description = "Typical painkiller",
                Type = "Painkiller",
            },
            new()
            {
                IdMedicament = 2,
                Name = "Naproxen",
                Description = "Reduces inflammation and pain",
                Type = "Anti-inflammatory",
            },
            new()
            {
                IdMedicament = 3,
                Name = "Amoxicillin",
                Description = "Broad spectrum antibiotic",
                Type = "Antibiotic",
            },
            new()
            {
                IdMedicament = 4,
                Name = "Metformin",
                Description = "Lowers blood glucose",
                Type = "Antidiabetic",
            },
            new()
            {
                IdMedicament = 5,
                Name = "Aspirin",
                Description = "aaaa",
                Type = "Painkiller"
            },
            new()
            {
                IdMedicament = 6,
                Name = "Some drug",
                Description = "Some drug that does something",
                Type = "Painkiller"
            },
            new()
            {
                IdMedicament = 7,
                Name = "aaaaaaaaaaaaa",
                Description = "Does some \'a\'",
                Type = "a blocker",
            },
            new()
            {
                IdMedicament = 8,
                Name = "bbbbbbbb",
                Description = "Does some \'b\'",
                Type = "b blocker",
            },
            new()
            {
                IdMedicament = 9,
                Name = "cccccccc",
                Description = "Does some \'c\'",
                Type = "c blocker"
            },
            new()
            {
                IdMedicament = 10,
                Name = "dddddddd",
                Description = "Does some \'d\'",
                Type = "d blocker"
            },
            new()
            {
                IdMedicament = 11,
                Name = "eeeeeeeeee",
                Description = "Does some \'e\'",
                Type = "ee blocker"
            }
        };

        var prescriptionMedicaments = new List<PrescriptionMedicament>
        {
            new()
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Dose = 5,
                Details = "Some details"
            },
            new ()
            {
                IdMedicament = 2,
                IdPrescription = 1,
                Dose = 99,
                Details = "Some details"
            },
            new ()
            {
                IdMedicament = 3,
                IdPrescription = 2,
                Dose = 1,
                Details = "Some other details"
            }
        };
        
        
        
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<Medicament>().HasData(medicaments);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicaments);
    }
}