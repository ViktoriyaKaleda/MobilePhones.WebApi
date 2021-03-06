﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MobilePhones.Services.Models
{
    /// <summary>
    /// Represents a request for updating a mobile phone.
    /// </summary>
    public class UpdateMobilePhoneRequest
    {
        /// <summary>
        /// Gets or sets a mobile phone name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a mobile phone path to photo.
        /// </summary>
        [DataType(DataType.Upload)]
        public IFormFile PhotoFile { get; set; }
    }
}
