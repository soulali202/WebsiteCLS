using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pfeCLS_website.Models;

namespace pfeCLS_website.Controllers
{
    public class InscriptionsController : Controller
    {
        private DbInscriptions db = new DbInscriptions();

        // GET: Inscriptions
        //public ActionResult Index()
        //{
        //    var inscriptions = db.Inscriptions.Include(i => i.Branche);
        //    return View(inscriptions.ToList());
        //}

        // GET: Inscriptions/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Inscription inscription = db.Inscriptions.Find(id);
        //    if (inscription == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(inscription);
        //}

        // GET: Inscriptions/Create
        public ActionResult Create()
        {
            ViewBag.ID_branche = new SelectList(db.Branches, "ID_branche", "Nom_branche");
            return View();
        }

        // POST: Inscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_part,Nom_part,Prenom_part,Age_part,Email_part,Profil_part,Adresse_part,Tele_part,Date_Inscr,ID_branche")] Inscription inscription)
        {
            
                if (ModelState.IsValid)
                {
                    db.Inscriptions.Add(inscription);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }

                ViewBag.ID_branche = new SelectList(db.Branches, "ID_branche", "Nom_branche", inscription.ID_branche);
           
              
               

           
                ViewBag.Error = " Inscription non effectuer ";
            ViewBag.Message = "Inscription effectuer";
            return View(inscription);
           
      
        }

        // GET: Inscriptions/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Inscription inscription = db.Inscriptions.Find(id);
        //    if (inscription == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ID_branche = new SelectList(db.Branches, "ID_branche", "Nom_branche", inscription.ID_branche);
        //    return View(inscription);
        //}

        // POST: Inscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_part,Nom_part,Prenom_part,Age_part,Email_part,Profil_part,Adresse_part,Tele_part,Date_Inscr,ID_branche")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_branche = new SelectList(db.Branches, "ID_branche", "Nom_branche", inscription.ID_branche);
            return View(inscription);
        }

        // GET: Inscriptions/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Inscription inscription = db.Inscriptions.Find(id);
        //    if (inscription == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(inscription);
        //}

        // POST: Inscriptions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Inscription inscription = db.Inscriptions.Find(id);
        //    db.Inscriptions.Remove(inscription);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
