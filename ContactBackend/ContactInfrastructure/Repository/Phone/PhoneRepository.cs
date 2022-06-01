using Contact.Core.Entities.Contacts;
using Contact.Core.Interfaces.Phone;
using Contact.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Repository.Phone
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly AppDbContext _context;
        public PhoneRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Delete(Guid PhoneId)
        {
            var obj = _context.PhoneNumbers.Find(PhoneId);
            _context.PhoneNumbers.Remove(obj);
        }

        public PhoneNumbersEntity GetById(Guid ContactId)
        {
            return _context.PhoneNumbers.Find(ContactId);
        }

        public void Insert(PhoneNumbersEntity phone)
        {
            _context.PhoneNumbers.Add(phone);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(PhoneNumbersEntity phone)
        {
            _context.Entry(phone).State = EntityState.Modified;
        }
    }
}
