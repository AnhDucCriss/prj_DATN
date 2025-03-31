using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
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

        public async Task<string> Create(Patients model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.Id = Guid.NewGuid().ToString(); // Tạo ID mới
            await _db.Patients.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }

        public async Task<string> Delete(string id)
        {
            var patient = await _db.Patients.FindAsync(id);
            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();
            return id;
        }

        public Task<List<Patients>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Patients> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(string id, Patients model)
        {
            throw new NotImplementedException();
        }
    }
}
