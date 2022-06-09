using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleTextProcessor.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimplTextProcessor.Api.Controllers
{
    [Route("api/texts")]
    [ApiController]
    public class TextsController : ControllerBase
    {
        private readonly string _uploadsFolder;
        private readonly IFileInfoConverter _fileInfoConverter;

        public TextsController(IWebHostEnvironment hostingEnvironment, IFileInfoConverter converter)
        {
            _fileInfoConverter = converter;
            var webRootPath = hostingEnvironment.WebRootPath;
            _uploadsFolder = Path.Combine(webRootPath, "uploads");
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        // POST api/texts/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(TextDto dto)
        {
            try
            {
                var fileName = Path.ChangeExtension(dto.Name, ".txt");
                string fullPath = Path.Combine(_uploadsFolder, fileName);
                using (var writer = new StreamWriter(fullPath))
                {
                    await writer.WriteLineAsync(dto.Text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            var result = new
            {
                message = "Upload successful"
            };

            return Ok(result);
        }

        // GET: api/texts/files
        [HttpGet("files")]
        public IActionResult GetFiles()
        {
            var dirInfo = new DirectoryInfo(_uploadsFolder);
            var files = dirInfo.GetFiles();
            var fileDtos = files.Select(f => _fileInfoConverter.Convert(f));
            return Ok(fileDtos);
        }

        // GET: api/texts/file?name={name}
        [HttpGet("file")]
        public async Task<IActionResult> GetFileContent([FromQuery] string name)
        {
            var dirInfo = new DirectoryInfo(_uploadsFolder);
            var files = dirInfo.GetFiles();
            var file = files.FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (file == null)
            {
                return NotFound();
            }
            string fullPath = Path.Combine(_uploadsFolder, name);
            using var sr = new StreamReader(fullPath);
            var content = await sr.ReadToEndAsync();
            var textDto = new TextDto()
            {
                Name = name,
                Text = content
            };

            return Ok(textDto);
        }

        // DELETE: api/texts/file?name={name}
        [HttpDelete("file")]
        public IActionResult DeleteFile([FromQuery] string name)
        {
            var message = "Delete successful";
            var fullPath = Path.Combine(_uploadsFolder, name);
            var file = new FileInfo(fullPath);
            if (file.Exists)
            {
                file.Delete();
            }
            else
            {
                message = "File not found";
            }

            var result = new
            {
                message = message
            };

            return Ok(result);
        }
    }
}
