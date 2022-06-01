using Contact.Core.Entities.Common;
using Contact.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Interfaces.Auth
{
    public interface IAccountService
    {
        AuthenticationResponse Authenticate(string email, string password);
        void RegisterUser(UserEntity userEntity);
        void Save();
    }
}
