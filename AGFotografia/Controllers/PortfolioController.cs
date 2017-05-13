using AGFotografia.Models;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGFotografia.Controllers
{
    public class PortfolioController : Controller
    {
        //public ActionResult NuevoAlbum()
        //{
        //    if (Session["usuario"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }

        //}

        [HttpGet]
        public ActionResult AgregarFotos()
        {

            if (Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Portfolio
        public ActionResult Ver(int id)
        {
            FotosManager managerFotos = new FotosManager();
            List<Foto> Fotos = managerFotos.Consultar(id);

            ViewBag.Fotos = Fotos;

            return View("Portfolio");
        }

        public ActionResult Index()
        {
            AlbumManager managerAlbum = new AlbumManager();
            List<Album> Albunes = managerAlbum.Consultar();


            ViewBag.Albunes = Albunes;

            return View();
        }
        [ValidateInput(false)]
        public ActionResult InsertarAlbum(FormCollection formulario)
        {

            if (Session["usuario"] != null)
            {
                Album nuevoAlbum = new Album();
                nuevoAlbum.Titulo = formulario["titulo"];
                nuevoAlbum.Tags = formulario["tags"];
                nuevoAlbum.Portada = formulario["portada"];


                AlbumManager albumManager = new AlbumManager();
                Album album = albumManager.Agregar(nuevoAlbum);


                ViewBag.Album = album;


                return View("AgregarFotos");
            }
            else
            {
                return View("Error");
            }

        }


        [ValidateInput(false)]
        [HttpPost]
        public JsonResult InsertarAlbumNew(FormCollection formulario, HttpPostedFileBase[] FotoFile)
        {

            if (Session["usuario"] == null || Request.Cookies["OAuthToken"] == null)
            {
                return Json(new { succes = false, mensaje = "se requiere autenticacion." });
            }


            bool success = true;
            string errores = "";

            List<Foto> fotosSubidas = new List<Foto>();

            var f = FlickrManager.GetAuthInstance();
            FotosManager fotoManager = new FotosManager();
            AlbumManager albumManager = new AlbumManager();


            Album nuevoAlbum = new Album();
            nuevoAlbum.Titulo = formulario["titulo"];
            nuevoAlbum.Tags = formulario["tags"];

            if (Request.Files["portadaFile"] != null)
            {
                var file = Request.Files["portadaFile"];

                string photoId = f.UploadPicture(file.InputStream,  //file
                                                                file.FileName,      //Filename
                                                                nuevoAlbum.Titulo,
                                                                "portada de: " + nuevoAlbum.Titulo,
                                                                nuevoAlbum.Tags,
                                                                true,//esPublica,
                                                                false,//isFamily,
                                                                false,//isFriend,
                                                                ContentType.Photo,
                                                                SafetyLevel.Safe,
                                                                HiddenFromSearch.Hidden
                                                            );
                PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);
                nuevoAlbum.Portada = oPhotoInfo.OriginalUrl;
            }
            else
            {
                nuevoAlbum.Portada = formulario["portada"];
            }

            Album album = albumManager.Agregar(nuevoAlbum);

            var tituloFoto = formulario["titulo"];
            var descripcion = formulario["titulo"];
            var tags = formulario["tags"];
            bool esPublica = true; //ojo con los bool
            bool isFamily = false; // ??
            bool isFriend = false; // ? 


            foreach (var file in FotoFile)
            {
                if (!albumManager.HasFile(file)) continue;
                try
                {
                    string photoId = f.UploadPicture(file.InputStream,  //file
                                                                file.FileName,      //Filename
                                                                tituloFoto,
                                                                descripcion,
                                                                tags,
                                                                esPublica,
                                                                isFamily,
                                                                isFriend,
                                                                ContentType.Photo,
                                                                SafetyLevel.Safe,
                                                                HiddenFromSearch.Hidden
                                                                );
                    PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);

                    Foto foto = new Foto();

                    foto.ID_Album = album.ID;

                    foto.SRC = oPhotoInfo.OriginalUrl;

                    foto.Miniatura = oPhotoInfo.ThumbnailUrl;

                    fotosSubidas.Add(foto);
                }
                catch (Exception e)
                {

                    errores += e.Message + " ";
                    success = false;
                }
            }



            try
            {
                fotoManager.Agregar(fotosSubidas);

            }
            catch (Exception e)
            {

                errores += e.Message + " ";
                success = false;
            }

            return Json(new { succes = success, mensaje = errores });


        }


        //TODO: revisar las url para ver cual corresponde a la imagen grande y ver los metodos de edicion-
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult EditarFotos(FormCollection formulario, HttpPostedFileBase[] FotoFile)
        {

            if (Session["usuario"] == null || Request.Cookies["OAuthToken"] == null)
            {
                return Json(new { succes = false, mensaje = "se requiere autenticacion." });
            }

            var f = FlickrManager.GetAuthInstance();
            //toma los datos de la vista "AgregarFotos"
            //toma solo los datos para agregar fotos.

            string[] fotos = Request.Form.GetValues("srcFotos");
            int cantidad = fotos.Length;
            int idAlbum = Convert.ToInt32(formulario["ID"]);
            List<Foto> listaDeFotos = new List<Foto>();

            for (int i = 0; i < cantidad; i++)
            {
                Foto foto = new Foto();
                foto.SRC = fotos[i];
                if (foto.SRC == null | foto.SRC.Length == 0)
                {
                    continue;
                }
                foto.ID_Album = idAlbum;

                listaDeFotos.Add(foto);
            }


            var tituloFoto = formulario["titulo"];
            var descripcion = formulario["titulo"];
            var tags = formulario["tags"];
            bool esPublica = true; //ojo con los bool
            bool isFamily = false; // ??
            bool isFriend = false; // ? 


            if (FotoFile.Length > 0)
            {
                foreach (var file in FotoFile)
                {
                    if (file != null && file.ContentLength > 0) continue;
                    try
                    {
                        string photoId = f.UploadPicture(file.InputStream,  //file
                                                                    file.FileName,      //Filename
                                                                    tituloFoto,
                                                                    descripcion,
                                                                    tags,
                                                                    esPublica,
                                                                    isFamily,
                                                                    isFriend,
                                                                    ContentType.Photo,
                                                                    SafetyLevel.Safe,
                                                                    HiddenFromSearch.Hidden
                                                                    );
                        PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);

                        Foto foto = new Foto();

                        foto.ID_Album = idAlbum;

                        foto.SRC = oPhotoInfo.OriginalUrl;

                        foto.Miniatura = oPhotoInfo.ThumbnailUrl;

                        listaDeFotos.Add(foto);
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }
                }
            }



            FotosManager managerDeFotos = new FotosManager();
            managerDeFotos.Agregar(listaDeFotos);

            //return RedirectToAction("Index");

            return Json(new { succes = true, mensaje = "aaasssdd" });



        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AgregarFotos(FormCollection formulario, HttpPostedFileBase[] FotoFile)
        {
            //Valido que exista session
            if (Session["usuario"] == null || Request.Cookies["OAuthToken"] == null)
            {
                return Json(
                     new
                     {
                         resultado = -3,
                         mensaje = "falta autenticación",
                     },
                     JsonRequestBehavior.AllowGet);
            }
            var mensaje = "Todo Ok";
            var resultado = 0;
            string[] fotos = Request.Form.GetValues("srcFotos");
            //Instancio el Controll de flickr
            var f = FlickrManager.GetAuthInstance();

            AlbumManager am = new AlbumManager();

            Album album = am.ConsultarPorID(Convert.ToInt32(formulario["Id"]));

            /*FOTOS*/
            var listaDeFotos = new List<Foto>();

            if (FotoFile.Length > 0)
            {
                foreach (var file in FotoFile)
                {
                    if (file != null && file.ContentLength > 0) continue;
                    try
                    {
                        string photoId = f.UploadPicture(file.InputStream,  //file
                                                                    file.FileName,      //Filename
                                                                    file.FileName,
                                                                    "del album : " + album.Titulo,
                                                                    album.Tags,
                                                                    true,//esPublica,
                                                                    false,//isFamily,
                                                                    false,//isFriend,
                                                                    ContentType.Photo,
                                                                    SafetyLevel.Safe,
                                                                    HiddenFromSearch.Hidden
                                                                    );
                        PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);

                        Foto foto = new Foto();

                        foto.ID_Album = album.ID;

                        foto.SRC = oPhotoInfo.OriginalUrl;

                        foto.Miniatura = oPhotoInfo.ThumbnailUrl;

                        listaDeFotos.Add(foto);
                    }
                    catch (Exception e)
                    {

                        mensaje = e.Message;
                        resultado = -1;
                        return Json(
                             new
                             {
                                 resultado = resultado,
                                 mensaje = "no se puedieron agregar las fotos, mensaje: " + mensaje + "stackTrace: " + e.StackTrace,
                             },
                         JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else if (fotos.Length > 0)
            {

                int cantidad = fotos.Length;
                int idAlbum = Convert.ToInt32(formulario["ID"]);

                for (int i = 0; i < cantidad; i++)
                {
                    Foto foto = new Foto();
                    foto.SRC = fotos[i];
                    if (foto.SRC == null | foto.SRC.Length == 0)
                    {
                        continue;
                    }
                    foto.ID_Album = idAlbum;

                    listaDeFotos.Add(foto);
                }
            }


            try
            {
                FotosManager managerDeFotos = new FotosManager();
                managerDeFotos.Agregar(listaDeFotos);
            }
            catch (Exception e)
            {
                resultado = -1;
                mensaje = " Ocurrió un error: " + e.Message;

            }

            return Json(
                          new
                          {
                              resultado = resultado,
                              mensaje = mensaje
                          },
                      JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditarAlbumJsonResult(AlbumFill album, HttpPostedFileBase[] portadaFile)
        {

            //Valido que exista session
            if (Session["usuario"] == null || Request.Cookies["OAuthToken"] == null)
            {

                return Json(
                     new
                     {
                         resultado = -3,
                         mensaje = "falta autenticación",
                     },
                     JsonRequestBehavior.AllowGet);
            }

            var mensaje = "Todo Ok";
            var resultado = 0;

            if (!ModelState.IsValid)
            {
                mensaje = "los datos son incorrectos, por favor revisalos.";
                resultado = -1;

                return Json(
                     new
                     {
                         resultado = resultado,
                         mensaje = mensaje,
                         album = album
                     },
                     JsonRequestBehavior.AllowGet);
            }


            //Instancio el Control de flickr
            var f = FlickrManager.GetAuthInstance();

            try
            {
                //si viene un archivo para portada lo subo y updateo
                if (portadaFile != null)
                {
                    //var file = Request.Files["portadaFile"];

                    string photoId = f.UploadPicture(portadaFile[0].InputStream,  //file
                                                                    portadaFile[0].FileName,      //Filename
                                                                    album.Titulo,
                                                                    "portada de: " + album.Titulo,
                                                                    album.Tags,
                                                                    true,//esPublica,
                                                                    false,//isFamily,
                                                                    false,//isFriend,
                                                                    ContentType.Photo,
                                                                    SafetyLevel.Safe,
                                                                    HiddenFromSearch.Hidden
                                                                );
                    PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);
                    album.Portada = oPhotoInfo.OriginalUrl;
                }




                AlbumManager managerDeAlbum = new AlbumManager();
                managerDeAlbum.EditarFill(album);

            }
            catch (Exception e)
            {
                mensaje = e.Message;
                resultado = -1;

            }
            /* FIN ALBUM*/

            /*FOTOS*/
            //var listaDeFotos = new List<Foto>();

            //foreach (var file in FotoFile)
            //{
            //    if (file != null && file.ContentLength > 0) continue;
            //    try
            //    {
            //        string photoId = f.UploadPicture(file.InputStream,  //file
            //                                                    file.FileName,      //Filename
            //                                                    albumNuevo.Titulo,
            //                                                    "del album : " + albumNuevo.Titulo,
            //                                                    albumNuevo.Tags,
            //                                                    true,//esPublica,
            //                                                    false,//isFamily,
            //                                                    false,//isFriend,
            //                                                    ContentType.Photo,
            //                                                    SafetyLevel.Safe,
            //                                                    HiddenFromSearch.Hidden
            //                                                    );
            //        PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);

            //        Foto foto = new Foto();

            //        foto.ID_Album = albumNuevo.ID;

            //        foto.SRC = oPhotoInfo.OriginalUrl;

            //        foto.Miniatura = oPhotoInfo.ThumbnailUrl;

            //        listaDeFotos.Add(foto);
            //    }
            //    catch (Exception e)
            //    {

            //        throw e;
            //    }
            //}

            //FotosManager managerDeFotos = new FotosManager();
            //managerDeFotos.Agregar(listaDeFotos);

            return Json(
                     new
                     {
                         resultado = resultado,
                         mensaje = mensaje,
                         album = album
                     }, "json", JsonRequestBehavior.AllowGet);

        }

        [ValidateInput(false)]
        public ActionResult BorrarFoto(FormCollection formulario)
        {
            FotosManager manager = new FotosManager();

            string[] borrar = Request.Form.GetValues("borrar");
            foreach (string asd in borrar)
            {
                int id = Convert.ToInt32(asd);
                manager.Borrar(id);
            }

            string idAlbum = formulario["ID"];

            return RedirectToAction("ModificarAlbum", new { id = idAlbum });
        }
        public JsonResult BorrarFotosJSonResult()
        {
            int resultado = 0;
            string mensaje = "<strong>Listo!</strong> ya borramos esas fotos.";



            try
            {
                FotosManager manager = new FotosManager();

                string borrar = Request.Params["borrar"];
                string[] ids = borrar.Split(',');
                foreach (string asd in ids)
                {
                    int id = Convert.ToInt32(asd);
                    manager.Borrar(id);
                }
            }
            catch (Exception e)
            {

                resultado = -1;
                mensaje = e.Message;

            }
            return Json(
                    new
                    {
                        resultado = resultado,
                        mensaje = mensaje
                    },
                    JsonRequestBehavior.AllowGet);
        }


        [ValidateInput(false)]
        public ActionResult ModificarAlbum(int id)
        {
            if (Session["usuario"] != null)
            {
                AlbumManager albumManager = new AlbumManager();
                Album album = albumManager.ConsultarPorID(id);


                FotosManager managerFotos = new FotosManager();
                List<Foto> Fotos = managerFotos.Consultar(id);

                ViewBag.Fotos = Fotos;
                ViewBag.Album = album;
                return View();
            }
            else
            {
                return View("Error");
            }

        }

        public ActionResult Borrar(int id)
        {
            if (Session["usuario"] != null)
            {
                AlbumManager albumManager = new AlbumManager();
                albumManager.Borrar(id);

                string album = "borrado";

                return RedirectToAction("Admin", "Home", new { album });
            }
            else
            {
                return View("Error");
            }

        }

        public ActionResult BorrarAlbum()
        {
            AlbumManager managerAlbum = new AlbumManager();
            List<Album> Albunes = managerAlbum.Consultar();

            ViewBag.Albunes = Albunes;

            return View();
        }

        [HttpGet]
        public ActionResult NuevoAlbum()
        {

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NuevoAlbum(FormCollection form)
        {

            return View();
        }


    }
}