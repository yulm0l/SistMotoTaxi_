using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistMotoTaxi.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 505:
                    //ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Title = error;
                    ViewBag.Description = "Problema de actualización o versión!!! ..";
                    break;

                case 404:
                    //ViewBag.Title = "Página no encontrada";
                    ViewBag.Title = error;
                    ViewBag.Description = "La URL que está intentando ingresar no existe!!!";
                    break;

                default:
                    //ViewBag.Title = "Página no encontrada";
                    ViewBag.Title = error;
                    ViewBag.Description = "Algo salio muy mal :( ..";
                    break;
            }
            return View();
        }

        [HttpGet]
        public ActionResult UnauthorizedOperation(String modulo, String msjeErrorExcepcion)
        {
            //ViewBag.operacion = operacion;
            ViewBag.modulo = modulo;
            //ViewBag.msjeErrorExcepcion = msjeErrorExcepcion;
            return View();
        }
    }
}
