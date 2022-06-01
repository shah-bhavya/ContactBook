using Contact.Core.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Interfaces.Phone
{
    public interface IPhoneRepository
    {
        PhoneNumbersEntity GetById(Guid PhoneId);
        void Insert(PhoneNumbersEntity phone);
        void Update(PhoneNumbersEntity phone);
        void Delete(Guid PhoneId);
        void Save();
    }
}
