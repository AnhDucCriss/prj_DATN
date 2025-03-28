using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Models;
using prj_QLPKDK.Services;

namespace prj_QLPKDK.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtService _service;
        public LoginController(JwtService service) 
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var data = await _service.Authenticate(model);

            return data == null ? NotFound() : Ok(data);

        }

    }
}
