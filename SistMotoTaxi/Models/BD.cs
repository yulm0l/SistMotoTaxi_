using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SistMotoTaxi.Models
{
    public class BDGateway
    {

        public static IDbConnection AbreConexion()
        {
            IDbConnection con;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnBD"].ConnectionString);
            con.Open();

            return con;
        }

        public static void CierraConexion(IDbConnection con)
        {
            con.Dispose();
            con = null;
        }
    }


    public class ConexionSQL
    {
        #region Devuelve la cadena de conexión a la Base de Datos
        public static string getCadenaConexion()
        {
            SqlConnection conn = new SqlConnection();
            string CadenaDeConexion = ConfigurationManager.ConnectionStrings["ConnBD"].ConnectionString;
            return CadenaDeConexion;
        }
        #endregion

        #region Conectarse a la Base de Datos
        public static SqlConnection getConnection()
        {
            SqlConnection conn = new SqlConnection();
            string CadenaDeConexion = ConfigurationManager.ConnectionStrings["ConnBD"].ConnectionString;
            conn.ConnectionString = CadenaDeConexion;
            conn.Open();
            return conn;
        }
        #endregion

        #region Seleccionar datos con Store Procedure
        public static DataTable ConsultarBD(string nombreStore, Dictionary<string, string> parametros)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = ConexionSQL.getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(nombreStore, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (KeyValuePair<String, String> entry in parametros)
                {
                    // do something with entry.Value or entry.Key
                    cmd.Parameters.Add(new SqlParameter(entry.Key, entry.Value));
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public static DataTable ConsultarBD2(string nombreStore)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = ConexionSQL.getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(nombreStore, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion

        #region Seleccionar datos con Store Procedure BIEN
        public static DataTable ConsultarBD(string nombreStore, List<parametros> parametros)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = null;
            try
            {
                conn = ConexionSQL.getConnection();
                SqlCommand cmd = new SqlCommand(nombreStore, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (var p in parametros)
                {
                    switch (p.tipodato)
                    {
                        case "string":
                            cmd.Parameters.Add(p.parametro, SqlDbType.VarChar).Value = p.valorCadena;
                            break;
                        case "int":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Int).Value = p.valorEntero;
                            break;
                        case "double":
                            cmd.Parameters.Add(p.parametro, SqlDbType.BigInt).Value = p.valorDouble;
                            break;
                        case "datetime":
                            cmd.Parameters.Add(p.parametro, SqlDbType.DateTime).Value = p.valorFecha ?? null;
                            break;
                    }
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
            }
            finally
            {
                try
                {
                    conn.Close();
                }
                catch
                {
                }
            }

            return dt;
        }
        #endregion

        #region Ejecutar Store Procedure sin retornar datos con posible parámetro de salida tipo int
        public static int EjecutarStoreProc(string nombreStore, List<parametros> parametros, Boolean ParamOutInt, string nombreParamOut)
        {
            int exito = 0;
            SqlConnection conn = ConexionSQL.getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(nombreStore, conn);
                cmd.CommandTimeout = 200;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (var p in parametros)
                {
                    switch (p.tipodato)
                    {
                        case "string":
                            cmd.Parameters.Add(p.parametro, SqlDbType.VarChar).Value = p.valorCadena;
                            break;
                        case "int":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Int).Value = p.valorEntero;
                            break;
                        case "double":
                            cmd.Parameters.Add(p.parametro, SqlDbType.BigInt).Value = p.valorDouble;
                            break;
                        case "datetime":
                            cmd.Parameters.Add(p.parametro, SqlDbType.DateTime).Value = p.valorFecha ?? null;
                            break;
                        case "decimal":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Money).Value = p.valorDecimal;
                            break;
                    }
                }
                if (ParamOutInt == true)
                {
                    cmd.Parameters.Add(nombreParamOut, SqlDbType.Int);
                    cmd.Parameters[nombreParamOut].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    exito = (Int32)cmd.Parameters[nombreParamOut].Value;

                }
                else
                {
                    cmd.ExecuteNonQuery();
                    exito = 1;
                }

            }
            catch (Exception e)
            {
                exito = -1;
            }
            finally
            {
                conn.Close();
            }

            return exito;
        }
        #endregion

        #region Ejecutar Store Procedure con parametro de salida string --SMOK[en uso]
        public static string EjecutarStoreProc_string(string nombreStore, List<parametros> parametros, Boolean ParamOutInt, string nombreParamOut, int precision = 10)
        {
            string exito = "";
            SqlConnection conn = ConexionSQL.getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(nombreStore, conn);
                cmd.CommandTimeout = 200;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (var p in parametros)
                {
                    switch (p.tipodato)
                    {
                        case "string":
                            cmd.Parameters.Add(p.parametro, SqlDbType.VarChar).Value = p.valorCadena;
                            break;
                        case "int":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Int).Value = p.valorEntero;
                            break;
                        case "double":
                            cmd.Parameters.Add(p.parametro, SqlDbType.BigInt).Value = p.valorDouble;
                            break;
                        case "datetime":
                            cmd.Parameters.Add(p.parametro, SqlDbType.DateTime).Value = p.valorFecha ?? null;
                            break;
                        case "decimal":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Money).Value = p.valorDecimal;
                            break;
                        case "byte":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Image).Value = p.valorByte;
                            break;
                        case "boolean":
                            cmd.Parameters.Add(p.parametro, SqlDbType.Bit).Value = p.valorBoolean;
                            break;
                    }
                }
                if (ParamOutInt == true)
                {
                    cmd.Parameters.Add(nombreParamOut, SqlDbType.VarChar, precision);
                    cmd.Parameters[nombreParamOut].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    exito = cmd.Parameters[nombreParamOut].Value.ToString();

                }
                else
                {
                    cmd.ExecuteNonQuery();
                    exito = "OK";
                }

            }
            catch (Exception e)
            {
                exito = e.ToString();
            }
            finally
            {
                conn.Close();
            }

            return exito;
        }
        #endregion

        public static SqlDataReader Get_DataReader(string Consulta)
        {
            SqlConnection conn = ConexionSQL.getConnection();
            SqlCommand command = new SqlCommand(Consulta, conn);
            try
            {
                // conn.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    #region clase para parámetros de store
    public class parametros
    {
        public string parametro { get; set; }
        public string tipodato { get; set; }
        public string valorCadena { get; set; }
        public int valorEntero { get; set; }
        public double valorDouble { get; set; }
        public DateTime? valorFecha { get; set; }
        public Decimal valorDecimal { get; set; }
        public byte[] valorByte { get; set; }
        public Boolean valorBoolean { get; set; }
    }
    #endregion
}