using System;

namespace PagamentiEdilizia.Models
{
    public class Pagamenti
    {
        public int IDPagamento { get; set; }
        public bool Acconto { get; set; }
        public bool Stipendio { get; set; }
        public DateTime Periodo { get; set; }
        public decimal Importo { get; set; }
        public int IDDipendente { get; set; }

    }
}