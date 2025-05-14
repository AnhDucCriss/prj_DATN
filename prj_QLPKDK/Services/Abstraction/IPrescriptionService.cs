using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Response;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IPrescriptionService
    {
        Task<string> CreateAsync(PrescriptionResquestModel model);
        Task<string> UpdateAsync(string id, PrescriptionResquestModel model);
        Task<string> DeleteAsync(string id);
        Task<PrescriptionResponse> GetByIdAsync(string id);
        Task<string> AddPresDetail(PrescriptionDetailRequest dto);
        Task<List<Prescriptions>> GetAllAsync();
        Task UpdatePrescriptionDetailsAsync(UpdatePrescriptionDetailsRequest request);
    }
}
