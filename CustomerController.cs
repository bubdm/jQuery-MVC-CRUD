using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnboardingjQuery.Models;


namespace OnboardingjQuery.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //It fetches Customer data from server
        public ActionResult GetData()
        {
            using (OnboardingTaskEntities db = new OnboardingTaskEntities())
            {
                var CustomerData = db.CustomerTables.OrderBy(a => a.customerId).ToList();
                return Json(new { data = CustomerData }, JsonRequestBehavior.AllowGet);
            }
        }
        //Save (Edit) Get
        [HttpGet]
        public ActionResult Save(int id)
        {
            using (OnboardingTaskEntities dc = new OnboardingTaskEntities())
            {
                var v = dc.CustomerTables.Where(a => a.customerId == id).FirstOrDefault();
                return View(v);

            }
        }

        //Save Post

        [HttpPost]
        public ActionResult Save(CustomerTable cus)
        {
            if (ModelState.IsValid)
            {
                using (OnboardingTaskEntities dc = new OnboardingTaskEntities())
                {
                    if (cus.customerId > 0)
                    {
                        //Edit 
                        var v = dc.CustomerTables.Where(a => a.customerId == cus.customerId).FirstOrDefault();
                        if (v != null)
                        {
                            v.customerId = cus.customerId;

                            v.customerName = cus.customerName;
                            v.customerAge = cus.customerAge;
                            v.customerAddress = cus.customerAddress;
                        }
                    }
                    else
                    {
                        //Save
                        var customers = dc.CustomerTables.OrderBy(a => a.customerId).ToList();
                        dc.CustomerTables.Add(cus);
                    }
                    dc.SaveChanges();
                }
            }

            return RedirectToAction("Index");

        }


        //Delete Post
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (OnboardingTaskEntities dc = new OnboardingTaskEntities())
            {
                var v = dc.CustomerTables.Where(a => a.customerId == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();

                }
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCustomer(int id)
        {
            using (OnboardingTaskEntities dc = new OnboardingTaskEntities())
            {
                var v = dc.CustomerTables.Where(a => a.customerId == id).FirstOrDefault();
                if (v != null)
                {
                    dc.CustomerTables.Remove(v);
                    dc.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }

    }

}