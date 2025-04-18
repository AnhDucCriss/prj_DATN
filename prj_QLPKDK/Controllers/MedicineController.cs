using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // GET: api/Medicine
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicineService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Medicine/5
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _medicineService.GetByIdAsync(id);
            if (result == null)
                return NotFound("Không tìm thấy thuốc.");
            return Ok(result);
        }

        // GET: api/Medicine/search?name=paracetamol
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var result = await _medicineService.GetByNameAsync(name);
            return Ok(result);
        }

        // POST: api/Medicine
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] MedicineRequestModel model)
        {
            var result = await _medicineService.CreateAsync(model);
            return Ok(result);
        }

        // PUT: api/Medicine/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MedicineRequestModel model)
        {
            var result = await _medicineService.UpdateAsync(id, model);
            return Ok(result);
        }

        // DELETE: api/Medicine/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medicineService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
