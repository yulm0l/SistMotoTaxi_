using SistMotoTaxi.Filters;
using SistMotoTaxi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistConstAutorizacion.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        //[Permisos(_IdRol: 1, _IdRol2: 0)]
        public ActionResult Inicio()
        {
            Usuarios oUsuario = (Usuarios)Session["Usuario"];
            if (oUsuario != null)
            {
                return View();
                //Dashboard listaG = Dashboard.General();
                //return View(listaG);
            }
            else
            {
                //var user = Session["IdUsuario"];
                //HomeController Ctrl = new HomeController();
                //Ctrl.LogOff(Convert.ToInt32(user));
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult Alta(Usuarios model)

        {
            if (ModelState.IsValid)
            {
                Usuarios Usuario = Users.IsValid(model.Nombre, model.Usuario, model.Password, model.Rol);
                if (Usuario.Usuario != "Usuario no Valido")
                {
                    Session["Usuario"] = Usuario;

                    System.Web.Security.FormsAuthentication.SetAuthCookie(Usuario.Usuario, false);

                    return RedirectToAction("Alta", "Home");
                }
                else if (Usuario.Usuario == "Usuario no Valido")
                {
                    ModelState.AddModelError("", "El usuario o la contraseña son incorrectos.");
                }

            }
            else { ModelState.AddModelError("", "El usuario o la contraseña son incorrectos."); }
            return View(model);
        }



        [Permisos(_IdRol: 1, _IdRol2: 0)]
        public ActionResult LlamarJson()
        {
            var output = ObtenerConcesionado();
            var jsonResult = Json(new
            {
                graphStatus = output
            },
                JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        private List<Dashboard> ObtenerConcesionado()
        {
            int i = 1;
            IEnumerable<Dashboard> lista = Dashboard.Concesion();
            List<Dashboard> lsconcesion = new List<Dashboard>();
            for (i = 1; i <= lista.Count(); i++)
            {
                lsconcesion.Add(
                    new Dashboard()
                    {
                        Año = lista.ElementAt(i - 1).Año.ToString(),
                        Conteo = lista.ElementAt(i - 1).Conteo
                    });
            }
            return lsconcesion;
        }


        [Permisos(_IdRol: 1, _IdRol2: 0)]
        public ActionResult LlamarJson_Permiso()
        {
            var output = ObtenerPermisionado();
            var jsonResult = Json(new
            {
                graphStatus = output
            },
                JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [Permisos(_IdRol: 1, _IdRol2: 0)]
        private List<Dashboard> RegistrarUsuario()
        {
            int i = 1;
            IEnumerable<Dashboard> lista = Dashboard.Permiso();
            List<Dashboard> DatosA = new List<Dashboard>();
            for (i = 1; i <= lista.Count(); i++)
            {
                DatosA.Add(
                    new Dashboard()
                    {
                        //Año = lista.ElementAt(i - 1).AñoP.ToString(),
                        Conteo = lista.ElementAt(i - 1).ConteoP
                    });
            }
            return DatosA;

        }

        private List<Dashboard> ObtenerPermisionado()
        {
            int i = 1;
            IEnumerable<Dashboard> lista = Dashboard.Permiso();
            List<Dashboard> lsconcesion = new List<Dashboard>();
            for (i = 1; i <= lista.Count(); i++)
            {
                lsconcesion.Add(
                    new Dashboard()
                    {
                        Año = lista.ElementAt(i - 1).AñoP.ToString(),
                        Conteo = lista.ElementAt(i - 1).ConteoP
                    });
            }
            return lsconcesion;
        }
    }
}