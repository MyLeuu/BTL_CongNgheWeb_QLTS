using ApplicationCore.DTOs;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PhanBoTaiSanController : ControllerBase
    {
        private readonly PhanBoTaiSanServices _phanBoTaiSanServices;
        public PhanBoTaiSanController(PhanBoTaiSanServices phanBoTaiSanServices)
        {
            _phanBoTaiSanServices = phanBoTaiSanServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var PhanBoTaiSans = await _phanBoTaiSanServices.GetAllPhanBoTS();

            // Chuyển đổi entity sang DTO
            var phanBoTSDTOs = PhanBoTaiSans.Select(t => new PhanBoTaiSan
            {
                MaPhanBo = t.MaPhanBo,
                MaTaiSan = t.MaTaiSan,
                MaPhong = t.MaPhong,
                SoLuongPhanBo = t.SoLuongPhanBo,
                SoLuongDangBaoTri = t.SoLuongDangBaoTri,
                TinhTrang = t.TinhTrang,
            }).ToList();

            return Ok(phanBoTSDTOs);
        }

        // API thêm người dùng
        [HttpPost]
        public async Task<IActionResult> Create(PhanBoTaiSan phanBoTaiSanDTO)
        {
            var phanBoTS = new PhanBoTaiSan
            {
                MaPhanBo = phanBoTaiSanDTO.MaPhanBo,
                MaTaiSan = phanBoTaiSanDTO.MaTaiSan,
                MaPhong = phanBoTaiSanDTO.MaPhong,
                SoLuongPhanBo = phanBoTaiSanDTO.SoLuongPhanBo,
                SoLuongDangBaoTri = phanBoTaiSanDTO.SoLuongDangBaoTri,
                TinhTrang = phanBoTaiSanDTO.TinhTrang,
            };

            await _phanBoTaiSanServices.AddPhanBoTS(phanBoTaiSanDTO);
            return CreatedAtAction(nameof(GetAll), new { id = phanBoTS.MaPhanBo }, phanBoTaiSanDTO);
        }

        // API sửa người dùng
        [HttpPut]
        public async Task<IActionResult> Update(PhanBoTaiSan phanBoTaiSanDTO)
        {

            await _phanBoTaiSanServices.UpdatePhanBoTS(phanBoTaiSanDTO);
            return Ok(phanBoTaiSanDTO);
        }

        // API phân bổ
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
                await _phanBoTaiSanServices.DeletePhanBoTS(id);
                return Ok();
        }

    }
}
