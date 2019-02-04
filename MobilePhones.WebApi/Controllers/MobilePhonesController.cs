using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MobilePhones.Services.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MobilePhones.WebApi.Controllers
{
    [Route("api/phones")]
    [ApiController]
    public class MobilePhonesController : ControllerBase
    {
        private readonly IMobilePhoneService _mobilePhoneService;

        public MobilePhonesController(IMobilePhoneService mobilePhoneService)
        {
            _mobilePhoneService = mobilePhoneService;
        }
        
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a list of mobile phones.", Type = typeof(MobilePhone[]))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<IEnumerable<MobilePhone>> GetMobilePhones([FromQuery] int start = 0, [FromQuery] int amount = 100)
        {
            if (start < 0)
            {
                return BadRequest("start");
            }
            if (amount < 0)
            {
                return BadRequest("end");
            }

            var mobilePhones = _mobilePhoneService.GetMobilePhones(start, amount);
            return Ok(mobilePhones);
        }
        
        [HttpGet]
        [Route("{id:int:min(1)}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a mobile phone.", Type = typeof(MobilePhone))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<MobilePhone> GetMobilePhone(int id)
        {
            var mobilePhone = _mobilePhoneService.GetMobilePhone(id);
            return Ok(mobilePhone);
        }
        
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = "Creates a new mobile phone.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromForm] CreateMobilePhoneRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mobilePhone = await _mobilePhoneService.CreateMobilePhoneAsync(createRequest);
            var path = String.Format("/api/phones/{0}", mobilePhone.Id);
            return Created(path, mobilePhone);
        }
        
        [HttpPut("{id:int:min(1)}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Updates an existed mobile phone.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put([FromRoute]int id, [FromForm] UpdateMobilePhoneRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mobilePhone = await _mobilePhoneService.UpdateMobilePhoneAsync(id, updateRequest);
            return NoContent();
        }
        
        [HttpDelete("{id:int:min(1)}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Deletes an existed mobile phone.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Delete(int id)
        {
            _mobilePhoneService.DeleteMobilePhone(id);
            return NoContent();
        }

        [HttpGet("{id:int:min(1)}/image")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Returns mobile phone image.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetMobilePhoneImage(int id)
        {
            var mobilePhone = _mobilePhoneService.GetMobilePhone(id);
            try
            {
                var file = System.IO.File.OpenRead(mobilePhone.PhotoPath);
                return File(file, "image/jpeg");
            }
            catch(FileNotFoundException e)
            {
                return NotFound();
            }            
        }
    }
}
