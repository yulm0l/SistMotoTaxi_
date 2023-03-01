using SistMotoTaxi.Filters;
using SistMotoTaxi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SistConstAutorizacion.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        [Permisos(_IdRol: 1, _IdRol2: 0)]
        public ActionResult Roles()
        {
            Usuarios oUsuario = (Usuarios)Session["Usuario"];
            if (oUsuario != null)
            {
                SelectList sRol = Usuarios.ObtenerRol();
                ViewData["Rol"] = sRol;
                //TempData["mensaje"] = "";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home"); ///???
            }
        }



        [Permisos(_IdRol: 1, _IdRol2: 0)]
        public ActionResult Bandeja()
        {

            List<string[]> list = new List<string[]>();
            try
            {
                IEnumerable<Usuarios> lista = Usuarios.ObtenerUsuario();          //===>>>>>> esta es la clase que obtiene los datos desde la base de datos.
                int i = 1;
                //string[] aData = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                string[] aData = new string[] { "1", "2", "3", "4", "5", "6", "7" };
                int idusuario;
                string nombre = "";
                string usuario = "";
                string rol = "";
                string status = "";
                string edit = "";
                string delete = "";

                for (i = 1; i <= lista.Count(); i++)
                {
                    idusuario = lista.ElementAt(i - 1).IdUsuario;
                    nombre = lista.ElementAt(i - 1).Nombre.ToString();
                    usuario = lista.ElementAt(i - 1).Usuario.ToString();
                    rol = lista.ElementAt(i - 1).Desc_Rol.ToString();
                    status = lista.ElementAt(i - 1).Status.ToString();
                    if (status == "Activo")
                    {
                        delete = "<a class='btn btn-sm btn-danger btn-delete' href=\"./Eliminar?Id_Usuario=" + idusuario + " \" title=\"Eliminar Usuario\"><i class=\"fa fa-trash\"></i></a>";
                        edit = "<a class='btn btn-sm btn-danger btn-edit' id='datos' href='#' onclick=\"Editar(" + idusuario + ",'" + nombre + "','" + usuario + "','" + rol + "'); return false;\" title=\"Editar Usuario\"><i class=\"fa fa-edit\"></i></a>";
                    }
                    else
                    {
                        delete = "<a disabled='disabled' class='btn btn-sm btn-disbled btn-delete' href=\'#' \"><i class=\"fa fa-trash\"></i></a>";
                        edit = "<a id='datos' class='btn btn-sm btn-disbled btn-delete' href='#'><i class=\"fa fa-edit\"></i></a>";
                    }
                    aData = new string[] { idusuario.ToString(), nombre, usuario, rol, status, delete + ' ' + edit };
                    list.Add(aData);
                };

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue; // Whatever max length you want here

                return Json(new
                {
                    sEcho = "1",//param.sEcho,
                    iTotalRecords = (i - 1),
                    iTotalDisplayRecords = 10,
                    aaData = list
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return Json(new
                {
                    sEcho = "1",//param.sEcho,
                    iTotalRecords = 1,
                    iTotalDisplayRecords = 1,
                    aaData = 0
                },
                JsonRequestBehavior.AllowGet);
            }

        }

        public String Desc_Rol { get; set; }
        #region ObtenerUsuario?
        /*
        
        
        public static List<Usuarios> ObtenerUsuario()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_ListadoUsuarios", null, commandType: CommandType.StoredProcedure);

                return (List<Usuarios>)registros;
            }
            catch (Exception e)
            {
                List<Usuarios> error = new List<Usuarios>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }*/
        #endregion

        #region CREAR NUEVO USUARIO

        [Permisos(_IdRol: 1, _IdRol2: 0)]
        [HttpGet]
        public ActionResult Alta()
        {
            return View();
        }

        [Permisos(_IdRol: 1, _IdRol2: 0)]
        [HttpPost]
        public ActionResult Alta(FormCollection formCollection)
        {
            IDbConnection cnn = null;
            cnn = BDGateway.AbreConexion();
            try
            {
                Usuarios guardar = new Usuarios();
                guardar.Nombre = Convert.ToString(formCollection["nombre"]);
                guardar.Usuario = Convert.ToString(formCollection["usuario"]);
                guardar.Password = GenerateSHA1Hash(Convert.ToString(formCollection["password"]));
                guardar.IdRol = Convert.ToInt32(formCollection["IdRol"]);

                if (guardar.Nombre != "" || guardar.Usuario != "" || guardar.Password != "" || guardar.IdRol != 0)
                {
                    var p = new Dapper.DynamicParameters();
                    p.Add("@nombre", guardar.Nombre);
                    p.Add("@usuario", guardar.Usuario);
                    p.Add("@password", guardar.Password);
                    p.Add("@rol", guardar.IdRol);
                    cnn = BDGateway.AbreConexion();
                    var registros = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_AltaUsuario", p, commandType: CommandType.StoredProcedure);
                }
                else
                {
                    TempData["mensaje"] = "No se Registraron lo Datos";
                    return RedirectToAction("Alta", "Usuarios");  //****
                    //return RedirectToAction("Bandeja", "Usuarios");
                }

            }
            catch (Exception e)
            {
                TempData["mensaje"] = "No se Registraron lo Datos";
                //return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }

            TempData["mensaje"] = "Datos registrados correctamente. ";

            //return Redirect("/Usuario/Usuario");
            return RedirectToAction("Alta", "Usuarios");   //********
        }
        #endregion

        [Permisos(_IdRol: 1, _IdRol2: 0)]
        public ActionResult Eliminar(Int32 Id_Usuario)
        {
            IDbConnection cnn = null;
            cnn = BDGateway.AbreConexion();
            try
            {
                var p = new Dapper.DynamicParameters();
                p.Add("@idusuario", Id_Usuario);
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_BorraUsuario", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                TempData["mensaje"] = "No se Registraron lo Datos";
                return RedirectToAction("Eliminar", "Usuarios");
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }

            TempData["mensaje"] = "Usuario Borrado correctamente. ";

            //return Redirect("/Usuario/Usuario");
            return RedirectToAction("Eliminar", "Usuarios");
        }


        [Permisos(_IdRol: 1, _IdRol2: 0)]
        [HttpPost]
        public ActionResult Editar(FormCollection formCollection)
        {
            IDbConnection cnn = null;
            cnn = BDGateway.AbreConexion();
            try
            {
                Usuarios guardar = new Usuarios();
                guardar.Password = GenerateSHA1Hash(Convert.ToString(formCollection["passwordEdit"]));
                guardar.IdUsuario = Convert.ToInt32(formCollection["IdEdit"]);

                if (guardar.Password != "")
                {
                    var p = new Dapper.DynamicParameters();
                    p.Add("@password", guardar.Password);
                    p.Add("@idusuario", guardar.IdUsuario);
                    cnn = BDGateway.AbreConexion();
                    var registros = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_EditarUsuario", p, commandType: CommandType.StoredProcedure);
                }
                else
                {
                    TempData["mensaje"] = "No se Registraron lo Datos";
                    return RedirectToAction("Editar", "Usuarios");
                }

            }
            catch (Exception e)
            {
                TempData["mensaje"] = "No se Registraron lo Datos";
                return RedirectToAction("Editar", "Usuarios");
                //return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }

            TempData["mensaje"] = "Datos actualizados correctamente. ";

            //return Redirect("/Usuario/Usuario");
            return RedirectToAction("Bandeja", "Usuarios");
        }



        public static string GenerateSHA1Hash(string SourceText)
        {
            // Create an encoding object to ensure the encoding standard for the source text
            UnicodeEncoding Ue = new UnicodeEncoding();
            // Retrieve a byte array based on the source text
            byte[] ByteSourceText = Ue.GetBytes(SourceText);
            // Instantiate an MD5 Provider object
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            // Compute the hash value from the source
            byte[] ByteHash = SHA1.ComputeHash(ByteSourceText);
            // And convert it to String format for return
            return Convert.ToBase64String(ByteHash);
        }
    }
}