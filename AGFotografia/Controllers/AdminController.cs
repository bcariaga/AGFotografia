using AGFotografia.Models;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGFotografia.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return Json(new { succes = false, mensaje = "se requiere autenticacion." }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Index", "Home");
                
            }


            try
            {
                AlbumManager am = new AlbumManager();
                ViewBag.Albunes = am.ConsultarAlbumFill();
            }
            catch (Exception e)
            {

                return Json(new { mensaje = "exploto AlbumManager " + e.Message ,result = -1}, JsonRequestBehavior.AllowGet) ;
            }

            try
            {
                UsuarioManager um = new UsuarioManager();
                ViewBag.Usuarios = um.UsuariosTodos();
            }
            catch (Exception e)
            {

                return Json(new{mensaje = "exploto UsuarioManager " + e.Message ,result = -1}, JsonRequestBehavior.AllowGet);
            }
            try
            {

                PortadasManager pm = new PortadasManager();
                ViewBag.Portadas = pm.Consultar();
            }
            catch (Exception e)
            {

                return Json(new{mensaje = "exploto PortadasManager " + e.Message ,result = -1},JsonRequestBehavior.AllowGet);
            }
            try
            {

                IngresoManager im = new IngresoManager();
                ViewBag.Ingresos = im.IngresoDatos();
            }
            catch (Exception e)
            {

                return Json(new{mensaje = "exploto IngresoManager" + e.Message ,result = -1}, JsonRequestBehavior.AllowGet);
            }

            try
            {
                DatosContactoManager dcm = new DatosContactoManager();
                ViewBag.Contacto = dcm.Consultar();

            }
            catch (Exception e)
            {

                return Json(new{mensaje = "exploto DatosContactoManager" + e.Message ,result = -1}, JsonRequestBehavior.AllowGet) ;
            }
            try
            {
                SobreMiManager smm = new SobreMiManager();
                ViewBag.SobreMi = smm.Consultar();

            }
            catch (Exception e)
            {

                return Json(new{mensaje = "exploto SobreMiManager" + e.Message ,result = -1}, JsonRequestBehavior.AllowGet) ;
            }


            return View();
        }

        [HttpPost]
        public ActionResult EditharTheme(string color)
        {
            string mensaje = "Ya ta!";
            int resultado = 0;
            try
            {
                //Permite cambiar el web config por los colores
                Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
                //Edit
                if (objAppsettings != null)
                {
                    objAppsettings.Settings["Color.Theme"].Value = color;
                    objConfig.Save();
                }
            }
            catch (Exception e)
            {
                mensaje += e.Message;
                resultado = -1;
            }


            return Json(
               new
               {
                   mensaje = mensaje,
                   resultado = resultado
               }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditarPortada(Portada portada, HttpPostedFileBase portadaFile)
        {
            string mensaje = "";
            int resultado = 0;

            try
            {
                PortadasManager pm = new PortadasManager();
                var f = FlickrManager.GetAuthInstance();

                if (!ModelState.IsValid)
                {
                    mensaje += "Ocurrio un error en la portada" + portada.ID;
                    resultado = resultado != 0 ? resultado : -1;
                }
                else
                {
                    if (portadaFile != null && portadaFile.ContentLength > 0)
                    {
                        string photoId = f.UploadPicture(
                            portadaFile.InputStream,  //file
                            portadaFile.FileName,      //Filename
                            "portada de aldanagonz.com ("+portada.ID+")",
                            portada.Texto,
                            "portada",
                            true,
                            false,
                            false,
                            ContentType.Photo,
                            SafetyLevel.Safe,
                            HiddenFromSearch.Hidden);

                        PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);
                        portada.SRC = oPhotoInfo.OriginalUrl;

                    }
                    pm.Editar(portada);
                    mensaje = "Listo! la portada "+ portada.ID +" se edito bien.";
                }
            }
            catch (Exception e)
            {
                mensaje += e.Message;
                resultado = -1;
            }

            return Json(
                new {
                    mensaje = mensaje,
                    resultado = resultado
                }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult ContactEdit(DatosContacto datosContacto)
        {
            if (Session["Admin"] == null || Request.Cookies["OAuthToken"] == null)
            {
                return Json(new { succes = false, mensaje = "se requiere autenticacion." },JsonRequestBehavior.AllowGet);
            }

            var mensaje = "Listo!";
            var resultado = 0;

            
            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Files != null)
                    {
                        var f = FlickrManager.GetAuthInstance();
                        var file = Request.Files[0];

                        string photoId = f.UploadPicture(file.InputStream, file.FileName, "portada de aldanagonz.com (Contacto)", "", "portada", true, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden);

                        PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);
                        datosContacto.Portada = oPhotoInfo.OriginalUrl;
                    }

                    DatosContactoManager dcm = new DatosContactoManager();
                    dcm.Editar(datosContacto);
                }
                catch (Exception e)
                {
                    mensaje = e.Message;
                    resultado = -1;
                }

            }
            else
            {
                mensaje = "Los datos no coinciden";
                resultado = -1;
            }

            return Json(
                new
                {
                    mensaje = mensaje,
                    resultado = resultado
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SobreMiEdit(SobreMi sobreMi)
        {
            if (Session["Admin"] == null || Request.Cookies["OAuthToken"] == null)
            {
                return Json(new { succes = false, mensaje = "se requiere autenticacion." }, JsonRequestBehavior.AllowGet);
            }

            var mensaje = "Listo!";
            var resultado = 0;

            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Files != null)
                    {
                        var f = FlickrManager.GetAuthInstance();
                        var file = Request.Files[0];

                        string photoId = f.UploadPicture(file.InputStream,file.FileName,"portada de aldanagonz.com (Sobre Mi)","","portada",true,false,false,ContentType.Photo,SafetyLevel.Safe,HiddenFromSearch.Hidden);

                        PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);
                        sobreMi.Portada = oPhotoInfo.OriginalUrl;
                    }

                    SobreMiManager smm = new SobreMiManager();
                    smm.Editar(sobreMi);
                }
                catch (Exception e)
                {
                    mensaje = e.Message;
                    resultado = -1;
                }
            }
            else
            {
                mensaje = "Los datos no coinciden";
                resultado = -1;
            }

            return Json(
                new
                {
                    mensaje = mensaje,
                    resultado = resultado
                }, JsonRequestBehavior.AllowGet);
        }
    }
}