using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly WebContext _db;
        public MedicalRecordService(WebContext db)
        {
            _db = db;
        }

        public async Task<string> Create(MedicalRecords model)
        {
            
            _db.MedicalRecords!.Add(model);
            await _db.SaveChangesAsync();
            return model.Id.ToString();
        }

        public async Task<string> Delete(int id)
        {
            var dellData = _db.MedicalRecords!.SingleOrDefault(x => x.Id == id);
            if (dellData != null)
            {
                _db.MedicalRecords!.Remove(dellData);
                await _db.SaveChangesAsync();
                return "Xoá thành công MedicalRecord có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }

        public async Task<List<MedicalRecords>> GetAll()
        {
            var datas = await _db.MedicalRecords!.ToListAsync();
            return datas;
        }

        public async Task<MedicalRecords> GetById(int id)
        {
            var data = _db.MedicalRecords!.FirstOrDefault(x => x.Id == id);
            return data;
        }

        public async Task<string> Update(int id, MedicalRecords model)
        {
            if (id == model.Id)
            {
                _db.MedicalRecords!.Update(model);
                await _db.SaveChangesAsync();
                return "Cập nhật thành công cho MedicalRecord có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }
    }
}
