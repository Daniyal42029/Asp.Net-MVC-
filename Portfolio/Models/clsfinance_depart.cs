using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class clsfinance_depart
    {
        public DbActionResult Save_Inquiry(mdlfinance_depart fd)
        {

            var dbar = new DbActionResult();
            var dbhelper = new dbhelper();
            string query = "";
            if (fd.mode == "Save")
            {
                query += "INSERT INTO Fin_Dept ";
                query += "(Name, Position, Salary, HireDate, Department, Email, Phone ) ";
                query += "VALUES( ";
                query += " '" + fd.Name + "','" + fd.Position + "','" + fd.Salary + "','" + fd.Hire_Date + "','" + fd.Department + "','" + fd.Email + "','" + fd.Phone + "')";
            }
            else if (fd.mode == "Update")                  
            {
                query += "UPDATE Fin_Dept SET ";
                query += "Name='" + fd.Name + "',Position='" + fd.Position + "', ";
                query += "Salary='" + fd.Salary + "',HireDate='" + fd.Hire_Date + "', ";
                query += "Department='" + fd.Department + "',Email='" + fd.Email + "',Phone='" + fd.Phone + "' ";
                //query += "Date = GETDATE() ";
                query += "where Id='" + fd.Id + "' ";
            }


            dbar = dbhelper.SaveChangesWithoutPara(query, System.Data.CommandType.Text);
            return dbar;

        }

        //get data from datbase
        //public DataTable GetInquiry()
        //{

        //    DataTable dt;
        //    var dbhelper = new dbhelper();
        //    var dbar = new DbActionResult();
        //    string str = "select Id , Name,Salary,HireDate,Department,Email,Phone,Date from Fin_Dept ";
        //    dt = dbhelper.ExecQueryReturnTable(str, CommandType.Text);
        //    return dt;
        //}


        public DataTable GetInquiry()
        {

            DataTable dt;
            var dbhelper = new dbhelper();
            var dbar = new DbActionResult();
            string str = "select Id , Name, Position, Salary, HireDate, Department, Email, Phone from Fin_Dept ";
            dt = dbhelper.ExecQueryReturnTable(str, CommandType.Text);
            return dt;
        }
    }
}