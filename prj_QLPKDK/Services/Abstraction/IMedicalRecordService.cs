using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IMedicalRecordService
    {
        Task<string> CreateAsync(MedicalRecordRequestModel model);
        Task<string> UpdateAsync(string id, MedicalRecordRequestModel model);
        Task<string> DeleteAsync(string id);
        Task<MedicalRecords> GetByIdAsync(string id);
        Task<PagedResult<MedicalRecords>> GetAllAsync(string id, PagedQuery query);
        Task<PagedResult<MedicalRecords>> SearchAsync(string id, MedicalRecordFilter filter);
        Task<Prescriptions> GetPrescription(string id);
        Task<Invoices> GetInvoice(string id);

    }
}
