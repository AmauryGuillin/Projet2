using Microsoft.AspNetCore.Authentication;
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
using Projet2.Models.Informations;

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
            InscriptionViewModel inscriptionViewModel = new InscriptionViewModel
            {
                Authentificate = HttpContext.User.Identity.IsAuthenticated
            };

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
            inscriptionViewModel.Messagerie =
                dal.AddMessagerie();


            inscriptionViewModel.Account =
            dal.AddAccount(
                 inscriptionViewModel.Account.Username,
                 inscriptionViewModel.Account.Password,
                 inscriptionViewModel.Contact.Id,
                 inscriptionViewModel.Infos.Id,
                 inscriptionViewModel.Profile.Id,
                 Projet2.Models.Role.Benevole,
                 inscriptionViewModel.Messagerie.Id
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


            inscriptionViewModel.Stuffs = dal.GetStuffs();
            List<Stuff> Stuffs = inscriptionViewModel.Stuffs;
            inscriptionViewModel.ReservationStuffs = dal.GetReservations();
            List<ReservationStuff> ListReservations = inscriptionViewModel.ReservationStuffs;



            return RedirectToAction("ProfileViewBenevole", inscriptionViewModel);



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

            InscriptionViewModel inscriptionViewModel = new InscriptionViewModel
            {
                Authentificate = HttpContext.User.Identity.IsAuthenticated
            };

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
            string uploadsPdf = Path.Combine(_webEnv.WebRootPath, "AdherentsDocuments");
            string filePathpdf = Path.Combine(uploadsPdf, inscriptionViewModel.Adherent.DocAdherent.FileName);
            using (Stream fileStreamPdf = new FileStream(filePathpdf, FileMode.Create))
            {
                inscriptionViewModel.Adherent.DocAdherent.CopyTo(fileStreamPdf);
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
            inscriptionViewModel.Messagerie =
               dal.AddMessagerie();

            inscriptionViewModel.Account =
            dal.AddAccount(
                 inscriptionViewModel.Account.Username,
                 inscriptionViewModel.Account.Password,
                 inscriptionViewModel.Contact.Id,
                 inscriptionViewModel.Infos.Id,
                 inscriptionViewModel.Profile.Id,
                 Projet2.Models.Role.Adherent,
                 inscriptionViewModel.Messagerie.Id
                 );
            inscriptionViewModel.Contribution =
               dal.CreateNewContribution(
                   inscriptionViewModel.Contribution.RIB
                   );

            inscriptionViewModel.Adhesion =
                dal.CreateNewAdhesion(
                   inscriptionViewModel.Contribution.Id,
                   DateTime.UtcNow.AddDays(365),
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
                 "/AdherentsDocuments/" + inscriptionViewModel.Adherent.DocAdherent.FileName
                 );

            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, inscriptionViewModel.Account.Id.ToString()),
                };
            var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
            HttpContext.SignInAsync(userPrincipal);
            //inscriptionViewModel.Account = dal.GetAccounts().Where(r => r.Id == inscriptionViewModel.Account.Id).FirstOrDefault();

            inscriptionViewModel.Stuffs = dal.GetStuffs();
            List<Stuff> Stuffs = inscriptionViewModel.Stuffs;
            inscriptionViewModel.ReservationStuffs = dal.GetReservations();
            List<ReservationStuff> ListReservations = inscriptionViewModel.ReservationStuffs;

            return RedirectToAction("ProfileViewAdherent", inscriptionViewModel);

        }



        public ActionResult ProfileViewBenevole()
        {
            InscriptionViewModel inscriptionViewModel = new InscriptionViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            Account accountUser = dal.GetAccount(HttpContext.User.Identity.Name);
            inscriptionViewModel.Account = accountUser;

            if (accountUser != null)
            {
                inscriptionViewModel.Profile = dal.GetProfiles().Where(r => r.Id == accountUser.ProfileId).FirstOrDefault();
                inscriptionViewModel.Infos = dal.GetInformations().Where(r => r.Id == accountUser.InfoPersoId).FirstOrDefault();
                inscriptionViewModel.Contact = dal.GetContacts().Where(r => r.Id == accountUser.ContactId).FirstOrDefault();
                inscriptionViewModel.Stuffs = dal.GetOwnedStuff(accountUser.Id);
                List<Stuff> Stuffs = inscriptionViewModel.Stuffs;
                inscriptionViewModel.ReservationStuffs = dal.GetReservations();
                List<ReservationStuff> ListReservations = inscriptionViewModel.ReservationStuffs;
                IEnumerable<Activity> lastactivities = dal.GetActivities();
                inscriptionViewModel.Activities = lastactivities.Reverse<Activity>().Take(3);
                IEnumerable<Publication> lastpublications = dal.GetPublications();
                inscriptionViewModel.Publications = lastpublications.Reverse<Publication>().Take(3);
                foreach (var Publication in inscriptionViewModel.Publications)
                {
                    Account AuthorPubli = dal.GetAccounts().Where(r => r.Id == Publication.AccountId).FirstOrDefault();
                    if (AuthorPubli != null) { Publication.Account = AuthorPubli; }
                }

                return View(inscriptionViewModel);
            }

            return RedirectToAction("Login", "Login");

        }

        public ActionResult ProfileViewAdherent()
        {
            InscriptionViewModel inscriptionViewModel = new InscriptionViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            string accountId = (HttpContext.User.Identity.Name);
            Account account = dal.GetAccount(accountId);

            if (account != null)
            {
                inscriptionViewModel.Account = dal.GetAccount(accountId);
                Account accountUser = inscriptionViewModel.Account;
                Adherent adherent = dal.GetAdherents().Where(r => r.AccountId == account.Id).FirstOrDefault();
                inscriptionViewModel.Adherent = adherent;
                inscriptionViewModel.Profile = dal.GetProfiles().Where(r => r.Id == accountUser.ProfileId).FirstOrDefault();
                inscriptionViewModel.Infos = dal.GetInformations().Where(r => r.Id == accountUser.InfoPersoId).FirstOrDefault();
                inscriptionViewModel.Contact = dal.GetContacts().Where(r => r.Id == accountUser.ContactId).FirstOrDefault();
                inscriptionViewModel.Stuffs = dal.GetStuffs();
                List<Stuff> Stuffs = inscriptionViewModel.Stuffs;
                inscriptionViewModel.ReservationStuffs = dal.GetReservations();
                List<ReservationStuff> ListReservations = inscriptionViewModel.ReservationStuffs;
                IEnumerable<Activity> lastactivities = dal.GetActivities();
                inscriptionViewModel.Activities = lastactivities.Reverse<Activity>().Take(3);
                IEnumerable<Publication> lastpublications = dal.GetPublications();
                inscriptionViewModel.Publications = lastpublications.Reverse<Publication>().Take(3);
                foreach (var Publication in inscriptionViewModel.Publications)
                {
                    Account AuthorPubli = dal.GetAccounts().Where(r => r.Id == Publication.AccountId).FirstOrDefault();
                    if (AuthorPubli != null) { Publication.Account = AuthorPubli; }
                }
                return View(inscriptionViewModel);
            }
            return RedirectToAction("Login", "Login");

        }


        public IActionResult ProfileViewParamsBenevole()
        {

            InscriptionViewModel inscriptionViewModel = new InscriptionViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            string accountId = (HttpContext.User.Identity.Name);
            Account account = dal.GetAccount(accountId);

            if (account != null)
            {
                inscriptionViewModel.Account = account;
                inscriptionViewModel.Profile = dal.GetProfiles().Where(r => r.Id == account.ProfileId).FirstOrDefault();
                inscriptionViewModel.Contact = dal.GetContacts().Where(r => r.Id == account.ContactId).FirstOrDefault();
                inscriptionViewModel.Infos = dal.GetInformations().Where(r => r.Id == account.InfoPersoId).FirstOrDefault();
                return View(inscriptionViewModel);
            }

            return RedirectToAction("Login", "Login");
        }


        [HttpPost]
        public IActionResult ProfileViewParamsBenevole(InscriptionViewModel inscriptionViewModel)
        {
            string imagepath;
            string accountId = (HttpContext.User.Identity.Name);
            Account account = dal.GetAccount(accountId);
            if (account != null)
            {
                Contact contact = dal.GetContacts().Where(r => r.Id == account.ContactId).FirstOrDefault();
                InfoPerso infos = dal.GetInformations().Where(r => r.Id == account.InfoPersoId).FirstOrDefault();
                Profile profile = dal.GetProfiles().Where(r => r.Id == account.ProfileId).FirstOrDefault();


                string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                if (inscriptionViewModel.Profile.ProfilImage != null)
                {
                    string filePath = Path.Combine(uploads, inscriptionViewModel.Profile.ProfilImage.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        inscriptionViewModel.Profile.ProfilImage.CopyTo(fileStream);
                    }
                    imagepath = "/images/" + inscriptionViewModel.Profile.ProfilImage.FileName;
                }
                else { imagepath = profile.ImagePath; }

                inscriptionViewModel.Account = dal.EditAccount(
                        account.Id,
                        inscriptionViewModel.Account.Username,
                        inscriptionViewModel.Account.Password
                        );
                inscriptionViewModel.Profile = dal.EditProfileS(
                        profile.Id,
                        imagepath,
                         inscriptionViewModel.Profile.Games,
                         inscriptionViewModel.Profile.Bio
                        );
                inscriptionViewModel.Contact = dal.EditContacts(
                        contact.Id,
                        inscriptionViewModel.Contact.EmailAdress,
                        inscriptionViewModel.Contact.TelephoneNumber
                        );

                    return RedirectToAction("ProfileViewBenevole", inscriptionViewModel);
                }

                return RedirectToAction("Login", "Login");
            }



        public IActionResult ProfileViewParamsAdherent()
        {

            InscriptionViewModel inscriptionViewModel = new InscriptionViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            string accountId = (HttpContext.User.Identity.Name);
            Account account = dal.GetAccount(accountId);

            if (account != null)
            {
                inscriptionViewModel.Account = account;
                inscriptionViewModel.Profile = dal.GetProfiles().Where(r => r.Id == account.ProfileId).FirstOrDefault();
                inscriptionViewModel.Contact = dal.GetContacts().Where(r => r.Id == account.ContactId).FirstOrDefault();
                inscriptionViewModel.Infos = dal.GetInformations().Where(r => r.Id == account.InfoPersoId).FirstOrDefault();
                Adherent adherent=dal.GetAdherents().Where(r => r.AccountId== account.Id).FirstOrDefault();
               inscriptionViewModel.Adherent= adherent;
                return View(inscriptionViewModel);
            }

            return RedirectToAction("Login", "Login");
        }


        [HttpPost]
        public IActionResult ProfileViewParamsAdherent(InscriptionViewModel inscriptionViewModel)
        {

            string accountId = (HttpContext.User.Identity.Name);
            Account account = dal.GetAccount(accountId);
            if (account != null)
            {
                string filepath;
                string imagepath;
                Contact contact = dal.GetContacts().Where(r => r.Id == account.ContactId).FirstOrDefault();
                InfoPerso infos = dal.GetInformations().Where(r => r.Id == account.InfoPersoId).FirstOrDefault();
                Profile profile = dal.GetProfiles().Where(r => r.Id == account.ProfileId).FirstOrDefault();
                Adherent adherent=dal.GetAdherents().Where(r=>r.AccountId== account.Id).FirstOrDefault();
                inscriptionViewModel.Adherent= adherent;

                string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                if (inscriptionViewModel.Profile.ProfilImage != null) {
                    string filePath = Path.Combine(uploads, inscriptionViewModel.Profile.ProfilImage.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        inscriptionViewModel.Profile.ProfilImage.CopyTo(fileStream);
                    }
                    imagepath = "/images/" + inscriptionViewModel.Profile.ProfilImage.FileName;
                }
                else { imagepath = profile.ImagePath; }

                string uploadsPdf = Path.Combine(_webEnv.WebRootPath, "AdherentsDocuments");
                if (inscriptionViewModel.Adherent.DocAdherent != null)
                {
                    string filePathpdf = Path.Combine(uploadsPdf, inscriptionViewModel.Adherent.DocAdherent.FileName);
                    using (Stream fileStreamPdf = new FileStream(filePathpdf, FileMode.Create))
                    {
                        inscriptionViewModel.Adherent.DocAdherent.CopyTo(fileStreamPdf);
                    }
                    filepath = "/AdherentsDocuments/" + inscriptionViewModel.Adherent.DocAdherent.FileName;
                }
                else { filepath = adherent.DocPath; }
                
               
                inscriptionViewModel.Account = dal.EditAccount(
                        account.Id,
                        inscriptionViewModel.Account.Username,
                        inscriptionViewModel.Account.Password
                        );
                inscriptionViewModel.Profile = dal.EditProfileS(
                        profile.Id,
                         imagepath,
                         inscriptionViewModel.Profile.Games,
                         inscriptionViewModel.Profile.Bio
                        );
                inscriptionViewModel.Contact = dal.EditContacts(
                        contact.Id,
                        inscriptionViewModel.Contact.EmailAdress,
                        inscriptionViewModel.Contact.TelephoneNumber
                        );
                
                inscriptionViewModel.Adherent = dal.EditAdherent(
                    adherent.Id,
                    filepath
                    );

                return RedirectToAction("ProfileViewAdherent", inscriptionViewModel);
            }

            return RedirectToAction("Login", "Login");
        }



        



        public ActionResult Deconnexion()
            {
                HttpContext.SignOutAsync();
                return Redirect("/");
            }







            /////////////////////////////END
        }
    } 
