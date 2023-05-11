using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUDEntityFramework.Models;

namespace CRUDEntityFramework.Controllers
{
    public class platosController : Controller
    {
        private BD db = new BD();

        // GET: platos
        public ActionResult Index()
        {
            var plato = db.plato.Include(p => p.categoria);
            return View(plato.ToList());
        }

        // GET: platos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // GET: platos/Create
        public ActionResult Create()
        {
            ViewBag.idcategoria = new SelectList(db.categoria, "id", "categoria1");
            return View();
        }

        // POST: platos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,precio,imagen,idcategoria")] plato plato)
        {
            if (ModelState.IsValid)
            {
                db.plato.Add(plato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcategoria = new SelectList(db.categoria, "id", "categoria1", plato.idcategoria);
            return View(plato);
        }

        // GET: platos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcategoria = new SelectList(db.categoria, "id", "categoria1", plato.idcategoria);
            return View(plato);
        }

        // POST: platos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,precio,imagen,idcategoria")] plato plato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcategoria = new SelectList(db.categoria, "id", "categoria1", plato.idcategoria);
            return View(plato);
        }

        // GET: platos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // POST: platos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            plato plato = db.plato.Find(id);
            db.plato.Remove(plato);
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
