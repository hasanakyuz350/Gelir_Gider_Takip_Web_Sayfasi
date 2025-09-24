using Gelir_Gider_Takibi.DBConTexT;
using Gelir_Gider_Takibi.EnTiTy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gelir_Gider_Takibi.Controllers
{
    public class UserController : Controller
    {
        private readonly dbconTexT conTexT;
        public UserController(dbconTexT conTexT)
        {
            this.conTexT = conTexT;
        }

        public IActionResult Sign_in()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sign_in(string email, string password)
        {
            var user = conTexT.user.FirstOrDefault(u => u.email == email && u.password == password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("userID", user.userID);

                TempData["SuccessMessage"] = "Basarılı giris yapıldı.";

                return RedirectToAction("HisToryMonThSalarySpending", "ThisMonThSalary");
            }
            else
            {
                ViewBag.Error = "Email veya sifre yanlıs.";

                return View();
            }
        }

        public IActionResult Sign_up()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sign_up(User users)
        {
            conTexT.user.Add(users);
            conTexT.SaveChanges();

            TempData["SuccessMessage"] = "Hesap basarıyla olusTuruldu. Giris yapabilirsiniz.";

            return RedirectToAction("Sign_in", "User");
        }

        public IActionResult Sign_ouT()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Sign_in", "User");
        }
    }
}
