using Newtonsoft.Json;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class HR_DepartController : Controller
    {
        // GET: HR_Depart
        public ActionResult HR_Depart()
        {
            return View();
        }

        public ActionResult Save_HR(mdlHR_Depart md)
        {

            clsHR_Depart cls = new clsHR_Depart();
            // DbActionResult dbar = new DbActionResult();
            DbActionResult dbar = cls.Save_Inquiry(md);
            if (dbar.Action == true)
            {
                if (md.mode == "Save")
                {

                    dbar.Message = "Saved Successfully!";
                }
                else
                {

                    dbar.Message = "Update Successfully!";
                }
            }

            else
            {
                dbar.Message = "Error 404!";
            }
            var jsonData = JsonConvert.SerializeObject(dbar, Formatting.None);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInquiryList()
        {

            var dbhelper = new dbhelper();
            clsHR_Depart cls = new clsHR_Depart();
            //var Query = "select Id as Inquiry_ID, Name,Age,Contact,F_Name,Address,Date from Inquiry ";
            DataTable dt = cls.GetInquiry();
            //DataTable dt = dbhelper.ExecQueryReturnTable(Query, CommandType.Text);
            var jsonData = JsonConvert.SerializeObject(dt, Formatting.None);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}