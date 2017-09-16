using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGFotografia.Models
{
    public class UltimaModificacion
    {
        public int ModificacionId { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }

        public UltimaModificacion(int ModificacionId, DateTime Fecha, int UsuarioId)
        {
            this.ModificacionId = ModificacionId;
            this.Fecha = Fecha;
            this.UsuarioId = UsuarioId;

            
        }

        public UltimaModificacion()
        {
        }

        public UltimaModificacion GetUtimaModificacion() {

            /*TODO hacer consulta a DB */

            /*la tabla seria UltimaModificacion*/
            /*ModificacionId (INT, PK)*/
            /*Fecha (DATETIME)*/
            /*UsuarioId (INT)*/
            return new UltimaModificacion(
                    ModificacionId = 1,
                    Fecha = DateTime.Now,
                    UsuarioId = 1
                );

        }

    }

    public class MyCookieStatus
    {
        //private string name { get; set; }
        //private bool status { get; set; }

        public string Name { get; set; }
        public bool Status { get; set; }

        public MyCookieStatus(string name, bool status)
        {
            this.Name = name;
            this.Status = status;

           
        }

        //public MyCookie MyCookie(string name, bool status)
        //{
        //    this.name = name;
        //    this.status = status;

        //    return this;
        //}
    }
}