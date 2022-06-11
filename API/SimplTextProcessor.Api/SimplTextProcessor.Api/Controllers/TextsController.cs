using Microsoft.AspNetCore.Mvc;
using SimpleTextProcessor.Services;
using SimpleTextProcessor.Services.Dto;

namespace SimplTextProcessor.Api.Controllers
{
    [Route("api/texts")]
    [ApiController]
    public class TextsController : ControllerBase
    {
        private readonly string _uploadsFolder;
        private readonly ITextProcessor _textProcessor;

        public TextsController(IWebHostEnvironment hostingEnvironment, ITextProcessor textProcessor)
        {
            _textProcessor = textProcessor;
            var webRootPath = hostingEnvironment.WebRootPath;
            _uploadsFolder = Path.Combine(webRootPath, "uploads");
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        // GET: api/texts/files
        [HttpGet("files")]
        public IActionResult GetFiles()
        {
            var fileDtos = _textProcessor.ExecuteGetFiles(_uploadsFolder);
            return Ok(fileDtos);
        }

        // POST api/texts/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(TextDto dto)
        {
            await _textProcessor.ExecuteUploadAsync(dto, _uploadsFolder);
            var result = new
            {
                message = "Upload successful"
            };

            return Ok(result);
        }

        // GET: api/texts/download/{name}/start/{start:int}/chunk-size/{chunkSize}"
        [HttpGet("download/{name}/start/{start:int}/chunk-size/{chunkSize}")]
        public async Task<IActionResult> Download(string name, int start, int chunkSize)
        {
            var textDto = await _textProcessor.ExecuteDownloadAsync(name, start, chunkSize, _uploadsFolder);
            if (textDto == null)
            {
                return NotFound();
            }

            return Ok(textDto);
        }

        // DELETE: api/texts/file?name={name}
        [HttpDelete("file")]
        public IActionResult DeleteFile([FromQuery] string name)
        {
            var message = _textProcessor.ExecuteDeleteFile(name, _uploadsFolder);
            var result = new
            {
                message
            };

            return Ok(result);
        }
    }
}
