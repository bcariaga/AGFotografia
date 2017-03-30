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

        [HttpPost]
        public ActionResult Editar(FormCollection formulario)
        {
            SobreMi datosEditados = new SobreMi();
            datosEditados.Texto1 = formulario["texto1"];
            datosEditados.Texto2 = formulario["texto2"];
            datosEditados.Texto3 = formulario["texto3"];
            datosEditados.Portada = formulario["portada"];
            datosEditados.Titulo = formulario["titulo"];
            datosEditados.Subtitulo = formulario["subtitulo"];

            SobreMiManager sobreMi = new SobreMiManager();
            sobreMi.Editar(datosEditados);

            return RedirectToAction("SobreMi");
        }
    }
}