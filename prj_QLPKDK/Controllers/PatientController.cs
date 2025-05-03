using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.FilterResquest;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/patient")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Lấy danh sách tất cả bệnh nhân
        /// </summary>
        [HttpPost("get-all")]
        public async Task<ActionResult<List<Patients>>> GetAll([FromBody] PagedQuery query)
        {
            var patients = await _patientService.GetAllAsync(query);
            return Ok(patients);
        }

        /// <summary>
        /// Lấy thông tin bệnh nhân theo ID
        /// </summary>
        [HttpGet("get-patient-by-id/{id}")]
        public async Task<ActionResult<Patients>> GetById(string id)
        {
            try
            {
                var patient = await _patientService.GetByIdAsync(id);
                return Ok(patient);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("get-medicalrecords/{patientId}")]
        public async Task<IActionResult> GetByPatientId(string patientId, [FromBody] PagedQuery paged)
        {
            var records = await _patientService.GetMedicalRecordsAsync(patientId, paged);

            if (records == null)
            {
                return Ok(new { message = "Bệnh nhân chưa có hồ sơ khám bệnh trên hệ thống" });
            }

            return Ok(records);
        }
        /// <summary>
        /// Thêm mới bệnh nhân
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<string>> Create([FromBody] PatientRequestModel model)
        {
            try
            {
                var id = await _patientService.CreateAsync(model);
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
        [HttpPut("update/{id}")]
        public async Task<ActionResult<string>> Update(string id, [FromBody] PatientRequestModel model)
        {
            try
            {
                var message = await _patientService.UpdateAsync(id, model);
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
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            try
            {
                var message = await _patientService.DeleteAsync(id);
                return Ok(new { message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPost("search")]
        public async Task<ActionResult<string>> SearchAsync([FromBody] PatientFilter filter)
        {
            try
            {
                var message = await _patientService.SearchPatient(filter);
                return Ok(message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
