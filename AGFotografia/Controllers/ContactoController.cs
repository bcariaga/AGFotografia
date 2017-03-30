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

        [ValidateInput(false)]
        public ActionResult Editar(FormCollection formulario)
        {
            DatosContacto datosEditados = new DatosContacto();
            datosEditados.Email = formulario["email"];
            datosEditados.Tel = formulario["tel"];
            datosEditados.Facebook = formulario["facebook"];
            datosEditados.Flickr = formulario["flickr"];
            datosEditados.Texto1 = formulario["texto1"];
            datosEditados.Texto2 = formulario["texto2"];
            datosEditados.Portada = formulario["portada"];
            datosEditados.Titulo = formulario["titulo"];
            datosEditados.Subtitulo = formulario["subtitulo"];

            DatosContactoManager edicion = new DatosContactoManager();
            edicion.Editar(datosEditados);

            return RedirectToAction("Contacto", "Contacto");
        }
    }
}