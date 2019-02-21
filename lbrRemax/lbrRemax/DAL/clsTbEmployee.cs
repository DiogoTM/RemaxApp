using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using lbrRemax.BLL;

namespace lbrRemax.DAL
{                      
    public class clsTbEmployee
    {
        private DataTable myTb;       
        public clsTbEmployee()
        {
            this.myTb = clsGlobal.mySet.Tables["Employees"];
            //DataColumn[] col = { myTb.Columns["RefEmp"] };
            //int lastRef = (Int32)myTb.Rows[myTb.Rows.Count - 1]["RefEmp"];
            //myTb.Columns["RefEmp"].AutoIncrement = true;
            //myTb.Columns["RefEmp"].AutoIncrementSeed = lastRef+1;
            //myTb.Columns["RefEmp"].AutoIncrementStep = 1;
            //this.myTb.PrimaryKey = col;        
        }
   
        public DataTable MyTb { get => myTb; set => myTb = clsGlobal.mySet.Tables["Employees"]; }

        //Format table to display
        public static DataTable formatDisplay(DataTable inputTable)
        {
            if (inputTable != null)
            {
                inputTable.Columns["RefEmp"].ColumnName = "EmployeeID";
                inputTable.Columns["Pos"].ColumnName = "Position";

                return inputTable;
            }
            return null;
          
        }
        public static DataTable formatAfterSearch(DataTable inputTable)
        {
            if (inputTable != null)
            {
                DataTable myResult = inputTable.DefaultView.ToTable(false, "RefEmp", "FirstName", "LastName", "Pos", "Email", "PhoneNumber");
                myResult.Columns["RefEmp"].ColumnName = "EmployeeID";
                myResult.Columns["Pos"].ColumnName = "Position";
                return myResult;
            }
            return null;
        }  
        //Return a table containing only not sensitive data from the table 
        public DataTable getAllEmployees()
        {                                                              
            return MyTb.DefaultView.ToTable(false, "RefEmp", "FirstName", "LastName", "Pos", "Email", "PhoneNumber"); 
        }
        //Return all employees who are Admins
        public DataTable getAllAdmins()
        {
            DataRow[] myRow = getAllEmployees().Select("Pos = '" + "Admin" + "'");         
            if (myRow.Count() > 0)              
            {
                return formatDisplay(myRow.CopyToDataTable());
            }
            return null;
        }
        //Return all employees who are Agents
        public DataTable getAllAgents()
        {                   
            DataRow[] myRow = getAllEmployees().Select("Pos = '" + "Agent" + "'");
            if (myRow.Count() > 0)
            {
                return formatDisplay(myRow.CopyToDataTable());
            }
            return null;
        }
        //Return all employees who are Users
        public DataTable getAllUsers()
        {                                        
            DataRow[] myRow = getAllEmployees().Select("Pos = '" + "User" + "'");
            if (myRow.Count() > 0)
            {
                return formatDisplay(myRow.CopyToDataTable());
            }
            return null;
        }
        //Return all employees who are NOT Users
        public DataTable getAllNotUsers()
        {  
            var myRow = from DataRow emp in getAllEmployees().Rows where emp.Field<string>("Pos") != "User" select emp;
            if (myRow.Count() > 0)
            {
                return formatDisplay(myRow.CopyToDataTable());
            }
            return null;
        }                              
        //Check if employee refNumber exists
        public bool Exist(int refNumber)
        {
            if (myTb.Select("RefEmp = " + refNumber).Count() <= 0)
            {
                return false;
            }
            return true;
        }
        //Check if employee LastName exists    
        public bool Exist(string name)
        {
            if (myTb.Select("LastName = '" + name + "'").Count() <= 0)
            {
                return false;
            }
            return true;
        }
        //Check if employee Login exists
        public bool ExistLogin(string login)
        {
            if (myTb.Select("Login = '" + login + "'").Count() <= 0)
            {
                return false;
            }
            return true;
        }
        //Returns the employee if found
        public DataRow[] Find(int refNumber)
        {
            if (Exist(refNumber))
            {
                return myTb.Select("RefEmp = " + refNumber);
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
        public DataRow[] FindLogin(string login)
        {
            if (ExistLogin(login))
            {
                return myTb.Select("Login = '" + login + "'");
            }
            return null;
        }                 
        //Add new employee
        public bool Add(clsEmployee aEmp)
        {
            DataRow myRow = MyTb.NewRow();
            myRow["Pos"] = aEmp.Position.ToString();
            myRow["FirstName"] = aEmp.FName.ToString();
            myRow["LastName"] = aEmp.LName.ToString();
            myRow["PhoneNumber"] = aEmp.PhoneNumber.ToString();
            myRow["Email"] = aEmp.Email.ToString();
            myRow["Pswd"] = aEmp.Password.ToString();
            myRow["Photo"] = aEmp.Photo.ToString();
            myRow["Login"] = aEmp.Login.ToString();
            MyTb.Rows.Add(myRow);
            return true;
        }
        //Update existing employee
        public bool Update(int refNumber, clsEmployee aEmp)
        {
            if (Exist(refNumber))
            {
                DataRow myRow = myTb.Select("RefEmp = " + refNumber)[0];
                myRow["Pos"] = aEmp.Position.ToString();
                myRow["FirstName"] = aEmp.FName.ToString();
                myRow["LastName"] = aEmp.LName.ToString();
                myRow["PhoneNumber"] = aEmp.PhoneNumber.ToString();
                myRow["Email"] = aEmp.Email.ToString();
                myRow["Pswd"] = aEmp.Password.ToString();
                myRow["Photo"] = aEmp.Photo.ToString();
                myRow["Login"] = aEmp.Login.ToString();

                return true;
            }
            return false;
        }  
        //Remove an employee
        public int Remove(int refNumber)
        {
            clsTbClient tbClient = new clsTbClient();
            clsTbProperty tbProp = new clsTbProperty();
            clsTbSales tbSales = new clsTbSales();

            DataRow delRow = Find(refNumber)[0];
            if (delRow != null)
            {
                if (tbClient.MyTb.Select("RefEmp = " + refNumber).Count() > 0 || tbProp.MyTb.Select("RefEmp = " + refNumber).Count() > 0 || tbSales.MyTb.Select("RefEmp = " + refNumber).Count() > 0)
                {
                    return 1;   
                }
                delRow.Delete();
                return 0;
            }
            return 2;           
        }
        //Returns an object clsEmployee from a row from the table
        public static clsEmployee rowToEmp(DataRow myRow)
        {
            clsEmployee aEmp = new clsEmployee();
            aEmp.RefNumber = (Int32)myRow["RefEmp"];
            aEmp.FName = myRow["FirstName"].ToString();
            aEmp.LName = myRow["LastName"].ToString();
            aEmp.PhoneNumber = myRow["PhoneNumber"].ToString();
            aEmp.Email = myRow["Email"].ToString();
            aEmp.Position = myRow["Pos"].ToString();
            aEmp.Password = myRow["Pswd"].ToString();
            aEmp.Photo = myRow["Photo"].ToString();
            aEmp.Login = myRow["Login"].ToString();

            return aEmp;
        }
    }
}
