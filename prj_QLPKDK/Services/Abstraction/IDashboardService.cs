using prj_QLPKDK.Models.Response;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IDashboardService
    {
        Task<MedicalRecordsDataResponse> GetMedicalRecordsDataAsync();
        Task<SaleStatsResponse> GetSalesStatsAsync();
        Task<RevenueStatsResponse> GetRevenueStatsAsync(DateTime? fromDate = null, DateTime? toDate = null);
    }
}
