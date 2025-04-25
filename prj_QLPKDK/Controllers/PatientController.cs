using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/patient")]
    [ApiController]
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
        [HttpGet("get-patients")]
        public async Task<ActionResult<List<Patients>>> GetAll()
        {
            var patients = await _patientService.GetAll();
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
                var patient = await _patientService.GetById(id);
                return Ok(patient);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("get-medicalrecords/{patientId}")]
        public async Task<IActionResult> GetByPatientId(string patientId)
        {
            var records = await _patientService.GetListMC(patientId);

            if (records == null || !records.Any())
            {
                return Ok(new { message = "Bệnh nhân chưa có hồ sơ khám bệnh trên hệ thống" });
            }

            return Ok(records);
        }
        /// <summary>
        /// Thêm mới bệnh nhân
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<string>> Create([FromBody] PatientResquestModel model)
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
        [HttpPut("update/{id}")]
        public async Task<ActionResult<string>> Update(string id, [FromBody] PatientResquestModel model)
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
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> Delete(string id)
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
