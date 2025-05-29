using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Enum;
using prj_QLPKDK.Models.Response;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly WebContext _context;

        public DashboardService(WebContext context)
        {
            _context = context;
        }

        public async Task<MedicalRecordsDataResponse> GetMedicalRecordsDataAsync()
        {
            var currentDate = DateTime.Now;
            var startDate = currentDate.AddMonths(-5).Date; // 6 tháng gần nhất
            startDate = new DateTime(startDate.Year, startDate.Month, 1); // Đầu tháng
            var endDate = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month)); // Cuối tháng hiện tại

            // Lấy dữ liệu hồ sơ theo tháng
            var monthlyData = await _context.MedicalRecords
                .Where(mr => mr.ExaminationDate >= startDate && mr.ExaminationDate <= endDate)
                .GroupBy(mr => new { mr.ExaminationDate.Year, mr.ExaminationDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    RecordCount = g.Count(),
                    UniquePatients = g.Select(x => x.PatientId).Distinct().Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            var result = new MedicalRecordsDataResponse();

            // Tạo danh sách tháng tiếng Anh
            var monthNames = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                   "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            // Tạo dữ liệu cho 6 tháng gần nhất
            for (int i = 5; i >= 0; i--)
            {
                var targetDate = currentDate.AddMonths(-i);
                var targetYear = targetDate.Year;
                var targetMonth = targetDate.Month;

                var monthData = monthlyData.FirstOrDefault(x => x.Year == targetYear && x.Month == targetMonth);

                // Format: "MMM yyyy" (ví dụ: "Jan 2024")
                var monthLabel = $"{monthNames[targetMonth - 1]} {targetYear}";

                result.Months.Add(monthLabel);
                result.TotalRecords.Add(monthData?.RecordCount ?? 0);
                result.UniquePatients.Add(monthData?.UniquePatients ?? 0);
            }

            return result;
        }

        public async Task<SaleStatsResponse> GetSalesStatsAsync()
        {
            // Giả sử bạn có các bảng này, thay thế bằng model thực tế
            var stats = new SaleStatsResponse
            {
                TotalStaff = await _context.Staffs.CountAsync(),
                TotalDoctors = await _context.Staffs.CountAsync(u => u.Position == "Bác sĩ"),
                TotalPatients = await _context.Patients.CountAsync(),
                TotalRecords = await _context.MedicalRecords.CountAsync()
            };

            return stats;
        }

        public async Task<RevenueStatsResponse> GetRevenueStatsAsync(DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Invoices.AsQueryable();

            // Chỉ áp dụng filter khi có ít nhất một trong hai tham số fromDate hoặc toDate
            if (fromDate.HasValue || toDate.HasValue)
            {
                if (fromDate.HasValue && toDate.HasValue)
                {
                    // Lấy theo khoảng thời gian
                    query = query.Where(i => i.CreatedAt.Date >= fromDate.Value.Date &&
                                            i.CreatedAt.Date <= toDate.Value.Date);
                }
                else if (fromDate.HasValue)
                {
                    // Chỉ có fromDate
                    query = query.Where(i => i.CreatedAt.Date >= fromDate.Value.Date);
                }
                else if (toDate.HasValue)
                {
                    // Chỉ có toDate
                    query = query.Where(i => i.CreatedAt.Date <= toDate.Value.Date);
                }
            }
            // Nếu không có fromDate và toDate, lấy tất cả dữ liệu trong CSDL (không filter gì cả)

            var invoices = await query.ToListAsync();
            var paidInvoices = invoices.Where(i => i.PaymentStatus == PaymentStatus.Paid).ToList();
            var unpaidInvoices = invoices.Where(i => i.PaymentStatus == PaymentStatus.Unpaid).ToList();

            return new RevenueStatsResponse
            {
                TotalRevenuePaid = (decimal)paidInvoices.Sum(i => i.TotalAmount),
                TotalRevenueUnpaid = (decimal)unpaidInvoices.Sum(i => i.TotalAmount),
                TotalInvoicesPaid = paidInvoices.Count,
                TotalInvoicesUnpaid = unpaidInvoices.Count
            };
        }

    } 
}
