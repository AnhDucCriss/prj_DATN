using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Enum;
using prj_QLPKDK.Models.Resquest;
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
            var check = _db.Users.FirstOrDefault(x => x.Username == model.Username);
            if(check == null)
            {
                var user = new Users();
                user.Username = model.Username;
                user.Password = model.Password;
                user.Role = model.Role;
                _db.Users.Add(user);

                await _db.SaveChangesAsync();
                return user.Id.ToString();
            } else
            {
                return "Trùng username";
            }
            
        }

        public async Task<string> Delete(int id)
        {
            var dellData = _db.Users!.SingleOrDefault(x => x.Id == id);
            if (dellData != null)
            {
                dellData.IsActive = false;
                _db.Users.Remove(dellData);
                await _db.SaveChangesAsync();
                return "Xoá thành công";
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

        public async Task<List<Users>> GetByUserName(string name)
        {
            var dsUS = new List<Users>();
            foreach(var item in _db.Users)
            {
                if(item.Username.Contains(name))
                {
                    dsUS.Add(item);
                }
            }

            return dsUS;
        }

        public async Task<string> Update(int id, UserRequestModel model)
        {

            var existingUser = await _db.Users.FindAsync(id);
            if (existingUser == null)
            {
                return $"Không tìm thấy user có id = {id}";
            }

            existingUser.Username = model.Username;
            existingUser.Password = model.Password;
            existingUser.Role = model.Role;

            await _db.SaveChangesAsync();
            return "Cập nhật tài khoản thành công";
        }

    }
}
