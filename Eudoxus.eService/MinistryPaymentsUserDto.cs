using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Eudoxus.eService
{
    public class MinistryPaymentsUserDto
    {
        public int ID { get; set; }        
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }        
        public string ContactEmail { get; set; }
        public int MinistryAuthorization { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int VerificationStatus { get; set; }
        public bool IsActivated { get; set; }        
        public string Password { get; set; }
        public string PasswordSalt { get; set; }        

        private PropertyInfo[] _PropertyInfos = null;

        public override string ToString()
        {
            if (_PropertyInfos == null)
                _PropertyInfos = this.GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var info in _PropertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }

            return sb.ToString();
        }
    }
}
