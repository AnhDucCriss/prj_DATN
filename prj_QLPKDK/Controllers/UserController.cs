using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Services;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _service;
        
        public UserController(IUserServices service)
        {
            _service = service;
            
        }
        [HttpPost("get-users")]
        public async Task<IActionResult> GetUsers(string id)
        {
            var User = await _service.GetUsers();

            return User == null ? NotFound() : Ok(User);

        }
        [HttpPost("get-user-by-id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var User = await _service.GetUserById(id);
            
            return User == null ? NotFound() : Ok(User);

        }
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserAsync([FromBody] Users model)
        {
            var newUser = await _service.addUser(model);
            var user = await _service.GetUserById(model.Id);
            return newUser == null ? NotFound() : Ok(newUser);

        }
        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] Users model)
        {
            var newUser = await _service.updateUser(id, model);
            //var user = await _service.addUser();
            return newUser == null ? NotFound() : Ok(newUser);

        }
        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var delUser = await _service.delUser(id);
            //var user = await _service.addUser();
            return delUser == null ? NotFound() : Ok("Xoá thành công");

        }

    }
}
