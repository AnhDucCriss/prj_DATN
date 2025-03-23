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

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserAsync([FromBody] Users model)
        {
            var newUser = await _service.addUser(model);
            //var user = await _service.addUser();
            return newUser == null ? NotFound() : Ok(newUser);

        }
    }
}
