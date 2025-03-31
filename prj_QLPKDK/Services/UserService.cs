using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Enum;
using prj_QLPKDK.Models;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class UserService :  IUserServices
    {
        private readonly WebContext _db;
        public UserService(WebContext db) 
        {
            _db = db;
        }

        public async Task<string> Create(UserRequestModel model)
        {
            var user = new Users();
            user.Username = model.Username;
            user.Password = model.Password;
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Role = model.Role;
            _db.Users.Add(user);
            
            await _db.SaveChangesAsync();
            return user.Id.ToString();
        }

        public async Task<string> Delete(int id)
        {
            var dellData = _db.Users!.SingleOrDefault(x => x.Id == id);
            if (dellData != null)
            {
                _db.Users!.Remove(dellData);
                await _db.SaveChangesAsync();
                return "Xoá thành công user có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }

        public async Task<List<Users>> GetAll()
        {
            var datas = await _db.Users!.ToListAsync();
            return datas;
        }

        public async Task<Users> GetById(int id)
        {
            var data = _db.Users!.FirstOrDefault(x => x.Id == id);
            return data;
        }

        public async Task<string> Update(int id, UserRequestModel model)
        {

            var user = _db.Users.FirstOrDefault(x => x.Id == id);
            
            if (user != null)
            {
                user.Username = model.Username;
                user.Password = model.Password;
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                //user.Role = model.Role;
                _db.Users!.Update(user);
                await _db.SaveChangesAsync();
                return "Cập nhật thành công cho user có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }

    }
}
