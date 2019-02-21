using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using lbrRemax.BLL;

namespace lbrRemax.DAL
{
    public class clsTbPhoto
    {
        private DataTable myTb;
        public clsTbPhoto()
        {
            this.myTb = clsGlobal.mySet.Tables["Photos"];
            //DataColumn[] col = { myTb.Columns["refPhoto"] };
            //int lastRef = (Int32)(myTb.Rows[myTb.Rows.Count-1]["refPhoto"]);
            //myTb.Columns["refPhoto"].AutoIncrement = true;
            //myTb.Columns["refPhoto"].AutoIncrementSeed = lastRef;
            //myTb.Columns["refPhoto"].AutoIncrementStep = 1;
            //this.myTb.PrimaryKey = col;
        }

        public DataTable MyTb { get => myTb; set => myTb = clsGlobal.mySet.Tables["Photos"];}

        //Returns all the photos from the table
        public DataTable getAllPhotos()
        {
            return myTb.Copy();
        }
        //Returns the photos from a given property 
        public DataTable getAllPhotosFrom(int refNumber)
        {
            DataRow[] myRow = myTb.Select("refProp = " + refNumber);    
            if (myRow.Count() > 0)
            {
                return myRow.CopyToDataTable();
            }
            return null;
        }      
        //Check if the photo exists
        public bool Exist(int refNumber)
        {
            if (myTb.Select("refPhoto = " + refNumber).Count() < 0)
            {
                return false;
            }
            return true;
        }             
        //Returns the row from the photo if found
        public DataRow[] Find(int refNumber)
        {
            if (Exist(refNumber))
            {
                return myTb.Select("refPhoto = " + refNumber);
            }
            return null;
        }                                                   
        //Adds a new photo
        public bool Add(clsPhoto aPhoto)
        {
            DataRow myRow = myTb.NewRow();
            //POG*******************************************
            //myRow["refPhoto"] = (Int32)myRow["refPhoto"] + 1;
            myRow["refProp"] = aPhoto.Property.RefNumber;
            myRow["Description"] = aPhoto.Description.ToString();
            myRow["Path"] = aPhoto.Path.ToString();

            myTb.Rows.Add(myRow);
            return true;
        }
        //Update an existing photo
        public bool Update(int refNumber, clsPhoto aPhoto)
        {
            if (Exist(refNumber))
            {
                DataRow myRow = myTb.Select("refPhoto = " + refNumber)[0];
                myRow["refProp"] = aPhoto.Property.RefNumber;
                myRow["Description"] = aPhoto.Description.ToString();
                myRow["Path"] = aPhoto.Path.ToString();   
                return true;
            }
            return false;
        }
        //Remove a photo
        public bool Remove(int refNumber)
        {
            DataRow delRow = Find(refNumber)[0];
            if (delRow != null)
            {
                delRow.Delete();
                return true;
            }
            return false;
        }
        //Returns an object clsPhoto given a Row and the tables related to it
        public static clsPhoto rowToPhoto(DataRow myRow, clsTbProperty tbProp, clsTbEmployee tbEmp, clsTbClient tbClient)
        {
            clsPhoto aPhoto = new clsPhoto();

            aPhoto.RefNumber = (Int32)myRow["refPhoto"];
            aPhoto.Property = clsTbProperty.rowToProp(tbProp.Find((Int32)myRow["refProp"])[0], tbEmp,tbClient);           
            aPhoto.Description = myRow["Description"].ToString();
            aPhoto.Path = myRow["Path"].ToString();         

            return aPhoto;
        }

    }
}
