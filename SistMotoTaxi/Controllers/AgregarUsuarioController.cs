using SistMotoTaxi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistMotoTaxi.Controllers
{
    public class AgregarUsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }





        /*public ActionResult Registrar()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult Registrar(string nombre, string usuario, string password, int rol)
            {
            //ool registro = this.exa.RegistrarUsuario(nombre, usuario, password, rol);
            bool registro = false;
            if (registro)
            {

                ModelState.AddModelError("", " Usuario registrado con exito.");

            }
            else
            {
                ModelState.AddModelError("", "Error al registrar el usuario");
            }

            return View();
             }
        */
        public ActionResult LogOff()
        {
            Session.Contents.RemoveAll();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Inicio");
        }

    }
}