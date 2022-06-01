using AutoMapper;
using Contact.API.DTOs.Contact;
using Contact.API.Error;
using Contact.Core.Entities.Contacts;
using Contact.Core.Interfaces.Contact;
using Contact.Core.Interfaces.Phone;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace ContactBackend.Controllers
{
    [Authorize]
    public class ContactController : BaseController
    {
        private readonly IContactRepository contactRepository;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IPhoneRepository phoneRepository;

        public ContactController(IContactRepository contactRepository,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            IPhoneRepository phoneRepository)
        {
            this.contactRepository = contactRepository;
            this.mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
            this.phoneRepository = phoneRepository;
        }

        //public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        //{
        //    var basket = await _basketRepository.GetBasketAsync(id);
        //    return Ok(basket ?? new CustomerBasket(id));
        //}

        [HttpGet]
        public IActionResult Get()
        {
            //var lstContact = this.contactRepository.GetAll();
            var lstContact = this.contactRepository.GetContacts();
            return Ok(mapper.Map<IEnumerable<ContactResponseDTO>>(lstContact));
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            var objContact = this.contactRepository.GetById(Id);
            return Ok(mapper.Map<ContactResponseDTO>(objContact));
        }

        [HttpPost]
        public IActionResult Post([FromForm] ContactRequestDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Enter Required fields.");
                }
                var fileName = UploadFile();
                if (!string.IsNullOrEmpty(fileName))
                    dto.PhotoPath = fileName;

                if (!string.IsNullOrEmpty(dto.PhoneStr))
                    dto.Phones = JsonSerializer.Deserialize<List<PhoneNumberDTO>>(dto.PhoneStr);

                var contactEntity = mapper.Map<ContactRequestDTO, ContactEntity>(dto);
                contactEntity.ContactId = Guid.NewGuid();

                foreach (var item in dto.Phones)
                {
                    var phoneEntity = mapper.Map<PhoneNumberDTO, PhoneNumbersEntity>(item);
                    phoneEntity.PhoneNumberId = Guid.NewGuid();
                    phoneEntity.ContactId = contactEntity.ContactId;

                    this.phoneRepository.Insert(phoneEntity);
                }

                this.contactRepository.Insert(contactEntity);
                this.contactRepository.Save();
                return Ok(new ApiResponse(202, "Contact Successfully created."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(502, ex.Message));
            }
        }



        [NonAction]
        public string UploadFile()
        {
            string fileName = "";
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    string folderName = "wwwroot/Resources/Images";
                    string webRootPath = hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }
                return fileName;
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }

        [HttpPut("{Id}")]
        public IActionResult Put(Guid Id, [FromForm] ContactRequestDTO dto)
        {
            try
            {
                var fileName = UploadFile();
                if (!string.IsNullOrEmpty(fileName))
                    dto.PhotoPath = fileName;

                var contactEntity = mapper.Map<ContactRequestDTO, ContactEntity>(dto);
                contactEntity.ContactId = Id;

                if (!string.IsNullOrEmpty(dto.PhoneStr))
                    dto.Phones = JsonSerializer.Deserialize<List<PhoneNumberDTO>>(dto.PhoneStr);

                if (!string.IsNullOrEmpty(dto.RemovedPhonesStr))
                    dto.RemovedPhones = JsonSerializer.Deserialize<List<string>>(dto.RemovedPhonesStr);

                foreach (var item in dto.Phones)
                {
                    var phoneEntity = mapper.Map<PhoneNumberDTO, PhoneNumbersEntity>(item);
                    phoneEntity.ContactId = contactEntity.ContactId;

                    //Add Phone If dosn't exist else Update To Existing
                    if (string.IsNullOrEmpty(item.phoneId))
                    {
                        phoneEntity.PhoneNumberId = Guid.NewGuid();
                        this.phoneRepository.Insert(phoneEntity);
                    }
                    else
                    {
                        phoneEntity.PhoneNumberId = new Guid(item.phoneId);
                        this.phoneRepository.Update(phoneEntity);
                    }
                }


                //Delete All the Removed Phones
                foreach (var phone in dto.RemovedPhones)
                {
                    this.phoneRepository.Delete(new Guid(phone));
                }

                
                this.contactRepository.Update(contactEntity);
                this.contactRepository.Save();
                return Ok(new ApiResponse(200, "Contact Updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(502, ex.Message));
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                this.contactRepository.Delete(Id);
                this.contactRepository.Save();
                return Ok(new ApiResponse(200, "Contact Deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(502, ex.Message));
            }
        }

        [HttpGet("Favourite/{Id}")]
        [AllowAnonymous]
        public IActionResult UpdateFavourite(Guid Id)
        {
            try
            {
                this.contactRepository.MarkFavourite(Id);
                this.contactRepository.Save();
                return Ok(new ApiResponse(200, "Contact Updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(502, ex.Message));
            }
        }
    }
}
