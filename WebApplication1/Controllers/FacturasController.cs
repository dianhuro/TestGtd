using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FacturasController : Controller
    {
        private FacturasContext db = new FacturasContext();

        // GET: Facturas
        public ActionResult Index()
        {
            return View(db.Facturas.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idFactura,NumeroFactura,Fecha,TipodePago,DocumentoCliente,NombreCliente,Subtotal,Descuento,IVA,TotalDescuento,TotalImpuesto,Total")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                //validacion fecha
                if (factura.Fecha.Date<DateTime.Now.Date)
                {
                    TempData["mensaje"] = "La fecha no puede ser inferior al día de hoy";
                }
                factura.TotalDescuento = factura.Subtotal * factura.Descuento / 100;
                factura.TotalImpuesto = factura.Subtotal * factura.IVA / 100;
                factura.Total = factura.Subtotal - factura.TotalDescuento + factura.TotalImpuesto;
                db.Facturas.Add(factura);
                db.SaveChanges();
                if (factura.Total == 0)
                {
                    TempData["mensaje"] = "Debe agregar detalles a la factura";
                }
                return RedirectToAction("Create");
            }

            return View(factura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            DetallesController detalles = new DetallesController();
            factura.Subtotal = detalles.SubtotalDetallesFactura(factura.idFactura);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFactura,NumeroFactura,Fecha,TipodePago,DocumentoCliente,NombreCliente,Subtotal,Descuento,IVA,TotalDescuento,TotalImpuesto,Total")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Facturas.Find(id);
            db.Facturas.Remove(factura);
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

        public bool Details(int id)
        {
            Factura _factura = db.Facturas.Find(id);
            if (_factura == null)
            {
                return false;
            }
            return true;
        }
    }
}
