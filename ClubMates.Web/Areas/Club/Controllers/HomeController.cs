using Microsoft.AspNetCore.Mvc;

namespace ClubMates.Web.Areas.Club.Controllers
{
    [Area("Club")]
    [Route("Club/[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
