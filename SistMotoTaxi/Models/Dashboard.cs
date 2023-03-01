using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SistMotoTaxi.Models
{
    public class Dashboard
    {
        public string Año { get; set; }
        public int Conteo { get; set; }
        public string AñoP { get; set; }
        public int ConteoP { get; set; }
        public int ConteoGeneral { get; set; }
        public int ConteoConcesion { get; set; }
        public int ConteoPermiso { get; set; }
        public string Nombre { get; set; }
        public string Tipo_modalidad { get; set; }
        public string NombreP { get; set; }
        public string Tipo_modalidadP { get; set; }
        public string Nombre_municipio { get; set; }
        public int SC { get; set; }
        public int RG { get; set; }
        public int IE { get; set; }

        public static List<Dashboard> Concesion()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "SP_GET_Concesion_Anio", null, commandType: CommandType.StoredProcedure);

                return (List<Dashboard>)registros;
            }
            catch (Exception e)
            {
                List<Dashboard> error = new List<Dashboard>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }
        public static List<Dashboard> Permiso()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "SP_GET_Permiso_Anio", null, commandType: CommandType.StoredProcedure);

                return (List<Dashboard>)registros;
            }
            catch (Exception e)
            {
                List<Dashboard> error = new List<Dashboard>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }

        public static Dashboard General()
        {
            IDbConnection cnn = null;
            try
            {
                var datos = new Dapper.DynamicParameters();
                datos.Add("@ConteoPermiso", dbType: DbType.Int32, direction: ParameterDirection.Output);
                datos.Add("@ConteoConcesion", dbType: DbType.Int32, direction: ParameterDirection.Output);
                datos.Add("@ConteoGeneral", dbType: DbType.Int32, direction: ParameterDirection.Output);
                datos.Add("@SC", dbType: DbType.Int32, direction: ParameterDirection.Output);
                datos.Add("@RG", dbType: DbType.Int32, direction: ParameterDirection.Output);
                datos.Add("@IE", dbType: DbType.Int32, direction: ParameterDirection.Output);

                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "SP_GET_General", datos, commandType: CommandType.StoredProcedure);
                Dashboard DatosG = new Dashboard();
                DatosG.ConteoPermiso = datos.Get<int>("@ConteoPermiso");
                DatosG.ConteoConcesion = datos.Get<int>("@ConteoConcesion");
                DatosG.ConteoGeneral = datos.Get<int>("@ConteoGeneral");
                DatosG.SC = datos.Get<int>("@SC");
                DatosG.RG = datos.Get<int>("@RG");
                DatosG.IE = datos.Get<int>("@IE");
                return DatosG;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }

        public static List<Dashboard> GraficaConcesion()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "SP_Grafica_Modalidad_Concesion", null, commandType: CommandType.StoredProcedure);

                return (List<Dashboard>)registros;
            }
            catch (Exception e)
            {
                List<Dashboard> error = new List<Dashboard>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }

        public static List<Dashboard> GraficaPermisos()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "SP_Grafica_Modalidad_Permiso", null, commandType: CommandType.StoredProcedure);

                return (List<Dashboard>)registros;
            }
            catch (Exception e)
            {
                List<Dashboard> error = new List<Dashboard>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }

        public static List<Dashboard> GraficaConcesionMunicipio()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "SP_Grafica_ModMuni_Concesion", null, commandType: CommandType.StoredProcedure);

                return (List<Dashboard>)registros;
            }
            catch (Exception e)
            {
                List<Dashboard> error = new List<Dashboard>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }

        public static List<Dashboard> GraficaConcesionMunicipioPermiso()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "SP_Grafica_ModMuni_Permiso", null, commandType: CommandType.StoredProcedure);

                return (List<Dashboard>)registros;
            }
            catch (Exception e)
            {
                List<Dashboard> error = new List<Dashboard>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }

        public static List<Dashboard> Roles()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "SP_GET_rol", null, commandType: CommandType.StoredProcedure);

                return (List<Dashboard>)registros;
            }
            catch (Exception e)
            {
                List<Dashboard> error = new List<Dashboard>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }

        public static List<Dashboard> AgregarUsuario()
        {
            IDbConnection cnn = null;
            try
            {
                cnn = BDGateway.AbreConexion();
                var registros = Dapper.SqlMapper.Query<Dashboard>(cnn, "sp_AltaUsuario", null, commandType: CommandType.StoredProcedure);

                return (List<Dashboard>)registros;
            }
            catch (Exception e)
            {
                List<Dashboard> error = new List<Dashboard>();
                return error;
            }
            finally
            {
                BDGateway.CierraConexion(cnn);
            }
        }



    }
}