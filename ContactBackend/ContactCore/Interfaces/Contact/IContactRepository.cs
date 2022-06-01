using Contact.Core.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Interfaces.Contact
{
    public interface IContactRepository
    {
        IEnumerable<ContactEntity> GetAll();
        List<ContactPhoneEntity> GetContacts();
        ContactPhoneEntity GetById(Guid ContactID);

        void MarkFavourite(Guid ContactID);
        void Insert(ContactEntity contact);
        void Update(ContactEntity contact);
        void Delete(Guid ContactID);
        void Save();

    }
}
