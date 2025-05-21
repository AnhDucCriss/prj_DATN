using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly WebContext _db;
        public PrescriptionController(IPrescriptionService prescriptionService, WebContext db)
        {
            _prescriptionService = prescriptionService;
            _db = db;
        }

        // POST: api/Prescription
        [HttpPost("create")]
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

        [HttpGet("export-pdf/{medicalRecordId}")]
        public async Task<IActionResult> ExportPdf(string medicalRecordId)
        {
            var pdfBytes = await _prescriptionService.GeneratePrescriptionPdfAsync(medicalRecordId);
            var pres = await _db.Prescriptions.FirstOrDefaultAsync(x => x.MedicalRecordId == medicalRecordId);
            return File(pdfBytes, "application/pdf", $"donthuoccua_{pres.PatientName}.pdf");
        }
        //[HttpPost("add-prescriptiondetail")]
        //public async Task<IActionResult> AddPrescriptionDetail([FromBody] PrescriptionDetailRequest dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _prescriptionService.AddPresDetail(dto);

        //    if (result == "Không có thuốc trong kho thuốc" || result == "Số lượng thuốc trong kho đã hết")
        //    {
        //        return BadRequest(new { message = result });
        //    }

        //    return Ok(new { id = result });
        //}

        [HttpPost("add-detail")]
        public async Task<IActionResult> AddPrescriptionDetail([FromBody] PrescriptionDetailRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var detail = await _prescriptionService.AddPrescriptionDetailAsync(model);
                return Ok(new
                {
                    message = "Thêm thuốc vào đơn thuốc thành công",
                    data = detail
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/Prescription/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PrescriptionResquestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _prescriptionService.UpdateAsync(id, model);
            return Ok(new { message = result });
        }

        [HttpPost("update-details")]
        public async Task<IActionResult> UpdateDetails([FromBody] UpdatePrescriptionDetailsRequest request)
        {
            try
            {
                var result = await _prescriptionService.UpdatePrescriptionDetailsAsync(request);
                return Ok(new { message = "Cập nhật đơn thuốc thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
