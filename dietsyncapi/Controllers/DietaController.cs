using Microsoft.AspNetCore.Mvc;

namespace dietsync.Controllers
{
    public class DietaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
