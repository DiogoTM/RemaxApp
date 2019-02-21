using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace lbrRemax.BLL
{
    public class clsEmployee : clsPerson
    {
        int refNumber;
        string position, password ,photo, login;
        
      
        public clsEmployee()
        {
            RefNumber = 0;
            Position = "Not defined";
            Password = "1234";
            Photo = "Not defined";
            Login = "Not defined";
         
        }

        public clsEmployee(int refNumber, string position, string password, string photo, string login)
        {
            this.refNumber = refNumber;
            this.position = position;
            this.password = password;
            this.photo = photo;
            this.login = login;
        }

        public int RefNumber { get => refNumber; set => refNumber = value; }
        public string Position { get => position; set => position = value; }
        public string Password { get => password; set => password = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Login { get => login; set => login = value; }
    }
}
