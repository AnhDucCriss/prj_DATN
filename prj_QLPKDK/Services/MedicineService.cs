using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly WebContext _db;

        public MedicineService(WebContext db)
        {
            _db = db;
        }

        public async Task<string> CreateAsync(MedicineRequestModel model)
        {
            if (model == null)
                return "Dữ liệu thuốc không hợp lệ.";

            var medicine = new Medicines
            {
                MedicineName = model.MedicineName,
                Unit = model.Unit,
                Price = model.Price,
                Description = model.Description,
                Category = model.Category
            };

            _db.Medicines.Add(medicine);
            await _db.SaveChangesAsync();
            return "Thêm thuốc thành công.";
        }

        public async Task<string> UpdateAsync(int id, MedicineRequestModel model)
        {
            var medicine = await _db.Medicines.FindAsync(id);
            if (medicine == null)
                return $"Không tìm thấy thuốc có ID = {id}";

            medicine.MedicineName = model.MedicineName;
            medicine.Unit = model.Unit;
            medicine.Price = model.Price;
            medicine.Description = model.Description;
            medicine.Category = model.Category;

            await _db.SaveChangesAsync();
            return "Cập nhật thuốc thành công.";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var medicine = await _db.Medicines.FindAsync(id);
            if (medicine == null)
                return "Không tìm thấy thuốc.";

            _db.Medicines.Remove(medicine);
            await _db.SaveChangesAsync();
            return "Xoá thuốc thành công.";
        }

        public async Task<Medicines> GetByIdAsync(int id)
        {
            return await _db.Medicines.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Medicines>> GetAllAsync()
        {
            return await _db.Medicines.ToListAsync();
        }

        public async Task<List<Medicines>> GetByNameAsync(string name)
        {
            return await _db.Medicines
                .Where(m => m.MedicineName.Contains(name))
                .ToListAsync();
        }
    }
}
