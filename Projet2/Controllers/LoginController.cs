using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace Projet2.Controllers
{
    public class LoginController : Controller
    {
        private Dal dal;
        public LoginController()
        {
            dal = new Dal();
        }
        public IActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentificate == true)
            {
                viewModel.Account = dal.GetAccount(HttpContext.User.Identity.Name);
                return View(viewModel);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Account account = dal.Authentificate(viewModel.Account.Username, viewModel.Account.Password);
                if (account != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Id.ToString())
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return Redirect("/");
                }
                ModelState.AddModelError("Account.Username", "Nom d'utilisateur et/ou mot de passe incorrect(s)");
            }
            return View(viewModel);
        }


        //public IActionResult CreateAccount()
        //{
        //    LoginViewModel accountViewModel = new LoginViewModel();
        //    return View(accountViewModel);
        //}


        //[HttpPost]
        //public IActionResult CreateAccount(LoginViewModel accountViewModel)
        //{
        //    accountViewModel= new LoginViewModel();
        //    if (accountViewModel.Authentificate==true)
        //    {
        //        Account accountCreated = dal.AddAccount(accountViewModel.Account.Username, accountViewModel.Account.Password);
                
                
        //        Benevole benevole= dal.CreateNewBenevole(accountCreated.Id);

        //        var userClaims = new List<Claim>()
        //        {
        //            new Claim(ClaimTypes.Name, accountCreated.Id.ToString()),
        //        };

        //        var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

        //        var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
        //        HttpContext.SignInAsync(userPrincipal);

        //        return RedirectToAction("EditProfile", "Profile", new {id= accountCreated.ProfileId, accountCreated.ContactId ,accountCreated.infoPerso, accountCreated.InventoryId,benevole.Id});
        //    }
        //    return View(accountViewModel);
        //}




        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }



    }
}



