using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IPrescriptionService
    {
        Task<string> CreateAsync(PrescriptionResquestModel model);
        Task<string> UpdateAsync(string id, PrescriptionResquestModel model);
        Task<string> DeleteAsync(string id);
        Task<Prescriptions> GetByIdAsync(string id);
        Task<List<Prescriptions>> GetAllAsync();
    }
}
