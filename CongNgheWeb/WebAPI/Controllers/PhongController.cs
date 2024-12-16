using ApplicationCore.DTOs;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization; // Thêm namespace này
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ApplicationCore.Entities;
using ApplicationCore.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PhongController : ControllerBase
    {
        private readonly PhongServices _phongServices;

        public PhongController(PhongServices phongServices)
        {
            _phongServices = phongServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Phongs = await _phongServices.GetAllPhong();

            // Chuyển đổi entity sang DTO
            var phongDTOs = Phongs.Select(t => new Phong
            {
                MaPhong = t.MaPhong,
                ViTri = t.ViTri,
                TenPhong = t.TenPhong,
            }).ToList();

            return Ok(phongDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phong phongDTO)
        {
            var phong = new Phong
            {
                MaPhong = phongDTO.MaPhong,
                ViTri = phongDTO.ViTri,
                TenPhong = phongDTO.TenPhong,
            };

            await _phongServices.AddPhong(phongDTO);
            return CreatedAtAction(nameof(GetAll), new { id = phong.MaPhong }, phongDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Phong phongDTO)
        {
            await _phongServices.UpdatePhong(phongDTO);
            return Ok(phongDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
       
                await _phongServices.DeletePhong(id);
                return Ok();
            
        }
    }
}
