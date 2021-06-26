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
    public class OrdersController : Controller
    {
        private my_panaceyaEntities db = new my_panaceyaEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Baskets).Include(o => o.Pay_Method).Include(o => o.Pharmacies).Include(o => o.Status);
            return View(orders.ToList());
        }

        public ActionResult MedInBasket(int id)
        {
            var meds = db.Basket_Consist.Where(o => o.ID_Basket == id).Include(o => o.Medicines).Include(o => o.Medicines.Categories);
            return PartialView(meds);
        }

        // GET: Orders/Details/5    
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ID_Basket = new SelectList(db.Baskets, "ID_Basket", "ID_Basket");
            ViewBag.ID_Pay = new SelectList(db.Pay_Method, "ID_Pay", "Method");
            ViewBag.ID_Pharm = new SelectList(db.Pharmacies, "ID_Pharm", "Name");
            ViewBag.ID_Status = new SelectList(db.Status, "ID_Status", "Status1");
            return View();
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Order,ID_Status,ID_Pay,ID_Pharm,ID_Basket")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Basket = new SelectList(db.Baskets, "ID_Basket", "ID_Basket", orders.ID_Basket);
            ViewBag.ID_Pay = new SelectList(db.Pay_Method, "ID_Pay", "Method", orders.ID_Pay);
            ViewBag.ID_Pharm = new SelectList(db.Pharmacies, "ID_Pharm", "Name", orders.ID_Pharm);
            ViewBag.ID_Status = new SelectList(db.Status, "ID_Status", "Status1", orders.ID_Status);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Basket = new SelectList(db.Baskets, "ID_Basket", "ID_Basket", orders.ID_Basket);
            ViewBag.ID_Pay = new SelectList(db.Pay_Method, "ID_Pay", "Method", orders.ID_Pay);
            ViewBag.ID_Pharm = new SelectList(db.Pharmacies, "ID_Pharm", "Name", orders.ID_Pharm);
            ViewBag.ID_Status = new SelectList(db.Status, "ID_Status", "Status1", orders.ID_Status);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Order,ID_Status,ID_Pay,ID_Pharm,ID_Basket")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Basket = new SelectList(db.Baskets, "ID_Basket", "ID_Basket", orders.ID_Basket);
            ViewBag.ID_Pay = new SelectList(db.Pay_Method, "ID_Pay", "Method", orders.ID_Pay);
            ViewBag.ID_Pharm = new SelectList(db.Pharmacies, "ID_Pharm", "Name", orders.ID_Pharm);
            ViewBag.ID_Status = new SelectList(db.Status, "ID_Status", "Status1", orders.ID_Status);
            return View(orders);
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
