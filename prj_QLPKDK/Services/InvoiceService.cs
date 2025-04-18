using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly WebContext _db;
        public InvoiceService(WebContext db)
        {
            _db = db;
        }

        public async Task<string> CreateAsync(InvoiceRequestModel model)
        {
            // Kiểm tra nếu thông tin cần thiết không hợp lệ
            if (model == null)
                return "Dữ liệu hóa đơn không được để trống.";

            var newInvoice = new Invoices
            {
               
                TotalAmount = model.TotalAmount,
                PaymentMethod = model.PaymentMethod,
                PaymentStatus = model.PaymentStatus
            };

            _db.Invoices.Add(newInvoice);
            await _db.SaveChangesAsync();

            return "Thêm hóa đơn thành công.";
        }

        // Lấy tất cả hóa đơn
        public async Task<List<Invoices>> GetAllAsync()
        {
            return await _db.Invoices
                             .Include(i => i.Patient)
                             .Include(i => i.MedicalRecord)
                             .ToListAsync();
        }

        // Lấy hóa đơn theo ID
        public async Task<Invoices> GetByIdAsync(int id)
        {
            return await _db.Invoices
                             .Include(i => i.Patient)
                             .Include(i => i.MedicalRecord)
                             .FirstOrDefaultAsync(i => i.Id == id);
        }

        // Lấy hóa đơn theo ID bệnh nhân
        public async Task<List<Invoices>> GetByPatientIdAsync(int patientId)
        {
            return await _db.Invoices
                             .Where(i => i.Patient.Id == patientId)
                             .Include(i => i.Patient)
                             .Include(i => i.MedicalRecord)
                             .ToListAsync();
        }

        // Lấy hóa đơn theo ID hồ sơ bệnh án
        public async Task<List<Invoices>> GetByMedicalRecordIdAsync(int medicalRecordId)
        {
            return await _db.Invoices
                             .Where(i => i.MedicalRecord.Id == medicalRecordId)
                             .Include(i => i.Patient)
                             .Include(i => i.MedicalRecord)
                             .ToListAsync();
        }

        // Cập nhật hóa đơn
        public async Task<string> UpdateAsync(int id, InvoiceRequestModel model)
        {
            var invoice = await _db.Invoices.FindAsync(id);

            if (invoice == null)
                return "Không tìm thấy hóa đơn với ID = " + id;

            // Kiểm tra và cập nhật các trường của hóa đơn
            invoice.Patient.Id = model.PatientId;
            invoice.MedicalRecord.Id = model.MedicalRecordId;
            invoice.TotalAmount = model.TotalAmount;
            invoice.PaymentMethod = model.PaymentMethod;
            invoice.PaymentStatus = model.PaymentStatus;

            await _db.SaveChangesAsync();

            return "Cập nhật hóa đơn thành công.";
        }

        // Xóa hóa đơn
        public async Task<string> DeleteAsync(int id)
        {
            var invoice = await _db.Invoices.FindAsync(id);

            if (invoice == null)
                return "Không tìm thấy hóa đơn với ID = " + id;

            _db.Invoices.Remove(invoice);
            await _db.SaveChangesAsync();

            return "Xóa hóa đơn thành công.";
        }

    }
}
