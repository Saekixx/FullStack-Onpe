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
            // Usamos las listas de Departamentos puras, no de votos
            var todosLosDeptos = daoVoto.getDepartamentos();

            // Filtramos por ID como me indicaste (1-25 Perú, 26-30 Extranjero)
            ViewBag.DepartamentosPeru = todosLosDeptos.Where( d => (d.idDepartamento) <= 25).ToList();
            ViewBag.DepartamentosExtranjero = todosLosDeptos.Where( d => (d.idDepartamento) > 25).ToList();

            return View();
        }

        [HttpPost]
        public JsonResult GetProvincias(string idDepartamento)
        {
            // Usamos el ID para obtener las provincias
            var provincias = daoVoto.GetProvincias(idDepartamento);
            return Json(provincias);
        }

        [HttpPost]
        public JsonResult GetDistritos(string idProvincia)
        {
            // Usamos el ID para obtener los distritos
            var distritos = daoVoto.GetDistritos(idProvincia);
            return Json(distritos);
        }

        [HttpPost]
        public JsonResult GetLocalesVotacion(string idDistrito)
        {
            var local = daoVoto.getLocalesVotacion(idDistrito);
            return Json(local);
        }

        [HttpPost]
        public JsonResult GetLocales(string idDistrito)
        {
            var lista = daoVoto.getGrupoVotacion(idDistrito);
            return Json(lista);
        }

        [HttpPost]
        public JsonResult GetMesa(string idLocal)
        {
            var mesas = daoVoto.GetGruposVotacions(idLocal);
            return Json( mesas );
        }

        [HttpPost]
        public IActionResult MesaDetalle(string nroMesa)
        {
            var model = daoVoto.getGrupoVotacion(nroMesa);

            if (model == null)
            {
                return Content("<div class='alert alert-warning'>No se encontró información detallada para la mesa " + nroMesa + "</div>");
            }

            return PartialView("_DetalleMesa", model);
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
