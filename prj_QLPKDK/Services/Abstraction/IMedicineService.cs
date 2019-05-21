using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Response;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IMedicineService
    {
        Task<bool> CreateAsync(MedicineRequestModel model);
        Task<bool> UpdateAsync(string id, MedicineRequestModel model);
        Task<bool> DeleteAsync(string id);
        Task<Medicines> GetByIdAsync(string id);
        Task<PagedResult<Medicines>> GetAllAsync(PagedQuery query);
        Task<PagedResult<Medicines>> SearchMedicine(MedicineFilter request);
        Task<List<MedicineNameResponse>> GetAllMedicineName();
    }
}
