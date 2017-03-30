using FlickrNet;
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
    public class Foto
    {
        public int ID { get; set; }
        public string SRC { get; set; }
        public string Miniatura { get; set; }
        public int ID_Album { get; set; }
    }

    public class FotosManager
    {   
        /// <summary>
        /// Consulta las Fotos, por ID_Album
        /// </summary>
        /// <returns>Una lista de fotos, agrupadas por Album</returns>
        public List<Foto> Consultar(int id_album)
        {
            List<Foto> fotos = new List<Foto>();

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            SqlCommand consulta = new SqlCommand("SELECT * FROM Fotos WHERE ID_Album=@id_album");

            consulta.Parameters.AddWithValue("@id_album ", id_album);

            consulta.Connection = conexion;

            DataTable tabla = new DataTable();

            SqlDataAdapter adaptador = new SqlDataAdapter(consulta);

            adaptador.Fill(tabla);

            foreach(DataRow fila in tabla.Rows)
            {
                Foto foto = new Foto();
                foto.ID = (int)fila["ID"];
                foto.SRC = (string)fila["SRC"];
                //foto.Miniatura = String.IsNullOrEmpty((string)fila["Miniatura"]) ? "" : (string)fila["Miniatura"];
                foto.Miniatura = fila["Miniatura"] == DBNull.Value ? String.Empty : (string)fila["Miniatura"];
                foto.ID_Album = (int)fila["ID_Album"];

                fotos.Add(foto);

            }

            return fotos;
        }
        /// <summary>
        /// agrega un grupo de fotos
        /// </summary>
        /// <param name="fotos">lista de fotos</param>
        public void Agregar(List<Foto> fotos)
        {
            
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();

            foreach (Foto foto in fotos)
            {
                string strLimpia = Cleaner.Limpiador.Limpiar(foto.SRC);
                SqlCommand insert = new SqlCommand("INSERT INTO Fotos VALUES(@src, @ID_Album,@Miniatura)");
                insert.Parameters.AddWithValue("@src", strLimpia);
                insert.Parameters.AddWithValue("@ID_Album", foto.ID_Album);
                insert.Parameters.AddWithValue("@Miniatura", foto.Miniatura);

                insert.Connection = conexion;

                insert.ExecuteNonQuery();
            }

            conexion.Close();

        }

        //public void UploadFlickr(IEnumerable<HttpPostedFileBase> FotoFile)
        //{
        //    var f = FlickrManager.GetAuthInstance();

        //    foreach (var file in FotoFile)
        //    {
        //        if (! HasFile(file)) continue;
        //        try
        //        {
        //            string photoId = f.UploadPicture(file.InputStream,  //file
        //                                                        file.FileName,      //Filename
        //                                                        tituloFoto,
        //                                                        descripcion,
        //                                                        tags,
        //                                                        esPublica,
        //                                                        isFamily,
        //                                                        isFriend,
        //                                                        ContentType.Photo,
        //                                                        SafetyLevel.Safe,
        //                                                        HiddenFromSearch.Hidden
        //                                                     );
        //            PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);

        //            Foto foto = new Foto();

        //            foto.ID_Album = album.ID;

        //            foto.SRC = oPhotoInfo.OriginalUrl;

        //            //foto.Miniatura = oPhotoInfo.ThumbnailUrl;

        //            fotosSubidas.Add(foto);
        //        }
        //        catch (Exception e)
        //        {

        //            errores += e.Message + " ";
        //            success = false;
        //        }
        //    }

        //}
       

        /// <summary>
        /// Edita las fotos de un album
        /// </summary>
        /// <param name="fotos"> lista de fotos a editar</param>
        public void Editar(List<Foto> fotos)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();

            foreach(Foto foto in fotos)
            {
                string strLimpia = Cleaner.Limpiador.Limpiar(foto.SRC);
                SqlCommand update = conexion.CreateCommand();
                update.CommandText = "UPDATE Fotos SET SRC=@src, Miniatura=@Miniatura WHERE ID_Album=@ID_Album";
                update.Parameters.AddWithValue("@src", strLimpia);
                update.Parameters.AddWithValue("@Id_Album", foto.ID_Album);
                update.Parameters.AddWithValue("@Miniatura", foto.Miniatura);


                update.ExecuteNonQuery();
            }

            conexion.Close();

        }
        /// <summary>
        /// borra una foto por ID de la misma.
        /// </summary>
        /// <param name="ID">id de la foto a borrar</param>
        public void Borrar(int ID)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            conexion.Open();
            SqlCommand update = conexion.CreateCommand();
            update.CommandText = "Delete Fotos WHERE ID=@ID";
            update.Parameters.AddWithValue("@ID", ID);

            update.ExecuteNonQuery();

            conexion.Close();

        }
    }
}