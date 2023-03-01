using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace SistMotoTaxi.Models
{
    public class Users
    {
        [Required(ErrorMessage = "idusuario")]
        public string idusuario { get; set; }
        [Required(ErrorMessage = "contraseña")]
        public string contrasena { get; set; }

        public static Usuarios IsValid(string _nombre, string _usuario, string _password, string _Rol)
        {
            Usuarios usuario = new Usuarios();
            try
            {
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                Boolean cambios = false;

                /*parametros.Add("Id_Aplicacion", "0");*/
                parametros.Add("Nombre", _nombre);
                parametros.Add("Usuario", _usuario);
                parametros.Add("Contrasena", GenerateSHA1Hash(_password));
                parametros.Add("Rol", _Rol);


                DataTable dt = ConexionSQL.ConsultarBD("sp_AltaUsuario", parametros);

                foreach (DataRow dr in dt.Rows)
                {
                  //  usuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);

                    usuario.Nombre = dr["Nombre"].ToString();
                    usuario.Usuario = dr["Usuario"].ToString();
                   // usuario.IdRol = Convert.ToInt32(dr["IdRol"]);
                     usuario.Rol = dr["Rol"].ToString();
                    //usuario.Activo = 1;
                    cambios = true;
                }
                if (cambios == true)
                {
                    usuario.Nombre = "Usuario no Valido"; //Los datos son incorrectos;

                }
            }
            catch (Exception e)
            {
                /*usuario.Activo = 3;*/ //Hubo un error
                usuario.Nombre = e.Message;
            }

            return usuario;
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