using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class clsServiceContact

        // save data in database
    {
        public DbActionResult Save_Inquiry (mdlServiceContact md)
        {

            var dbar = new DbActionResult();
            var dbhelper = new dbhelper();
            string query = "";
            if (md.mode == "Save")
            {
                query += "INSERT INTO __PPORTFOLIO ";
                query += "(NAME, F_Name, AGE, CONTACT, ADDRESS, DATE ) ";
                query += "VALUES( ";
                query += " '" + md.Name + "', '" + md.F_Name + "', '" + md.Age + "', '" + md.Contact + "', '" + md.Address + "', GETDATE())";
            }
            else if(md.mode=="Update")
            {
                query += "UPDATE __PPORTFOLIO SET ";
                query += "Name='" + md.Name + "',F_Name='" + md.F_Name + "', ";
                query += "Age='" + md.Age + "',Contact='" + md.Contact + "', ";
                query += "Address='" + md.Address + "', ";
                query += "Date = GETDATE() ";
                query += "where Id='" + md.id + "' ";
            }


            dbar = dbhelper.SaveChangesWithoutPara(query, System.Data.CommandType.Text);
            return dbar;

        }

        //get data from datbase
        public DataTable GetInquiry()
        {

            DataTable dt;
            var dbhelper = new dbhelper();
            var dbar = new DbActionResult();
            string str = "select ID , Name,Age,Contact,F_Name,Address,Date from __PPORTFOLIO ";
            dt = dbhelper.ExecQueryReturnTable(str, CommandType.Text);
            return dt;
        }

        
    }
}