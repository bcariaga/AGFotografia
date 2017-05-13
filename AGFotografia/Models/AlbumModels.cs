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
    public class Album
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Tags { get; set; }
        public string Portada { get; set; }
    }

    public class AlbumFill
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Tags { get; set; }
        public string Portada { get; set; }
        public List<Foto> Fotos { get; set; }
    }



    public class AlbumManager
    {
        /// <summary>
        /// Consulta los albunes en la bas de datos
        /// </summary>
        /// <returns> una lista con todos los albunes</returns>
        public List<Album> Consultar()
        {
            List<Album> albunes = new List<Album>();

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            SqlCommand consulta = new SqlCommand("SELECT * FROM Albunes");

            consulta.Connection = conexion;

            DataTable tabla = new DataTable();

            SqlDataAdapter adaptador = new SqlDataAdapter(consulta);

            adaptador.Fill(tabla);

            foreach (DataRow fila in tabla.Rows)
            {
                Album album = new Album();
                album.ID = (int)fila["ID"];
                album.Titulo = fila["Titulo"].ToString();
                album.Tags = fila["Tags"].ToString();
                album.Portada = fila["Portada"].ToString();

                albunes.Add(album);
            }

            return albunes;
        }

        public List<AlbumFill> ConsultarAlbumFill()
        {
            List<AlbumFill> albunes = new List<AlbumFill>();
            FotosManager fm = new FotosManager();

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            SqlCommand consulta = new SqlCommand("SELECT  A.ID " +
                                                        ",A.Titulo " +
                                                        ",A.Tags " +
                                                        ",A.Portada " +
                                                    "FROM Albunes A " +
                                                    "ORDER BY A.ID");

            consulta.Connection = conexion;

            DataTable tabla = new DataTable();

            SqlDataAdapter adaptador = new SqlDataAdapter(consulta);

            adaptador.Fill(tabla);

            foreach (DataRow fila in tabla.Rows)
            {
                AlbumFill album = new AlbumFill();
                album.ID = (int)fila["ID"];
                album.Titulo = fila["Titulo"].ToString();
                album.Tags = fila["Tags"].ToString();
                album.Portada = fila["Portada"].ToString();

                albunes.Add(album);
            }
            //lleno las fotos de los albunes
            foreach (AlbumFill album in albunes)
            {
                album.Fotos = fm.Consultar(album.ID);
            }

            return albunes;
        }

        /// <summary>
        /// Consulta 1 album
        /// </summary>
        /// <param name="id">ID del Album</param>
        /// <returns> list</returns>
        public Album ConsultarPorID(int id)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);


            SqlCommand consulta = new SqlCommand("SELECT  A.ID" +
                                                        ",A.Titulo" +
                                                        ",A.Tags" +
                                                        ",A.Portada" +
                                                    "FROM Albunes A" +
                                                    "WHERE A.ID=@ID" +
                                                    "ORDER BY A.ID");
            consulta.Parameters.AddWithValue("@ID", id);

            //SqlCommand consulta = new SqlCommand("SELECT * FROM Albunes WHERE ID=@ID");
            //consulta.Parameters.AddWithValue("@ID", id);

            consulta.Connection = conexion;

            DataTable tabla = new DataTable();

            SqlDataAdapter adaptador = new SqlDataAdapter(consulta);

            adaptador.Fill(tabla);

            Album albumConsultado = new Album();

            foreach (DataRow fila in tabla.Rows)
            {

                albumConsultado.ID = (int)fila["ID"];
                albumConsultado.Titulo = fila["Titulo"].ToString();
                albumConsultado.Tags = fila["Tags"].ToString();
                albumConsultado.Portada = fila["Portada"].ToString();
            }
            return albumConsultado;
        }


        /// <summary>
        /// Recibe un objeto Album y lo inserta en la base de datos
        /// </summary>
        /// <param name="album"> objetos con los datos de nuevo album</param>
        public Album Agregar(Album album)
        {
            string strLimpia = Cleaner.Limpiador.Limpiar(album.Portada);
            Album albumNew = new Album();
            albumNew.Portada = strLimpia;
            albumNew.Tags = album.Tags;
            albumNew.Titulo = album.Titulo;

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();


            SqlCommand insert = conexion.CreateCommand();
            insert.CommandText = "INSERT INTO Albunes (Titulo, Tags, Portada) VALUES (@titulo, @tags, @portada)";
            insert.Parameters.AddWithValue("@titulo", album.Titulo);
            insert.Parameters.AddWithValue("@tags", album.Tags);
            insert.Parameters.AddWithValue("@portada", strLimpia);

            insert.ExecuteNonQuery();

            SqlCommand select = conexion.CreateCommand();
            select.CommandText = "SELECT TOP 1 * FROM Albunes ORDER BY ID DESC";
            albumNew.ID = (int)select.ExecuteScalar();

            //albumNew.ID =Convert.ToInt32(insert.ExecuteScalar()); no funca
            conexion.Close();


            return albumNew;
        }
        /// <summary>
        /// Edita un Album por id del mismo
        /// </summary>
        /// <param name="album">Objeto con los datos a editar</param>
        public void Editar(Album album)
        {
            string strLimpia = Cleaner.Limpiador.Limpiar(album.Portada);
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();

            SqlCommand update = conexion.CreateCommand();
            update.CommandText = "UPDATE Albunes SET Titulo=@titulo, Tags=@tags, Portada=@portada WHERE ID=@id";
            update.Parameters.AddWithValue("@titulo", album.Titulo);
            update.Parameters.AddWithValue("@tags", album.Tags);
            update.Parameters.AddWithValue("@portada", strLimpia);
            update.Parameters.AddWithValue("@ID", album.ID);

            update.ExecuteNonQuery();

            conexion.Close();

        }
        public void EditarFill(AlbumFill album)
        {
            string strLimpia = Cleaner.Limpiador.Limpiar(album.Portada);
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();

            SqlCommand update = conexion.CreateCommand();
            update.CommandText = "UPDATE Albunes SET Titulo=@titulo, Tags=@tags, Portada=@portada WHERE ID=@id";
            update.Parameters.AddWithValue("@titulo", album.Titulo);
            update.Parameters.AddWithValue("@tags", album.Tags);
            update.Parameters.AddWithValue("@portada", strLimpia);
            update.Parameters.AddWithValue("@ID", album.ID);

            update.ExecuteNonQuery();

            conexion.Close();

        }

        public bool HasFile(HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
        /// <summary>
        /// Borra un album por ID
        /// </summary>
        /// <param name="idAlbum">id del album a borrar</param>
        public void Borrar(int idAlbum)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();

            SqlCommand deleteFotos = conexion.CreateCommand();
            deleteFotos.CommandText = "DELETE FROM Fotos WHERE ID_Album=@id";
            deleteFotos.Parameters.AddWithValue("@id", idAlbum);

            deleteFotos.ExecuteNonQuery();

            SqlCommand deleteAlbum = conexion.CreateCommand();
            deleteAlbum.CommandText = "DELETE FROM Albunes WHERE ID=@id";
            deleteAlbum.Parameters.AddWithValue("@id", idAlbum);

            deleteAlbum.ExecuteNonQuery();

            conexion.Close();

        }

    }
}