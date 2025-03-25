using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Entities;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IUserServices
    {
        public Task<List<Users>> GetUsers();
        public Task<Users> GetUserById(string id);
        public Task<string> addUser(Users model);
        public Task<string> updateUser(string id, Users model);
        public Task<string> delUser(string id);

    }
}
