using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Entities.Contacts
{
    [Table("Contact", Schema = "dbo")]
    public class ContactEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContactId { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }
       
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string PhotoPath { get; set; }
        [Required]
        public bool IsFavourite { get; set; }
        public ICollection<PhoneNumbersEntity> PhoneNumbers { get; set; }
    }
}
