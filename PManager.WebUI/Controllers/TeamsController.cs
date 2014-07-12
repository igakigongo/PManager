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
    public class TeamsController : Controller
    {

        private EFDbContext context;

        public TeamsController()
        {
            context = new EFDbContext();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            return View(context.Teams.Where(x => x.Status == Status.Active).ToList());
        }

        public ActionResult GetTeams()
        {
            return Json(context.Teams.Where(x=>x.Status==Status.Active).ToList(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Details(int id)
        {
            //ViewBag.projectsDone = db.Projects.Where(p => p.Team.Id == id).Distinct().ToList();
            return View(context.Teams.Include(x => x.Users).Include(x => x.Tasks).FirstOrDefault(p => p.Id == id));

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
                    team.Users.Add(user);
                    context.Teams.AddOrUpdate(team);
                }

                context.SaveChanges();

            }

            ViewBag.users = new SelectList(context.UserProfiles.ToList(), "UserId", "UserName");

            return View(team);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int teamId)
        {
            bool success;
            var teamToRemove = context.Teams.Find(teamId);
            teamToRemove.Status = Status.Inactive;
            //context.Teams.AddOrUpdate(teamToRemove);
            context.Entry(teamToRemove).State = EntityState.Modified;
            try
            {
                context.SaveChanges();
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveUser(int teamId, int userId)
        {
            var team = context.Teams.Include(x => x.Users).FirstOrDefault(d=>d.Id==teamId);
            var userToRemove = context.Users.FirstOrDefault(x => x.Id == userId);
            team.Users.Remove(userToRemove);
            context.SaveChanges();

            return RedirectToAction("Edit", "Teams", new { id = teamId });
        }


        public ActionResult CreateTeam(TeamViewModel teamViewModel)
        {
            var team = new Team()
            {
                Name = teamViewModel.Name

            };

            var users = new List<User>();

            if (teamViewModel.UserIds!=null)
            {
                foreach (var userId in teamViewModel.UserIds)
                {
                    users.Add(context.Users.Find(userId));
                }

                team.Users = users;
            }
            context.Teams.Add(team);

            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return Json(teamViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            return View(context.Teams.Include(x => x.Users).FirstOrDefault(p => p.Id == id));
        }

        public ActionResult GetTeam(int teamId=0)
        {
            Team team = null;
            if (teamId != 0)
            {
                team = context.Teams.Find(teamId);
            }
            return Json(team, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditTeam(TeamViewModel teamViewModel)
        {
            
            bool successful = false;
            var teamToEdit = context.Teams.Include(x=>x.Users).FirstOrDefault(x=>x.Id==teamViewModel.Id);

            if (teamToEdit != null)
            {
                teamToEdit.Name = teamViewModel.Name;

                var users = new List<User>();

                if (teamViewModel.UserIds!=null)
                {
                    foreach (var userId in teamViewModel.UserIds)
                    {
                        if (!teamToEdit.Users.Exists(x => x.Id == userId))
                        {
                            //var users.Add(context.Users.Find(userId));
                            teamToEdit.Users.Add(context.Users.Find(userId));
                        }

                    }
                }

                try
                {
                    context.Entry(teamToEdit).State = EntityState.Modified;
                    context.SaveChanges();
                    successful = true;

                }
                catch (Exception)
                {
                    successful = false;
                    //throw;
                }
            }

            return Json(successful, JsonRequestBehavior.AllowGet);
        }
    }
}