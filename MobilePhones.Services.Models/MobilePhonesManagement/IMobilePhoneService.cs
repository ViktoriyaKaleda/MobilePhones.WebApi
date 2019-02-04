using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobilePhones.Services.Models
{
    /// <summary>
    /// Represents a mobile phone service.
    /// </summary>
    public interface IMobilePhoneService
    {
        /// <summary>
        /// Gets a mobile phones list.
        /// </summary>
        /// <param name="start">A start.</param>
        /// <param name="amount">An amount.</param>
        /// <returns>A <see cref="List{MobilePhone}"/>.</returns>
        List<MobilePhone> GetMobilePhones(int start, int amount);

        /// <summary>
        /// Gets a mobile phone with specified identifier.
        /// </summary>
        /// <param name="mobilePhoneId">A mobile phone identifier.</param>
        /// <returns>A <see cref="MobilePhone"/>.</returns>
        MobilePhone GetMobilePhone(int mobilePhoneId);

        /// <summary>
        /// Creates a new mobile phone.
        /// </summary>
        /// <param name="createRequest">A <see cref="CreateMobilePhoneRequest"/>.</param>
        /// <returns>A <see cref="Task{MobilePhone}"/>.</returns>
        Task<MobilePhone> CreateMobilePhoneAsync(CreateMobilePhoneRequest createRequest);

        /// <summary>
        /// Updates an existed mobile phone.
        /// </summary>
        /// <param name="mobilePhoneId">A mobile phone identifier.</param>
        /// <param name="updateRequest">A <see cref="UpdateMobilePhoneRequest"/>.</param>
        /// <returns>A <see cref="Task{MobilePhone}"/>.</returns>
        Task<MobilePhone> UpdateMobilePhoneAsync(int mobilePhoneId, UpdateMobilePhoneRequest updateRequest);

        /// <summary>
        /// Deletes an existed mobile phone.
        /// </summary>
        /// <param name="mobilePhoneId">A mobile phone identifier.</param>
        void DeleteMobilePhone(int mobilePhoneId);
    }
}
