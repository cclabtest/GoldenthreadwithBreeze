using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldenthreadwithBreeze.Controllers
{
    public class ChapController : Controller
    {
        //
        // GET: /Chap/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Chap/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Chap/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Chap/Create
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
        // GET: /Chap/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Chap/Edit/5
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
        // GET: /Chap/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Chap/Delete/5
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
