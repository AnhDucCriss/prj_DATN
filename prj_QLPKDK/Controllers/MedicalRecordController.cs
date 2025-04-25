using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        // GET: api/MedicalRecord
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicalRecordService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/MedicalRecord/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _medicalRecordService.GetByIdAsync(id);
            if (result == null)
                return NotFound($"Không tìm thấy hồ sơ khám bệnh với ID: {id}");

            return Ok(result);
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalRecordRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _medicalRecordService.CreateAsync(model);
            return Ok(new { message = "Tạo hồ sơ khám bệnh thành công", id });
        }

        // PUT: api/MedicalRecord/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] MedicalRecordRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var message = await _medicalRecordService.UpdateAsync(id, model);
            return Ok(new { message });
        }

        // DELETE: api/MedicalRecord/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var message = await _medicalRecordService.DeleteAsync(id);
            return Ok(new { message });
        }
    }
}
