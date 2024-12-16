using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Repositories;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;


namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TaiSanController : ControllerBase
    {
        private readonly TaiSanServices _taiSanServices;
        public TaiSanController(TaiSanServices taiSanServices)
        {
            _taiSanServices = taiSanServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taiSans = await _taiSanServices.GetAllTaiSan();

            // Chuyển đổi entity sang DTO
            var taiSanDTOs = taiSans.Select(t => new TaiSan
            {
                MaTaiSan = t.MaTaiSan,
                TenTaiSan = t.TenTaiSan,
                MaLoaiTaiSan = t.MaLoaiTaiSan,
                NgayMua = t.NgayMua,
                GiaTri = t.GiaTri,
                SoLuong = t.SoLuong,
                TinhTrang = t.TinhTrang,
                HinhAnh = t.HinhAnh,
                MaQr = t.MaQr,
                ThoiGianBaoHanh = t.ThoiGianBaoHanh
            }).ToList();

            return Ok(taiSanDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaiSan taiSanDTO)
        {
            var taiSan = new TaiSan
            {
                MaTaiSan = taiSanDTO.MaTaiSan,
                TenTaiSan = taiSanDTO.TenTaiSan,
                MaLoaiTaiSan = taiSanDTO.MaLoaiTaiSan,
                NgayMua = taiSanDTO.NgayMua,
                GiaTri = taiSanDTO.GiaTri,
                SoLuong = taiSanDTO.SoLuong,
                TinhTrang = taiSanDTO.TinhTrang,
                HinhAnh = taiSanDTO.HinhAnh,
                MaQr = taiSanDTO.MaQr,
                ThoiGianBaoHanh = taiSanDTO.ThoiGianBaoHanh
            };

            await _taiSanServices.AddTaiSan(taiSanDTO);
            return CreatedAtAction(nameof(GetAll), new { id = taiSan.MaTaiSan }, taiSanDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TaiSan taiSanDTO)
        {

            await _taiSanServices.UpdateTaiSan(taiSanDTO);
            return Ok(taiSanDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
                await _taiSanServices.DeleteTaiSan(id);
                return Ok();            
        }
    }
}
