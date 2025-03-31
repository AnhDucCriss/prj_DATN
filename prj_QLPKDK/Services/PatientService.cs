using Microsoft.EntityFrameworkCore;
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

            
            await _db.Patients.AddAsync(model);
            await _db.SaveChangesAsync();
            return "aa";
        }

        public async Task<string> Delete(int id)
        {
            var patient = await _db.Patients.FindAsync(id);
            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();
            return id.ToString();
        }

        public async Task<List<Patients>> GetAll()
        {
            return await _db.Patients.ToListAsync();
        }

        public async Task<Patients> GetById(int id)
        {
            return await _db.Patients.FindAsync(id);
        }

        public async Task<string> Update(int id, Patients model)
        {
            var existingPatient = await _db.Patients.FindAsync(id);
            if (existingPatient == null)
                throw new KeyNotFoundException("Patient not found");

            // Cập nhật thông tin
            existingPatient.FullName = model.FullName;
            existingPatient.DateOfBirth = model.DateOfBirth;
            existingPatient.Email = model.Email;
            existingPatient.Gender = model.Gender;
            existingPatient.Address = model.Address;
            existingPatient.PhoneNumber = model.PhoneNumber;
            existingPatient.MedicalHistory = model.MedicalHistory;

            _db.Patients.Update(existingPatient);
            await _db.SaveChangesAsync();
            return "d";
        }
    }
}
