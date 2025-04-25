using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IUserServices
    {
        public Task<List<Users>> GetAll();
        public Task<Users> GetById(string id);
        public Task<List<Users>> GetByUserName(string name);

        public Task<string> Create(UserRequestModel model);
        public Task<string> Update(string id, UserRequestModel model);
        public Task<string> Delete(string id);
    }
}
