using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(new { message = "Nhân viên đã được thêm thành công" });
        }

        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] StaffRequestModel model)
        {
            var result = await _staffService.UpdateAsync(id, model);
            return Ok(new { message = "Nhân viên đã được cập nhật thành công" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _staffService.DeleteAsync(id);
            return Ok(new { message = "Nhân viên đã được xoá thành công" });
        }
    }
}
