using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;


namespace Projet2.Controllers
{
    public class AccountController : Controller
    {
        private Dal dal;
        public AccountController()
        {
            dal = new Dal();
        }
        public IActionResult CreateAccount()
        {
            //AccountViewModel accountViewModel = new AccountViewModel();
            return View();
        }


        [HttpPost]
        public IActionResult CreateAccount(AccountViewModel accountViewModel)
        {
            //accountViewModel = new AccountViewModel();
            accountViewModel.Account = 
             dal.AddAccount(accountViewModel.Account.Username, accountViewModel.Account.Password);
           accountViewModel.Benevole = 
              dal.CreateNewBenevole(accountViewModel.Account.Id);

            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, accountViewModel.Account.Id.ToString()),
                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);


            return RedirectToAction("EditProfile", "Profile", new { id = 
                accountViewModel.Account.ProfileId, 
                accountViewModel.Account.ContactId,
                accountViewModel.Account.infoPerso,
                accountViewModel.Account.InventoryId 
            });
            
     
        }




        ///////////////////////////END 
        ///
        //public IActionResult CreateAccountAdherent()
        //{
        //    //AccountViewModel accountViewModel = new AccountViewModel();
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult CreateAccountAdherent(AccountViewModel accountViewModel)
        //{
        //    //accountViewModel = new AccountViewModel();
        //    accountViewModel.Account =

        //     dal.AddAccount(accountViewModel.Account.Username, accountViewModel.Account.Password);
        //    accountViewModel.Adherent = dal.CreateNewAdherent(accountViewModel.Account.Id);

        //    var userClaims = new List<Claim>()
        //        {
        //            new Claim(ClaimTypes.Name, accountViewModel.Account.Id.ToString()),
        //        };

        //    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

        //    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
        //    HttpContext.SignInAsync(userPrincipal);
        //    return View("AdhesionAdherent", new { id = accountViewModel.Account.ProfileId, accountViewModel.Account.ContactId, accountViewModel.Account.infoPerso, accountViewModel.Account.InventoryId });


        //}


        /////////////////////END
    }
}
