using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Xml.Linq;

namespace Projet2.Controllers
{
    public class StuffController : Controller
    {
        private Dal dal;

        public StuffController()
        {
            dal = new Dal();
        }
        //private IWebHostEnvironment _webEnv;
        //public StuffController(IWebHostEnvironment environment)
        //{
        //    _webEnv = environment;
        //    this.dal = new Dal();
        //}

        public IActionResult CreateStuff(int accountId)
        {
            //StuffViewModel model = new StuffViewModel();
            //model.InventoryId = inventoryId;
            //model.AccountId = accountId;

            Stuff stuff = new Stuff();
            stuff.AccountOwnerId = accountId;


            return View(stuff);
        }

        [HttpPost]
        public IActionResult CreateStuff(Stuff stuff)
        { 

            if (ModelState.IsValid)
            {
                //string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                //string filePath = Path.Combine(uploads, model.StuffImage.FileName);
                //using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    model.StuffImage.CopyTo(fileStream);
                //}
                //dal.CreateStuff(
                //    model.Name, "/images/" + model.StuffImage.FileName, model.Type, model.State);

                Stuff stuffCreated = dal.CreateStuff(stuff);

         
                return View("Index");
            }
            return View();
        }

        public IActionResult StuffCatalog()
        {
            List<Stuff> listStuff = dal.GetStuffs();
            return View(listStuff);
        }

        public IActionResult CreateBookStuff(int id)
        {
            ProfileViewModel model = new ProfileViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {

                

                string accountId = (HttpContext.User.Identity.Name);
                Stuff stuff = dal.GetOneStuff(id);
                model.Stuff = stuff;
                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;
                model.Stuff.AccountBorrowerId = userAccount.Id;
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();


                //inscriptionViewModel.Account = dal.GetAccount(accountId);
                //Account accountUser = inscriptionViewModel.Account;
                //inscriptionViewModel.Profile = dal.GetProfiles().Where(r => r.Id == accountUser.ProfileId).FirstOrDefault();
                //inscriptionViewModel.Infos = dal.GetInformations().Where(r => r.Id == accountUser.InfoPersoId).FirstOrDefault();
                //inscriptionViewModel.Contact = dal.GetContacts().Where(r => r.Id == accountUser.ContactId).FirstOrDefault();
                //return View(inscriptionViewModel);



                //model.Stuff.AccountBorrowerId = dal.GetAccount(accountId);

             return View(model);
            }
            
            return View(model);

        }

        [HttpPost]
        public IActionResult CreateBookStuff(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ReservationStuff.StuffId = model.Stuff.Id;

                
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;
                //int idStuff = model.Stuff.Id;
                //int idAccount = model.Account.Id;
                //dal.EditStuffReservation(idStuff, idAccount);
                //model.Stuff.AccountBorrowerId = userAccount.Id;
                dal.EditStuffReservation(model.Stuff.Id, model.Account.Id);
                ReservationStuff reservationCreated = dal.CreateReservationStuff(model.ReservationStuff);


                return View("Index");
            }

            return View();
        }

    }
}
