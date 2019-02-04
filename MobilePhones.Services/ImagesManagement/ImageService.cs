using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MobilePhones.Services.Models;

namespace MobilePhones.Services
{
    public class ImageService : IImageService
    {
        private readonly string _imagesPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class with specified path./>.
        /// </summary>
        /// <param name="imagesPath">A path to images.</param>
        public ImageService(string imagesPath)
        {
            _imagesPath = imagesPath;
        }

        /// <inheritdoc/>
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string newImagePath = GetImagePath(imageFile, _imagesPath);
            using (var fileStream = new FileStream(newImagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            };

            return newImagePath;
        }

        private string GetImagePath(IFormFile image, string folderName)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.FileName);
            string extension = Path.GetExtension(image.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            return folderName + "/" + fileName;
        }
    }
}
