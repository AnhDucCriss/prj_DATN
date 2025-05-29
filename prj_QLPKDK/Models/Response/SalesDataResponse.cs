namespace prj_QLPKDK.Models.Response
{
    public class SalesDataResponse
    {
        public List<int> PaidInvoices { get; set; } = new List<int>();
        public List<int> UnpaidInvoices { get; set; } = new List<int>();
        public List<string> Months { get; set; } = new List<string>();
    }
    public class MedicalRecordsDataResponse
    {
        public List<int> TotalRecords { get; set; } = new List<int>();
        public List<int> UniquePatients { get; set; } = new List<int>();
        public List<string> Months { get; set; } = new List<string>();
    }
    public class RevenueStatsResponse
    {
        public decimal TotalRevenuePaid { get; set; } // Số tiền đã thanh toán
        public decimal TotalRevenueUnpaid { get; set; } // Số tiền chưa thanh toán
        public int TotalInvoicesPaid { get; set; } // Số hóa đơn đã thanh toán
        public int TotalInvoicesUnpaid { get; set; } // Số hóa đơn chưa thanh toán
    }
}
