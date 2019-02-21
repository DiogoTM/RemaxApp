using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lbrRemax.BLL
{
    public class clsSale
    {
        int refNumber;
        clsClient clientSeller, clientBuyer;
        clsEmployee agent;
        clsProperty property;
        DateTime date;

        public clsSale(int refNumber, clsClient clientSeller, clsClient clientBuyer, clsEmployee agent, clsProperty property, DateTime date)
        {
            this.refNumber = refNumber;
            ClientSeller = clientSeller;
            ClientBuyer = clientBuyer;
            Agent = agent;
            Property = property;
            this.date = date;
        }

        public clsSale()
        {
            this.refNumber = 0;
            ClientSeller = null;
            ClientBuyer = null;
            Agent = null;
            Property = null;
            this.date = DateTime.Today;
        }


        public int RefNumber { get => refNumber; set => refNumber = value; }
        public clsClient ClientSeller { get => clientSeller; set => clientSeller = value; }
        public clsClient ClientBuyer { get => clientBuyer; set => clientBuyer = value; }
        public clsEmployee Agent { get => agent; set => agent = value; }
        public clsProperty Property { get => property; set => property = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
