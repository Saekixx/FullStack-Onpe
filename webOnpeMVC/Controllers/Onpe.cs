using Microsoft.AspNetCore.Mvc;

namespace webOnpeMVC.Controllers
{
    public class Onpe : Controller
    {
        public IActionResult Inicio()
        {
            return View();
        }

        public IActionResult Presidencial()
        {
            return View();
        }

        public IActionResult Participacion()
        {
            return View();
        }

        public IActionResult Actas_Ubigeo()
        {
            return View();
        }

        public IActionResult Actas_Numero()
        {
            return View();
        }
    }
}
