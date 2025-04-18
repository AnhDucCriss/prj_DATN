using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class StaffService : IStaffService
    {
        private readonly WebContext _db;

        public StaffService(WebContext db)
        {
            _db = db;
        }

        public async Task<string> CreateAsync(StaffRequestModel model)
        {
            if (model == null)
                return "Dữ liệu nhân viên không hợp lệ.";

            var newStaff = new Staffs
            {
                FullName = model.FullName,
                Gender = model.Gender,
                Phone = model.Phone,
                Email = model.Email,
                Position = model.Position
            };

            _db.Staffs.Add(newStaff);
            await _db.SaveChangesAsync();
            return "Thêm nhân viên thành công.";
        }

        public async Task<string> UpdateAsync(int id, StaffRequestModel model)
        {
            var staff = await _db.Staffs.FindAsync(id);
            if (staff == null)
                return $"Không tìm thấy nhân viên có id = {id}";

            staff.FullName = model.FullName;
            staff.Gender = model.Gender;
            staff.Phone = model.Phone;
            staff.Email = model.Email;
            staff.Position = model.Position;

            await _db.SaveChangesAsync();
            return "Cập nhật nhân viên thành công.";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var staff = await _db.Staffs.FindAsync(id);
            if (staff == null)
                return "Không tìm thấy nhân viên.";

            _db.Staffs.Remove(staff);
            await _db.SaveChangesAsync();
            return "Xoá nhân viên thành công.";
        }

        public async Task<Staffs> GetByIdAsync(int id)
        {
            return await _db.Staffs.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Staffs>> GetAllAsync()
        {
            return await _db.Staffs.ToListAsync();
        }

        public async Task<List<Staffs>> GetByNameAsync(string name)
        {
            return await _db.Staffs
                .Where(s => s.FullName.Contains(name))
                .ToListAsync();
        }
    }
}
