using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Entities.Common
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
    }
}
