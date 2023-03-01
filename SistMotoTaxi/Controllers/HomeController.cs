using SistMotoTaxi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistMotoTaxi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Logeo model)
        {
            if (ModelState.IsValid)
            {
                //var Rol
                Usuarios Usuario = Logeo.IsValid(model.user, model.contrasena);
                if (Usuario.Usuario != "Usuario no Valido")
                {
                    Session["Usuario"] = Usuario;
                  
                    System.Web.Security.FormsAuthentication.SetAuthCookie(Usuario.Usuario, false);

                    return RedirectToAction("Inicio", "Menu");
                }
                else if (Usuario.Usuario == "Usuario no Valido")
                {
                    ModelState.AddModelError("", "El usuario o la contraseña son incorrectos.");
                }

            }
            else { ModelState.AddModelError("", "El usuario o la contraseña son incorrectos."); }
            return View(model);
        }
        






        /*public ActionResult Registrar(Usuarios oUsuario)
        {
            

            IDbConnection cnn = null;
            //using (SqlConnection connection = new SqlConnectikon(ConBD))
            try{

                var datos = new Dapper.DynamicParameters();
                datos.Add("@Nombre", dbType: DbType.String, direction: ParameterDirection.Output);
                datos.Add("@Uusario", dbType: DbType.String, direction: ParameterDirection.Output);
                datos.Add("@Password", dbType: DbType.String, direction: ParameterDirection.Output);
                datos.Add("@Rol", dbType: DbType.Int32, direction: ParameterDirection.Output);

                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "sp_AltaUsuario", datos, commandType: CommandType.StoredProcedure);
                Usuarios DatosA = new Usuarios();
                DatosA.Nombre = datos.Get<string>("@Nombre");
                DatosA.Usuario = datos.Get<string>("@Usuario");
                DatosA.Password = datos.Get<string>("@Password");
                DatosA.Rol = datos.Get<string>("@Rol");
                return DatosA;
                //return null;



            catch (Exception e)
            {
                return null;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }

            // cmd.ExecuteNonQuery();

            //registro = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
            // mensaje = cmd.Parameters["Mensaje"].Value.ToString();


        }

        */
        public ActionResult LogOff()
        {
            Session.Contents.RemoveAll();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}