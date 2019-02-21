using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using lbrRemax.BLL;


namespace lbrRemax.DAL
{
    public class clsTbClient
    {
        private DataTable myTb;

        //Constructor defines myTb as the table Clients in the dataset
        public clsTbClient()
        {
            this.myTb = clsGlobal.mySet.Tables["Clients"];     
        }

        public DataTable MyTb { get => myTb; set => myTb = clsGlobal.mySet.Tables["Clients"]; }       

        //Format the table to display it changing the field names 
        public static DataTable formatDisplay(DataTable inputTable)
        {
            if (inputTable != null)
            {
                DataTable displayTb = inputTable;
                displayTb.Columns["RefClient"].ColumnName = "ClientID";
                displayTb.Columns["RefEmp"].ColumnName = "AgentID";
                return displayTb;
            }
            return null;
           
        }                       
        //Return all the clients in the table
        public DataTable getAllClients()
        {                                    
            return myTb.Copy();
        }                          
        //Return all the clients for determined Agent
        public DataTable getAllClientsFrom(int refEmp)
        {
            DataRow[] myRow = getAllClients().Select("RefEmp = " + refEmp);
            if (myRow.Count() > 0)
            {
                return formatDisplay(myRow.CopyToDataTable());
            }
            return null;
        }              
        //Check if client number exists
        public bool Exist(int refNumber)
        {
            if (myTb.Select("RefClient = " + refNumber).Count() <= 0)
            {
                return false;
            }
            return true;
        }
        //Check if client lastname exists
        public bool Exist(string name)
        {
            if (myTb.Select("LastName = '" + name + "'").Count() <= 0)
            {
                return false;
            }
            return true;
        }
        //Returns the row of client if found 
        public DataRow[] Find(int refNumber)
        {
            if (Exist(refNumber))
            {
                return myTb.Select("RefClient = " + refNumber);
            }
            return null;
        }
        public DataRow[] Find(string name)
        {
            if (Exist(name))
            {
                return myTb.Select("LastName = '" + name + "'");
            }
            return null;
        }
        //Add a new client from object clsClient
        public bool Add(clsClient aClient)
        {
            DataRow myRow = MyTb.NewRow();
            myRow["FirstName"] = aClient.FName.ToString();
            myRow["LastName"] = aClient.LName.ToString();
            myRow["PhoneNumber"] = aClient.PhoneNumber.ToString();
            myRow["Email"] = aClient.Email.ToString();
            myRow["RefEmp"] = aClient.Agent.RefNumber;
            myRow["Credit"] = aClient.Credit.ToString();
            MyTb.Rows.Add(myRow);
            return true;
        }
        //Update a client given its refNumber and the new information as an object clsClient
        public bool Update(int refNumber, clsClient aClient)
        {
            if (Exist(refNumber))
            {
                DataRow myRow = myTb.Select("RefClient = " + refNumber)[0];
                myRow["FirstName"] = aClient.FName.ToString();
                myRow["LastName"] = aClient.LName.ToString();
                myRow["PhoneNumber"] = aClient.PhoneNumber.ToString();
                myRow["Email"] = aClient.Email.ToString();
                myRow["RefEmp"] = aClient.Agent.RefNumber;
                myRow["Credit"] = aClient.Credit.ToString();

                return true;
            }
            return false;
        }       
        //Remove a client given it's refNumber
        public int Remove(int refNumber)
        {                                                
            clsTbProperty tbProp = new clsTbProperty();
            clsTbSales tbSales = new clsTbSales();

            DataRow delRow = Find(refNumber)[0];
            if (delRow != null)
            {
                if (tbProp.MyTb.Select("RefClient = " + refNumber).Count() > 0 || tbSales.MyTb.Select("RefClientSeller = " + refNumber).Count() > 0 || tbSales.MyTb.Select("RefClientBuyer = " + refNumber).Count() > 0)
                {
                    return 1;
                }
                delRow.Delete();
                return 0;
            }
            return 2;
        }                           
        //Static method to return an object of type clsClient given a row from the table
        public static clsClient rowToCl(DataRow myRow, clsTbEmployee tbEmp)
        {
            clsClient aClient = new clsClient();

            aClient.RefNumber = (Int32)myRow["RefClient"];
            aClient.FName = myRow["FirstName"].ToString();
            aClient.LName = myRow["LastName"].ToString();
            aClient.PhoneNumber = myRow["PhoneNumber"].ToString();
            aClient.Email = myRow["Email"].ToString();
            aClient.Agent = clsTbEmployee.rowToEmp(tbEmp.Find((Int32)(myRow["RefEmp"]))[0]);
            aClient.Credit = Convert.ToDecimal(myRow["Credit"].ToString());

            return aClient;
        }
    }
}
