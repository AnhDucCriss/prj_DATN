using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IStaffService
    {
        Task<bool> CreateAsync(StaffRequestModel model);
        Task<bool> UpdateAsync(string id, StaffRequestModel model);
        Task<bool> DeleteAsync(string id);
        Task<Staffs> GetByIdAsync(string id);
        Task<PagedResult<Staffs>> GetAllAsync(PagedQuery query);
        Task<PagedResult<Staffs>> GetByNameAsync(StaffFilterResquest request);
    }
}
