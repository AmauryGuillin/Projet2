using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Projet2.Controllers
{
    /// <summary>
    /// Controller for Login.
    /// </summary>
    public class LoginController : Controller
    {
        private Dal dal;//An instance of the "Dal" class
        
        public LoginController()
        {
            dal = new Dal();
        }

        /// <summary>
        /// Display the login page.
        /// </summary>
        /// <returns>View of the login page with the appropriate view template.</returns>
        public IActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel 
            { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentificate == true)
            {
                viewModel.Account = dal.GetAccount(HttpContext.User.Identity.Name);
             
                return View(viewModel);
            }
            return View(viewModel);
        }

        /// <summary>
        /// Authenticate a user and redirect to a page based on their role.
        /// </summary>
        /// <param name="viewModel">The view model containing the connection information.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
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
                    //viewModel.Account.role = dal.GetAccount(HttpContext.User.Identity.Name).role;
                    //viewModel.Account.ProfileId = dal.GetAccount(HttpContext.User.Identity.Name).ProfileId;

                   
                    switch (account.role)
                    {
                        case Role.Admin:
                            return RedirectToAction("Index", new {id =account.Id});
                        //break;

                        case Role.Salarie:
                     return RedirectToAction("Index", new { id = account.Id }); 
            
                        //break;
                        case Role.Benevole:
                            
                            dal.GetProfiles().Where(r => r.Id == account.ProfileId);
                            return RedirectToAction("Index", new { id=
                            account.Id
                            });
                        //break;
                        case Role.Adherent:
                            dal.GetProfiles().Where(r => r.Id == account.ProfileId);
                            return RedirectToAction("Index", new
                            {
                                id =
                             account.Id
                            });
                    }

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


        //     switch (viewModel.Account.role)
        //            {
        //                case Role.Admin:
        //                    return View("Index");
        //                //break;
        //                case Role.Salarié:
        //                    return View("Index");
        //                //break;
        //                case Role.Benevole:
        //                    return RedirectToAction("ProfileViewBenevole", "Inscription");
        //                //break;
        //                case Role.Adherent:
        //                    return RedirectToAction("ProfileViewAdherent", "Inscription");

        //}

        /// <summary>
        /// Returns the view for the Index page
        /// </summary>
        /// <returns>View Index</returns>
        public ActionResult Index()
        {
            LoginViewModel viewModel = new LoginViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated==true)
            {
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
               
                string accountId = (HttpContext.User.Identity.Name);
                viewModel.Account = dal.GetAccount(accountId);
               
                return View(viewModel);
            }
            return View(viewModel);
        }


        /// <summary>
        /// Logs the user out by deleting the authentication cookies and redirects him to the login page.
        /// </summary> 
        /// <returns>Redirect to login page</returns>

        public IActionResult About()
        {
            LoginViewModel viewModel = new LoginViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                string accountId = (HttpContext.User.Identity.Name);
                viewModel.Account = dal.GetAccount(accountId);

                return View(viewModel);
            }
            return View(viewModel);
        }

        public IActionResult Materiel()
        {
            LoginViewModel viewModel = new LoginViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                string accountId = (HttpContext.User.Identity.Name);
                viewModel.Account = dal.GetAccount(accountId);

                return View(viewModel);
            }
            return View(viewModel);
        }

        public IActionResult Coaching()
        {
            LoginViewModel viewModel = new LoginViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                string accountId = (HttpContext.User.Identity.Name);
                viewModel.Account = dal.GetAccount(accountId);

                return View(viewModel);
            }
            return View(viewModel);
        }

        public IActionResult Contact()
        {
            LoginViewModel viewModel = new LoginViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                string accountId = (HttpContext.User.Identity.Name);
                viewModel.Account = dal.GetAccount(accountId);

                return View(viewModel);
            }
            return View(viewModel);
        }


        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }



    }
}



