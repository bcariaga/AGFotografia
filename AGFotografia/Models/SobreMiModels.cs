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
    public class SobreMi
    {
        public string Portada { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Texto1 { get; set; }
        public string Texto2 { get; set; }
        public string Texto3 { get; set; }
    }

    public class SobreMiManager
    {
        /// <summary>
        /// Consulta los datos de Sobremi para la vista
        /// </summary>
        /// <returns>un objeto con los datos de SobreMi</returns>
        public SobreMi Consultar()
        {
            SobreMi sobreMi = new SobreMi();

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            SqlCommand consulta = new SqlCommand("SELECT * FROM SobreMi");

            consulta.Connection = conexion;

            DataTable tabla = new DataTable();

            SqlDataAdapter adaptador = new SqlDataAdapter(consulta);

            adaptador.Fill(tabla);

            foreach (DataRow fila in tabla.Rows)
            {
                
                sobreMi.Portada = (string)fila["Portada"];
                sobreMi.Titulo = (string)fila["Titulo"];
                sobreMi.Subtitulo = (string)fila["Subtitulo"];
                sobreMi.Texto1= (string)fila["Texto1"];
                sobreMi.Texto2 = (string)fila["Texto2"];
                sobreMi.Texto3 = (string)fila["Texto3"];

            }

            return sobreMi;
        }

        /// <summary>
        /// Edita la pagina Sobre Mi
        /// </summary>
        /// <param name="datosAEditar">recibe un objeto con los datos editados.</param>
        public void Editar(SobreMi datosAEditar)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();

            SqlCommand update = new SqlCommand("UPDATE SobreMi SET Portada= @portada,Titulo= @titulo, Subtitulo= @subtitulo, Texto1= @texto1, Texto2= @texto2, Texto3= @texto3");
            update.Parameters.AddWithValue("@portada", datosAEditar.Portada);
            update.Parameters.AddWithValue("@titulo", datosAEditar.Titulo);
            update.Parameters.AddWithValue("@subtitulo", datosAEditar.Subtitulo);
            update.Parameters.AddWithValue("@texto1", datosAEditar.Texto1);
            update.Parameters.AddWithValue("@texto2", datosAEditar.Texto2);
            update.Parameters.AddWithValue("@texto3", datosAEditar.Texto3);

            update.Connection = conexion;
            update.ExecuteNonQuery();

            conexion.Close();

        }


    }
}