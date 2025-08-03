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
    public class finance_departController : Controller
    {
        // GET: finance_depart
        public ActionResult finance_depart()
        {
            return View();
        }

        public ActionResult Save_Finance(mdlfinance_depart fd)
        {

            clsfinance_depart cls = new clsfinance_depart();
            // DbActionResult dbar = new DbActionResult();
            DbActionResult dbar = cls.Save_Inquiry(fd);
            if (dbar.Action == true)
            {
                if (fd.mode == "Save")
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
            clsfinance_depart cls = new clsfinance_depart();
            //var Query = "select Id as Inquiry_ID, Name,Age,Contact,F_Name,Address,Date from Inquiry ";
            DataTable dt = cls.GetInquiry();
            //DataTable dt = dbhelper.ExecQueryReturnTable(Query, CommandType.Text);
            var jsonData = JsonConvert.SerializeObject(dt, Formatting.None);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EDITInquiry(string Id)
        {
            var dbar = new DbActionResult();
            var DBhelper = new dbhelper();
            mdlfinance_depart fd = new mdlfinance_depart();

            var query = "Select * from Fin_Dept where Id = '" + Id + "' ";
            DataTable dt = DBhelper.ExecQueryReturnTable(query, CommandType.Text);
            var jsonData = JsonConvert.SerializeObject(dt, Formatting.None);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DELETEInquiry(string Id)
        {
            var dbar = new DbActionResult();
            var DBhelper = new dbhelper();
            var query = "Delete from Fin_Dept where Id ='" + Id + "' ";
            dbar = DBhelper.SaveChangesWithoutPara(query, CommandType.Text);
            if (dbar.Action == true)
            {
                dbar.Message = "Delete Successfully!";
            }
            var jsonData = JsonConvert.SerializeObject(dbar, Newtonsoft.Json.Formatting.None);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
} 