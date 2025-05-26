using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Response;
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

        
        // Lấy tất cả hóa đơn
        public async Task<List<Invoices>> GetAllAsync()
        {
            return await _db.Invoices

                             .Include(i => i.MedicalRecord)
                             .ToListAsync();
        }

        // Lấy hóa đơn theo ID
        public async Task<Invoices> GetByIdAsync(string id)
        {
            return await _db.Invoices
                             .Include(i => i.MedicalRecord)
                             .FirstOrDefaultAsync(i => i.Id == id);
        }

        // Lấy hóa đơn theo ID bệnh nhân


        // Lấy hóa đơn theo ID hồ sơ bệnh án

        // Cập nhật hóa đơn
        public async Task<string> UpdateAsync(string medicalRecordID)
        {
            var invoice = await _db.Invoices.FirstOrDefaultAsync(x => x.MedicalRecordId == medicalRecordID);

            if (invoice == null)
                return "Không tìm thấy hóa đơn" ;

            // Kiểm tra và cập nhật các trường của hóa đơn

           
            if(invoice.PaymentStatus == true)
            {
                invoice.PaymentStatus = false;
            } else
            {
                invoice.PaymentStatus = true;
            }

            await _db.SaveChangesAsync();
            
            return "Cập nhật hóa đơn thành công.";
        }

        // Xóa hóa đơn
        public async Task<string> DeleteAsync(string id)
        {
            var invoice = await _db.Invoices.FindAsync(id);

            if (invoice == null)
                return "Không tìm thấy hóa đơn với ID = " + id;

            _db.Invoices.Remove(invoice);
            await _db.SaveChangesAsync();

            return "Xóa hóa đơn thành công.";
        }

        public async Task<InvoiceResponseModel> GetByMedicalRecordIdAsync(string medicalRecordId)
        {
            var medicalRecord = await _db.MedicalRecords
                .Include(x => x.Patient)
                .Include(x => x.Invoice)
                .FirstOrDefaultAsync(x => x.Id == medicalRecordId);

            if (medicalRecord == null || medicalRecord.Invoice == null || medicalRecord.Patient == null)
            {
                throw new Exception("Không tìm thấy hồ sơ khám bệnh.");
            }
            var patient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == medicalRecord.PatientId);
            if (patient == null)
            {
                throw new Exception("Không tìm thấy bệnh nhân.");
            }
            var pescription = await _db.Prescriptions.FirstOrDefaultAsync(x => x.MedicalRecordId == medicalRecordId);
            var listThuoc = await _db.PrescriptionDetails.Where(x => x.PrescriptionId == pescription.Id).ToListAsync();
            float totalAmount = 0;
            var invoice = await _db.Invoices.FirstOrDefaultAsync(x => x.MedicalRecordId == medicalRecordId);
            foreach ( var item in listThuoc )
            {
                var thuoc = await _db.Medicines.FirstOrDefaultAsync(x => x.Id == item.MedicineId);
                totalAmount += ((float)item.Quantity * thuoc.Price);
            }
            return new InvoiceResponseModel
            {
                PatientName = patient.FullName, // giả định Patients có FullName
                DoctorName = medicalRecord.DoctorName,
                ExaminationDate = medicalRecord.ExaminationDate,
                Conclusion = medicalRecord.Conclusion,
                TotalAmout = totalAmount,
                PaymentStatus = invoice.PaymentStatus,
                
            };
        }
    }
}
