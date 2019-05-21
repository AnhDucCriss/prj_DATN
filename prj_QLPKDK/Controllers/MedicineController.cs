using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // GET: api/Medicine
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PagedQuery query)
        {
            var result = await _medicineService.GetAllAsync(query);
            return Ok(result);
        }
        
        // GET: api/Medicine/5
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _medicineService.GetByIdAsync(id);
            if (result == null)
                return NotFound("Không tìm thấy thuốc.");
            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetByName([FromBody] MedicineFilter filter)
        {
            var result = await _medicineService.SearchMedicine(filter);
            return Ok(result);
        }

        // POST: api/Medicine
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] MedicineRequestModel model)
        {
            var isCreated = await _medicineService.CreateAsync(model);

            if (!isCreated)
                return BadRequest(new { message = "Dữ liệu thuốc không hợp lệ." });

            return Ok(new { message = "Thêm thuốc thành công." });
        }

        // PUT: api/Medicine/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] MedicineRequestModel model)
        {
            var isUpdated = await _medicineService.UpdateAsync(id, model);

            if (!isUpdated)
                return NotFound(new { message = "Không tìm thấy thuốc." });

            return Ok(new { message = "Cập nhật thuốc thành công." });
        }

        // DELETE: api/Medicine/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _medicineService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(new { message = "Không tìm thấy thuốc." });

            return Ok(new { message = "Xoá thuốc thành công." });
        }
        [HttpGet("get-all-name")]
        public async Task<IActionResult> GetAllMedicines()
        {
            var result = await _medicineService.GetAllMedicineName();
            return Ok(result);
        }
    }
}
