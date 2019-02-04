using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MobilePhones.DataAccess
{
    /// <summary>
    /// Represents a mobile phone json repository.
    /// </summary>
    public class MobilePhonesRepository : IMobilePhonesJsonRepository
    {
        private string _filePath;

        private List<MobilePhone> _mobilePhones;

        /// <inheritdoc/>
        public List<MobilePhone> MobilePhones { get => _mobilePhones; }

        /// <summary>
        ///  Initializes a new instance of the <see cref="MobilePhonesRepository"/> class with specified file path for storing data.
        /// </summary>
        /// <param name="filePath">A path to json file for storing data.</param>
        public MobilePhonesRepository(string filePath)
        {
            _filePath = filePath;
            _mobilePhones = Load();
        }

        /// <inheritdoc/>
        public void Save(List<MobilePhone> mobilePhones)
        {
            _mobilePhones = mobilePhones;

            string jsonData = JsonConvert.SerializeObject(mobilePhones, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }

        /// <summary>
        /// Loads mobile phones from json file.
        /// </summary>
        /// <returns>A <see cref="List{MobilePhone}"/>.</returns>
        private List<MobilePhone> Load()
        {
            string jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<MobilePhone>>(jsonData) ?? new List<MobilePhone>();
        }
    }
}
