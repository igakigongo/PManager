using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PManager.Domain.Entities;
using PManager.Domain.Concrete;

namespace PManager.WebUI.Controllers
{
    public class ProjectResourceController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: /ProjectResource/
        public ActionResult Index()
        {
            return View();
        }

        // JSONRESULT /Get
        public JsonResult GetResourceUsage()
        {
            //var laptops = db.Laptops.Include(l => l.Project).ToList();
            //var vehicles = db.Vehicles.Include(v => v.Project).ToList();
            var projects = db.Projects.Take(10).ToList();

            //foreach(var project in projects)
            //{
            //    for (int i = 0; i < laptops.Count; i++)
            //    {
            //        if (project.Id == laptops[i].Project.Id)
            //        {
            //            project.Laptops.Add(laptops[i]);
            //        }
            //    }
            //}
            return Json(projects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Resources()
        {
            return View();
        }
        // GET: /ProjectResource/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjectResource projectresource = db.ProjectResources.Find(id);
        //    if (projectresource == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(projectresource);
        //}

        // GET: /ProjectResource/Create
        public ActionResult Create()
        {
            return View();
        }

        #region Resources
        // GET: /ProjectResource/CreateLaptop
        [HttpGet]
        public ActionResult CreateLaptop()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateLaptop(Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                db.Laptops.Add(laptop);
                db.SaveChanges();
                return RedirectToAction("Resources");
            }
            return View(laptop);
        }

        public PartialViewResult LaptopsPartial()
        {
            var laptops = db.Laptops.Include(l => l.Project).ToList();
            return PartialView(laptops);
        }

        [HttpGet]
        public ActionResult CreateVehicle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateVehicle(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Resources");
            }
            return View(vehicle);
        }

        public PartialViewResult VehiclesPartial()
        {
            var vehicles = db.Vehicles.Include(v => v.Project).ToList();
            return PartialView(vehicles);
        }
        #endregion
        

        

        // POST: /ProjectResource/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="Id,IsAssignedToProject")] ProjectResource projectresource)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //db.ProjectResources.Add(projectresource);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(projectresource);
        //}

        // GET: /ProjectResource/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjectResource projectresource = db.ProjectResources.Find(id);
        //    if (projectresource == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(projectresource);
        //}

        // POST: /ProjectResource/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="Id,IsAssignedToProject")] ProjectResource projectresource)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(projectresource).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(projectresource);
        //}

        // GET: /ProjectResource/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjectResource projectresource = db.ProjectResources.Find(id);
        //    if (projectresource == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(projectresource);
        //}

        // POST: /ProjectResource/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ProjectResource projectresource = db.ProjectResources.Find(id);
        //    db.ProjectResources.Remove(projectresource);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
