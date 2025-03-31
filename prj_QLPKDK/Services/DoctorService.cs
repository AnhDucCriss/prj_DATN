using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class DoctorService : BaseService, IDoctorService
    {
        private readonly WebContext _db;
        public DoctorService(WebContext db) : base(db)
        {
            _db = db;
        }

        public async Task<string> Create(Doctors model)
        {
            
            _db.Doctors!.Add(model);
            await _db.SaveChangesAsync();
            return model.Id.ToString();
        }

        public async Task<string> Delete(int id)
        {
            var dellData = _db.Doctors!.SingleOrDefault(x => x.Id == id);
            if (dellData != null)
            {
                _db.Doctors!.Remove(dellData);
                await _db.SaveChangesAsync();
                return "Xoá thành công user có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }

        public async Task<List<Doctors>> GetAll()
        {
            var datas = await _db.Doctors!.ToListAsync();
            return datas;
        }

        public async Task<Doctors> GetById(int id)
        {
            var data = _db.Doctors!.FirstOrDefault(x => x.Id == id);
            return data;
        }

        public async Task<string> Update(int id, Doctors model)
        {
            if (id == model.Id)
            {
                _db.Doctors!.Update(model);
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
