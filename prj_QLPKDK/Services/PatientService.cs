using System.Text.RegularExpressions;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace prj_QLPKDK.Services
{
    public class PatientService : IPatientService
    {
        private readonly WebContext _db;
        public PatientService(WebContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(PatientRequestModel model)
        {
            if (model == null)
                return false;

            var newPatient = new Patients
            {
                FullName = model.FullName,
                Gender = model.Gender,
                Phone = model.Phone,
                Email = model.Email,
                Address = model.Address,
                Age = model.Age
            };

            _db.Patients.Add(newPatient);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var patient = await _db.Patients.FindAsync(id);
            if (patient == null) return false;

            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<Patients>> GetAllAsync(PagedQuery query)
        {
            const int pageSize = 10;
            int pageNumber = query.PageNumber <= 0 ? 1 : query.PageNumber;

            var queryable = _db.Patients.AsQueryable();
            var totalRecords = await queryable.CountAsync();

            var patients = await queryable
                .OrderBy(m => m.FullName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Patients>
            {
                Items = patients,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Patients> GetByIdAsync(string id)
        {
            return await _db.Patients
            .Include(p => p.MedicalRecords)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PagedResult<MedicalRecords>> GetMedicalRecordsAsync(string patientId, PagedQuery page)
        {
            var query = _db.MedicalRecords.Where(m => m.PatientId == patientId);
            var list = await query.ToListAsync();
            return new PagedResult<MedicalRecords>
            {
                Items = list,
                TotalRecords = list.Count,
                PageSize = 10,
                PageNumber = page.PageNumber,
            };
        }

        public async Task<PagedResult<Patients>> SearchPatient(PatientFilter filter)
        {
            var query = _db.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.PatientName))
            {
                query = query.Where(p => p.FullName.Contains(filter.PatientName));
            }
            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            {
                query = query.Where(p => p.Phone.Contains(filter.PhoneNumber));
            }

            var totalRecords = await query.CountAsync();

            var patients = await query
            .OrderBy(m => m.FullName)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

            return new PagedResult<Patients>
            {
                Items = patients,
                TotalRecords = totalRecords,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }


        public async Task<bool> UpdateAsync(string id, PatientRequestModel model)
        {
            var patient = await _db.Patients.FindAsync(id);
            if (patient == null)
                return false;

            patient.FullName = model.FullName;
            patient.Gender = model.Gender;
            patient.Age = model.Age;
            patient.Phone = model.Phone;
            patient.Address = model.Address;
            patient.Email = model.Email;

            var medical = await _db.MedicalRecords.FirstOrDefaultAsync(x => x.PatientId == id);
            var pres = await _db.Prescriptions.FirstOrDefaultAsync(x => x.MedicalRecordId == medical.Id);
            pres.PatientName = model.FullName;

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
