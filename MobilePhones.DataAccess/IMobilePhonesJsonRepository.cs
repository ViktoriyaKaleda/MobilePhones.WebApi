using System;
using System.Collections.Generic;

namespace MobilePhones.DataAccess
{
    /// <summary>
    /// Represents a mobile phone repository for storing data in json file.
    /// </summary>
    public interface IMobilePhonesJsonRepository
    {
        /// <summary>
        /// Gets a lost of mobile phones.
        /// </summary>
        List<MobilePhone> MobilePhones { get; }

        /// <summary>
        /// Saves list of mobile phones in json file.
        /// </summary>
        /// <param name="items">A <see cref="List{MobilePhone}"./></param>
        void Save(List<MobilePhone> items);
    }
}
