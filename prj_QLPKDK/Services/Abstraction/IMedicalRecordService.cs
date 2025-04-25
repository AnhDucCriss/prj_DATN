using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IMedicalRecordService
    {
        Task<string> CreateAsync(MedicalRecordRequestModel model);
        Task<string> UpdateAsync(string id, MedicalRecordRequestModel model);
        Task<string> DeleteAsync(string id);
        Task<MedicalRecords> GetByIdAsync(string id);
        Task<List<MedicalRecords>> GetAllAsync();
        Task<Prescriptions> GetPrescription(string id);
        Task<Invoices> GetInvoice(string id);

    }
}
