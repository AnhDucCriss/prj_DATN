using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
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
        private readonly WebContext _db;

        public StaffController(IStaffService staffService, WebContext db)
        {
            _staffService = staffService;
            _db = db;
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
        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            try
            {
                var doctors = await _db.Staffs
                    .Where(s => s.Position == "Bác sĩ")
                    .Select(s => new
                    {
                        s.Id,
                        s.FullName,
                        s.Position
                    })
                    .ToListAsync();

                return Ok(new { data = doctors });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi lấy danh sách bác sĩ", error = ex.Message });
            }
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
