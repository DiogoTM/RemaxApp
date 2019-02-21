using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lbrRemax.BLL
{
    public class clsPhoto
    {
        int refNumber;
        clsProperty property;
        string description, path;

        public clsPhoto(int refNumber, clsProperty property, string description, string path)
        {
            this.refNumber = refNumber;
            this.property = property;
            this.description = description;
            this.path = path;
        }
        public clsPhoto()
        {
            this.refNumber = 0;
            this.property = null;
            this.description = "TBD";
            this.path = "TBD";
        }

        public int RefNumber { get => refNumber; set => refNumber = value; }
        public clsProperty Property { get => property; set => property = value; }
        public string Description { get => description; set => description = value; }
        public string Path { get => path; set => path = value; }
    }
}
