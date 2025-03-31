using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly WebContext _db;
        public DepartmentService(WebContext db) : base(db) 
        {
            _db = db;
        }
        public async Task<string> Create(Departments model)
        {
            
            _db.Departments!.Add(model);
            await _db.SaveChangesAsync();

            return model.Id.ToString();
        }

        public async Task<string> Delete(int id)
        {
            var dellData = _db.Departments!.SingleOrDefault(x => x.Id == id);
            if (dellData != null)
            {
                _db.Departments!.Remove(dellData);
                await _db.SaveChangesAsync();
                return "Xoá thành công user có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }

        public async Task<List<Departments>> GetAll()
        {
            var datas = await _db.Departments!.ToListAsync();
            return datas;
        }

        public async Task<Departments> GetById(int id)
        {
            var data = _db.Departments!.FirstOrDefault(x => x.Id == id);
            return data;
        }

        public async Task<string> Update(int id, Departments model)
        {
            if (id == model.Id)
            {
                _db.Departments!.Update(model);
                await _db.SaveChangesAsync();
                return "Cập nhật thành công cho user có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }
    }
}
