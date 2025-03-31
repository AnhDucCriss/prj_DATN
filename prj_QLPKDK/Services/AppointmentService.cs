using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class AppointmentService : BaseService ,IAppointmentService
    {
        private readonly WebContext _db;
        public AppointmentService(WebContext db) : base(db)
        {
            _db = db;
        }

        public async Task<string> Create(Appointments model)
        {
            
            _db.Appointments!.Add(model);
            await _db.SaveChangesAsync();

            return model.Id.ToString();
        }

        public async Task<string> Delete(int id)
        {
            var delData = _db.Appointments!.SingleOrDefault(x => x.Id == id);
            if (delData != null)
            {
                _db.Appointments!.Remove(delData);
                await _db.SaveChangesAsync();
                return "Xoá thành công user có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }

        public async Task<List<Appointments>> GetAll()
        {
            var datas = await _db.Appointments!.ToListAsync();
            return datas;
        }

        public async Task<Appointments> GetById(int id)
        {
            var data = _db.Appointments!.FirstOrDefault(x => x.Id == id);
            return data;
        }

        public async Task<string> Update(int id, Appointments model)
        {
            if (id == model.Id)
            {
                _db.Appointments!.Update(model);
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
