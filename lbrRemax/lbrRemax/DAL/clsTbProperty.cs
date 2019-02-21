using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using lbrRemax.BLL;

namespace lbrRemax.DAL
{
    public class clsTbProperty
    {
        private DataTable myTb;
        public clsTbProperty()
        {
            this.myTb = clsGlobal.mySet.Tables["Properties"];
            //DataColumn[] col = { myTb.Columns["RefProp"] };
            //int lastRef = (Int32)myTb.Rows[myTb.Rows.Count - 1]["RefProp"];
            //myTb.Columns["RefProp"].AutoIncrement = true;
            //myTb.Columns["RefProp"].AutoIncrementSeed = lastRef+1;
            //myTb.Columns["RefProp"].AutoIncrementStep = 1;
            //this.myTb.PrimaryKey = col;
        }

        public DataTable MyTb { get => myTb; set => myTb = clsGlobal.mySet.Tables["Properties"]; }

        //Format table to display
        public static DataTable formatDisplay(DataTable inputTable)
        {
            if (inputTable != null)
            {
                inputTable.Columns["RefProp"].ColumnName = "PropertyID";
                inputTable.Columns["RefEmp"].ColumnName = "AgentID";
                inputTable.Columns["RefClient"].ColumnName = "ClientID";
                return inputTable;
            }
            return null;
        }
        public static DataTable formatAfterSearch(DataTable inputTable)
        {
            if (inputTable != null)
            {
                DataTable myResult = inputTable.DefaultView.ToTable(false, "RefProp", "Title", "Type", "Address", "Price", "RefEmp", "RefClient");
                myResult.Columns["RefProp"].ColumnName = "PropertyID";
                myResult.Columns["RefEmp"].ColumnName = "AgentID";
                myResult.Columns["RefClient"].ColumnName = "ClientID";
                return myResult;
            }
            return null;
        }         
        //Returns all the properties 
        public DataTable getAllProperties()
        {
            return MyTb.DefaultView.ToTable(false, "RefProp", "Title", "Type", "Address", "Price", "RefEmp", "RefClient"); ;
        }
        //Returns all the properties of given type
        public DataTable getAllPropertiesType(string type)
        {
            DataRow[] myRow = getAllProperties().Select("Type = '" + type + "'");
            if (myRow.Count() > 0)
            {
                return myRow.CopyToDataTable();
            }
            return null;
        }
        //Returns all properties from given client
        public DataTable getAllPropFromClient(int refClient)
        {
            DataRow[] myRow = getAllProperties().Select("RefClient = " + refClient.ToString());
            if (myRow.Count() > 0)
            {
                return myRow.CopyToDataTable();
            }
            return null;
        }
        //Returns all properties from given Agent
        public DataTable getAllPropFromAgent(int refEmp)
        {
            DataRow[] myRow = getAllProperties().Select("RefEmp = " + refEmp.ToString());
            if (myRow.Count() > 0)
            {
                return myRow.CopyToDataTable();
            }
            return null;
        }       
        //Check if exists
        public bool Exist(int refNumber)
        {
            if (myTb.Select("RefProp = " + refNumber).Count() <= 0)
            {
                return false;
            }
            return true;
        }                       
        //Returns property row if found
        public DataRow[] Find(int refNumber)
        {
            if (Exist(refNumber))
            {
                return myTb.Select("RefProp = " + refNumber);
            }
            return null;
        }                   
        public DataRow[] FindByClient(int refNumber)
        {
            if (myTb.Select("RefClient = " + refNumber).Count() > 0)
            {
                return myTb.Select("RefClient = " + refNumber);
            }
            return null;
        }
        public DataRow[] FindByClient(string lName, clsTbClient tbClient)
        {
            DataRow[] myClient = tbClient.Find(lName);

            if (tbClient.Exist(lName) && (myTb.Select("RefClient = " + myClient[0]["RefClient"].ToString()).Count() > 0))
            {
                return myTb.Select("RefClient = " + myClient[0]["RefClient"].ToString());
            }
            return null;
        }
        public DataRow[] FindByAgent(int refNumber)
        {
            if (myTb.Select("RefEmp = " + refNumber).Count() > 0)
            {
                return myTb.Select("RefEmp = " + refNumber);
            }
            return null;
        }
        public DataRow[] FindByAgent(string lName, clsTbEmployee tbEmp)
        {
            DataRow[] myAgent = tbEmp.Find(lName);    
            if (tbEmp.Exist(lName) && (myTb.Select("RefEmp = " + myAgent[0]["RefEmp"].ToString()).Count() > 0))
            {
                return myTb.Select("RefEmp = " + myAgent[0]["RefEmp"].ToString());
            }
            return null;
        }  
        //Add new property
        public bool Add(clsProperty aProp)
        {
            DataRow myRow = MyTb.NewRow();
            myRow["Address"] = aProp.Address;
            myRow["Area"] = aProp.Area;
            myRow["Description"] = aProp.Description;
            myRow["NumberOfRooms"] = aProp.NumRooms;
            myRow["Price"] = aProp.Price;
            myRow["Status"] = aProp.Status;
            myRow["Title"] = aProp.Title;
            myRow["Type"] = aProp.Type;
            myRow["YearBuilt"] = aProp.YearBuilt;
            myRow["RefEmp"] = aProp.Agent.RefNumber;
            myRow["RefClient"] = aProp.Client.RefNumber;

            MyTb.Rows.Add(myRow);
            return true;
        }
        //Update property
        public bool Update(int refNumber, clsProperty aProp)
        {
            if (Exist(refNumber))
            {
                DataRow myRow = myTb.Select("RefProp = " + refNumber)[0];
                myRow["Address"] = aProp.Address;
                myRow["Area"] = aProp.Area;
                myRow["Description"] = aProp.Description;
                myRow["NumberOfRooms"] = aProp.NumRooms;
                myRow["Price"] = aProp.Price;
                myRow["Status"] = aProp.Status;
                myRow["Title"] = aProp.Title;
                myRow["Type"] = aProp.Type;
                myRow["YearBuilt"] = aProp.YearBuilt;
                myRow["RefEmp"] = aProp.Agent.RefNumber;
                myRow["RefClient"] = aProp.Client.RefNumber;
                return true;
            }
            return false;
        }
        //Remove property
        public int Remove(int refNumber)
        {
            clsTbPhoto tbPhoto = new clsTbPhoto();
            clsTbSales tbSales = new clsTbSales();

            DataRow delRow = Find(refNumber)[0];
            if (delRow != null)
            {
                if (tbPhoto.MyTb.Select("refProp = " + refNumber).Count() > 0 || tbSales.MyTb.Select("refProp = " + refNumber).Count() > 0 )
                {
                    return 1;
                }
                delRow.Delete();
                return 0;
            }
            return 2;
        }
        //Returns an object of type clsProperty from a Row and tables related to it
        public static clsProperty rowToProp(DataRow myRow, clsTbEmployee tbEmp, clsTbClient tbClient)
        {
            clsProperty aProp = new clsProperty();
            aProp.RefNumber = (Int32)myRow["RefProp"];
            aProp.Address = myRow["Address"].ToString();
            aProp.Area = (double)myRow["Area"];
            aProp.Description = myRow["Description"].ToString();
            aProp.NumRooms = Convert.ToInt32(myRow["NumberOfRooms"].ToString());
            aProp.Price = (decimal)myRow["Price"];
            aProp.Status = (bool)myRow["Status"];
            aProp.Title = myRow["Title"].ToString();
            aProp.Type = myRow["Type"].ToString();
            aProp.YearBuilt = Convert.ToInt32(myRow["YearBuilt"].ToString());         
            aProp.Agent = clsTbEmployee.rowToEmp(tbEmp.Find((Int32)(myRow["RefEmp"]))[0]);
            aProp.Client = clsTbClient.rowToCl(tbClient.Find((Int32)(myRow["RefClient"]))[0],tbEmp);

            return aProp;
        }
    }
}
