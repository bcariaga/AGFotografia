using AGFotografia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGFotografia.Controllers
{
    public class SobreMiController : Controller
    {
        // GET: Contacto
        public ActionResult SobreMi()
        {
            AlbumManager managerAlbum = new AlbumManager();
            List<Album> Albunes = managerAlbum.Consultar();

            ViewBag.Albunes = Albunes;

            SobreMiManager sobreMi = new SobreMiManager();
            SobreMi datosSobreMi = sobreMi.Consultar();

            ViewBag.SobreMi = datosSobreMi;

            return View();
        }

        
    }
}