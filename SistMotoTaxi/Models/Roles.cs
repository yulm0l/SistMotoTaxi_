using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace SistMotoTaxi.Models
{
    public class Roles
    {
        public Int32 IdRol { get; set; }
        public String Rol { get; set; }


        public static List<Roles> ObtenerRoles()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Rol>(cnn, "sp_get_rol", null, commandType: CommandType.StoredProcedure);

                return (List<Roles>)registros;
            }
            catch (Exception e)
            {
                List<Roles> error = new List<Roles>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }

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