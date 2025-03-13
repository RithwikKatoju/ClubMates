using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubMates.Web.Controllers
{
    public class ClubsController : Controller
    {

        //[Authorize("MustBeAGuest")]||[Authorize("MustBeASuperAdmin")]
        [Authorize("GuestOrSuperAdmin")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
