using Contact.Core.Entities.Contacts;
using Contact.Core.Interfaces.Contact;
using Contact.Core.Extensions;
using Contact.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Contact.Infrastructure.Repository.Contact
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;
        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Delete(Guid ContactID)
        {
            var obj = _context.Contacts.Find(ContactID);
            _context.Contacts.Remove(obj);
        }

        public IEnumerable<ContactEntity> GetAll()
        {
            return _context.Contacts.ToList();

            //return (from contact in _context.Contacts
            //        join phone in _context.PhoneNumbers
            //        on contact.ContactId equals phone.ContactId
            //        select new ContactPhoneEntity { 
            //            ContactId = contact.ContactId,
            //            Email = contact.Email,
            //            Name = contact.Name,
            //            Number = phone.Number,
            //            Title = contact.Title,
            //            PhoneNumberId = phone.PhoneNumberId,
            //            ContactType = phone.ContactType,
            //        }).ToList();
        }

        public List<ContactPhoneEntity> GetContacts()
        {
            List<ContactPhoneEntity> list = new List<ContactPhoneEntity>();
            DbCommand cmd;
            DbDataReader rdr;

            string sql = "EXEC GetAllContacts";

            cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sql;

            _context.Database.OpenConnection();
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                list.Add(new ContactPhoneEntity
                {
                    ContactId = rdr.GetGuid(0),
                    Name = rdr.GetString(1) ?? "",
                    Email = rdr.GetString(2) ?? "",
                    PhotoPath = !rdr.IsDBNull(3) ? rdr.GetString(3) : "",
                    Title = rdr.GetString(4) ?? "",
                    IsFavourite = rdr.GetBoolean(5),
                    Number = rdr.GetString(6) ?? ""
                });
            }
            rdr.Close();
            _context.Database.CloseConnection();
            return list;
            //var data =  _context.Contacts.FromSqlRaw("GetAllContacts").AsNoTracking().ToList();
        }

        public ContactPhoneEntity GetById(Guid ContactID)
        {
            ContactPhoneEntity obj = new ContactPhoneEntity();
            DbCommand cmd;
            DbDataReader rdr;

            string sql = "GetContactById";

            cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.AddParameter("@ContactId", ContactID.ToString());

            cmd.CommandText = sql;
            _context.Database.OpenConnection();
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                obj = new ContactPhoneEntity
                {
                    ContactId = rdr.GetGuid(0),
                    Name = rdr.GetString(1) ?? "",
                    Email = rdr.GetString(2) ?? "",
                    PhotoPath = !rdr.IsDBNull(3) ? rdr.GetString(3) : "",
                    Title = rdr.GetString(4) ?? "",
                    IsFavourite = rdr.GetBoolean(5),
                    Number = rdr.GetString(6) ?? ""
                    
                };
            }
            rdr.Close();
            _context.Database.CloseConnection();

            return obj;

            //return _context.Contacts.Find(ContactID);
        }

        public void Insert(ContactEntity contact)
        {
            _context.Contacts.Add(contact);
        }

        public void MarkFavourite(Guid ContactID)
        {
            var contactObj = _context.Contacts.Where(x => x.ContactId == ContactID).FirstOrDefault();
            if(contactObj != null)
            {
                contactObj.IsFavourite = !contactObj.IsFavourite;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ContactEntity contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            _context.Entry(contact).Property(x => x.IsFavourite).IsModified = false;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
