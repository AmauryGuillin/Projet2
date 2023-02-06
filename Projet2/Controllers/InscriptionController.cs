﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Projet2.Models;
using Projet2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Projet2.Controllers
{
    public class InscriptionController : Controller
    {
        private Dal dal;
        private IWebHostEnvironment _webEnv;
        public InscriptionController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }
        public IActionResult SignUpBenevole()
        {
          InscriptionViewModel inscriptionViewModel= new InscriptionViewModel();

            return View(inscriptionViewModel);
        }


        [HttpPost]
        public IActionResult SignUpBenevole(InscriptionViewModel inscriptionViewModel)
        { 

            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            string filePath = Path.Combine(uploads, inscriptionViewModel.Profile.ProfilImage.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                inscriptionViewModel.Profile.ProfilImage.CopyTo(fileStream);
            }
            inscriptionViewModel.Contact =
                 dal.AddContact(
                     inscriptionViewModel.Contact.EmailAdress,
                     inscriptionViewModel.Contact.TelephoneNumber
                     );
            inscriptionViewModel.Infos =
                 dal.AddInfoPerso(
                     inscriptionViewModel.Infos.FirstName,
                     inscriptionViewModel.Infos.LastName,
                     inscriptionViewModel.Infos.Birthday
                     );
           
            inscriptionViewModel.Profile =
                dal.AddProfile(
                    "/images/" + inscriptionViewModel.Profile.ProfilImage.FileName,
                    inscriptionViewModel.Profile.Bio,
                    inscriptionViewModel.Profile.Games
                    
                    );

            inscriptionViewModel.Account =
            dal.AddAccount(
                 inscriptionViewModel.Account.Username,
                 inscriptionViewModel.Account.Password,
                 inscriptionViewModel.Contact.Id,
                 inscriptionViewModel.Infos.Id,
                 inscriptionViewModel.Profile.Id
                 );
            inscriptionViewModel.Benevole =
             dal.CreateNewBenevole(inscriptionViewModel.Account.Id);

            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, inscriptionViewModel.Account.Id.ToString()),
                };
            var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
            HttpContext.SignInAsync(userPrincipal);

            return View("ProfileViewBenevole", inscriptionViewModel);
           


        }
        //    //  {
        //    @id =

        //            inscriptionViewModel.Account.ProfileId,
        //            inscriptionViewModel.Account.ContactId,
        //            inscriptionViewModel.Account.InfoPersoId,
        //            inscriptionViewModel.Account.InventoryId

        //}


        public IActionResult SignUpAdherent()
        {
            InscriptionViewModel inscriptionViewModel = new InscriptionViewModel();

            return View(inscriptionViewModel);
        }


        [HttpPost]
        public IActionResult SignUpAdherent(InscriptionViewModel inscriptionViewModel)
        {
            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            string filePath = Path.Combine(uploads, inscriptionViewModel.Profile.ProfilImage.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                inscriptionViewModel.Profile.ProfilImage.CopyTo(fileStream);
            }
            inscriptionViewModel.Contact =
                 dal.AddContact(
                     inscriptionViewModel.Contact.EmailAdress,
                     inscriptionViewModel.Contact.TelephoneNumber
                     );
            inscriptionViewModel.Infos =
                 dal.AddInfoPerso(
                     inscriptionViewModel.Infos.FirstName,
                     inscriptionViewModel.Infos.LastName,
                     inscriptionViewModel.Infos.Birthday
                     );

                inscriptionViewModel.Profile =
                dal.AddProfile(
                    "/images/" + inscriptionViewModel.Profile.ProfilImage.FileName,
                    inscriptionViewModel.Profile.Bio,
                    inscriptionViewModel.Profile.Games
                    );

            inscriptionViewModel.Account =
            dal.AddAccount(
                 inscriptionViewModel.Account.Username,
                 inscriptionViewModel.Account.Password,
                 inscriptionViewModel.Contact.Id,
                 inscriptionViewModel.Infos.Id,
                 inscriptionViewModel.Profile.Id
                 );
            inscriptionViewModel.Contribution =
               dal.CreateNewContribution(
                   inscriptionViewModel.Contribution.RIB
                   );

            inscriptionViewModel.Adhesion =
                dal.CreateNewAdhesion(
                   inscriptionViewModel.Contribution.Id,
                   DateTime.UtcNow.AddDays( 365 ),
                   AdhesionStatus.EnCours
                    );
 
            inscriptionViewModel.Benevole =
            dal.CreateNewBenevole(inscriptionViewModel.Account.Id);

            inscriptionViewModel.Adherent =
             dal.AddAdherent(
                 inscriptionViewModel.Account.Id,
                 inscriptionViewModel.Benevole.Id,
                 inscriptionViewModel.Adhesion.Id,
                 inscriptionViewModel.Contribution.Id,
                 inscriptionViewModel.Adherent.IDDocuments
                 );

            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, inscriptionViewModel.Account.Id.ToString()),
                };
            var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
            HttpContext.SignInAsync(userPrincipal);
            //inscriptionViewModel.Account = dal.GetAccounts().Where(r => r.Id == inscriptionViewModel.Account.Id).FirstOrDefault();

            return View("ProfileViewAdherent", inscriptionViewModel);

        }



        public ActionResult ProfileViewBenevole()
        {
            
            return View();

        }

        public ActionResult ProfileViewAdherent()
        {

            return View();

        }

        public IActionResult EditProfilePIC()
        {
            InscriptionViewModel ivm = new InscriptionViewModel();
            return View();
        }

        [HttpPost]
        public IActionResult EditProfilePIC(InscriptionViewModel ivm)
        {
             
            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            string filePath = Path.Combine(uploads, ivm.Profile.ProfilImage.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                ivm.Profile.ProfilImage.CopyTo(fileStream);
            }
            dal.EditProfilePIC("/images/" + ivm.Profile.ProfilImage.FileName, ivm.Profile.Id);

            return View();
        }
        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }







        /////////////////////////////END
    }
}
