using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("by-medical-record/{medicalRecordId}")]
        public async Task<IActionResult> GetByMedicalRecordId(string medicalRecordId)
        {
            try
            {
                var result = await _invoiceService.GetByMedicalRecordIdAsync(medicalRecordId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Trả về mã lỗi 404 hoặc 400 nếu cần
                return NotFound(new { message = ex.Message });
            }
        }


    }
}
