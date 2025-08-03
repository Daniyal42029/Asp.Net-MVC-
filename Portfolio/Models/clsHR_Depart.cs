using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class clsHR_Depart
    {

        public DbActionResult Save_Inquiry(mdlHR_Depart md)
        {

            var dbar = new DbActionResult();
            var dbhelper = new dbhelper();
            string query = "";
            if (md.mode == "Save")
            {
                md.EmploymentStatus = "Full-time";
                query += "INSERT INTO HR_Depart ";
                query += "(FirstName, LastName, Gender, Email, PhoneNumber, Address, HireDate, JobTitle, Salary, EmploymentStatus ) ";
                query += "VALUES( ";
                query += " '" + md.FirstName + "','" + md.LastName + "','" + md.Gender + "', '" + md.Email + "','" + md.PhoneNumber + "', '" + md.Address + "',GETDATE(),'" + md.JobTitle + "','" + md.Salary + "','" + md.EmploymentStatus + "')";
            }
            else if (md.mode == "Update")
            {
                query += "UPDATE HR_Depart SET ";
                query += "FirstName='" + md.FirstName + "',LastName='" + md.LastName + "', ";
                query += "Gender='" + md.Gender + "',Email='" + md.Email + "', ";
                query += "PhoneNumber='" + md.PhoneNumber + "',Address='" + md.Address + "',HireDate='" + md.HireDate + "' ";
                query += "JobTitle='" + md.JobTitle + "',Salary='" + md.Salary + "',EmplyomentStatus='" + md.EmploymentStatus + "'";
                //query += "Date = GETDATE() ";
                query += "where ID='" + md.ID + "' ";
            }


            dbar = dbhelper.SaveChangesWithoutPara(query, System.Data.CommandType.Text);
            return dbar;

        }

        public DataTable GetInquiry()
        {

            DataTable dt;
            var dbhelper = new dbhelper();
            var dbar = new DbActionResult();
            string str = "select ID , FirstName, LastName, Gender, Email, PhoneNumber, Address, HireDate, JobTitle, Salary, EmploymentStatus from HR_Depart ";
            dt = dbhelper.ExecQueryReturnTable(str, CommandType.Text);
            return dt;
        }

    }
}