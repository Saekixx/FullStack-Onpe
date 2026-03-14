using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.Intrinsics.Arm;
using webOnpeMVC.Controllers.dao;
using webOnpeMVC.Models;

namespace webOnpeMVC.Controllers
{
    public class Onpe : Controller
    {
        dao.daoVoto daoVoto = new dao.daoVoto();

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

        public IActionResult Participacion_Nacional()
        {
            return View(daoVoto.getVotosDepartamentos());
        }

        public IActionResult Participacion_Extranjero()
        {
            return View( daoVoto.getVotosExtranjero() );
        }

        public IActionResult Participacion_Provincia(string departamento)
        {
            ViewBag.Departamento = departamento;
            return View( daoVoto.getVotosProvincia(departamento) );
        }
        public IActionResult Participacion_Distrito(string provincia, string departamento)
        {
            ViewBag.Provincia = provincia;
            ViewBag.Departamento = departamento;
            return View( daoVoto.getVotosDistrito(provincia) );
        }

        public IActionResult ParticipacionDistritoDetalle(string departamento, string provincia, string distrito, int TV, string PTV, int TA, string PTA, int EH)
        {
            ViewBag.Departamento = departamento;
            ViewBag.Provincia = provincia;
            ViewBag.Distrito = distrito;
            ViewBag.TV = TV;
            ViewBag.PTV = PTV;
            ViewBag.TA = TA;
            ViewBag.PTA = PTA;
            ViewBag.EH = EH;
            return View();
        }

        [HttpGet]
        public IActionResult Actas_Ubigeo()
        {
            ViewBag.DepartamentosPeru = daoVoto.getVotosDepartamentos();
            ViewBag.DepartamentosExtranjero = daoVoto.getVotosExtranjero();

            return View();
        }

        public IActionResult Actas_Numero(string nroMesa)
        {
            if( nroMesa == null)
            {
                ViewBag.Busqueda = false;
                return View();
            }
                

            var model = daoVoto.getGrupoVotacion(nroMesa);
            ViewBag.Busqueda = true;
            return View( model );
        }
    
    }
}
