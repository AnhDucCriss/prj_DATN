﻿using Azure.Core;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace prj_QLPKDK.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly WebContext _db;
        public MedicalRecordService(WebContext db)
        {
            _db = db;
        }

        public async Task<string> CreateAsync(MedicalRecordRequestModel model)
        {
            var doctorExists = await _db.Staffs
                .AnyAsync(s => s.FullName == model.DoctorName && s.Position.ToLower() == "bác sĩ");

            if (!doctorExists)
            {
                return $"Không tìm thấy bác sĩ có tên '{model.DoctorName}' trong danh sách nhân viên.";
            }
            var entity = new MedicalRecords
            {
                PatientId = model.PatientId,
                ExaminationDate = DateTime.Today,
                DoctorName = model.DoctorName,
                Symptoms = model.Symptoms,
                Conclusion = model.Conclusion
            };

            _db.MedicalRecords.Add(entity);
            await _db.SaveChangesAsync();

            var patient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == model.PatientId);
            var newPres = new Prescriptions { 
                MedicalRecordId = entity.Id,
                PatientName = patient.FullName,
                DoctorName = model.DoctorName,

            };
            var newInvoice = new Invoices { MedicalRecordId = entity.Id };

            _db.Prescriptions.Add(newPres);
            _db.Invoices.Add(newInvoice);

            await _db.SaveChangesAsync();

            return entity.Id;
        }


        public async Task<string> DeleteAsync(string id)
        {
            var dellData = _db.MedicalRecords.SingleOrDefault(x => x.Id == id);
            var dellpres = _db.Prescriptions.SingleOrDefault(x => x.MedicalRecordId == id);
            var dellinvoice = _db.Invoices.SingleOrDefault(x => x.MedicalRecordId == id);
            
            if (dellData != null && dellpres != null && dellinvoice != null)
            {
                _db.Prescriptions!.Remove(dellpres);
                _db.Invoices!.Remove(dellinvoice);
                _db.MedicalRecords!.Remove(dellData);
                await _db.SaveChangesAsync();
                return "Xoá thành công MedicalRecord có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }

        public async Task<PagedResult<MedicalRecords>> GetAllAsync(string id, PagedQuery query)
        {
            const int pageSize = 10;
            int pageNumber = query.PageNumber <= 0 ? 1 : query.PageNumber;

            var queryable = _db.MedicalRecords
                .Where(m => m.PatientId == id)
                .AsQueryable();

            var totalRecords = await queryable.CountAsync();

            var records = await queryable
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<MedicalRecords>
            {
                Items = records,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<MedicalRecords> GetByIdAsync(string id)
        {
            var data = await _db.MedicalRecords!.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public Task<Invoices> GetInvoice(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Prescriptions> GetPrescription(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<MedicalRecords>> SearchAsync(string id, MedicalRecordFilter filter)
        {
            int pageSize = filter.PageSize <= 0 ? 10 : filter.PageSize;
            int pageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;

            var query = _db.MedicalRecords
                .Where(m => m.PatientId == id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.DoctorName))
            {
                query = query.Where(m => m.DoctorName.Contains(filter.DoctorName));
            }

            if (filter.ExaminationDate.HasValue)
            {
                var ngayKham = filter.ExaminationDate.Value.Date;
                query = query.Where(m =>
                    EF.Functions.DateDiffDay(m.ExaminationDate.Date, ngayKham) == 0);
            }

            var totalRecords = await query.CountAsync();

            var records = await query
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<MedicalRecords>
            {
                Items = records,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<string> UpdateAsync(string id, MedicalRecordRequestModel model)
        {
            var existing = await _db.MedicalRecords.FindAsync(id);
            if (existing == null)
            {
                return $"Không tìm thấy hồ sơ khám bệnh với ID: {id}";
            }
            var doctorExists = await _db.Staffs
                .AnyAsync(s => s.FullName == model.DoctorName && s.Position.ToLower() == "bác sĩ");

            if (!doctorExists)
            {
                return $"Không tìm thấy bác sĩ có tên '{model.DoctorName}' trong danh sách nhân viên.";
            }
            // Cập nhật thông tin

            existing.ExaminationDate = model.ExaminationDate;
            existing.DoctorName = model.DoctorName;
            existing.Symptoms = model.Symptoms;
            existing.Conclusion = model.Conclusion;

            var pres = await _db.Prescriptions.FirstOrDefaultAsync(x => x.MedicalRecordId == existing.Id);
            pres.DoctorName = model.DoctorName;

            await _db.SaveChangesAsync();

            return $"Cập nhật thành công cho MedicalRecord có ID: {id}";
        }

    }
}
