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
            List<Album> Albunes = managerAlbum.Consultar();

            ViewBag.Albunes = Albunes;

            return View();
        }

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

        public ActionResult Admin( string mensaje)
        {
            if(Session["usuario"]!= null)
            {
                ViewBag.Mensaje = mensaje;
                return View();

            }
            else
            {
                return View("Error");
            }
           
        }

        public ActionResult Portadas()
        {
            PortadasManager portadasManager = new PortadasManager();
            ViewBag.Portadas = portadasManager.Consultar();

                        
            return View();           
        }

        [ValidateInput(false)]
        public ActionResult EditarPortada1(FormCollection formulario)
        {
                Portada portada1 = new Portada();
                portada1.ID =Convert.ToInt32(formulario["IdPortada1"]);
                portada1.SRC = formulario["SRC1"];
                portada1.Texto = formulario["textoPortada1"];

                PortadasManager portadasManager = new PortadasManager();
                portadasManager.Editar(portada1);

                
                return RedirectToAction("Portadas");
        }

        [ValidateInput(false)]
        public ActionResult EditarPortada2(FormCollection formulario)
        {
            PortadasManager portadasManager = new PortadasManager();

            Portada portada2 = new Portada();
            portada2.ID = Convert.ToInt32(formulario["IdPortada2"]);
            portada2.SRC = formulario["SRC2"];
            portada2.Texto = formulario["textoPortada2"];

            portadasManager.Editar(portada2);

            return RedirectToAction("Portadas");
        }

        [ValidateInput(false)]
        public ActionResult EditarPortada3(FormCollection formulario)
        {
            PortadasManager portadasManager = new PortadasManager();


            Portada portada3 = new Portada();
            portada3.ID = Convert.ToInt32(formulario["IdPortada3"]);
            portada3.SRC = formulario["SRC3"];
            portada3.Texto = formulario["textoPortada3"];

            portadasManager.Editar(portada3);

            return RedirectToAction("Portadas");
        }

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
                                        "<p style='font-style: normal;font-weight: 400;Margin-bottom: 22px;Margin-top: 18px;font-size: 13px;line-height: 22px;font-family: 'PT Serif',Georgia,serif;color: #7c7e7f;text-align: left;' >" + nombre +", ya recibi tu consulta, en breve me pondré en contacto con vos." + "</p>" +
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
            

            return RedirectToAction("Contacto","Contacto", new { enviado });
        }


    }
}