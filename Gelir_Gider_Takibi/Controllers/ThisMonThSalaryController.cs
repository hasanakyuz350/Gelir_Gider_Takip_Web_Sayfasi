using Gelir_Gider_Takibi.DBConTexT;
using Gelir_Gider_Takibi.EnTiTy;
using Gelir_Gider_Takibi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gelir_Gider_Takibi.Controllers
{
    public class ThisMonThSalaryController : Controller
    {
        private readonly dbconTexT conTexT;
        public ThisMonThSalaryController(dbconTexT conTexT)
        {
            this.conTexT = conTexT;
        }

        public IActionResult NewMonThSalary()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewMonThSalary(string monTh, int year, decimal salary)
        {
            var userID = HttpContext.Session.GetInt32("userID");

            if (userID == null)
            {
                return RedirectToAction("Sign_in", "User");
            }

            var monThsalary = new ThisMonThSalary
            {
                monThname = monTh,
                year = year,
                salary = salary,
                userID = userID.Value
            };

            conTexT.ThismonThsalary.Add(monThsalary);
            conTexT.SaveChanges();

            return RedirectToAction("HisToryMonThSalarySpending", "ThisMonThSalary");
        }

        public IActionResult HisToryMonThSalarySpending()
        {
            var userID = HttpContext.Session.GetInt32("userID");

            if (userID == null)
            {
                return RedirectToAction("Sign_in", "User");
            }

            var userspending = conTexT.ThismonThsalary
                .Where(s => s.userID == userID)
                .Include(s => s.ThismonThsalaryspending)
                .ThenInclude(x => x.caTegory)
                .ToList();

            if (userspending == null || !userspending.Any())
            {
                return RedirectToAction("NewMonThSalary", "ThisMonThSalary");
            }

            var model = userspending.Select(m => new ThismonThsalary_spendingModels
            {
                ThismonThsalary = m,
                ThismonThsalaryspending = m.ThismonThsalaryspending
            }).ToList();

            if (model == null || !model.Any())
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
        public IActionResult deleTemonTh(int monThID)
        {
            var userID = HttpContext.Session.GetInt32("userID");

            if (userID == null)
            {
                return RedirectToAction("Sign_in", "User");
            }

            var monTh = conTexT.ThismonThsalary
                .Include(x => x.ThismonThsalaryspending)
                .FirstOrDefault(x => x.monThID == monThID && x.userID == userID);

            if (monTh != null)
            {
                if (monTh.ThismonThsalaryspending != null && monTh.ThismonThsalaryspending.Any())
                {
                    conTexT.ThismonThsalaryspending.RemoveRange(monTh.ThismonThsalaryspending);
                }

                conTexT.ThismonThsalary.Remove(monTh);
                conTexT.SaveChanges();

                TempData["SuccessMessage"] = "Ay basarıyla silindi.";
            }

            return RedirectToAction("HisToryMonThSalarySpending", "ThisMonThSalary");
        }
    }
}
