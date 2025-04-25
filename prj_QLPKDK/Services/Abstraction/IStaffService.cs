using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IStaffService
    {
        Task<string> CreateAsync(StaffRequestModel model);
        Task<string> UpdateAsync(string id, StaffRequestModel model);
        Task<string> DeleteAsync(string id);
        Task<Staffs> GetByIdAsync(string id);
        Task<List<Staffs>> GetAllAsync();
        Task<List<Staffs>> GetByNameAsync(string name);
    }
}
