using Contact.API.Util.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contact.API.DTOs.Contact
{
    public class ContactRequestDTO
    {
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
        public List<PhoneNumberDTO> Phones { get; set; } = new List<PhoneNumberDTO>();
        public List<string> RemovedPhones { get; set; } = new List<string>();
        public string PhoneStr { get; set; }
        public string RemovedPhonesStr { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
    }

    public class PhoneNumberDTO
    {
        public string phoneId { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string phoneType { get; set; }
    }
}
