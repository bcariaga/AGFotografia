using AGFotografia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGFotografia.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Ingresar()
        {
            return View();
        }

        public ActionResult Login(FormCollection formLogin)
        {
            string usuario = formLogin["usuario"];
            string pass = formLogin["password"];
            // se toma el pass ingresado, se encripta y se compara con el de la base de datos.
            string passwordEncriptado = Encriptador.RijndaelSimple.Encriptar(pass);
            string password = passwordEncriptado;

            //var password =  formLogin["password"]; // no se encripta

            UsuarioManager userManager = new UsuarioManager();

            Usuario user = userManager.ValidarLogin(usuario, password);

            if (user != null)

            {
                Session["Admin"] = user;
                return RedirectToAction("Dashboard", "Admin");

            }

            else
            {
                ViewBag.Mensaje = " datos incorrectos.";
                return RedirectToAction("Ingresar", "Usuario");
            }


        }

        //public ActionResult CrearUser(FormCollection formulario)
        //{

        //    if (Session["usuario"] != null)
        //    {
        //        string usuario = formulario["usuario"].ToString();
        //        string password1 = formulario["password1"].ToString();
        //        string password2 = formulario["password2"].ToString();

        //        //valida que los pass sean iguales
        //        if (password1 == password2)
        //        {
        //            Usuario usuarioNuevo = new Usuario();
        //            usuarioNuevo.usuario = usuario;
        //            // Se usa una clase para encriptar, no la hice yo.
        //            string passwordEncriptado = Encriptador.RijndaelSimple.Encriptar(password1);
        //            usuarioNuevo.password = passwordEncriptado;

        //            UsuarioManager userManager = new UsuarioManager();
        //            Usuario userReg = userManager.Registro(usuarioNuevo);

        //            if (userReg != null)
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ViewBag.ErrorUser = "usuario ya registrado";
        //                return RedirectToAction("ErrorRegistro", "User");
        //            }

        //        }

        //        else
        //        {
        //            ViewBag.Pass = "Password no son iguales.";
        //            return View("Registro");
        //        }

        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}

        public ActionResult Logout()
        {
            Session.Remove("Admin");
            return RedirectToAction("Index", "Home");
        }
    }
}