using APBD_CW5.DTOs;
using APBD_CW5.Models;

namespace APBD_CW5.Services;

public interface IDbService
{
    public Task<ICollection<PrescriptionGetDto>> GetPrescriptionsAsync();
    public Task<PrescriptionGetDto> AddPrescriptionAsync(PrescriptionPostGto addPrescriptionPostGto);
    
    public Task<PatientGetDto> AddPatientAsync(PatientPutDto addPatientPutDto);
}