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

            //string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            //string filePath = Path.Combine(uploads, model.Profile.ProfilImage.FileName);

            //using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        model.Profile.ProfilImage.CopyTo(fileStream);
            //    }


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
                dal.CreateProfileEmployee(
                    model.Profile.Bio,
                    model.Profile.Games
                    );
           model.Messagerie =
              dal.AddMessagerie();
            model.Account =
            dal.AddAccount(
                 model.Account.Username,
                 model.Account.Password,
                 model.Contact.Id,
                 model.Infos.Id,
                 model.Profile.Id,
                 model.Account.role,
                 model.Messagerie.Id
                 );

            model.Employee = dal.CreateEmployee(model.Account.Id, model.Employee.JobName, model.Employee.SerialNumber);

            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.Account.Id.ToString()),
                };
            var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("ViewDashboard", model);

        }

        public IActionResult ViewDashboard()
        {
            return View();
        }
    }
}
