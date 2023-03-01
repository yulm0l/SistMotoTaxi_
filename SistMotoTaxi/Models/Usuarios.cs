using Dapper;
using SistMotoTaxi.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace SistMotoTaxi.Models
{
    public class Usuarios
    {
        public Int32 IdUsuario { get; set; }
        public Int32 IdRol { get; set; }
        public String Nombre { get; set; }
        public String Rol { get; set; }
        public String Usuario { get; set; }
        public String Status { get; set; }
        public String Password { get; set; }
        public String Desc_Rol { get; set; }
        #region ObtenerUsuario
        public static List<Usuarios> ObtenerUsuario()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var Lista = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_ListadoUsuarios", null, commandType: CommandType.StoredProcedure);

                return(List<Usuarios>)Lista;
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

        }
        #endregion



        #region 

        /*
         * 
         *         private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["BD_MotoTaxis"].ToString();
            con = new SqlConnection(constr);
        }
         *         public List<Usuarios> RecuperarTodos()
        {
            Conectar();
            //IDbConnection cnn = null;
            //cnn = BDGateway.AbreConexion();
            List<Usuarios> articulos = new List<Usuarios>();

            SqlCommand Com = new SqlCommand("select IdUsuario,Nombre,Usuario, Status from Usuarios", con);
            //
            con.Open();
            SqlDataReader registros = con.ExecuteReader();
            while (registros.Read())
            {
                Usuarios art = new Usuarios
                {
                    IdUsuario = int.Parse(registros["IdUsuario"].ToString()),
                    Nombre = registros["Nombre"].ToString(),
                    Usuario = registros["Usuario"].ToString(),
                    Rol = registros["Rol"].ToString(),
                    Status = registros["Status"].ToString(),




                    //Usuario = float.Parse(registros["Usuario"].ToString())
                };
                articulos.Add(art);
            }
            con.Close();
            return articulos;
        }
        var sp_ListadoUsuarios = Usuarios.ObtenerUsuario;
        foreach(ObtenerUsuario d in  ususario)
        {
            Usuarios.Where(c => c.IdUsuario == d.IdUsuario).Load();
                foreach(Usuario c in d.Usuario)
                    {usuarioList.Add(d.Nombre + c.Usuario)};

        }*/
        #endregion



        #region EditarUsuario
        public static List<Usuarios> EditarUsuario()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_EditarUsuario", null, commandType: CommandType.StoredProcedure);

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
        }
        #endregion

        #region Nuevo Usuario
        public static List<Usuarios> Alta()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_AltaUsuario", null, commandType: CommandType.StoredProcedure);

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
        }
        #endregion
        
        #region Obtener Roles
        /// <summary>
        /// Método para obtener el catálogo de Municipios
        /// </summary>
        /// <param name="idSelected"></param>
        /// <returns></returns>
        public static SelectList ObtenerRol(int idSelected = 0)
        {
            IDbConnection cnn = null;
            Dictionary<int, string> dRol;
            SelectList sRol;

            try
            {
                cnn = BDGateway.AbreConexion();

                var result = Dapper.SqlMapper.Query<Usuarios>(cnn, "SP_get_Rol", null, commandType: CommandType.StoredProcedure);
                dRol = new Dictionary<int, string>();

                foreach (var reg in result)
                {
                    dRol.Add(reg.IdRol, reg.Rol);
                }

                if (idSelected > 0)
                {
                    sRol = new SelectList(dRol, "key", "value", idSelected);
                }
                else
                {
                    sRol = new SelectList(dRol, "key", "value");
                }

                return sRol;
            }
            catch (Exception e)
            {
                dRol = new Dictionary<int, string>();
                dRol.Add(0, "Error al obtener el catálogo de Roles");
                sRol = new SelectList(dRol, "key", "value");

                return sRol;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }

        }
        #endregion



    }
}