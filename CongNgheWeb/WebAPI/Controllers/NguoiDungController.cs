using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly NguoiDungServices _nguoiDungServices;
        private readonly AuthServices _authServices;

        public NguoiDungController(NguoiDungServices nguoiDungServices, AuthServices authServices)
        {
            _nguoiDungServices = nguoiDungServices;
            _authServices = authServices;

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var nguoiDungs = await _nguoiDungServices.GetAllNguoiDung();

            // Chuyển đổi entity sang DTO
            var nguoiDungDTOs = nguoiDungs.Select(t => new NguoiDung
            {
                MaNguoiDung = t.MaNguoiDung,
                HoTen = t.HoTen,
                SoDienThoai = t.SoDienThoai,
                Email = t.Email,
                VaiTro = t.VaiTro,
                MatKhau = t.MatKhau
            }).ToList();

            return Ok(nguoiDungDTOs);
        }

        // API thêm người dùng
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(NguoiDung nguoiDungDTO)
        {
            var nguoiDung = new NguoiDung
            {
                HoTen = nguoiDungDTO.HoTen,
                SoDienThoai = nguoiDungDTO.SoDienThoai,
                Email = nguoiDungDTO.Email,
                VaiTro = nguoiDungDTO.VaiTro,
                MatKhau = nguoiDungDTO.MatKhau
            };

            await _nguoiDungServices.AddNguoiDung(nguoiDungDTO);
            return CreatedAtAction(nameof(GetAll), new { id = nguoiDung.MaNguoiDung }, nguoiDungDTO);
        }

        // API sửa người dùng
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update( NguoiDung nguoiDungDTO)
        {
            await _nguoiDungServices.UpdateNguoiDung(nguoiDungDTO);
            return Ok(nguoiDungDTO);
        }

        // API xóa người dùng
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var nguoiDung = await _nguoiDungServices.GetNguouDungById(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            await _nguoiDungServices.DeleteNguoiDung(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin taiKhoan)
        {
            var result = await _authServices.LoginAsync(taiKhoan);

            if (!result.Success)
            {
                return Unauthorized(new { message = result.Message });
            }

            return Ok(new
            {
                token = result.Token,
                message = result.Message,
                maNguoiDung = result.MaNguoiDung,
                email = result.Email,
                hoTen = result.HoTen,
                vaiTro = result.VaiTro
            });
        }


    }

}
