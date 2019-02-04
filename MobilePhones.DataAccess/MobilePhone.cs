namespace MobilePhones.DataAccess
{
    /// <summary>
    /// Represents a mobile phone.
    /// </summary>
    public class MobilePhone
    {
        /// <summary>
        /// Gets or sets a mobile phone identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a mobile phone name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a mobile phone path to photo.
        /// </summary>
        public string PhotoPath { get; set; }
    }
}
