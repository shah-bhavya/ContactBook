using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Entities.Contacts
{
    public class ContactPhoneEntity
    {
        public Guid ContactId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public bool IsFavourite { get; set; }
    }
}
