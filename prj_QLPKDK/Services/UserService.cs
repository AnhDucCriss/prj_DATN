﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
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

        public async Task<string> Create(Users model)
        {
            model.Id = Guid.NewGuid().ToString();
            _db.Users!.Add(model);
            await _db.SaveChangesAsync();
            return model.Id.ToString();
        }

        public async Task<string> Delete(string id)
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

        public async Task<Users> GetById(string id)
        {
            var data = _db.Users!.FirstOrDefault(x => x.Id == id);
            return data;
        }

        public async Task<string> Update(string id, Users model)
        {
            if (id == model.Id)
            {
                _db.Users!.Update(model);
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
