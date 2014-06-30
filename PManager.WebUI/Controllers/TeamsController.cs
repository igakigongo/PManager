using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using PManager.Domain.Concrete;
using System.Data.Entity;
using PManager.Domain.Entities;
using PManager.Domain.ViewModels;

namespace PManager.WebUI.Controllers
{
    public class TeamsController: Controller
    {

        private EFDbContext context = new EFDbContext();
        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            
                return View(context.Teams.Include(x => x.Project).ToList());
            
        }

        public ActionResult GetTeams()
        {
            
                var teams=context.Teams.Include(x => x.Project).Select(x=>new
                {
                    Team = x.Name,
                    Project = x.Project.Name
                });

                return Json(teams, JsonRequestBehavior.AllowGet);
           
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult Create(string teamName, string projectName, string projectCode, string projectDescription)
        {
            var newTeam = new Team
            {
                Name = teamName,
                Project = new Project()
                {
                    Name = projectName,
                    ProjectCode = projectCode,
                    Description = projectDescription
                }
            };

            bool message;

            using (var db = new EFDbContext())
            {
                db.Teams.Add(newTeam);
                try
                {
                    db.SaveChanges();
                    message = true;
                }
                catch (Exception)
                {
                    message = false;
                    throw;
                }
            }

            return Json(message,JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Details(int id)
        {
            using (var db = new EFDbContext())
            {
                ViewBag.projectsDone = db.Projects.Where(p => p.Team.Id == id).Distinct().ToList();
                return View(db.Teams.Include(x => x.Users).Include(x => x.Project).FirstOrDefault(p => p.Id == id));
            }
        }

        [Authorize(Roles = "Manager")]
        public ActionResult AddTeamMember(int id)
        {
            var team = context.Teams.FirstOrDefault(x => x.Id == id);
            
                ViewBag.users = new SelectList(context.UserProfiles.ToList(), "UserId", "UserName");

                return View(team);

        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult AddTeamMember(int id, int userId)
        {
            var team = context.Teams.FirstOrDefault(x => x.Id == id);
            var user = context.Users.FirstOrDefault(x => x.Id == userId);

            if (team != null)
            {
                if (user != null)
                {
                    user.Team = team;
                    context.Users.AddOrUpdate(user);
                }

                context.SaveChanges();

            }

             ViewBag.users = new SelectList(context.UserProfiles.ToList(), "UserId", "UserName");

                return View(team);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete()
        {
            string message = "";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        
    }
}