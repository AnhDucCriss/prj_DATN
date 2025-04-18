﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj_QLPKDK.Models.Resquest;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

       
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _staffService.GetAllAsync();
            return Ok(result);
        }

       
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _staffService.GetByIdAsync(id);
            if (result == null)
                return NotFound("Không tìm thấy nhân viên.");
            return Ok(result);
        }

        
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var result = await _staffService.GetByNameAsync(name);
            return Ok(result);
        }

        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] StaffRequestModel model)
        {
            var result = await _staffService.CreateAsync(model);
            return Ok(result);
        }

        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StaffRequestModel model)
        {
            var result = await _staffService.UpdateAsync(id, model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _staffService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
