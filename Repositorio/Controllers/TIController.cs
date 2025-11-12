using Microsoft.AspNetCore.Mvc;

namespace Repositorio.Controllers
{
    public class TIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
