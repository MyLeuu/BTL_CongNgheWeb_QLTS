using ApplicationCore.DTOs;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Services;
using Infrastructure.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaoTriController : ControllerBase
    {
        private readonly BaoTriServices _baoTriServices ;
        public BaoTriController(BaoTriServices baoTriRepository)
        {
            _baoTriServices = baoTriRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var baoTris = await _baoTriServices.GetAllBaoTri();

            // Chuyển đổi entity sang DTO
            var baoTriDTOs = baoTris.Select(t => new BaoTri
            {
                MaBaoTri = t.MaBaoTri,
                MaPhanBo = t.MaPhanBo,
                NgayBaoTri = t.NgayBaoTri,
                TrangThai = t.TrangThai,
                SoLuongBaoTri= t.SoLuongBaoTri,
                NgayHoanThanh = t.NgayHoanThanh,
                NguoiThucHien = t.NguoiThucHien
            }).ToList();

            return Ok(baoTriDTOs);
        }

        // API thêm người dùng
        [HttpPost]
        public async Task<IActionResult> Create(BaoTri baoTriDTO)
        {
            var baoTri = new BaoTri
            {
                MaBaoTri = baoTriDTO.MaBaoTri,
                MaPhanBo = baoTriDTO.MaPhanBo,
                NgayBaoTri = baoTriDTO.NgayBaoTri,
                TrangThai = baoTriDTO.TrangThai,
                SoLuongBaoTri = baoTriDTO.SoLuongBaoTri,
                NgayHoanThanh = baoTriDTO.NgayHoanThanh,
                NguoiThucHien = baoTriDTO.NguoiThucHien
            };

            await _baoTriServices.AddBaoTri(baoTriDTO);
            return CreatedAtAction(nameof(GetAll), new { id = baoTri.MaBaoTri }, baoTriDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BaoTri baoTriDTO)
        {

            await _baoTriServices.UpdateBaoTri(baoTriDTO);
            return Ok(baoTriDTO);
        }

        // API xóa người dùng
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _baoTriServices.DeleteBaoTri(id);
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
