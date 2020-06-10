using pfeCLS_website.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;



namespace pfeCLS_website.Controllers
{
    public class AdminLogController : Controller
    {

        private DbContacts db = new DbContacts();
        private DbInscriptions dbI = new DbInscriptions();
        private DBranches dbR = new DBranches();

        // GET: IndexLog
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
     
  
       
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Utilisateur model, string returnUrl)
        {
            try
            {
                DbUtilisateurs db = new DbUtilisateurs();

                var dataItem = db.Utilisateurs.Where(x => x.Nom_Uti == model.Nom_Uti && x.motPasse == model.motPasse).First();
                if (dataItem != null)
                {
                    FormsAuthentication.SetAuthCookie(dataItem.Nom_Uti, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index");


                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nom d'utilisateur ou mot de passe est invalide !");
                    return View();
                }
            }
            catch (Exception)
            {
                //ViewBag.Error = "Verifier vos information";
                return View();
            }


        }
        /// CONTACTS OPERATIONS : 
          // GET: Contacts
        [Authorize]
        public ActionResult ListContact(string searching)
        {
            return View(db.Contacts.Where(c => c.Nom_msg.Contains(searching) || searching == null).ToList());
            //return View(db.Contacts.ToList());
        }
        [Authorize]

        // GET: Contacts/Details/5
        public ActionResult DetailsContact(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [Authorize]
        // GET: Contacts/Edit/5
        public ActionResult EditContact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact([Bind(Include = "id_msg,Nom_msg,Email_msg,Objet_msg,Message_msg")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListContact");
            }
            return View(contact);
        }
        [Authorize]
        // GET: Contacts/Delete/5
        public ActionResult DeleteContact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
        [Authorize]
        //POST: Contacts/Delete/5
        [HttpPost, ActionName("DeleteContact")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("ListContact");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        // -----------------------------------------------------------------INSCRIPTION OPERATION :----------------------------------------------------------------
        [Authorize]
        // GET: Inscriptions
        public ActionResult ListInscription(string searching)
        {
            var inscriptions = dbI.Branches.Include(i => i.Nom_branche);
            return View(dbI.Inscriptions.Where(I => I.Nom_part.Contains(searching) || searching == null).ToList());
            //return View(inscriptions.ToList());
        }
        [Authorize]
        // GET: Inscriptions/Details/5
        public ActionResult DetailsInscription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = dbI.Inscriptions.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }
        [Authorize]
        // GET: Inscriptions/Edit/5
        public ActionResult EditInscription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = dbI.Inscriptions.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_branche = new SelectList(dbI.Branches, "ID_branche", "Nom_branche", inscription.ID_branche);
            return View(inscription);
        }

        // POST: Inscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInscription([Bind(Include = "id_part,Nom_part,Prenom_part,Age_part,Email_part,Profil_part,Adresse_part,Tele_part,Date_Inscr,ID_branche")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                dbI.Entry(inscription).State = EntityState.Modified;
                dbI.SaveChanges();
                return RedirectToAction("ListInscription");
            }
            ViewBag.ID_branche = new SelectList(dbI.Branches, "ID_branche", "Nom_branche", inscription.ID_branche);
            return View(inscription);
        }
        [Authorize]
        // GET: Inscriptions/Delete/5
        public ActionResult DeleteInscription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = dbI.Inscriptions.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }
        [Authorize]
        // POST: Inscriptions/Delete/5
        [HttpPost, ActionName("DeleteInscription")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedInscription(int id)
        {
            Inscription inscription = dbI.Inscriptions.Find(id);
            dbI.Inscriptions.Remove(inscription);
            dbI.SaveChanges();
            return RedirectToAction("ListInscription");
        }
        [Authorize]
        // GET: Inscriptions/Create
        public ActionResult CreateInscription()
        {
            ViewBag.ID_branche = new SelectList(dbI.Branches, "ID_branche", "Nom_branche");
            return View();
        }
        [Authorize]
        // POST: Inscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInscription([Bind(Include = "id_part,Nom_part,Prenom_part,Age_part,Email_part,Profil_part,Adresse_part,Tele_part,Date_Inscr,ID_branche")] Inscription inscription)
        {

            if (ModelState.IsValid)
            {
                inscription.Date_Inscr = DateTime.Now;
                dbI.Inscriptions.Add(inscription);

                dbI.SaveChanges();

                return RedirectToAction("CreateInscription");

            }

            ViewBag.ID_branche = new SelectList(dbI.Branches, "ID_branche", "Nom_branche", inscription.ID_branche);



            return View(inscription)



;

        }
        //-------------------------Branches :-------------------------------------------------------------------------------
        [Authorize]
        // GET: Branches
        public ActionResult ListBranches(string searching)
        {
           
            return View(dbR.Branches.Where(b=>b.Nom_branche.Contains(searching)||searching==null).ToList());
        }
 

        [Authorize]
        // GET: Branches/Details/5
        public ActionResult DetailsBr(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branche branche = dbR.Branches.Find(id);
            if (branche == null)
            {
                return HttpNotFound();
            }
            return View(branche);
        }
        [Authorize]
        // GET: Branches/Create
        public ActionResult CreateBr()
        {
          
            return View();
        }
    
        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBr([Bind(Include = "ID_branche,Nom_branche,prix_branche,frais_inscri")] Branche branche)
        {
            if (ModelState.IsValid)
            {
                dbR.Branches.Add(branche);
                dbR.SaveChanges();
                return RedirectToAction("ListBranches");
            }
           
            return View(branche);
        }
        [Authorize]
        // GET: Branches/Edit/5
        public ActionResult EditBr(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branche branche = dbR.Branches.Find(id);
            if (branche == null)
            {
                return HttpNotFound();
            }
            return View(branche);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBr([Bind(Include = "ID_branche,Nom_branche,prix_branche,frais_inscri")] Branche branche)
        {
            if (ModelState.IsValid)
            {
                dbR.Entry(branche).State = EntityState.Modified;
                dbR.SaveChanges();
                return RedirectToAction("ListBranches");
            }
        
            return View(branche);
        }
        [Authorize]
        // GET: Branches/Delete/5
        public ActionResult DeleteBr(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branche branche = dbR.Branches.Find(id);
            if (branche == null)
            {
                return HttpNotFound();
            }
            return View(branche);
        }
        
        // POST: Branches/Delete/5
        [HttpPost, ActionName("DeleteBr")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedBr(int id)
        {
            Branche branche = dbR.Branches.Find(id);
            dbR.Branches.Remove(branche);
            dbR.SaveChanges();
            return RedirectToAction("ListBranches");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbI.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminLog");
        }


    }

 
}