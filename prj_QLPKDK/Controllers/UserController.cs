using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [EnableCors("AllowAngularApp")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _service;
        
        public UserController(IUserServices service)
        {
            _service = service;
            
        }
        [HttpGet("get-users")]

        public async Task<IActionResult> GetUsers()
        {
            var User = await _service.GetAll();

            return Ok(User);

        }
        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var User = await _service.GetById(id);
            
            return Ok(User);

        }
        [HttpGet("get-user-by-username/{name}")]
        public async Task<IActionResult> GetUserByUsername(string name)
        {
            var User = await _service.GetByUserName(name);

            return Ok(User);

        }
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserRequestModel model)
        {
            var newUser = await _service.Create(model);
            return Ok(new { message = "Tài khoản đã được thêm thành công" });

        }
        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] UserRequestModel model)
        {
            var newUser = await _service.Update(id, model);

            return Ok(new { message = "Cập nhật tài khoản thành công" });

        }
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var delUser = await _service.Delete(id);
            
            return  Ok(new { message = "Xoá thành công" }); ;

        }

    }
}
