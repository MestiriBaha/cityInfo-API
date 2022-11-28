using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace FirstApiCreated.Controllers
{
    [Route("api/files")]
    [Authorize]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider; 
        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider   ??  throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }
        [HttpGet("{fileId}")]

        public IActionResult getFiles ( string fileId )
        {
            var pathFile = "baha.pdf";
            if(!System.IO.File.Exists(pathFile))
            { return NotFound();  }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(pathFile, out var contentType))
            {
                contentType = "application/octet-stream";
            } ;

            var files = System.IO.File.ReadAllBytes(pathFile);
            return File(files, contentType, Path.GetFileName(pathFile));


        }       
    }
}
