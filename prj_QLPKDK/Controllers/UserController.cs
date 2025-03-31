using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models;
using prj_QLPKDK.Services;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
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
        [HttpPost("get-users")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var User = await _service.GetAll();

            return User == null ? NotFound() : Ok(User);

        }
        [HttpPost("get-user-by-id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var User = await _service.GetById(id);
            
            return User == null ? NotFound() : Ok(User);

        }
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserRequestModel model)
        {
            var newUser = await _service.Create(model);
            return newUser == null ? NotFound() : Ok(newUser);

        }
        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserRequestModel model)
        {
            var newUser = await _service.Update(id, model);
            
            return newUser == null ? NotFound() : Ok(newUser);

        }
        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var delUser = await _service.Delete(id);
            
            return delUser == null ? NotFound() : Ok("Xoá thành công");

        }

    }
}
