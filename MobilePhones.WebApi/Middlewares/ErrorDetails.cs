using Newtonsoft.Json;

namespace MobilePhones.WebApi.Middlewares
{
    /// <summary>
    /// Represents an error details.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Gets or sets an error status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets an error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Returns string presentation of an <see cref="ErrorDetails"./>
        /// </summary>
        /// <returns>A string presentation of a <see cref="ErrorDetails"/>.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
