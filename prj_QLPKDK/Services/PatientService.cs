using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class PatientService : IPatientService
    {
        private readonly WebContext _db;
        public PatientService(WebContext db)
        {
            _db = db;
        }

        public async Task<string> Create(PatientResquestModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Dữ liệu bệnh nhân không được để trống.");

            // Kiểm tra các trường dữ liệu cần thiết có hợp lệ hay không
            if (string.IsNullOrWhiteSpace(model.FullName) || string.IsNullOrWhiteSpace(model.Phone))
                return "Tên và Số điện thoại là bắt buộc.";

            // Kiểm tra email có hợp lệ không (optional)
            if (!string.IsNullOrWhiteSpace(model.Email) && !Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Địa chỉ email không hợp lệ.";

            // Tạo mới đối tượng bệnh nhân
            var newPatient = new Patients
            {
                FullName = model.FullName,
                Gender = model.Gender,
                Age = model.Age,
                Phone = model.Phone,
                Address = model.Address,
                Email = model.Email,
                MedicalRecords = model.MedicalRecords,
           
            };

            // Thêm bệnh nhân mới vào cơ sở dữ liệu
            _db.Patients.Add(newPatient);
            await _db.SaveChangesAsync();

            return "Thêm bệnh nhân thành công";
        }

        public async Task<string> Delete(string id)
        {
            var patient = await _db.Patients.FindAsync(id);

            // Kiểm tra xem bệnh nhân có tồn tại không
            if (patient == null)
                return "Không tìm thấy bệnh nhân.";

            // Xóa bệnh nhân khỏi cơ sở dữ liệu
            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();

            return "Xóa bệnh nhân thành công";
        }

        public async Task<List<Patients>> GetAll()
        {
            return await _db.Patients
            .Include(p => p.MedicalRecords)
            
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Patients> GetById(string id)
        {
            var patient = await _db.Patients
                           .Include(p => p.MedicalRecords)
                           
                           .AsNoTracking()
                           .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                throw new Exception($"Không tìm thấy bệnh nhân với ID = {id}");

            return patient;
        }

        public async Task<List<Patients>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<Patients>();  // Trả về danh sách rỗng nếu tên không hợp lệ
            }

            // Lấy danh sách bệnh nhân với tên chứa chuỗi tìm kiếm
            return await _db.Patients
                            .Where(p => p.FullName.Contains(name))
                            .Include(p => p.MedicalRecords)
                            .AsNoTracking()  // Tối ưu hóa khi không thay đổi dữ liệu
                            .ToListAsync();
        }

        public async Task<List<MedicalRecords>> GetListMC(string id)
        {
            return await _db.MedicalRecords
                           .Where(x => x.PatientId == id)
                           .ToListAsync();
        }

        public async Task<string> Update(string id, PatientResquestModel model)
        {
            if (model == null)
                return "Dữ liệu cập nhật không được để trống.";

            if (string.IsNullOrWhiteSpace(model.FullName) || string.IsNullOrWhiteSpace(model.Phone))
                return "Tên và Số điện thoại là bắt buộc.";

            if (!string.IsNullOrWhiteSpace(model.Email) &&
                !Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return "Địa chỉ email không hợp lệ.";
            }

            // 2. Lấy bệnh nhân cần cập nhật
            var patient = await _db.Patients
                .Include(p => p.MedicalRecords)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                return $"Không tìm thấy bệnh nhân với ID = {id}";

            // 3. Kiểm tra xem có gì thay đổi không
            var isSame =
                   patient.FullName == model.FullName
                && patient.Gender == model.Gender
                && patient.Age == model.Age
                && patient.Phone == model.Phone
                && patient.Address == model.Address
                && patient.Email == model.Email;
            if (isSame)
                return "Không có thông tin nào thay đổi.";

            // 4. Ánh xạ các trường cần cập nhật
            patient.FullName = model.FullName;
            patient.Gender = model.Gender;
            patient.Age = model.Age;
            patient.Phone = model.Phone;
            patient.Address = model.Address;
            patient.Email = model.Email;

            // Lưu và trả về kết quả
            await _db.SaveChangesAsync();
            return "Cập nhật thông tin bệnh nhân thành công";
        }
    }
}
