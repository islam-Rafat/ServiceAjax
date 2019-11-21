using ConstructionSystem.Models;
using ConstructionSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConstructionSystem.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //GET: Projects
        public ActionResult Project_Details()
        {
            return View(db.Projects.ToList());
        }
        public ActionResult index()
        {
            return View();
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create_Ptoject
        public ActionResult Create_Ptoject()
        {
            return View();
        }

        // POST: Projects/Create_Ptoject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Ptoject(Project project)
        {

            if (ModelState.IsValid)
            {
                //var path = Server.MapPath("~/images/");
                //Image.SaveAs(path + Image.FileName);
                //project.Image = Image.FileName;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Project_Details");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit_Project(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Project(Project project)
        {
            //if (ModelState.IsValid)
            //{
            
            //db.Entry(project).State = EntityState.Modified;
            var p = db.Projects.Where(a => a.ProjectId == project.ProjectId).FirstOrDefault();

            p.Name = project.Name;
            p.Location = project.Location;
            p.Description = project.Description;
            p.StartDate = project.StartDate;
            p.ExpectedPeriod = project.ExpectedPeriod;
            db.SaveChanges();
            return RedirectToAction("Project_Details");
            //}
            //return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete_Project(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete_Project")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ps = db.ProjectServices.Where(a => a.ProjectID == id);
            foreach (var item in ps)
            {
                db.ProjectServices.Remove(item);
            }
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Project_Details");
        }

        public bool Delete(int id)
        {
            var ps = db.ProjectServices.Where(a => a.ProjectID == id);
            foreach (var item in ps)
            {
                db.ProjectServices.Remove(item);
            }
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return true;
        }
        [HttpGet]
        public ActionResult AssignServicToProject()
        {
            ViewBag.proj_list = new SelectList(db.Projects, "ProjectID", "Name");
            ViewBag.serv_list = new SelectList(db.Services, "ServiceID", "ServiceName");
            return View();
        }
        [HttpPost]
        public ActionResult AssignServicToProject(ProjectService model)
        {
            ProjectService ps = new ProjectService();
            if (ModelState.IsValid)
            {
                ps.ProjectID = model.ProjectID;
                ps.ServiceID = model.ServiceID;
                db.ProjectServices.Add(ps);
                db.SaveChanges();
                return RedirectToAction("Project_Details");
            }
            return View(model);
        }

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