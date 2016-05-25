using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFI.MenuCardsAggregator.Web.Controllers
{
    public class MenuCardsController : Controller
    {
        // GET: MenuCards
        public ActionResult MenuCards()
        {
            return View();
        }

        // GET: MenuCards/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuCards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuCards/Create
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

        // GET: MenuCards/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuCards/Edit/5
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

        // GET: MenuCards/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuCards/Delete/5
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
