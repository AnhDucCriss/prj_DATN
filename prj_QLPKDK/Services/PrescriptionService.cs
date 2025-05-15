using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Response;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly WebContext _db;
        public PrescriptionService(WebContext db)
        {
            _db = db;
        }

        public async Task<string> AddPresDetail(PrescriptionDetailRequest dto)
        {
            var medicine = _db.Medicines.FirstOrDefault(x => x.MedicineName == dto.medicineName);
            
            if(medicine == null)
            {
                return "Không có thuốc trong kho thuốc";
            }
            if(medicine.Quantity == 0)
            {
                return "Số lượng thuốc trong kho đã hết";
            }
            var entity = new PrescriptionDetails
            {
                PrescriptionId = dto.prescriptionID,
                Medicine = medicine,
                Quantity = dto.quantity,
                Unit = dto.unit,


            };

            _db.PrescriptionDetails.Add(entity);
            await _db.SaveChangesAsync();

            return entity.Id; 
        }

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

        public async Task UpdatePrescriptionDetailsAsync(UpdatePrescriptionDetailsRequest request)
        {
            var prescription = await _db.Prescriptions
                .Include(p => p.PrescriptionDetails)
                .FirstOrDefaultAsync(p => p.Id == request.PrescriptionId);

            if (prescription == null)
                throw new Exception("Prescription not found");

            // Xóa chi tiết thuốc cũ
            _db.PrescriptionDetails.RemoveRange(prescription.PrescriptionDetails);

            // Thêm danh sách thuốc mới
            var newDetails = request.PrescriptionDetails.Select(d => new PrescriptionDetails
            {
                PrescriptionId = prescription.Id,
                //dicine = d.medicineName,
                Quantity = d.Quantity,
                Unit = d.Unit,
                
            }).ToList();

            await _db.PrescriptionDetails.AddRangeAsync(newDetails);

            await _db.SaveChangesAsync();
        }
    }
}
