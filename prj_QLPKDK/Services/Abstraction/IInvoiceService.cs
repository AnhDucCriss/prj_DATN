using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IInvoiceService 
    { 
        Task<string> CreateAsync(InvoiceRequestModel model);
        Task<List<Invoices>> GetAllAsync();
        Task<Invoices> GetByIdAsync(int id);
        Task<List<Invoices>> GetByPatientIdAsync(int patientId);
        Task<List<Invoices>> GetByMedicalRecordIdAsync(int medicalRecordId);
        Task<string> UpdateAsync(int id, InvoiceRequestModel model);
        Task<string> DeleteAsync(int id);
    }
}
