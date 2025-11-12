using Microsoft.AspNetCore.Mvc;

namespace Repositorio.Controllers
{
    public class GestorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
