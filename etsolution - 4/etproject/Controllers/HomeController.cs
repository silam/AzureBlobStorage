using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using etproject.Models;

namespace etproject.Controllers
{
    public class HomeController : Controller
    {
        DatabaseLogic dblogic;
        public HomeController()
        {
            dblogic = new DatabaseLogic();
        }
        public ActionResult Index()
        {
            return View(dblogic.GetAll());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(int id)
        {
            var model = dblogic.GetCourseDetails(id);
            if (model == null)
            {
                return View("DetailsError");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = dblogic.GetCourseDetails(id);
            if (model == null)
            {
                return View("DetailsError");
            }
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course viewobj)
        {
            if (ModelState.IsValid)
            {
                dblogic.EditCourse(viewobj);
                // Here we want to execure the Details method again on the controller so that this action is performed after the edit
                return RedirectToAction("Details", new { id = viewobj.Id });
            }
            // If the model state is not valid, then the same edit page will be returned to the user
            return View(viewobj);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course viewobj)
        {
            if (ModelState.IsValid)
            {
                dblogic.CreateCourse(viewobj);

                return RedirectToAction("Details", new { id = viewobj.Id });
            }

            return View(viewobj);
        }

    }


}
