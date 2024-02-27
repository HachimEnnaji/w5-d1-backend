using PagamentiEdilizia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PagamentiEdilizia.Controllers
{
    public class PagamentiController : Controller
    {
        // GET: Pagamenti

        public ActionResult Index(bool? id)
        {
            List<Pagamenti> pagamento = new List<Pagamenti>();
            string myConnection = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(myConnection);

            try
            {
                conn.Open();
                string query = id.HasValue ? $"SELECT * FROM PAGAMENTI ORDER BY PERIODO ASC" : "SELECT * FROM PAGAMENTI";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Pagamenti p = new Pagamenti();
                    p.Acconto = Convert.ToBoolean(reader["Acconto"]);
                    p.Stipendio = Convert.ToBoolean(reader["Stipendio"]);
                    p.Periodo = Convert.ToDateTime(reader["Periodo"]);
                    p.Importo = Convert.ToDecimal(reader["Ammontare"]);
                    p.IDDipendente = Convert.ToInt32(reader["IDDipendente"]);
                    pagamento.Add(p);


                }

            }
            catch (System.Exception ex)
            {
                Response.Write("Errore: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(pagamento);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Pagamenti pagamento)
        {
            string myConnection = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(myConnection);

            try
            {
                conn.Open();
                string query = "INSERT INTO PAGAMENTI (Acconto, Stipendio, Periodo, Ammontare, IDDipendente) VALUES(@Acconto, @Stipendio, @Periodo, @Ammontare, @IDDipendente)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Acconto", pagamento.Acconto);
                cmd.Parameters.AddWithValue("@Stipendio", pagamento.Stipendio);
                cmd.Parameters.AddWithValue("@Periodo", pagamento.Periodo);
                cmd.Parameters.AddWithValue("@Ammontare", pagamento.Importo);
                cmd.Parameters.AddWithValue("@IDDipendente", pagamento.IDDipendente);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
            finally { conn.Close(); }

            return RedirectToAction("Index");
        }



    }
}