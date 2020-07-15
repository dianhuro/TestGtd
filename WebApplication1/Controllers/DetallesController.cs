using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;


namespace WebApplication1.Controllers
{
    public class DetallesController : Controller
    {
        private DetallesContext db = new DetallesContext();

        // GET: Detalles
        public ActionResult Index()
        {
            return View(db.Detalles.ToList());
        }

        // GET: Detalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalles.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // GET: Detalles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Detalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetalle,idFactura,idProducto,Cantidad,PrecioUnitario")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                if (detalle.PrecioUnitario < 0)
                {
                    TempData["mensajeerror"] = "No puede tener precio negativo";
                    return RedirectToAction("Create");
                  //  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FacturasController facturasController = new FacturasController();
                if (!facturasController.Details(detalle.idFactura))
                {
                    TempData["mensajeerror"] = "Debe crear la cabecera de la factura";
                    return RedirectToAction("Create");
                }
                db.Detalles.Add(detalle);
                db.SaveChanges();
                TempData["mensaje"] = "Detalle creado";
                return RedirectToAction("Create");
            }

            return View(detalle);
        }

        // GET: Detalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalles.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // POST: Detalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetalle,idFactura,idProducto,Cantidad,PrecioUnitario")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(detalle);
        }

        // GET: Detalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalles.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // POST: Detalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle detalle = db.Detalles.Find(id);
            db.Detalles.Remove(detalle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public long SubtotalDetallesFactura(int idFactura)
        {
            var detalles = db.Detalles.Where(x => x.idFactura == idFactura);
            long total = 0;
            foreach(var detalle in detalles)
            {
                total += detalle.PrecioUnitario * detalle.Cantidad;
            }
            return total;
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
