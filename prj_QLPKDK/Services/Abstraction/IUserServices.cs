using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IUserServices
    {
        public Task<List<Users>> GetAll();
        public Task<Users> GetById(int id);
        public Task<string> Create(UserRequestModel model);
        public Task<string> Update(int id, UserRequestModel model);
        public Task<string> Delete(int id);
    }
}
