using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MobilePhones.Services.Models
{
    /// <summary>
    /// Represents a service for storing images in server folder.
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Saves an image in server folder.
        /// </summary>
        /// <param name="imageFile">A <see cref="IFormFile"/>.</param>
        /// <returns></returns>
        Task<string> SaveImage(IFormFile imageFile);
    }
}
