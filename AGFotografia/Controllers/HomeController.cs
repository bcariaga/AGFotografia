using AGFotografia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace AGFotografia.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PortadasManager portadasManager = new PortadasManager();
            ViewBag.Portadas = portadasManager.Consultar();

            AlbumManager managerAlbum = new AlbumManager();
            //ViewBag.Albunes = managerAlbum.Consultar();
            ViewBag.Albunes = managerAlbum.ConsultarAlbumFill();
            //ViewBag.ColorTheme = ConfigurationManager.AppSettings["Color.Theme"];


            return View();
        }
        [HttpPost]
        public JsonResult NuevoUser() {

            string mensaje = "Ok";
            int resultado = 0;

            var im = new IngresoManager();
            try
            {

                im.NuevoIngreso();
                Session["invitado"] = "Bienvenido!";
            }
            catch (Exception e)
            {

                mensaje = e.Message;
                resultado = -1;
            }

            return Json(
                new
                {
                    mensaje = mensaje,
                    resultado = resultado
                }, JsonRequestBehavior.AllowGet);

        }

        //public List<MyCookieStatus> myCookies;
        /*algun dia, uso de cookies (deprecated)*/
        //public ActionResult Index()
        //{
        //    //bool havePing = ping.HasValue ? ping.Value : false;

        //    //Request.Cookies
        //    if (Session.IsNewSession)
        //    {
        //        Session.Add("Invitado",new { ultimoIngreso = DateTime.Now});
        //        return View();

        //    }
        //    else //hacer toda la logica de las cookies
        //    {

        //        /*Control de cookies*/
        //        CheckCookies(Request.Cookies);

        //        var ultimoIngreso = myCookies.Single(x => x.Name == "ultimoIngreso");

        //        if (ultimoIngreso.Status)
        //        {
        //           List<Portada> portadas = Request.Cookies["portadas"].Value;
        //        }

        //        PortadasManager portadasManager = new PortadasManager();
        //        ViewBag.Portadas = portadasManager.Consultar();

        //        AlbumManager managerAlbum = new AlbumManager();
        //        List<Album> Albunes = managerAlbum.Consultar();

        //        ViewBag.Albunes = Albunes;

        //        return View("_index");
        //    }


        //}
       
        //private void CheckCookies(HttpCookieCollection cookies) {


        //    var _ultimaVisita =cookies["ultimaVisita"];
        //    var _index = cookies["index"];
        //    var _albunes= cookies["albunes"];

            
        //    bool ultimaVisita = _ultimaVisita != null ? true : false;
        //    bool index = _index != null ? true : false;
        //    bool albunes = _albunes != null ? true : false;

        //    var ultimaModificacion = new UltimaModificacion().GetUtimaModificacion();

        //    if (ultimaVisita && Convert.ToDateTime(_ultimaVisita.Values[0]) <= ultimaModificacion.Fecha)
        //    {
        //        ultimaVisita = false;
        //    }



        //    myCookies.Add(
        //        new MyCookieStatus(
        //            "ultimaVisita", ultimaVisita
        //        )
        //    );

        //    myCookies.Add(
        //        new MyCookieStatus(
        //            "index ", index
        //        )
        //    );

        //    myCookies.Add(
        //        new MyCookieStatus(
        //            "albunes", albunes
        //        )
        //    );

        
        //}

        public ActionResult Todos()
        {
            if (Session["usuario"] != null)
            {
                AlbumManager managerAlbum = new AlbumManager();
                List<Album> Albunes = managerAlbum.Consultar();


                ViewBag.Albunes = Albunes;

                return View();
            }
            else
            {
                return View("Error");
            }

        }

        //public ActionResult Admin(string mensaje)
        //{
        //    if (Session["usuario"] != null)
        //    {
        //        ViewBag.Mensaje = mensaje;
        //        return View();

        //    }
        //    else
        //    {
        //        return View("Error");
        //    }

        //}

       

        public ActionResult Comentario(FormCollection formulario)
        {
            string nombre = formulario["nombre"];
            string email = formulario["email"];
            string tel = formulario["tel"];
            string msj = formulario["msj"];

            SmtpClient SmtpClient = new SmtpClient(ConfigurationManager.AppSettings["MailClient"], Convert.ToInt32(ConfigurationManager.AppSettings["MailClientPort"]));
            SmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailNetworkCredentialName"], ConfigurationManager.AppSettings["MailNetworkCredentialPass"]);
            SmtpClient.EnableSsl = true;

            MailMessage envioNota = new MailMessage();
            envioNota.From = new MailAddress(ConfigurationManager.AppSettings["MailNetworkCredentialName"]);
            envioNota.To.Add(email);
            envioNota.Subject = "¡Gracias por tu contacto!";
            envioNota.IsBodyHtml = true;
            envioNota.Body = "<div style='margin: 0;padding: 0;background-color:##e5ffe5;'>" +
                                 "<div style='margin:7% 25% 7% 25%;text-align: center; width:50%; height:50%; background-color: #ffffff' >" +
                                      "<div>" +
                                          "<h4 style='font-style: normal;font-weight: 400;Margin-bottom: 0;Margin-top: 0;font-size: 16px;line-height: 24px;font-family: 'PT Serif',Georgia,serif;color: #788991;text-align: center;' >Aldana Gonz Fotografia</ h4 >" +
                                      "</div >" +
                                      "<br />" +
                                      "<div>" +
                                        "<span style='font-style: normal;font-weight: 400;Margin-bottom: 0;Margin-top: 14px;font-size: 22px;line-height: 30px;font-family: Ubuntu,sans-serif;color: #3e4751;text-align: center;' >" + "¡Gracias por tu contato!" + "</span>" +
                                      "</div>" +
                                      "<div>" +
                                        "<p style='font-style: normal;font-weight: 400;Margin-bottom: 22px;Margin-top: 18px;font-size: 13px;line-height: 22px;font-family: 'PT Serif',Georgia,serif;color: #7c7e7f;text-align: left;' >" + nombre + ", ya recibi tu consulta, en breve me pondré en contacto con vos." + "</p>" +
                                      "</div>" +
                                      "<div style='border-radius: 3px;display: inline-block;font-size: 14px;font-weight: 700;line-height: 24px;padding: 13px 35px 12px 35px;text-align: center;text-decoration: none !important;transition: opacity 0.2s ease-in;font-family: 'PT Serif',Georgia,serif; background-color: #2b6b21; color: #ffffff;'>" +
                                         "<a href='http://aldanagonz.com'> AldanaGonz fotografia.</a>" +
                                      "</div>" +
                                      "</div>" +
                                " </div>";

            MailMessage mailAAdmin = new MailMessage();
            mailAAdmin.From = new MailAddress(ConfigurationManager.AppSettings["MailNetworkCredentialName"]);
            mailAAdmin.To.Add("aldanagonz.fotografia1@gmail.com");
            mailAAdmin.Subject = "Dejaron un comentario en la pagina.";
            mailAAdmin.Body = nombre + " dejo este comentario; " + msj + " , el mail que dejo es: " + email + " y este telefono:" + tel;


            SmtpClient.Send(envioNota);
            SmtpClient.Send(mailAAdmin);

            string enviado = "enviado";


            return RedirectToAction("Contacto", "Contacto", new { enviado });
        }


    }
}