using System.Globalization;
using APBD_CW5.DAL;
using APBD_CW5.DTOs;
using APBD_CW5.Exceptions;
using APBD_CW5.Migrations;
using APBD_CW5.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW5.Services;

public class DbService(AppDbContext data) : IDbService
{
    public async Task<ICollection<PrescriptionGetDto>> GetPrescriptionsAsync()
    {
        return await data.Prescriptions.Select(pr => new PrescriptionGetDto
        {
            IdPrescription = pr.IdPrescription,
            Patient = new PatientGetDto()
            {
                IdPatient = pr.Patient.IdPatient,
                FirstName = pr.Patient.FirstName,
                LastName = pr.Patient.LastName,
                BirthDate = pr.Patient.BirthDate,
            },
            Date = pr.Date,
            DueTo = pr.DueDate,
            Doctor = new DoctorGetDto()
            {
                IdDoctor = pr.Doctor.IdDoctor,
                FirstName = pr.Doctor.FirstName,
                LastName = pr.Doctor.LastName,
                Email = pr.Doctor.Email,
            },
            Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentGetDto()
            {
                Id = pm.Medicament.IdMedicament,
                Name = pm.Medicament.Name
            }).ToList(),
        }).ToListAsync();
    }

    public async Task<PrescriptionGetDto> AddPrescriptionAsync(PrescriptionPostGto prescriptionData)
    {
        List<Medicament> medicaments = [];

        if (prescriptionData.Date > prescriptionData.DueTo)
        {
            throw new ArgumentException("DueTo must be greater than Date");
        }

        if (prescriptionData.Medicaments.Count >= 10)
        {
            throw new ArgumentException("Maximum 10 medicaments per prescription is allowed");
        }

        if (prescriptionData.Medicaments.Count != 0)
        {
            foreach (var medicId in prescriptionData.Medicaments)
            {
                var medic = await data.Medicaments.FirstOrDefaultAsync(m => m.IdMedicament == medicId.Id);

                if (medic is null)
                {
                    throw new NotFoundException($"Medicament {medicId.Id} not found");
                }
            }
        }
        
        var doctor = await data.Doctors.FirstOrDefaultAsync(d => prescriptionData.IdDoctor == d.IdDoctor);

        if (doctor is null)
        {
            throw new NotFoundException($"Doctor {prescriptionData.IdDoctor} not found");
        }
        
        var patient = await data.Patients.FirstOrDefaultAsync(p => p.IdPatient == prescriptionData.Patient.IdPatient);
        if (patient is null)
        {
            var result = await AddPatientAsync(prescriptionData.Patient);
            prescriptionData.Patient.IdPatient = result.IdPatient;
        }

        var prescription = new Prescription()
        {
            Date = prescriptionData.Date,
            DueDate = prescriptionData.DueTo,
            IdDoctor = prescriptionData.IdDoctor,
            IdPatient = prescriptionData.Patient.IdPatient,
            PrescriptionMedicaments = prescriptionData.Medicaments.Select(m => new PrescriptionMedicament()
            {
                IdMedicament = m.Id,
                Dose = m.Dose,
                Details = prescriptionData.Details
            }).ToList(),
        };
        
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();

        return new PrescriptionGetDto()
        {
            IdPrescription = prescription.IdPrescription,
            Patient = new PatientGetDto()
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate,
            },
            Date = prescription.Date,
            DueTo = prescription.DueDate,
            Doctor = new DoctorGetDto()
            {
                IdDoctor = prescription.Doctor.IdDoctor,
                FirstName = prescription.Doctor.FirstName,
                LastName = prescription.Doctor.LastName,
                Email = prescription.Doctor.Email,
            },
            Medicaments = prescription.PrescriptionMedicaments.Select(m => new MedicamentGetDto()
            {
                Id = m.IdMedicament,
                Name = m.Medicament.Name,
                Dose = m.Dose,
            }).ToList(),
        };
    }

    public async Task<PatientGetDto> AddPatientAsync(PatientPutDto addPatientPutDto)
    {
        var patient = await data.Patients.FirstOrDefaultAsync(p => p.IdPatient == addPatientPutDto.IdPatient);
        if (patient is not null)
        {
            throw new Exception("Patient already exists");
        }

        patient = new Patient()
        {
            FirstName = addPatientPutDto.FirstName,
            LastName = addPatientPutDto.LastName,
            BirthDate = addPatientPutDto.BirthDate,
            Prescriptions = []
        };
        
        await data.Patients.AddAsync(patient);
        await data.SaveChangesAsync();
        
        return new PatientGetDto()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate
        };
    }
}