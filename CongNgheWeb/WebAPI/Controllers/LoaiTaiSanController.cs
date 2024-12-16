using ApplicationCore.DTOs;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LoaiTaiSanController : ControllerBase
    {
        private readonly LoaiTaiSanServices _loaiTaiSanServices;
        public LoaiTaiSanController(LoaiTaiSanServices loaiTaiSanServices)
        {
            _loaiTaiSanServices = loaiTaiSanServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loaiTaiSans = await _loaiTaiSanServices.GetAllLoaiTS();

            // Chuyển đổi entity sang DTO
            var loaiTSDTOs = loaiTaiSans.Select(t => new LoaiTaiSan   
            {
                MaLoaiTaiSan = t.MaLoaiTaiSan,
                TenLoaiTaiSan = t.TenLoaiTaiSan,
                MoTa = t.MoTa,
            }).ToList();

            return Ok(loaiTSDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoaiTaiSan loaiTaiSanDTO)
        {
            var loaiTS = new LoaiTaiSan
            {
                MaLoaiTaiSan = loaiTaiSanDTO.MaLoaiTaiSan,
                TenLoaiTaiSan = loaiTaiSanDTO.TenLoaiTaiSan,
                MoTa = loaiTaiSanDTO.MoTa,
            };

            await _loaiTaiSanServices.AddLoaiTS(loaiTaiSanDTO);
            return CreatedAtAction(nameof(GetAll), new { id = loaiTS.MaLoaiTaiSan }, loaiTaiSanDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(LoaiTaiSan loaiTaiSanDTO)
        {

            await _loaiTaiSanServices.UpdateLoaiTS(loaiTaiSanDTO);
            return Ok(loaiTaiSanDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _loaiTaiSanServices.DeleteLoaiTS(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ khác nếu có
                return StatusCode(500, "Đã xảy ra lỗi khi xóa tài sản.");
            }
        }
    }
}