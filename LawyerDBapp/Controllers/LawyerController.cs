using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LawyerDBapp.Models;

namespace LawyerDBapp.Controllers
{
    public class LawyerController : Controller
    {
        // GET: Lawyer
        public ActionResult Index()
        {
            return View();
        }

        //Get Lawyer Table records as a JSON request 
        public ActionResult GetData()
        {
            using(DBModel db = new DBModel())
            {
                List<Lawyer> lawyerList = db.Lawyers.ToList<Lawyer>();
                return Json(new {data=lawyerList},JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit (int id = 0)
        {
            if (id == 0)
            {
                return View(new Lawyer());
            }
            //View and update one record on where the ID was clicked
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.Lawyers.Where(x => x.LawyerID == id).FirstOrDefault<Lawyer>());
                }
            }
           
        }

        [HttpPost]
        public ActionResult AddOrEdit(Lawyer lawyer)
        {
            using (DBModel db = new DBModel())
            {
                if(lawyer.LawyerID == 0)
                {
                    db.Lawyers.Add(lawyer);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Record Saved Successfully!" }, JsonRequestBehavior.AllowGet);
                }
                //View and update one record on where the ID was clicked
                else
                {
                    db.Entry(lawyer).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Record Updated Successfully!" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Lawyer lawyer = db.Lawyers.Where(x => x.LawyerID == id).FirstOrDefault<Lawyer>();
                db.Lawyers.Remove(lawyer);
                db.SaveChanges();
                return Json(new { success = true, message = "Record Deleted Successfully" }, JsonRequestBehavior.AllowGet);


            }
        }
    }
}