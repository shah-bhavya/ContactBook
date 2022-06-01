using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Entities.Contacts
{
    public class PhoneNumbersEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhoneNumberId { get; set; }
        [Required]
        public int ContactType { get; set; }
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Number { get; set; }

        [ForeignKey("Contact")]
        public Guid ContactId { get; set; }
        public ContactEntity Contact { get; set; }

    }
}
