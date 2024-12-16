
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApplicationCore.DTOs;

namespace ApplicationCore.Services
{
    public class AuthServices
    {
        private readonly INguoiDungRepository _nguoiDungReponsitory;
        private readonly IConfiguration _configuration;

        public AuthServices(INguoiDungRepository nguoiDungRepository, IConfiguration configuration)
        {
            _nguoiDungReponsitory = nguoiDungRepository;
            _configuration = configuration;
        }

        private string GenerateJwtToken(NguoiDung user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.HoTen),
                new Claim("MaNguoiDung", user.MaNguoiDung.ToString()),
                new Claim(ClaimTypes.Role, user.VaiTro),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                //issuer: _configuration["Jwt:Issuer"],
                //audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResult> LoginAsync(UserLogin loginDTO)
        {
            var taiKhoan = await _nguoiDungReponsitory.GetByEmailAndPasswordAsync(loginDTO.Email,loginDTO.MatKhau);

            if (taiKhoan == null)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Email không tồn tại."
                };
            }

            if (loginDTO.MatKhau != taiKhoan.MatKhau)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Thông tin đăng nhập không chính xác."
                };
            }

            //var nguoiDung = await _nguoiDungReponsitory.GetNguoiDungByTaiKhoanIdAsync(taiKhoan.MaTk);
            var token = GenerateJwtToken(taiKhoan);

            return new LoginResult
            {
                Success = true,
                Token = token,
                Message = "Đăng nhập thành công.",
                MaNguoiDung = taiKhoan.MaNguoiDung,
                Email = taiKhoan.Email,
                HoTen = taiKhoan.HoTen,
                VaiTro = taiKhoan.VaiTro
            };
        }

    }

}
