using System.Threading.Tasks;
using CampusManagement.Business.File;
using Microsoft.AspNetCore.Mvc;

namespace CampusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("{*filePath}")]
        public async Task<IActionResult> GetFile(string filePath)
        {
            var fullPath = _fileService.CreateFullFilePath(filePath);

            var memory = await _fileService.GetFile(fullPath);
            if (memory == null)
                return NoContent();

            return File(memory, 
                _fileService.GetContentType(fullPath),
                _fileService.GetFileNameByPath(fullPath));
        }
    }
}