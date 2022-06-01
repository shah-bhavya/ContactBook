using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        //public AppDbContext(DbContextOptions<AppDbContext> options)
        //: base(options)
        //{

        //}

        public DbSet<Contact.Core.Entities.Contacts.ContactEntity> Contacts { get; set; }
        public DbSet<Contact.Core.Entities.Contacts.PhoneNumbersEntity> PhoneNumbers { get; set; }
        public DbSet<Contact.Core.Entities.Users.UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=SF-LAP-012; initial catalog=ContactBookDb;Persist Security Info = False; Integrated Security = true;");
        }
    }
}
