using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]/[action]")]
[ApiController]
public class UploadController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Đường dẫn thư mục uploads trong wwwroot
        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploadsPath))
        {
            Directory.CreateDirectory(uploadsPath); // Tạo thư mục nếu chưa có
        }

        // Tạo tên file duy nhất
        var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsPath, fileName);

        // Lưu file vào wwwroot/uploads
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Trả về URL file tĩnh
        var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
        return Ok(new { filePath = fileUrl });
    }
}
