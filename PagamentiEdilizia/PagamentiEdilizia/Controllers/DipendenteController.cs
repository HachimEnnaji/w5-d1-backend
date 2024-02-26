using PagamentiEdilizia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PagamentiEdilizia.Controllers
{
    public class DipendenteController : Controller
    {
        // GET: Dipendente
        public ActionResult Index()
        {
            List<Dipendente> dipendenti = new List<Dipendente>();
            string connectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "SELECT * FROM DIPENDENTE";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Dipendente d = new Dipendente();
                    d.Nome = reader["Nome"].ToString();
                    d.Cognome = reader["Cognome"].ToString();
                    d.CodiceFiscale = reader["CodiceFiscale"].ToString();
                    d.Indirizzo = reader["Indirizzo"].ToString();
                    d.Coniugato = Convert.ToBoolean(reader["Coniugato"]);
                    d.FigliACarico = Convert.ToInt32(reader["FigliACarico"]);
                    d.Mansione = reader["Mansione"].ToString();
                    dipendenti.Add(d);
                }

            }
            catch (Exception ex)
            {
                Response.Write("Errore: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(dipendenti);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Dipendente dipendente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "INSERT INTO DIPENDENTE (Nome, Cognome, CodiceFiscale, Indirizzo, Coniugato, FigliACarico, Mansione) VALUES (@Nome, @Cognome, @CodiceFiscale, @Indirizzo, @Coniugato, @FigliACarico, @Mansione)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@CodiceFiscale", dipendente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                cmd.Parameters.AddWithValue("@FigliACarico", dipendente.FigliACarico);
                cmd.Parameters.AddWithValue("@Mansione", dipendente.Mansione);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("Errore: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }
    }
}