using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lbrRemax.BLL
{
    public class clsProperty
    {
        int refNumber, yearBuilt, numRooms;
        string title, address, description, type;
        double area;
        decimal price;
        bool status;
        clsEmployee agent;
        clsClient client;

  

        public clsProperty()
        {
            this.refNumber = 0;
            this.yearBuilt = 0;
            this.numRooms = 0;
            this.title = "Not Defined";
            this.address = "Not Defined";
            this.description = "Not Defined";
            this.type = "Not Defined";
            this.area = 0;
            this.price = 0;
            this.status = false;
            this.agent = null;
            this.Client = null;       
        }

        public clsProperty(int refNumber, int yearBuilt, int numRooms, string title, string address, string description, string type, float area, decimal price, bool status, clsEmployee agent, clsClient client)
        {
            this.refNumber = refNumber;
            this.yearBuilt = yearBuilt;
            this.numRooms = numRooms;
            this.title = title;
            this.address = address;
            this.description = description;
            this.type = type;
            this.area = area;
            this.price = price;
            this.status = status;
            this.agent = agent;
            this.Client = client;
        }

        public int RefNumber { get => refNumber; set => refNumber = value; }
        public int YearBuilt { get => yearBuilt; set => yearBuilt = value; }
        public int NumRooms { get => numRooms; set => numRooms = value; }
        public string Title { get => title; set => title = value; }
        public string Address { get => address; set => address = value; }
        public string Description { get => description; set => description = value; }
        public string Type { get => type; set => type = value; }
        public double Area { get => area; set => area = value; }
        public decimal Price { get => price; set => price = value; }
        public bool Status { get => status; set => status = value; }
        public clsEmployee Agent { get => agent; set => agent = value; }
        public clsClient Client { get => client; set => client = value; }
    }
}
