using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lbrRemax.BLL;
using System.Data;

namespace lbrRemax.DAL
{
    public class clsTbSales
    {
        private DataTable myTb;

        public clsTbSales()
        {
            this.myTb = clsGlobal.mySet.Tables["Sales"];
        }

        public DataTable MyTb { get => myTb; set => myTb = clsGlobal.mySet.Tables["Sales"]; }

        //Format table to display
        public static DataTable formatDisplay(DataTable inputTable)
        {
            if (inputTable != null)
            {
                DataTable displayTb = inputTable;
                displayTb.Columns["RefSale"].ColumnName = "SaleID";
                displayTb.Columns["RefClientBuyer"].ColumnName = "BuyerID";
                displayTb.Columns["RefClientSeller"].ColumnName = "SellerID";
                displayTb.Columns["RefProp"].ColumnName = "PropertyID";
                displayTb.Columns["RefEmp"].ColumnName = "AgentID";

                return displayTb;
            }
            return null;
        }    
        //Returns all sales
        public DataTable getAllSales()
        {
            return myTb.Copy();
        }                                
        //Returns all sales from given Agent
        public DataTable getAllSalesFrom(int refEmp)
        {
            DataRow[] myRow = getAllSales().Select("RefEmp = " + refEmp);
            if (myRow.Count() > 0)
            {
                return formatDisplay(myRow.CopyToDataTable());
            }
            return null;
        }              
        //Check if sale exists
        public bool Exist(int refNumber)
        {
            if (myTb.Select("RefSale = " + refNumber).Count() <= 0)
            {
                return false;
            }
            return true;
        }                        
        //Returns sale's row if found
        public DataRow[] Find(int refNumber)
        {
            if (Exist(refNumber))
            {
                return myTb.Select("RefSale = " + refNumber);
            }
            return null;
        }                                 
        //Add new sale
        public bool Add(clsSale aSale)
        {
            DataRow myRow = MyTb.NewRow();

            myRow["RefClientSeller"] = aSale.ClientSeller.RefNumber;
            myRow["RefClientBuyer"] = aSale.ClientBuyer.RefNumber;
            myRow["RefProp"] = aSale.Property.RefNumber;
            myRow["RefEmp"] = aSale.Agent.RefNumber;
            myRow["SaleDate"] = aSale.Date;
            MyTb.Rows.Add(myRow);
            return true;
        }
        //Update existing sale
        public bool Update(int refNumber, clsSale aSale)
        {
            if (Exist(refNumber))
            {
                DataRow myRow = myTb.Select("RefSale = " + refNumber)[0];
                myRow["RefClientSeller"] = aSale.ClientSeller.RefNumber;
                myRow["RefClientBuyer"] = aSale.ClientBuyer.RefNumber;
                myRow["RefProp"] = aSale.Property.RefNumber;
                myRow["RefEmp"] = aSale.Agent.RefNumber;
                myRow["SaleDate"] = aSale.Date;

                return true;
            }
            return false;
        }
        //Remove sale
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
        //Returns an object of type clsSale given a row and tables related
        public static clsSale rowToSale(DataRow myRow, clsTbEmployee tbEmp, clsTbClient tbCl, clsTbProperty tbProp)
        {
            clsSale aSale = new clsSale();

            aSale.RefNumber = (Int32)myRow["RefSale"];
            aSale.Agent = clsTbEmployee.rowToEmp(tbEmp.Find((Int32)myRow["RefEmp"])[0]);
            aSale.ClientBuyer = clsTbClient.rowToCl(tbEmp.Find((Int32)myRow["RefClientBuyer"])[0],tbEmp);                       
            aSale.ClientSeller = clsTbClient.rowToCl(tbEmp.Find((Int32)myRow["RefClientSeller"])[0], tbEmp);   
            aSale.Property = clsTbProperty.rowToProp(tbEmp.Find((Int32)myRow["RefProp"])[0],tbEmp,tbCl);
            aSale.Date = Convert.ToDateTime(myRow["SaleDate"].ToString());    
            return aSale;
        }
    }
}

