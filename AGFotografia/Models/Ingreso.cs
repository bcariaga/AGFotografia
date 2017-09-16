using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AGFotografia.Models
{
    public class Ingreso
    {
        public int IngresoId { get; set; }
        public string UserAgent { get; set; }
        public DateTime FechaIngreso { get; set; }
    }

    public class IngresoDatos
    {
        public int IngresosDia { get; set; }
        public int IngresosSemana { get; set; }
        public int IngresoMes { get; set; }
        public int IngresosHistorico { get; set; }
    }

    public class IngresoManager {


        public void NuevoIngreso()
        {

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);
            var userAgent = HttpContext.Current.Request.UserAgent;

            try
            {
                SqlCommand insert = conexion.CreateCommand();
                insert.CommandText = "INSERT INTO Ingreso (UserAgent, FechaIngreso) VALUES ( @userAgent, GETDATE() )";
                insert.Parameters.AddWithValue("@userAgent", userAgent);

                conexion.Open();

                insert.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }


        }

        public IngresoDatos IngresoDatos() {

            var ingresoDatos = new IngresoDatos();

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ConectionString"]);

            SqlCommand select = conexion.CreateCommand();

            select.CommandText = "EXECUTE dbo.IngresoDatos";

            try
            {
                DataTable result = new DataTable();

                SqlDataAdapter adaptador = new SqlDataAdapter(select);

                adaptador.Fill(result);

                foreach (DataRow row in result.Rows)
                {
                    ingresoDatos.IngresosHistorico = (int)row["Historico"];
                    ingresoDatos.IngresosSemana = (int)row["Semana"];
                    ingresoDatos.IngresoMes = (int)row["Mes"];
                    ingresoDatos.IngresosDia = (int)row["Dia"];
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally {

                conexion.Close();
            }
            
            return ingresoDatos;

        }


    }
}