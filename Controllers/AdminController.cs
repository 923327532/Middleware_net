using Microsoft.AspNetCore.Mvc;

namespace Semana09.Caso1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return Content("Panel de administración - Acceso concedido");
        }
    }
}
