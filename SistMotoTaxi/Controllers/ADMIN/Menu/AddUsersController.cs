/*using SistMotoTaxi.Filters;
using SistMotoTaxi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistConstAutorizacion.Controllers
{
    public class AddUserController : Controller
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
}*/

using SistMotoTaxi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistMotoTaxi.Controllers
{
    public class AddUserContoller : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


      

        /*
        public ActionResult Alta( Users model)
        {
            if (ModelState.IsValid)
            {
                //Usuarios Usuario = Users.IsValid(model.IdUsuario, model.IdRol, model.Usuario,model.Password, model.Nombre, model.Password, model.FechaCreacion, model.Status);


                Usuarios Usuario = Users.IsValid(model ., model.contrasena);
                    //(int _iduser, int _idrol, string _user, string _name, string _password, string _date, int status);
                

                if (Usuario.Usuario != "Administrador")
                {
                    Session["Administrador"] = Usuario;

                    System.Web.Security.FormsAuthentication.SetAuthCookie(Usuario.Usuario, false);

                    return RedirectToAction("Inicio", "OpcionesMenu");
                }
                else if (Usuario.Usuario == "Usuario no Valido")
                {
                    ModelState.AddModelError("", "No se le permite realizar cambios");
                }

            }
            else { ModelState.AddModelError("", "No se le permite realizar cambios"); }
            return View(model);
        }
        
        

        /*
        public ActionResult LogOff()
        {
            Session.Contents.RemoveAll();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Inicio");
        }
        */
    }
}