using System;
using System.ComponentModel.DataAnnotations;

namespace Contact.API.DTOs.User
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must have 1 uppercase 1 lowercase 1 number and 1 non-alphanumeric and atleast 6 charactes")]
        public string Password { get; set; }
    }
}
