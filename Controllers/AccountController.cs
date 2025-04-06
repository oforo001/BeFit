using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
