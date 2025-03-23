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

        public async Task delUser(string id)
        {
            var delDungcu = _db.Users!.SingleOrDefault(dungcu => dungcu.Id == id);
            if (delDungcu != null)
            {
                _db.Users!.Remove(delDungcu);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Users> GetUserById(string id)
        {
            var dc = await _db.Users!.FindAsync(id);
            return dc;
        }

        public async Task<List<Users>> GetUsers()
        {
            var users = await _db.Users!.ToListAsync();
            return users;
        }

        public async Task updateUser(string id, Users model)
        {
            if (id == model.Id)
            {
                _db.Users!.Update(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
