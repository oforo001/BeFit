using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeFit.Controllers
{
    public class TrainingSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
