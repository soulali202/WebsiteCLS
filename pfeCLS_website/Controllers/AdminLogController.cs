using Microsoft.Ajax.Utilities;
using pfeCLS_website.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        private DbUtilisateurs dbU = new DbUtilisateurs();
        private DbCategories dbCg = new DbCategories();
        private DbOffres dbO = new DbOffres();

        public ActionResult Annonces(string searching)
        {
            //ViewBag.categorie_id = new SelectList(dbO.Categories, "Id_Cat", "Nom_Cat");
            return View(dbO.Offres.Where(O => O.Categorie.Nom_Cat.Contains(searching) || searching == null).ToList());



            //return View(dbO.Offres.ToList());
        }
        //[HttpPost]
        //public ActionResult Annonces(string categorie_id,string searching)
        //{
          
        //    ViewBag.categorie_id = new SelectList(dbO.Categories, "Id_Cat", "Nom_Cat",categorie_id);
        //    //if (!string.IsNullOrWhiteSpace(categorie_id))
        //    //{
        //    //    return View();
        //    //}

        //    //return View(dbO.Offres.Where(O =>O.Id_Cat.ToString() ==categorie_id).ToList());
        //    return View(dbO.Offres.Where(Ca => Ca.Categorie.Nom_Cat.Contains(searching) || searching == null).ToList());
        //}
       
        public ActionResult Error()
        {
            return View();
        }
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
                    return View("Error");
                }
            }
            catch (Exception)
            {
                //ViewBag.Error = "Verifier vos information";
                return View("Error");
            }


        }
        //--------------------------------Utilisateurs ----------------------------------------------------------------
        [Authorize]
        // GET: Utilisateurs
        public ActionResult ListUtili()
        {
            return View(dbU.Utilisateurs.ToList());
        }
        [Authorize]
        // GET: Utilisateurs/Details/5
        public ActionResult DetailsUtili(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = dbU.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }
        [Authorize]
        // GET: Utilisateurs/Create
        public ActionResult CreateUtili()
        {
            return View();
        }
     
        // POST: Utilisateurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUtili([Bind(Include = "id_Uti,Nom_Uti,motPasse,Role")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                dbU.Utilisateurs.Add(utilisateur);
                dbU.SaveChanges();
                return RedirectToAction("ListUtili");
            }

            return View(utilisateur);
        }
        [Authorize]
        // GET: Utilisateurs/Edit/5
        public ActionResult EditUtili(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = dbU.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }
   
        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUtili([Bind(Include = "id_Uti,Nom_Uti,motPasse,Role")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                dbU.Entry(utilisateur).State = EntityState.Modified;
                dbU.SaveChanges();
                return RedirectToAction("ListUtili");
            }
            return View(utilisateur);
        }
        [Authorize]
        // GET: Utilisateurs/Delete/5
        public ActionResult DeleteUtili(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = dbU.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }
     
        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("DeleteUtili")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedUtili(int id)
        {
            Utilisateur utilisateur = dbU.Utilisateurs.Find(id);
            dbU.Utilisateurs.Remove(utilisateur);
            dbU.SaveChanges();
            return RedirectToAction("ListUtili");
        }
        //-----------------------------------------------------------------------------------------------------------------
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
        //------------------------------------------------Categories----------------------------------------------------------------------------------------------
       [Authorize]
        // GET: Categories
        public ActionResult ListCat()
        {
            return View(dbCg.Categories.ToList());
        }
        [Authorize]
        // GET: Categories/Details/5
        public ActionResult DetailsCat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = dbCg.Categories.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }
        [Authorize]
        // GET: Categories/Create
        public ActionResult CreateCat()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCat([Bind(Include = "Id_Cat,Nom_Cat")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                dbCg.Categories.Add(categorie);
                dbCg.SaveChanges();
                return RedirectToAction("ListCat");
            }

            return View(categorie);
        }
        [Authorize]
        // GET: Categories/Edit/5
        public ActionResult EditCat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = dbCg.Categories.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCat([Bind(Include = "Id_Cat,Nom_Cat")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                dbCg.Entry(categorie).State = EntityState.Modified;
                dbCg.SaveChanges();
                return RedirectToAction("ListCat");
            }
            return View(categorie);
        }
        [Authorize]
        // GET: Categories/Delete/5
        public ActionResult DeleteCat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = dbCg.Categories.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("DeleteCat")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedCat(int id)
        {
            Categorie categorie = dbCg.Categories.Find(id);
            dbCg.Categories.Remove(categorie);
            dbCg.SaveChanges();
            return RedirectToAction("ListCat");
        }
        //----------------------------------------------Offres-------------------------------------------------------------------------------------------------------
        [Authorize]
        // GET: Offres
        public ActionResult ListOffr()
        {
            var offres = dbO.Offres.Include(o => o.Categorie);
            return View(offres.ToList());
        }
        [Authorize]
        // GET: Offres/Details/5
        public ActionResult DetailsOffr(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = dbO.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }
        [Authorize]
        // GET: Offres/Create
        public ActionResult CreateOffr()
        {
            ViewBag.Id_Cat = new SelectList(dbO.Categories, "Id_Cat", "Nom_Cat");
            return View();
        }

        // POST: Offres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOffr([Bind(Include = "Id_Off,Tittre_Off,Descr_Off,Imag_Off,Id_Cat")]HttpPostedFileBase ImageFile,Offre offre)
        {
          
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string _fileName = DateTime.Now.ToString("yymmssfff") +fileName;
                string extension = Path.GetExtension(ImageFile.FileName);
              
                string path = Path.Combine(Server.MapPath("~/Image/"), _fileName);
                offre.Imag_Off = "~/Image/" + _fileName;
                if(extension.ToLower()==".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    dbO.Offres.Add(offre);
                    ImageFile.SaveAs(path);
                    dbO.SaveChanges();
                    return RedirectToAction("ListOffr");
                }
             
            }

            ViewBag.Id_Cat = new SelectList(dbO.Categories, "Id_Cat", "Nom_Cat", offre.Id_Cat);
            return View(offre);
            //var result = "Successfully Added";
            //return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        // GET: Offres/Edit/5
        public ActionResult EditOffr(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = dbO.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cat = new SelectList(dbO.Categories, "Id_Cat", "Nom_Cat", offre.Id_Cat);
            return View(offre);
        }

        // POST: Offres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOffr([Bind(Include = "Id_Off,Tittre_Off,Descr_Off,Imag_Off,Id_Cat")] Offre offre)
        {
            if (ModelState.IsValid)
            {
                dbO.Entry(offre).State = EntityState.Modified;
                dbO.SaveChanges();
                return RedirectToAction("ListOffr");
            }
            ViewBag.Id_Cat = new SelectList(dbO.Categories, "Id_Cat", "Nom_Cat", offre.Id_Cat);
            return View(offre);
        }
        [Authorize]
        // GET: Offres/Delete/5
        public ActionResult DeleteOffr(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = dbO.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // POST: Offres/Delete/5
        [HttpPost, ActionName("DeleteOffr")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedOffr(int id)
        {
            Offre offre = dbO.Offres.Find(id);
            dbO.Offres.Remove(offre);
            dbO.SaveChanges();
            return RedirectToAction("ListOffr");
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