using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lbrRemax.BLL
{
    public class clsClient : clsPerson
    {
        int refNumber;
        decimal credit;
        clsEmployee agent;

        public clsClient(int refNumber, clsEmployee agent, decimal credit)
        {
            this.RefNumber = refNumber;
            this.Agent = agent;
            this.Credit = credit;
        }
        public clsClient()
        {
            this.RefNumber = 0;
            this.Agent = new clsEmployee();
            this.Credit = 0;
        }

        public int RefNumber { get => refNumber; set => refNumber = value; }
        public clsEmployee Agent { get => agent; set => agent = value; }
        public decimal Credit { get => credit; set => credit = value; }
    }
}
