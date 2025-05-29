using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Enum;
using prj_QLPKDK.Models.Response;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly WebContext _context;

        public DashboardController(IDashboardService dashboardService, WebContext context)
        {
            _dashboardService = dashboardService;
            _context = context;
        }

        [HttpGet("medical-records-data")]
        public async Task<ActionResult<MedicalRecordsDataResponse>> GetMedicalRecordsData()
        {
            try
            {
                var result = await _dashboardService.GetMedicalRecordsDataAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy dữ liệu hồ sơ khám bệnh", error = ex.Message });
            }
        }

        [HttpGet("stats")]
        public async Task<ActionResult<SaleStatsResponse>> GetStats()
        {
            try
            {
                var result = await _dashboardService.GetSalesStatsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy thống kê", error = ex.Message });
            }
        }

        [HttpGet("revenue-stats")]
        public async Task<ActionResult<RevenueStatsResponse>> GetRevenueStats([FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
        {
            try
            {
                var stats = await _dashboardService.GetRevenueStatsAsync(fromDate, toDate);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy thống kê doanh thu", error = ex.Message });
            }
        }
    }
}
