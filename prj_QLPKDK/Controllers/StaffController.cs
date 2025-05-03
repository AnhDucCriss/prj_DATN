using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

       
        [HttpGet("get-all")]
        public async Task<IActionResult> GetStaffs([FromQuery] PagedQuery query)
        {
            var result = await _staffService.GetAllAsync(query);
            return Ok(result);
        }


        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _staffService.GetByIdAsync(id);
            if (result == null)
                return NotFound("Không tìm thấy nhân viên.");
            return Ok(result);
        }


        [HttpPost("search")]
        public async Task<IActionResult> SearchStaffs([FromBody] StaffFilterResquest request)
        {
            var result = await _staffService.GetByNameAsync(request);
            return Ok(result);
        }




        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] StaffRequestModel model)
        {
            var result = await _staffService.CreateAsync(model);
            if (!result)
                return BadRequest(new { message = "Dữ liệu nhân viên không hợp lệ." });

            return Ok(new { message = "Thêm nhân viên thành công." });
        }

        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] StaffRequestModel model)
        {
            var result = await _staffService.UpdateAsync(id, model);
            if (!result)
                return BadRequest(new { message = "Dữ liệu nhân viên không hợp lệ." });

            return Ok(new { message = "Sửa nhân viên thành công." });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _staffService.DeleteAsync(id);
            if (!result)
                return BadRequest(new { message = "Không có nhân viên cần xóa" });

            return Ok(new { message = "Xóa nhân viên thành công." });
        }
    }
}
