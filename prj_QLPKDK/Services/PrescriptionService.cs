using System.Reflection.Metadata;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Response;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;

namespace prj_QLPKDK.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly WebContext _db;
        public PrescriptionService(WebContext db)
        {
            _db = db;
        }
        public async Task<PrescriptionDetails> AddPrescriptionDetailAsync(PrescriptionDetailRequest model)
        {
            // 1. Kiểm tra đơn thuốc có tồn tại không
            var prescription = await _db.Prescriptions
                .FirstOrDefaultAsync(p => p.Id == model.PrescriptionId);

            if (prescription == null)
                throw new Exception("Đơn thuốc không tồn tại");

            // 2. Tìm thuốc theo tên
            var medicine = await _db.Medicines
                .FirstOrDefaultAsync(m => m.MedicineName.ToLower() == model.MedicineName.ToLower());

            if (medicine == null)
                throw new Exception($"Thuốc '{model.MedicineName}' không tồn tại trong kho");

            // 3. Kiểm tra số lượng còn lại
            if (medicine.Quantity < model.Quantity)
                throw new Exception($"Thuốc '{model.MedicineName}' không đủ trong kho. Hiện còn {medicine.Quantity}");

            // 4. Trừ thuốc trong kho
            medicine.Quantity -= model.Quantity;

            // 5. Tạo PrescriptionDetail
            var detail = new PrescriptionDetails
            {
                PrescriptionId = model.PrescriptionId,
                MedicineId = medicine.Id,
                Quantity = model.Quantity,
                Unit = model.Unit
            };

            // 6. Lưu DB
            _db.PrescriptionDetails.Add(detail);
            await _db.SaveChangesAsync();

            return detail;
        }
        //public async Task<string> AddPresDetail(PrescriptionDetailRequest dto)
        //{
        //    var medicine = _db.Medicines.FirstOrDefault(x => x.MedicineName == dto.medicineName);
            
        //    if(medicine == null)
        //    {
        //        return "Không có thuốc trong kho thuốc";
        //    }
        //    if(medicine.Quantity == 0)
        //    {
        //        return "Số lượng thuốc trong kho đã hết";
        //    }
        //    var entity = new PrescriptionDetails
        //    {
        //        PrescriptionId = dto.prescriptionID,
        //        Medicine = medicine,
        //        Quantity = dto.quantity,
        //        Unit = dto.unit,


        //    };

        //    _db.PrescriptionDetails.Add(entity);
        //    await _db.SaveChangesAsync();

        //    return entity.Id; 
        //}

        public async Task<string> CreateAsync(PrescriptionResquestModel model)
        {
            
            var entity = new Prescriptions
            {
                MedicalRecordId = model.MedicalRecordId,
                PatientName = model.PatientName,
                DoctorName = model.DoctorName,
                
            };

            _db.Prescriptions.Add(entity);
            await _db.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var existing = await _db.Prescriptions.FindAsync(id);
            if (existing == null)
                return $"Không tìm thấy đơn thuốc với ID: {id}";

            _db.Prescriptions.Remove(existing);
            await _db.SaveChangesAsync();
            return $"Xoá thành công đơn thuốc có ID: {id}";
        }

        public async Task<List<Prescriptions>> GetAllAsync()
        {
            return await _db.Prescriptions.ToListAsync();
        }

        public async Task<PrescriptionResponse> GetByIdAsync(string id)
        {
            var pres =  await _db.Prescriptions.FirstOrDefaultAsync(p => p.MedicalRecordId == id);
            
            if(pres != null)
            {
                var presdetail = await _db.PrescriptionDetails
                     .Where(x => x.PrescriptionId == pres.Id)
                     .Include(x => x.Medicine) 
                     .ToListAsync();
                

                var resp = new PrescriptionResponse
                {
                    PatientName = pres.PatientName,
                    DoctorName = pres.DoctorName,
                    PrescriptionDetails = presdetail
                };
                return resp;
            }
            return new PrescriptionResponse();
        }

       

        public async Task<string> UpdateAsync(string id, PrescriptionResquestModel model)
        {
            var entity = await _db.Prescriptions.FindAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException("Prescription not found.");
            }

            // Cập nhật các trường cần thiết
            
            entity.PatientName = model.PatientName;
            entity.DoctorName = model.DoctorName;
            // Cập nhật thêm các trường nếu cần thiết (ví dụ: TotalAmount, Quantity, Dosage, UsageInstructions)

            // Lưu thay đổi vào cơ sở dữ liệu
            await _db.SaveChangesAsync();

            return entity.Id; // Trả về ID của đơn thuốc đã cập nhật
        }

        public async Task<bool> UpdatePrescriptionDetailsAsync(UpdatePrescriptionDetailsRequest request)
        {
            var prescription = await _db.Prescriptions
                .Include(p => p.PrescriptionDetails)
                .FirstOrDefaultAsync(p => p.MedicalRecordId == request.MedicalRecordId);
            
            if (prescription == null)
                throw new Exception("Không tìm thấy đơn thuốc.");
            var dellPresDetail = await _db.PrescriptionDetails.Where(x => x.PrescriptionId == prescription.Id).ToListAsync();
            // Xóa các PrescriptionDetail cũ
            _db.PrescriptionDetails.RemoveRange(dellPresDetail);
            await _db.SaveChangesAsync(); 

            // Sau đó mới thêm các chi tiết mới
            var newDetails = new List<PrescriptionDetails>();
            foreach (var item in request.Items)
            {
                var medicine = await _db.Medicines.FirstOrDefaultAsync(m => m.MedicineName == item.MedicineName);
                if (medicine == null)
                    throw new Exception($"Không tìm thấy thuốc: {item.MedicineName}");

                if (medicine.Quantity < item.Quantity)
                    throw new Exception($"Thuốc '{medicine.MedicineName}' không đủ số lượng trong kho.");

                medicine.Quantity -= item.Quantity;

                newDetails.Add(new PrescriptionDetails
                {
                    PrescriptionId = prescription.Id,
                    MedicineId = medicine.Id,
                    Quantity = item.Quantity,
                    Unit = medicine.Unit
                });
            }

            await _db.PrescriptionDetails.AddRangeAsync(newDetails);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<byte[]> GeneratePrescriptionPdfAsync(string medicalRecordId)
        {
            var prescription = await _db.Prescriptions
                .FirstOrDefaultAsync(p => p.MedicalRecordId == medicalRecordId);

            if (prescription == null)
                throw new Exception("Không tìm thấy đơn thuốc.");

            var details = await _db.PrescriptionDetails
                .Where(d => d.PrescriptionId == prescription.Id)
                .Include(d => d.Medicine)
                .ToListAsync();
            QuestPDF.Settings.License = LicenseType.Community;
            var stream = new MemoryStream();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.Header().Text("Đơn thuốc").FontSize(20).Bold().AlignCenter();

                    page.Content().PaddingTop(20).Column(col =>
                    {
                        col.Item().Text($"Tên bệnh nhân: {prescription.PatientName}").FontSize(14).Bold();
                        col.Item().Element(x => x.PaddingBottom(10))
                                 .Text($"Bác sĩ điều trị: {prescription.DoctorName}").FontSize(14).Bold();

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(4); // Tên thuốc
                                columns.RelativeColumn(2); // Đơn vị
                                columns.RelativeColumn(2); // Số lượng
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Tên thuốc").Bold();
                                header.Cell().Element(CellStyle).Text("Đơn vị").Bold();
                                header.Cell().Element(CellStyle).Text("Số lượng").Bold();
                            });

                            foreach (var item in details)
                            {
                                table.Cell().Element(CellStyle).Text(item.Medicine.MedicineName);
                                table.Cell().Element(CellStyle).Text(item.Unit);
                                table.Cell().Element(CellStyle).Text(item.Quantity.ToString());
                            }

                            static IContainer CellStyle(IContainer container) =>
                                container.PaddingVertical(5).PaddingHorizontal(10);
                        });


                        
                        col.Item().AlignRight().PaddingTop(50).Text($"Thanh Hóa, ngày {DateTime.Now.Day}, tháng {DateTime.Now.Month}, năm {DateTime.Now.Year}").FontSize(13);
                        
                        col.Item().AlignRight().PaddingRight(50).Text("Người tạo đơn").FontSize(13).Bold();
                        
                        col.Item().Height(50); 

                        
                        
                    });

                    
                });
            });

            return document.GeneratePdf();
        }

    }
}
