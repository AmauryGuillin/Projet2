using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Linq;

namespace Projet2.Controllers
{
    public class PlanningController : Controller
    {

        private Dal dal;
        public PlanningController()
        {
            dal = new Dal();
        }
        public IActionResult PlanningView()
        {
            PlanningViewModel model = new PlanningViewModel();
            model.Account= dal.GetAccount(HttpContext.User.Identity.Name);
            Account account = model.Account;
            //   model.slots = (System.Collections.Generic.List<Slot>)dal.GetSlots().Where(r => r.PlanningId == model.Account.PlanningId);
            model.slots=dal.GetSlots().Where(r => r.PlanningId == account.PlanningId).ToList();
            return View();
        }
    }
}
