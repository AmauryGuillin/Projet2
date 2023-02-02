using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using System.Linq;

namespace Projet2.Controllers
{
    public class AdherentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult EditAdherent(int id)
        {

            if (id != 0)
            {
                using (IDal  dal = new Dal())
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


        public IActionResult CreateAdherent(Adherent adherent)
        {
            using (Dal dal = new Dal())
            {
                dal.CreateAdherent(adherent);

                return RedirectToAction("EditAdherent", new { @id = adherent.Id });
            }


        }








    }
}
