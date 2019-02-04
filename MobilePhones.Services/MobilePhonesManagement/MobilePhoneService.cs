using AutoMapper;
using Microsoft.AspNetCore.Http;
using MobilePhones.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhones.Services
{
    /// <summary>
    /// Represents a mobile phone service.
    /// </summary>
    public class MobilePhoneService : IMobilePhoneService
    {
        private readonly MobilePhones.DataAccess.IMobilePhonesJsonRepository _repository;
        private readonly IImageService _imageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobilePhoneService"/> class with specified <see cref="MobilePhones.DataAccess.IMobilePhonesJsonRepository"/>.
        /// </summary>
        /// <param name="repository">A <see cref="MobilePhones.DataAccess.IMobilePhonesJsonRepository"/>.</param>
        /// <param name="imageService">A <see cref="IImageService"./></param>
        public MobilePhoneService(MobilePhones.DataAccess.IMobilePhonesJsonRepository repository, IImageService imageService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        }

        /// <inheritdoc/>
        public List<MobilePhone> GetMobilePhones(int start, int amount)
        {
            return Mapper.Map<List<MobilePhone>>(_repository.MobilePhones.Skip(start).Take(amount).ToList());
        }

        /// <inheritdoc/>
        public MobilePhone GetMobilePhone(int mobilePhoneId)
        {
            var mobilePhone = _repository.MobilePhones.FirstOrDefault(ph => ph.Id == mobilePhoneId);
            if (mobilePhone == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<MobilePhone>(mobilePhone);
        }

        /// <inheritdoc/>
        public async Task<MobilePhone> CreateMobilePhoneAsync(CreateMobilePhoneRequest createRequest)
        {            
            var mobilePhones = Mapper.Map<List<MobilePhone>>(_repository.MobilePhones);
            int id = mobilePhones.Count == 0 ? 1 : mobilePhones.Max(ph => ph.Id) + 1;

            var mobilePhone = new MobilePhone
            {
                Id = id,
                Name = createRequest.Name,
                PhotoPath = await _imageService.SaveImage(createRequest.PhotoFile)
            };
            mobilePhones.Add(mobilePhone);
            _repository.Save(Mapper.Map<List<DataAccess.MobilePhone>>(mobilePhones));
            return mobilePhone;
        }

        /// <inheritdoc/>
        public async Task<MobilePhone> UpdateMobilePhoneAsync(int mobilePhoneId, UpdateMobilePhoneRequest updateRequest)
        {
            var mobilePhones = Mapper.Map<List<MobilePhone>>(_repository.MobilePhones);

            foreach (var mobilePhone in mobilePhones)
            {
                if (mobilePhone.Id == mobilePhoneId)
                {
                    mobilePhone.Name = updateRequest.Name;

                    if (updateRequest.PhotoFile != null)
                        mobilePhone.PhotoPath = await _imageService.SaveImage(updateRequest.PhotoFile);

                    _repository.Save(Mapper.Map<List<DataAccess.MobilePhone>>(mobilePhones));
                    return mobilePhone;
                }
            }

            throw new RequestedResourceNotFoundException();
        }

        /// <inheritdoc/>
        public void DeleteMobilePhone(int mobilePhoneId)
        {
            var mobilePhones = Mapper.Map<List<MobilePhone>>(_repository.MobilePhones);

            var mobilePhone = mobilePhones.FirstOrDefault(ph => ph.Id == mobilePhoneId);
            if (mobilePhone == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            mobilePhones.Remove(mobilePhone);
            _repository.Save(Mapper.Map<List<DataAccess.MobilePhone>>(mobilePhones));
        }
    }
}
