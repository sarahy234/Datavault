using Microsoft.AspNetCore.Mvc;

namespace Repositorio.Controllers
{
    public class CoordinacionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
