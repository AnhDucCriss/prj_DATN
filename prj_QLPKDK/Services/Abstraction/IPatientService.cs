using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IPatientService 
    {
        Task<bool> CreateAsync(PatientRequestModel model);
        Task<bool> UpdateAsync(string id, PatientRequestModel model);
        Task<bool> DeleteAsync(string id);
        Task<Patients> GetByIdAsync(string id);
        Task<PagedResult<Patients>> GetAllAsync(PagedQuery query);
        Task<PagedResult<Patients>> SearchPatient(PatientFilter filter);
        Task<PagedResult<MedicalRecords>> GetMedicalRecordsAsync(string patientId, PagedQuery page);

    }
}
