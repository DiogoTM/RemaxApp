using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lbrRemax.BLL
{
    public class clsPerson
    {
        string fName, lName, phoneNumber, email;

        
        public clsPerson()
        {
            this.FName = "Not defined";
            this.LName = "Not defined";
            this.PhoneNumber = "Not defined";
            this.email = "Not defined";
        }

        public clsPerson(string fName, string lName, string phoneNumber, string email)
        {
            this.fName = fName;
            this.lName = lName;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }

        public string FName { get => fName; set => fName = value; }
        public string LName { get => lName; set => lName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public string Email { get => email; set => email = value; }
    }
}
