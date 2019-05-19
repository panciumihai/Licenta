using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CampusManagement.Business.File
{
    public interface IFileService
    {
        Task<IEnumerable<string>> SaveOnDisk(IList<IFormFile> files, string folderName = "Uploads");
        Task<string> SaveOnDisk(IFormFile file, string folderName = "Uploads");
        void CreateFolder(string folderName);
        Task<MemoryStream> GetFile(string filePath);
        string CreateFullFilePath(string filePath);
        string GetContentType(string path);
        string GetFileNameByPath(string path);
    }
}
