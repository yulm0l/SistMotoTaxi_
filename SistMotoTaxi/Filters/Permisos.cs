using SistMotoTaxi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistMotoTaxi.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Permisos : AuthorizeAttribute
    {
        private Usuarios oUsuario;
        private int IdRol;
        private int IdRol2;

        public Permisos(int _IdRol = 1, int _IdRol2 = 0) //*
        {
            this.IdRol = _IdRol;
            this.IdRol2 = _IdRol2;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Usuarios datosUser = (Usuarios)filterContext.HttpContext.Session["Usuario"];
            if (datosUser!= null) 
            {
                //String nombreOperacion = "";
                String nombreModulo = "";
                IDbConnection cnn = null;
                try
                {
                    cnn = BDGateway.AbreConexion();
                    oUsuario = (Usuarios)HttpContext.Current.Session["Usuario"];
                    var datosUsuario = new Dapper.DynamicParameters();
                    datosUsuario.Add("@IdUsuario", oUsuario.IdUsuario);
                    datosUsuario.Add("@IdRol", IdRol);
                    datosUsuario.Add("@IdRol2", IdRol2);
                    var lstMisOperaciones = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_PermisoUsuario", datosUsuario, commandType: CommandType.StoredProcedure);

                    if (lstMisOperaciones.ToList().Count() == 1) //*
                    {
                        var _IdOperacion = new Dapper.DynamicParameters();
                        _IdOperacion.Add("@IdRol", IdRol);
                        var RsOperacion = Dapper.SqlMapper.Query<Usuarios>(cnn, "SP_Check_Rol", datosUsuario, commandType: CommandType.StoredProcedure);
                        nombreModulo = RsOperacion.ElementAt(0).Rol.ToString();
                        filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?modulo=" + nombreModulo + "&msjeErrorExcepcion=");
                    }
                }
                catch (Exception ex)
                {
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?modulo=" + nombreModulo + "&msjeErrorExcepcion=" + ex.Message);
                }
                finally
                {
                    BDGateway.CierraConexion(cnn);
                }
            }
            else filterContext.Result = new RedirectResult("../Home/Index");

        }
    }
}