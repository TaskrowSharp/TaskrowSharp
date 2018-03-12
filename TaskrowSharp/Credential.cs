using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp
{
    public class Credential
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public Credential()
        {

        }

        public Credential(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(this.Email))
                throw new System.InvalidOperationException(string.Format(string.Format("Invalid e-mail")));

            if (string.IsNullOrEmpty(this.Password))
                throw new System.InvalidOperationException(string.Format(string.Format("Invalid password")));
        }
    }
}