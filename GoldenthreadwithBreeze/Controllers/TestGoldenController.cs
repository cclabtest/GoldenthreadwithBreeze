using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldenthreadwithBreeze.Controllers
{
    public class TestGoldenController : Controller
    {
        //
        // GET: /TestGolden/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TestGolden/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TestGolden/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TestGolden/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TestGolden/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TestGolden/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TestGolden/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TestGolden/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
