using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Panaceya.Models;

namespace Panaceya.Controllers
{
    public class PharmaciesController : Controller
    {
        private my_panaceyaEntities db = new my_panaceyaEntities();

        // GET: Pharmacies
        public ActionResult Index()
        {
            return View(db.Pharmacies.ToList());
        }

        // GET: Pharmacies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacies pharmacies = db.Pharmacies.Find(id);
            if (pharmacies == null)
            {
                return HttpNotFound();
            }
            return View(pharmacies);
        }

        // GET: Pharmacies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pharmacies/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Pharm,Name,Address,Time_Work")] Pharmacies pharmacies)
        {
            if (ModelState.IsValid)
            {
                db.Pharmacies.Add(pharmacies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pharmacies);
        }

        // GET: Pharmacies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacies pharmacies = db.Pharmacies.Find(id);
            if (pharmacies == null)
            {
                return HttpNotFound();
            }
            return View(pharmacies);
        }

        // POST: Pharmacies/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Pharm,Name,Address,Time_Work")] Pharmacies pharmacies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pharmacies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pharmacies);
        }

        // GET: Pharmacies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacies pharmacies = db.Pharmacies.Find(id);
            if (pharmacies == null)
            {
                return HttpNotFound();
            }
            return View(pharmacies);
        }

        // POST: Pharmacies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pharmacies pharmacies = db.Pharmacies.Find(id);
            db.Pharmacies.Remove(pharmacies);
            db.SaveChanges();
            return RedirectToAction("Index");
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
