using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Panaceya.Models;

namespace Panaceya.Controllers
{
    public class Medicines1Controller : Controller
    {
        private my_panaceyaEntities db = new my_panaceyaEntities();

        // GET: Medicines1
        public ActionResult Index()
        {
            var medicines = db.Medicines.Include(m => m.Categories);
            return View(medicines.ToList());
        } 
        public ActionResult ElementesMedicines(string id)
        {
            int idCat = Convert.ToInt32(id);
            var medicines = db.Medicines.Include(m => m.Categories).Where(e => e.ID_Category == idCat);
            return PartialView(medicines.ToList());
        }

        // GET: Medicines1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicines medicines = db.Medicines.Find(id);
            if (medicines == null)
            {
                return HttpNotFound();
            }
            return View(medicines);
        }

        // GET: Medicines1/Create
        public ActionResult Create()
        {
            ViewBag.ID_Category = new SelectList(db.Categories, "ID_Category", "Name");
            return View();
        }

        // POST: Medicines1/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicines medicines, HttpPostedFileBase Pic)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (Pic != null)
                {
                    fileName = Path.GetFileName(Pic.FileName);
                    string extensionImage = getFileExtension(fileName);
                    fileName = Guid.NewGuid() + "." + extensionImage;
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    Pic.SaveAs(path);
                }
                medicines.Photo = fileName;
                db.Medicines.Add(medicines);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Category = new SelectList(db.Categories, "ID_Category", "Name", medicines.ID_Category);
            return View(medicines);
        }

        // GET: Medicines1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicines medicines = db.Medicines.Find(id);
            if (medicines == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Category = new SelectList(db.Categories, "ID_Category", "Name", medicines.ID_Category);
            return View(medicines);
        }

        // POST: Medicines1/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Medicine,ID_Category,Name,Description,Price,Amount,Presence,Photo")] Medicines medicines, HttpPostedFileBase Pic)
        {
            if (ModelState.IsValid)
            {
                if (Pic != null)
                {
                    string fileName = null;
                    fileName = Path.GetFileName(Pic.FileName);
                    string extensionImage = getFileExtension(fileName);
                    fileName = Guid.NewGuid() + "." + extensionImage;
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    Pic.SaveAs(path);
                    medicines.Photo = fileName;
                }
                db.Entry(medicines).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Category = new SelectList(db.Categories, "ID_Category", "Name", medicines.ID_Category);
            return View(medicines);
        }

        // GET: Medicines1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicines medicines = db.Medicines.Find(id);
            if (medicines == null)
            {
                return HttpNotFound();
            }
            return View(medicines);
        }

        // POST: Medicines1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicines medicines = db.Medicines.Find(id);
            db.Medicines.Remove(medicines);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string getFileExtension(string fileName) // Получение типа фотографии
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1);
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
