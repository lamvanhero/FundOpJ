using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OppJar.Common.Helpers;
using OppJar.Core.Exceptions;
using OppJar.Domain;
using OppJar.Common.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OppJar.Core.Services
{
    public class FileService : ServiceBase, IFileService
    {
        public FileService(IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, configuration)
        {
        }

        public async Task<FileStreamResult> GetFileAsync(string fileName)
        {
            var file = await _unitOfWork.MediaFileRepository
                .FindAll(x => x.FileName.Equals(fileName))
                .FirstOrDefaultAsync();

            if (file == null) throw new NotFoundException("Not found.");

            FileStream stream = new FileStream(file.DiskPath, FileMode.Open);

            return new FileStreamResult(stream, file.Extension);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string userId = null, UsingForType type = UsingForType.Avatar)
        {
            var fileName = file.FileName.RemoveSpecialCharacter();

            string fileUrl = "";

            string diskPath = string.Empty;

            var contentType = MimeTypes.GetMimeType(fileName);

            var extension = Path.GetExtension(fileName);

            Enum.TryParse(contentType.GetFileTypeByContentType(), out FileType fileType);

            fileName = $"{Guid.NewGuid().ToString()}{extension}";

            FileHelper.SaveFile(ref diskPath, ref fileUrl, ref fileName, userId ?? UserId, file);

            var dbFile = new MediaFile
            {
                UserId = string.IsNullOrEmpty(userId) ? UserId : userId,
                DiskPath = diskPath,
                FileName = fileName,
                Url = fileUrl,
                Extension = contentType,
                CreatedDate = DateTime.UtcNow,
                FileType = fileType,
                UsingForType = type
            };

            await CreateFileInformationAsync(dbFile);

            return $"{ApiUrl}{fileUrl}";
        }

        public async Task<List<string>> UploadMultiFileAsync(IFormFileCollection files, UsingForType type = UsingForType.Avatar)
        {
            List<string> result = new List<string>();

            foreach (var file in files)
            {
                result.Add(await UploadFileAsync(file));
            }

            return result;
        }

        private async Task CreateFileInformationAsync(MediaFile file)
        {
            _unitOfWork.MediaFileRepository.Add(file);

            await _unitOfWork.CommitAsync();
        }

        private async Task UpdateFileInformationAsync(MediaFile file, string diskPath, string fileName, string fileUrl)
        {
            file.DiskPath = diskPath;
            file.FileName = fileName;
            file.Url = fileUrl;

            _unitOfWork.MediaFileRepository.Update(file);

            await _unitOfWork.CommitAsync();
        }
    }
}
