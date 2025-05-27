using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Models.Resquest;
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

        [HttpPut("update-payment-status")]
        public async Task<IActionResult> UpdatePaymentStatus([FromBody] InvoiceRequestModel dto)
        {
            try
            {
                var result = await _invoiceService.UpdateAsync(dto);
                return Ok(new { message = "Cập nhật hóa đơn thành công." });
            }
            catch (Exception ex)
            {
                // Trả về mã lỗi 404 hoặc 400 nếu cần
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("export-pdf/{medicalRecordId}")]
        public async Task<IActionResult> ExportInvoicePdf(string medicalRecordId)
        {
            try
            {
                var pdfBytes = await _invoiceService.GenerateInvoicePdfAsync(medicalRecordId);

                var fileName = $"HoaDon_{medicalRecordId}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
