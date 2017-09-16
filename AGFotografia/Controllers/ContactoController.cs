using AGFotografia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGFotografia.Controllers
{
    public class ContactoController : Controller
    {
        // GET: Contacto
        public ActionResult Contacto(string enviado)
        {
            AlbumManager managerAlbum = new AlbumManager();
            List<Album> Albunes = managerAlbum.Consultar();

            ViewBag.Albunes = Albunes;

            DatosContactoManager contactoManager = new DatosContactoManager();
            DatosContacto contacto = contactoManager.Consultar();

            ViewBag.Contacto = contacto;
           

            if(enviado == null)
            {
                return View();
            }
            else
            {
                ViewBag.Listo = "¡Gracias!";
                return View();
            }
            
        }

    }
}