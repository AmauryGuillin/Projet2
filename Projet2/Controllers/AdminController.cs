using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace Projet2.Controllers
{
    public class AdminController : Controller
    {

        private Dal dal;
        private IWebHostEnvironment _webEnv;
        public AdminController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }

        public IActionResult CreateEmployee()
        {

            AdminViewModel model = new AdminViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateEmployee(AdminViewModel model)
        {

            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            //string filePath = Path.Combine(uploads, model.Profile.ProfilImage.FileName);
            string filePath = "D:/Formation Ingé informatique/répertoire VS/Projet2/Projet2/wwwroot/images/a1wdD6zl_700w_0.jpg";

                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Profile.ProfilImage.CopyTo(fileStream);
                }


            model.Contact =
                 dal.AddContact(
                     model.Contact.EmailAdress,
                     model.Contact.TelephoneNumber
                     );
            model.Infos =
                 dal.AddInfoPerso(
                     model.Infos.FirstName,
                     model.Infos.LastName,
                     model.Infos.Birthday
                     );

            model.Profile =
                dal.AddProfile(
                    "/images/" + model.Profile.ProfilImage.FileName,
                    model.Profile.Bio,
                    model.Profile.Games
                    );

            model.Account =
            dal.AddAccount(
                 model.Account.Username,
                 model.Account.Password,
                 model.Contact.Id,
                 model.Infos.Id,
                 model.Profile.Id
                 );

            model.Employee = dal.CreateEmployee(model.Account.Id);

            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.Account.Id.ToString()),
                };
            var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("ViewDashboard", model);

            //redirect au dashboard avec la liste des employés
        }

        public IActionResult ViewDashboard()
        {
            return View();
        }
    }
}
