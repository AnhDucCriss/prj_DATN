using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Response;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IInvoiceService 
    { 
        
        Task<List<Invoices>> GetAllAsync();
        Task<Invoices> GetByIdAsync(string id);
        
        Task<InvoiceResponseModel> GetByMedicalRecordIdAsync(string medicalRecordId);
        Task<string> UpdateAsync(string medicalRecordID);
        Task<string> DeleteAsync(string id);
    }
}
