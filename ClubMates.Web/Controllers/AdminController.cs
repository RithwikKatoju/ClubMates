using Microsoft.AspNetCore.Mvc;

namespace ClubMates.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
