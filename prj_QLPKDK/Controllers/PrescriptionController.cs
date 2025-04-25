using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        // POST: api/Prescription
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrescriptionResquestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _prescriptionService.CreateAsync(model);
            return Ok(new { message = "Tạo đơn thuốc thành công", id = result });
        }

        // DELETE: api/Prescription/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _prescriptionService.DeleteAsync(id);
            return Ok(new { message = result });
        }

        // GET: api/Prescription
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _prescriptionService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Prescription/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _prescriptionService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Không tìm thấy đơn thuốc." });

            return Ok(result);
        }

        // PUT: api/Prescription/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PrescriptionResquestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _prescriptionService.UpdateAsync(id, model);
            return Ok(new { message = result });
        }
    }
}
