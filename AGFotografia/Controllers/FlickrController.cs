using AGFotografia.Models;
using FlickrNet;
using System;
using System.Web;
using System.Web.Mvc;

namespace AGFotografia.Controllers
{
    public class FlickrController : Controller
    {

        public enum Resultado
        {
            success = 0,
            error = 1
        }

        public ActionResult AuthResponse()
        {
            var mensaje = "";
            var resultado = Resultado.success;
            // The request token is stored in session - if it isn't present then we do nothing
            if (Request.QueryString["oauth_verifier"] != null && Session["RequestToken"] != null)
            {
                Flickr f = FlickrManager.GetInstance();
                OAuthRequestToken requestToken = Session["RequestToken"] as OAuthRequestToken;
                try
                {
                    OAuthAccessToken accessToken = f.OAuthGetAccessToken(requestToken,
                        Request.QueryString["oauth_verifier"]);
                    FlickrManager.OAuthToken = accessToken;

                    mensaje = "You successfully authenticated as " + accessToken.FullName;
                    resultado = Resultado.success;

                    Session["Flickr"] = true;
                }
                catch (OAuthException ex)
                {
                    mensaje = "An error occurred retrieving the token : " + ex.Message;
                    resultado = Resultado.error;

                }
            }

            ViewBag.Mensaje = mensaje;
            ViewBag.LoginFlickr = resultado;

            return RedirectToAction("Admin", "Home", new { mensaje = mensaje });
            //return View();
        }

        public void FlickrAuth()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            string authResponse = Request.Url.GetLeftPart(UriPartial.Authority) + u.Action("AuthResponse", "Flickr");


            Flickr f = FlickrManager.GetInstance();
            OAuthRequestToken token = f.OAuthGetRequestToken(authResponse);

            Session["RequestToken"] = token;

            string url = f.OAuthCalculateAuthorizationUrl(token.Token, AuthLevel.Write);// redireccionar bien al otro metodo.
            Response.Redirect(url);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {

            var f = FlickrManager.GetAuthInstance();
            //f.OnUploadProgress += new EventHandler<FlickrNet.UploadProgressEventArgs>(Flickr_OnUploadProgress);

            //var nombreFoto = "Foto de Prueba";
            var tituloFoto = "Titulo de foto de prueba";
            var descripcion = "Descripcion de Foto de Prueba, que tambien tiene un titulo de prueba";
            var tags = "tags(de prueba)";
            bool esPublica = true; //ojo con los bool
            bool isFamily = false; // ??
            bool isFriend = false; // ? 



            string photoId = f.UploadPicture(file.InputStream, file.FileName, tituloFoto, descripcion, tags, esPublica, isFamily, isFriend, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden);
            PhotoInfo oPhotoInfo = f.PhotosGetInfo(photoId);
            ViewBag.PhotoUrl = oPhotoInfo.Small320Url;
            return View("AuthResponse");
        }
    }
}