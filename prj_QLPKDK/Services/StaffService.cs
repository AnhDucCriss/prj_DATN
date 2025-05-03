using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.FilterResquest;
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

        public async Task<bool> CreateAsync(StaffRequestModel model)
        {
            if (model == null)
                return false;

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
            return true;
        }

        public async Task<bool> UpdateAsync(string id, StaffRequestModel model)
        {
            var staff = await _db.Staffs.FindAsync(id);
            if (staff == null)
                return false;

            staff.FullName = model.FullName;
            staff.Gender = model.Gender;
            staff.Phone = model.Phone;
            staff.Email = model.Email;
            staff.Position = model.Position;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var staff = await _db.Staffs.FindAsync(id);
            if (staff == null)
                return false;

            _db.Staffs.Remove(staff);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Staffs> GetByIdAsync(string id)
        {
            return await _db.Staffs.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<PagedResult<Staffs>> GetAllAsync(PagedQuery query)
        {
            const int pageSize = 10; // Luôn cố định 10 bản ghi

            int pageNumber = query.PageNumber <= 0 ? 1 : query.PageNumber;

            var staffsQuery = _db.Staffs.AsQueryable();

            var totalRecords = await staffsQuery.CountAsync();

            var staffs = await staffsQuery
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Staffs>
            {
                Items = staffs,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }



        public async Task<PagedResult<Staffs>> GetByNameAsync(StaffFilterResquest request)
        {
            const int pageSize = 10; // Luôn cố định 10 bản ghi

            int pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;

            var query = _db.Staffs.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(s => s.FullName.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.Position))
            {
                query = query.Where(s => s.Position.Contains(request.Position));
            }

            var totalRecords = await query.CountAsync();

            var staffs = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Staffs>
            {
                Items = staffs,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


    }
}
