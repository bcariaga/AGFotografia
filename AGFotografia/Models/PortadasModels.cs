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
    public class Portada
    {
        public int ID { get; set; }
        public string Texto { get; set; }
        public string SRC { get; set; } 

    }

    public class PortadasManager
    {
        /// <summary>
        /// Consulta las Portadas, son 3
        /// </summary>
        /// <returns> una lista con las portadas (siempre son 3)</returns>
        public List<Portada> Consultar()
        {
            List<Portada> portadas = new List<Portada>();

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            SqlCommand consulta = new SqlCommand("SELECT * FROM Portadas");

            consulta.Connection = conexion;

            DataTable tabla = new DataTable();

            SqlDataAdapter adaptador = new SqlDataAdapter(consulta);

            adaptador.Fill(tabla);  

            foreach (DataRow fila in tabla.Rows)
            {
                Portada porta = new Portada();
                porta.ID = (int)fila["ID"];
                porta.SRC = fila["SRC"].ToString();
                porta.Texto = fila["Texto"].ToString();
              

                portadas.Add(porta);
            }

            return portadas;
        }

        /// <summary>
        /// Consulta solo 1 portada
        /// </summary>
        /// <param name="id"> id de la portada a consultar</param>
        /// <returns>la portada consultada</returns>
        public Portada ConsultarPorID(int id)
        {
            Portada album = new Portada();

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            SqlCommand consulta = new SqlCommand("SELECT * FROM Portadas WHERE ID=@ID");
            consulta.Parameters.AddWithValue("@ID", id);

            consulta.Connection = conexion;

            DataTable tabla = new DataTable();

            SqlDataAdapter adaptador = new SqlDataAdapter(consulta);

            adaptador.Fill(tabla);

            foreach (DataRow fila in tabla.Rows)
            {
                Portada albumConsultado = new Portada();
                albumConsultado.ID = (int)fila["ID"];
                albumConsultado.Texto = fila["Texto"].ToString();
                albumConsultado.SRC = fila["SRC"].ToString();

            }

            return album;
        }



        /// <summary>
        /// Edita una portada
        /// </summary>
        /// <param name="portada">Portada con los datos nuevos</param>
        public void Editar(Portada portada)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);
            string srtLimpia = Cleaner.Limpiador.Limpiar(portada.SRC);
            conexion.Open();

            SqlCommand update = conexion.CreateCommand();
            update.CommandText = "UPDATE Portadas SET Texto=@texto, SRC=@SRC WHERE ID=@id";
            update.Parameters.AddWithValue("@texto", portada.Texto);
            update.Parameters.AddWithValue("@SRC", srtLimpia);
            update.Parameters.AddWithValue("@ID", portada.ID);

            update.ExecuteNonQuery();

            conexion.Close();

        }
        
    }
}