using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
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

        

        public async Task<string> CreateAsync(PrescriptionResquestModel model)
        {
            var entity = new Prescriptions
            {
                MedicalRecordId = model.MedicalRecordId,
                
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

        public async Task<Prescriptions> GetByIdAsync(string id)
        {
            return await _db.Prescriptions.FirstOrDefaultAsync(p => p.Id == id);
        }

       

        public Task<string> UpdateAsync(string id, PrescriptionResquestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
