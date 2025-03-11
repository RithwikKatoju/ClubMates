using Microsoft.AspNetCore.Mvc;

namespace ClubMates.Web.Controllers
{
    public class ClubsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
