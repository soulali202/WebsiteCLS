using pfeCLS_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace pfeCLS_website.Controllers
{
    public class AdminLogController : Controller
    {


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
        public ActionResult Login(Utilisateur model,string returnUrl)
        {
            try
            {
                LoginDb db = new LoginDb();

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
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminLog");
        }


    }
}