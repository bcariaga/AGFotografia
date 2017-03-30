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
    public class DatosContacto
    {
        public string Portada { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Flickr { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Texto1 { get; set; }
        public string Texto2 { get; set; }
    }

    public class DatosContactoManager
    {
        /// <summary>
        /// Consulta los datos para la vista de contacto
        /// </summary>
        /// <returns>un Objeto con los datos de contacto</returns>
        public DatosContacto Consultar()
        {
          
            DatosContacto contacto = new DatosContacto();

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            SqlCommand consulta = new SqlCommand("SELECT * FROM Contacto");

            consulta.Connection = conexion;

            DataTable tabla = new DataTable();

            SqlDataAdapter adaptador = new SqlDataAdapter(consulta);

            adaptador.Fill(tabla);  

            foreach (DataRow fila in tabla.Rows)
            {
               
                contacto.Portada = fila["Portada"].ToString();
                contacto.Tel = fila["Tel"].ToString();
                contacto.Email = fila["Email"].ToString();
                contacto.Facebook = fila["Facebook"].ToString();
                contacto.Flickr = fila["Flickr"].ToString();
                contacto.Titulo = fila["Titulo"].ToString();
                contacto.Subtitulo = fila["Subtitulo"].ToString();
                contacto.Texto1 = fila["Texto1"].ToString();
                contacto.Texto2 = fila["Texto2"].ToString();
   

            }
            conexion.Close();

            return contacto;

        }
        /// <summary>
        /// Edita la pagina contacto
        /// </summary>
        /// <param name="datosAEditar">recibe un objeto con los datos editados.</param>
         public void Editar(DatosContacto datosAEditar)
        {
            string strLimpia = Cleaner.Limpiador.Limpiar(datosAEditar.Portada);
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();

            SqlCommand update = new SqlCommand("UPDATE Contacto SET Portada= @portada, Tel= @tel, Email= @email, Facebook= @facebook, Flickr= @flickr, Titulo= @titulo, Subtitulo= @subtitulo, Texto1= @texto1, Texto2= @texto2");
            update.Parameters.AddWithValue("@portada", strLimpia);
            update.Parameters.AddWithValue("@tel", datosAEditar.Tel);
            update.Parameters.AddWithValue("@email", datosAEditar.Email);
            update.Parameters.AddWithValue("@facebook", datosAEditar.Facebook);
            update.Parameters.AddWithValue("@flickr", datosAEditar.Flickr);
            update.Parameters.AddWithValue("@titulo", datosAEditar.Titulo);
            update.Parameters.AddWithValue("@subtitulo", datosAEditar.Subtitulo);
            update.Parameters.AddWithValue("@texto1", datosAEditar.Texto1);
            update.Parameters.AddWithValue("@texto2", datosAEditar.Texto2);

            update.Connection = conexion;
            update.ExecuteNonQuery();

            conexion.Close();

        }
        
    }
}