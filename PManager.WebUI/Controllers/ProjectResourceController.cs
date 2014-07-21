using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using PManager.Domain.Concrete;
using PManager.Domain.Entities;
using PManager.WebUI.DTOS;

namespace PManager.WebUI.Controllers
{
    public class ProjectResourceController : Controller
    {
        private readonly EFDbContext db;

        public ProjectResourceController()
        {
            db = new EFDbContext();
        }

        // GET: /ProjectResource/
        public ActionResult Index()
        {
            return View();
        }

        // JSONRESULT /Get
        public JsonResult GetResourceUsage()
        {
            List<Project> projects = db.Projects
                .Include(p => p.Laptops)
                .Include(v => v.Vehicles)
                .Take(10).ToList();

            var projectsToRender = from project in projects
                                   select new
                                   {
                                       project.Id,
                                       Code = project.ProjectCode,
                                       Cost = project.Estimated.Budget,
                                       Laptops = project.Laptops.Select(p => new
                                       {
                                           p.Id,
                                           SN = p.SerialNumber
                                       }),
                                       Vehicles = project.Vehicles.Select(v => new
                                       {
                                           v.Id
                                       })
                                   };


            return Json(projectsToRender, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResourcesCount()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("vehicles", 5);
            dict.Add("laptops", 8);
            var jobj = new DynamicJsonObject(dict);
            return Json(jobj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Resources()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Laptops()
        {
            IQueryable<Laptop> laptops = db.Laptops.Include(l => l.Project).AsQueryable();
            return View(laptops);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Vehicles()
        {
            IQueryable<Vehicle> vehicles = db.Vehicles.AsQueryable();
            return View(vehicles);
        }

        // GET: /ProjectResource/Create
        public ActionResult Create()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Resources

        // GET: /ProjectResource/CreateLaptop
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
                return RedirectToAction("Laptops");
            }
            return View(laptop);
        }

        [Authorize(Roles = "Manager")]
        public PartialViewResult LaptopsPartial()
        {
            List<Laptop> laptops = db.Laptops.Include(l => l.Project).ToList();
            return PartialView(laptops);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
                return RedirectToAction("Vehicles");
            }
            return View(vehicle);
        }

        [Authorize(Roles = "Manager")]
        public PartialViewResult VehiclesPartial()
        {
            List<Vehicle> vehicles = db.Vehicles.Include(v => v.Project).ToList();
            return PartialView(vehicles);
        }

        public ActionResult AssignLaptop(int laptopId, int projectId)
        {
            var message = new Message
            {
                Status = false,
                Text = "The resource or project your are trying to update does not exist, please contact systems administrator for help"
            };

            var laptopToAssign = db.Laptops.Include(x => x.Project).FirstOrDefault(l => l.Id == laptopId);
            var projectToBeAssigned = db.Projects.Include(x => x.Laptops).FirstOrDefault(p => p.Id == projectId);

            if (laptopToAssign != null && projectToBeAssigned != null)
            {
                if (projectToBeAssigned.Laptops.Contains(laptopToAssign))
                {
                    message = new Message
                    {
                        Status = false,
                        Text = "Resource is already assigned to " + projectToBeAssigned.Name
                    };
                }
                else
                {
                    projectToBeAssigned.Laptops.Add(laptopToAssign);
                    db.Entry(laptopToAssign).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                        message = new Message
                        {
                            Status = true,
                            Text = "Resource successfully assigned to " + projectToBeAssigned.Name
                        };
                    }
                    catch (Exception)
                    {
                        message = new Message
                        {
                            Status = false,
                            Text = "An error occurred while trying to save the entries, please contact your systems administrator for help"
                        };
                    }

                }


                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return Json(message, JsonRequestBehavior.AllowGet);


        }

        #endregion
    }
}