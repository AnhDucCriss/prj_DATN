using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class UserService : IUserServices
    {
        private readonly WebContext _db;
        public UserService(WebContext db)
        {
            _db = db;
        }

        public async Task<string> addUser(Users model)
        {
            model.Id = Guid.NewGuid().ToString();
            _db.Users!.Add(model);
            await _db.SaveChangesAsync();

            return model.Id.ToString();
        }

        public async Task<string> delUser(string id)
        {
            var delUser = _db.Users!.SingleOrDefault(x => x.Id == id);
            if (delUser != null)
            {
                _db.Users!.Remove(delUser);
                await _db.SaveChangesAsync();
                return "Xoá thành công user có ID: " + id; 
            } else
            {
                return "ID đưa vào không hợp lệ";
            }
        }

        public async Task<Users> GetUserById(string id)
        {
            var user = _db.Users!.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public async Task<List<Users>> GetUsers()
        {
            var users = await _db.Users!.ToListAsync();
            return users;
        }

        public async Task<string> updateUser(string id, Users model)
        {
            if (id == model.Id)
            {
                _db.Users!.Update(model);
                await _db.SaveChangesAsync();
                return "Cập nhật thành công cho user có ID: " + id;
            } else
            {
                return "ID đưa vào không hợp lệ";
            }
        }
    }
}
