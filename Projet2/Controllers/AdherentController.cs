using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projet2.Models;
using Projet2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Projet2.Controllers
{
    public class AdherentController : Controller
    {

        private Dal dal;
        public AdherentController()
        {
            dal = new Dal();
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult EditAdherent(int id)
        {

            if (id != 0)
            {
                using (IDal dal = new Dal())
                {
                    Adherent adherent = dal.GetAdherents().Where(r => r.Id == id).FirstOrDefault();
                    if (adherent == null)
                    {
                        return View("Error");
                    }

                    return View(adherent);
                }
            }

            return View("Error");
        }

        [HttpPost]
        public IActionResult EditAdherent(Adherent adherent)
        {
            if (!ModelState.IsValid)
            {
                return View(adherent);
            }

            if (adherent.Id != 0)
            {
                using (Dal dal = new Dal())
                {
                    dal.EditAdherent(adherent);

                    return RedirectToAction("EditAdherent", new { @id = adherent.Id });
                }
            }

            else
            {
                return View("Error");
            }

        }


        //public IActionResult CreateAdherent(Adherent adherent)
        //{
        //    using (Dal dal = new Dal())
        //    {
        //        dal.CreateAdherent(adherent);

        //        return RedirectToAction("EditAdherent", new { @id = adherent.Id });
        //    }

       


        public IActionResult CreateAccountAdherent()
        {
           
            return View();
        }


        [HttpPost]
        public IActionResult CreateAccountAdherent(AdherentViewModel adherentvm)
        {

            //accountViewModel = new AccountViewModel();
            adherentvm.Account =
             dal.AddAccount(adherentvm.Account.Username, adherentvm.Account.Password);
            adherentvm.Adherent = 
             dal.CreateNewAdherent(adherentvm.Account.Id);
            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, adherentvm.Account.Id.ToString()),
                };

            var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

            var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("AdhesionAdherent", new
            {
                id =
                adherentvm.Adherent.Id,
                adherentvm.Account.ProfileId,
                adherentvm.Adherent.AdhesionId,
                //adherentvm.Adhesion.ContributionId,
                //adherentvm.Account.ContactId,
                //adherentvm.Account.infoPerso,
                //adherentvm.Account.InventoryId


            });

        }
        //    

        public IActionResult AdhesionAdherent(int id)
        {
            AdherentViewModel adherentVM = new AdherentViewModel();
            adherentVM.Account = dal.GetAccounts().Where(r => r.Id == id).FirstOrDefault();
            adherentVM.Adherent = dal.GetAdherents().Where(r => r.AccountId == id).FirstOrDefault();
            //Account account = adherentvm.Account;
            //adherentvm.Adhesion = dal.GetAdhesions().Where(r => r.Id == adherent.AdhesionId).FirstOrDefault();
            //Adhesion adhesionUser = adherentvm.Adhesion;
            //adherentvm.Contribution = dal.GetContributions().Where(x => x.Id == adhesionUser.ContributionId).FirstOrDefault();

            return View(adherentVM);
        }


        [HttpPost]
        public IActionResult AdhesionAdherent(AdherentViewModel adherentVM)
        {
            adherentVM.Account = dal.GetAccounts().Where(r => r.Id == adherentVM.Account.Id).FirstOrDefault();
            dal.EditAdherent(adherentVM.Adherent);
            dal.EditAdhesion(adherentVM.Adhesion);
            dal.EditContribution(adherentVM.Contribution);

            return RedirectToAction("EditProfile", "Profile", new
            {
                id =
                adherentVM.Account.Id,
                adherentVM.Adherent.Id,
                adherentVM.Account.ProfileId,

            });


        }

        //    new
        //        {
        //            id =
        //            adherentVM.Adherent.Id,
        //            adherentVM.Account.ProfileId,
        //            adherentVM.Account.ContactId,
        //            adherentVM.Account.infoPerso,
        //            adherentVM.Account.InventoryId,
        //            adherentVM.Adherent.AdhesionId,
        //            adherentVM.Adhesion.ContributionId
        //}
        ////////////////////////////////////////////END
    }

    }




  