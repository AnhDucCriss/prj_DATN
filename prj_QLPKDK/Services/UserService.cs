using System.Linq;
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
            if (model == null)
                return "Dữ liệu đầu vào không hợp lệ";

            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return "Username và Password không được để trống";

            if (model.Role != Role.Admin && model.Role != Role.Nurse && model.Role != Role.Accountant && model.Role != Role.Doctor)
                return "Vai trò không hợp lệ";

            var isExist = await _db.Users.AnyAsync(x => x.Username == model.Username);
            if (isExist)
                return "Trùng username";

            var user = new Users
            {
                Username = model.Username,
                Password = model.Password, // Nếu có login thật nên hash mật khẩu
                Role = model.Role
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return user.Id;

        }

        public async Task<string> Delete(string id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return "ID đưa vào không hợp lệ";
            if (!user.IsActive)
                return "Người dùng đã bị vô hiệu hoá trước đó.";
            // Đánh dấu là không hoạt động thay vì xóa trực tiếp
            user.IsActive = false;

            // Không cần phải Remove nếu chỉ cần đánh dấu là không hoạt động
            _db.Users.Remove(user); // Cập nhật entity thay vì xóa

            await _db.SaveChangesAsync();
            return "Xoá thành công";
        }

        public async Task<List<Users>> GetAll()
        {
            var datas = await _db.Users!.Where(x => x.IsActive == true).ToListAsync();
            return datas;
        }

        public async Task<Users> GetById(string id)
        {
            var user = await _db.Users
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new KeyNotFoundException("Không tìm thấy người dùng với ID: " + id);

            return user;
        }

        public async Task<List<Users>> GetByUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Users>(); // Trả về danh sách rỗng nếu tên trống

            // Dùng `Where` để lọc trực tiếp trong cơ sở dữ liệu thay vì duyệt qua tất cả người dùng
            var users = await _db.Users
                                  .Where(x => x.Username.Contains(name)) // Tìm kiếm theo tên người dùng
                                  .ToListAsync(); // Chuyển kết quả thành danh sách bất đồng bộ

            return users;
        }

        public async Task<string> Update(string id, UserRequestModel model)
        {

            // Tìm người dùng theo id trong cơ sở dữ liệu
            var existingUser = await _db.Users.FindAsync(id);

            // Kiểm tra xem người dùng có tồn tại không
            if (existingUser == null)
            {
                return $"Không tìm thấy user có id = {id}";
            }

            // Kiểm tra nếu không có thay đổi nào, tránh lưu không cần thiết
            if (existingUser.Username == model.Username &&
                existingUser.Password == model.Password &&
                existingUser.Role == model.Role)
            {
                return "Không có thay đổi nào để cập nhật";
            }

            // Cập nhật thông tin người dùng
            existingUser.Username = model.Username;
            existingUser.Password = model.Password; // Gợi ý: Mã hóa mật khẩu nếu cần
            existingUser.Role = model.Role;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _db.SaveChangesAsync();

            return "Cập nhật tài khoản thành công";
        }

    }
}
