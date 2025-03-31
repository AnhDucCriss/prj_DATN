using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Services;

namespace prj_QLPKDK.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Lấy danh sách tất cả bệnh nhân
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Patients>>> GetAll()
        {
            var patients = await _patientService.GetAll();
            return Ok(patients);
        }

        /// <summary>
        /// Lấy thông tin bệnh nhân theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Patients>> GetById(int id)
        {
            try
            {
                var patient = await _patientService.GetById(id);
                return Ok(patient);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Thêm mới bệnh nhân
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] Patients model)
        {
            try
            {
                var id = await _patientService.Create(model);
                return CreatedAtAction(nameof(GetById), new { id }, new { message = "Thêm thành công", id });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật bệnh nhân theo ID
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(int id, [FromBody] Patients model)
        {
            try
            {
                var message = await _patientService.Update(id, model);
                return Ok(new { message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Xóa bệnh nhân theo ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var message = await _patientService.Delete(id);
                return Ok(new { message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
