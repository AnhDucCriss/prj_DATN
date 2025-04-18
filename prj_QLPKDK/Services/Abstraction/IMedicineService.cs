﻿using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IMedicineService
    {
        Task<string> CreateAsync(MedicineRequestModel model);
        Task<string> UpdateAsync(int id, MedicineRequestModel model);
        Task<string> DeleteAsync(int id);
        Task<Medicines> GetByIdAsync(int id);
        Task<List<Medicines>> GetAllAsync();
        Task<List<Medicines>> GetByNameAsync(string name);
    }
}
