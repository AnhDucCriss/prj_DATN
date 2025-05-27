using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Enum;
using prj_QLPKDK.Models.Response;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
        public async Task<string> UpdateAsync(InvoiceRequestModel dto)
        {
            var invoice = await _db.Invoices.FirstOrDefaultAsync(x => x.MedicalRecordId == dto.MedicalRecordID);

            if (invoice == null)
                return "Không tìm thấy hóa đơn" ;

            invoice.PaymentMethod = dto.PaymentMethod;
            invoice.PaymentStatus = dto.PaymentStatus;
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
            invoice.TotalAmount = totalAmount;

            return new InvoiceResponseModel
            { 
                PatientName = patient.FullName, 
                DoctorName = medicalRecord.DoctorName,
                ExaminationDate = medicalRecord.ExaminationDate,
                Conclusion = medicalRecord.Conclusion,
                TotalAmout = totalAmount,
                PaymentMethod = invoice.PaymentMethod,
                PaymentStatus = invoice.PaymentStatus,
            };
        }

        public async Task<byte[]> GenerateInvoicePdfAsync(string medicalRecordId)
        {
            var medicalRecord = await _db.MedicalRecords
                .Include(x => x.Patient)
                .Include(x => x.Invoice)
                .FirstOrDefaultAsync(x => x.Id == medicalRecordId);

            if (medicalRecord == null || medicalRecord.Patient == null || medicalRecord.Invoice == null)
                throw new Exception("Không tìm thấy hồ sơ khám bệnh.");

            var patient = medicalRecord.Patient;
            var invoice = medicalRecord.Invoice;

            var prescription = await _db.Prescriptions
                .FirstOrDefaultAsync(p => p.MedicalRecordId == medicalRecordId);

            if (prescription == null)
                throw new Exception("Không tìm thấy đơn thuốc.");

            var details = await _db.PrescriptionDetails
                .Where(d => d.PrescriptionId == prescription.Id)
                .Include(d => d.Medicine)
                .ToListAsync();

            float totalAmount = 0;
            foreach (var item in details)
            {
                totalAmount += item.Quantity * item.Medicine.Price;
            }

            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.Header().Text("Hóa đơn").FontSize(20).Bold().AlignCenter();

                    page.Content().PaddingTop(20).Column(col =>
                    {
                        col.Item().Text($"Tên bệnh nhân: {patient.FullName}").FontSize(14);
                        col.Item().Text($"Ngày khám: {medicalRecord.ExaminationDate:dd/MM/yyyy}").FontSize(14);
                        col.Item().Text($"Bác sĩ điều trị: {medicalRecord.DoctorName}").FontSize(14);
                        col.Item().Text($"Kết luận: {medicalRecord.Conclusion}").FontSize(14);
                        col.Item().Text($"Tổng tiền thanh toán: {totalAmount:N0} VNĐ").FontSize(14);

                        string paymentMethodText = invoice.PaymentMethod == PaymentMethod.Transfer ? "Chuyển khoản" : "Tiền mặt";

                        
                       
                        col.Item().Text(text =>
                        {
                            text.Span("Phương thức thanh toán: ").FontSize(14);
                            text.Span(paymentMethodText).FontSize(14);
                        });

                        col.Item().PaddingTop(30).AlignRight()
                                 .Text($"Thanh Hóa, ngày {DateTime.Now:dd}, tháng {DateTime.Now:MM}, năm {DateTime.Now:yyyy}")
                                 .FontSize(13);
                        col.Item().AlignRight().PaddingRight(50).Text("Người tạo đơn").FontSize(13).Bold();
                        col.Item().Height(50);
                    });
                });
            });

            return document.GeneratePdf();
        }


    }
}
