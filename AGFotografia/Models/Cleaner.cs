using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGFotografia.Models
{
    public class Cleaner
    {
        public static class Limpiador
        {
            #region Limpiar
            /// <summary>
            /// Metodo para limpiar la cadena, permitiendo que el usuario pueda insertar cualquier codigo que haga referencia a un src (etiqueta HTML <img/>)
            /// y conseguir solo el src="".
            /// </summary>
            /// <param name="textoALimpiar"></param>
            /// <returns></returns>
            public static string Limpiar(string textoALimpiar)
            {
                string textoLimpio = ""; //se crea el string vacio que va a contener el nuevo string limpio 

                bool validacion = textoALimpiar.StartsWith("http") || textoALimpiar.StartsWith("HTTP"); //se consulta si viene directamente el src (click derecho --> copiar dirección de imagen)

                if (validacion == true)
                {
                    textoLimpio = textoALimpiar; //si viene directamente el src lo devuelve como viene.
                }
                else //si no esta el src de entrada se hace toda la busqueda
                {
                    int src = textoALimpiar.IndexOf("src=" + "'") + 5;     // busca el src= incluyendo la '
                    if (src == -1 | src == 0 | src == 4)               //si no hay comilla simple busca comilla doble. se lo iguala a 4 porque agrego 5 espacios y se supone que si da -1 mas 5 es 4
                    {
                        src = textoALimpiar.IndexOf("src=\"") + 5;                 //busca la proxima comilla doble desde src="
                        int final = textoALimpiar.IndexOf("\"", src);         // se calcula el largo del de src=" hasta ", se agregan 5 caracteres que corresponden a src="
                        int largo = final - src;                    //se crea el string desde src (src=") la cantidad de espacios necesarios hasta la proxima "
                        string strLimpia = textoALimpiar.Substring(src, largo);

                        textoLimpio = strLimpia;

                    }
                    else
                    {
                        int final = textoALimpiar.IndexOf("\'", src);         // se calcula el largo del de src=" hasta ", se agregan 5 caracteres que corresponden a src="
                        int largo = final - src;                    //se crea el string desde src (src=") la cantidad de espacios necesarios hasta la proxima "
                        string strLimpia = textoALimpiar.Substring(src, largo);

                        textoLimpio = strLimpia;
                    }
                }

                return textoLimpio.ToLower();
            }
            #endregion
        }
    }
}