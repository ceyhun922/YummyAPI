using Microsoft.AspNetCore.Mvc;

namespace YummyAPI.Controllers
{
    public class UploadImageRequest
    {
        public IFormFile File { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class FileImageController : ControllerBase
    {
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile([FromForm] UploadImageRequest request)
        {
            var file = request.File;
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return Ok(new { fileName });
        }
    }
}
