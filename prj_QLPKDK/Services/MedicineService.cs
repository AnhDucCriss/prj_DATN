using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.FilterResquest;
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

        public async Task<bool> CreateAsync(MedicineRequestModel model)
        {
            if (model == null)
                return false;

            var newMedicine = new Medicines
            {
                MedicineName = model.MedicineName,
                Unit = model.Unit,
                Price = model.Price,
                Description = model.Description,
                Category = model.Category
            };

            _db.Medicines.Add(newMedicine);
            await _db.SaveChangesAsync();
          
            return true;
        }

        public async Task<bool> UpdateAsync(string id, MedicineRequestModel model)
        {
            var medicine = await _db.Medicines.FindAsync(id);
            if (medicine == null)
                return false;

            medicine.MedicineName = model.MedicineName;
            medicine.Unit = model.Unit;
            medicine.Price = model.Price;
            medicine.Description = model.Description;
            medicine.Category = model.Category;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var medicine = await _db.Medicines.FindAsync(id);
            if (medicine == null)
                return false;

            _db.Medicines.Remove(medicine);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Medicines> GetByIdAsync(string id)
        {
            return await _db.Medicines.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedResult<Medicines>> GetAllAsync(PagedQuery query)
        {
            const int pageSize = 10;
            int pageNumber = query.PageNumber <= 0 ? 1 : query.PageNumber;

            var queryable = _db.Medicines.AsQueryable();
            var totalRecords = await queryable.CountAsync();

            var medicines = await queryable
                .OrderBy(m => m.MedicineName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Medicines>
            {
                Items = medicines,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<Medicines>> SearchMedicine(MedicineFilter request)
        {
            var query = _db.Medicines.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.MedicineName))
            {
                query = query.Where(m => m.MedicineName.Contains(request.MedicineName));
            }

            if (!string.IsNullOrWhiteSpace(request.Category))
            {
                query = query.Where(m => m.Category.Contains(request.Category));
            }

            var totalRecords = await query.CountAsync();

            var medicines = await query
                .OrderBy(m => m.MedicineName)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Medicines>
            {
                Items = medicines,
                TotalRecords = totalRecords,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

    }
}
