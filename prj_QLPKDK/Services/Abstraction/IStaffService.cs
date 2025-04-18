using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IStaffService
    {
        Task<string> CreateAsync(StaffRequestModel model);
        Task<string> UpdateAsync(int id, StaffRequestModel model);
        Task<string> DeleteAsync(int id);
        Task<Staffs> GetByIdAsync(int id);
        Task<List<Staffs>> GetAllAsync();
        Task<List<Staffs>> GetByNameAsync(string name);
    }
}
