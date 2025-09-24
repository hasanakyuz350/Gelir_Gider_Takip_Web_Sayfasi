using Gelir_Gider_Takibi.DBConTexT;
using Gelir_Gider_Takibi.EnTiTy;
using Gelir_Gider_Takibi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gelir_Gider_Takibi.Controllers
{
    public class ThisMonThSalarySpendingController : Controller
    {
        private readonly dbconTexT conTexT;
        public ThisMonThSalarySpendingController(dbconTexT conTexT)
        {
            this.conTexT = conTexT;
        }

        public IActionResult NewMonThSlarySpending(int monThID)
        {
            var userID = HttpContext.Session.GetInt32("userID");
            if (userID == null)
            {
                return RedirectToAction("Sign_in", "User");
            }

            var lasTsalary = conTexT.ThismonThsalary
                   .Include(x => x.ThismonThsalaryspending)
                   .FirstOrDefault(x => x.monThID == monThID && x.userID == userID);

            var caTegories = conTexT.caTegory.ToList();

            if (lasTsalary == null)
            {
                return RedirectToAction("NewMonThSalary", "ThisMonThSalary");
            }
            else if (lasTsalary.ThismonThsalaryspending.Any())
            {
                return RedirectToAction("HisToryMonThSalarySpending", "ThisMonThSalary");
            }

            var model = new ThismonThsalary_spendingModels
            {
                ThismonThsalary = lasTsalary,
                caTegoryspending = caTegories
            };

            if (model == null)
            {
                ViewBag.BosMu = true;
                return View();
            }
            else
            {
                ViewBag.BosMu = false;
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult NewMonThSlarySpending(int monThID, List<decimal> amounT, List<int> caTegoryID)
        {
            var userID = HttpContext.Session.GetInt32("userID");

            var selecTedMonTh = conTexT.ThismonThsalary
                .FirstOrDefault(x => x.userID == userID && x.monThID == monThID);

            if (selecTedMonTh == null)
            {
                return RedirectToAction("NewMonThSalary", "ThisMonThSalary");
            }

            var expense = conTexT.ThismonThsalaryspending
                .Where(x => x.monThID == monThID)
                .ToList();

            decimal currenToTal = 0;
            if (expense != null || expense.Count != 0)
            {
                foreach (var myexpense in expense)
                {
                    currenToTal += (decimal)myexpense.amounT;

                }
            }

            var newToTal = amounT.Sum();

            var remain = selecTedMonTh.salary - (currenToTal + newToTal);

            if (remain < 0)
            {
                TempData["ErrorMessage"] = "Girdiğiniz harcamalar maası asıyor!";

                return RedirectToAction("NewMonThSlarySpending", new { monThID = monThID });
            }

            var caTegories = conTexT.caTegory.ToList();

            for (int i = 0; i < caTegories.Count; i++)
            {
                if (amounT[i] > 0)
                {
                    var spending = new ThisMonThSalarySpending
                    {
                        monThID = selecTedMonTh.monThID,
                        caTegoryID = caTegoryID[i],
                        amounT = amounT[i],
                    };

                    conTexT.ThismonThsalaryspending.Add(spending);
                }
            }
            conTexT.SaveChanges();

            return RedirectToAction("HisToryMonThSalarySpending", "ThisMonThSalary");
        }
    }
}
