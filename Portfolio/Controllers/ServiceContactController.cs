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
    public class ServiceContactController : Controller
    {
        // GET: ServiceContact
        public ActionResult ServiceContact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save_Inquiry(mdlServiceContact md)
        {

            clsServiceContact cls = new clsServiceContact();
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
            clsServiceContact cls = new clsServiceContact();
            //var Query = "select Id as Inquiry_ID, Name,Age,Contact,F_Name,Address,Date from Inquiry ";
            DataTable dt = cls.GetInquiry();
            //DataTable dt = dbhelper.ExecQueryReturnTable(Query, CommandType.Text);
            var jsonData = JsonConvert.SerializeObject(dt, Formatting.None);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EDITInquiry(string ID)
        {
            var dbar = new DbActionResult();
            var DBhelper = new dbhelper();
            mdlServiceContact md = new mdlServiceContact();

            var query = "Select * from __PPORTFOLIO where ID = '" + ID + "' ";
            DataTable dt = DBhelper.ExecQueryReturnTable(query, CommandType.Text);
            var jsonData = JsonConvert.SerializeObject(dt, Formatting.None);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DELETEInquiry(string ID)
        {
            var dbar = new DbActionResult();
            var DBhelper = new dbhelper();
            var query = "Delete from __PPORTFOLIO where ID ='" + ID + "' ";
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