using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp
{
    public class EmailAndPasswordCredential : Credential
    {
        public string Email { get; private set; }

        public string Password { get; private set; }

        public EmailAndPasswordCredential(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(this.Email))
                throw new System.ArgumentNullException(string.Format(string.Format("E-mail is required")));

            if (string.IsNullOrEmpty(this.Password))
                throw new System.ArgumentNullException(string.Format(string.Format("Password is required")));
        }
    }
}
