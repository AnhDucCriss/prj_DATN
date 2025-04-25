using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IInvoiceService 
    { 
        Task<string> CreateAsync(InvoiceRequestModel model);
        Task<List<Invoices>> GetAllAsync();
        Task<Invoices> GetByIdAsync(string id);
        
        Task<List<Invoices>> GetByMedicalRecordIdAsync(string medicalRecordId);
        Task<string> UpdateAsync(string id, InvoiceRequestModel model);
        Task<string> DeleteAsync(string id);
    }
}
