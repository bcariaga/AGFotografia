using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGFotografia.Models
{     
        public class Usuario
        {
            public string password { get; set; }
            public string usuario { get; set; }

        }

        public class UsuarioManager
        {
            /// <summary>
            /// consulta si el usuario existe y lo conecta.
            /// </summary>
            public Usuario Consultar(string usuario, string password)
            {
                Usuario usuarioLogueado = new Usuario();

                SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

                SqlCommand select = new SqlCommand("SELECT * FROM Usuarios WHERE Usuario = @usuario AND Password = @password");
                select.Parameters.AddWithValue("@usuario", usuario);
                select.Parameters.AddWithValue("@password", password);
                select.Connection = conexion;



                DataTable tablaResultado = new DataTable();

                SqlDataAdapter adaptador = new SqlDataAdapter(select);

                adaptador.Fill(tablaResultado);

                if (tablaResultado.Rows.Count > 0)
                {
                    usuarioLogueado.usuario = tablaResultado.Rows[0]["Usuario"].ToString();
                    usuarioLogueado.password = tablaResultado.Rows[0]["Password"].ToString();
                }

                else
                {
                    usuarioLogueado = null;
                }
                conexion.Close();
                return usuarioLogueado;
            }

            /// <summary>
            /// Crea el objeto User y lo envia para validar, ademas lo mete en la session.
            /// </summary>
            /// <param name="usuario"></param>
            /// <param name="password"></param>
            /// <returns></returns>
            public Usuario ValidarLogin(string usuario, string password)
            {
                Usuario usuarioLog = this.Consultar(usuario, password);

                if (usuarioLog != null)
                {
                    return usuarioLog;
                }
                else
                {
                    return null;
                }

            }
        /// <summary>
        /// Crea un Usuario nuevo, antes valida si se esta usando el mail
        /// </summary>
        /// <param name="nuevoUser"></param>
        /// <returns></returns>
        public Usuario Registro(Usuario nuevoUser)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();
            // primero valida el mail en la base de datos
            SqlCommand consultar = conexion.CreateCommand();
            consultar.CommandText = "SELECT * FROM Usuarios WHERE Usuario = @usuario";
            consultar.Parameters.AddWithValue("@usuario", nuevoUser.usuario);

            //meto el resultado en un string para usarlo
            string respuesta = (string)consultar.ExecuteScalar();

            //si el mail no esta en la base de datos inserta los datos
            if (respuesta == null)
            {
                SqlCommand insert = conexion.CreateCommand();
                insert.CommandText = "INSERT INTO Usuarios (Usuario, Password) VALUES (@usuario, @password)";
                insert.Parameters.AddWithValue("@usuario", nuevoUser.usuario);
                insert.Parameters.AddWithValue("@password", nuevoUser.password);

                Usuario nuevoReg = new Usuario();
                nuevoReg.usuario = (string)insert.ExecuteScalar();


                conexion.Close();

                return nuevoReg;

            }
            //si el mail esta en la base devuelve null
            else
            {
                conexion.Close();
                return null;

            }

        }


    }
      
    }  
