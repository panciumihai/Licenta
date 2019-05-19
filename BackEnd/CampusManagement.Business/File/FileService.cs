using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CampusManagement.Business.File
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileService(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public async Task<IEnumerable<string>> SaveOnDisk(IList<IFormFile> files, string folderName = "Uploads")
        {
            if (files.Count == 0)
                return null;

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, folderName);
            var filePaths = new List<string>();

            foreach (var file in files)
            {
                if (file.Length <= 0) continue;
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    filePaths.Add(Path.Combine(folderName, file.FileName));
                }
            }

            return filePaths;
        }

        public async Task<string> SaveOnDisk(IFormFile file, string folderName = "Uploads")
        {
            if (file == null)
                return null;

            var relativePath = Path.Combine(folderName, file.FileName);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return relativePath;
        }

        public void CreateFolder(string folderName)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, folderName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public string CreateFullFilePath(string filePath)
        {
            return Path.Combine(_hostingEnvironment.WebRootPath, filePath);
        }

        public string GetFileNameByPath(string path)
        {
            return Path.GetFileName(path);
        }

        public async Task<MemoryStream> GetFile(string fullFilePath)
        {
            if (fullFilePath == null)
                return null;

            var memory = new MemoryStream();
            using (var stream = new FileStream(fullFilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return memory;
            //return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats.officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
