using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrareMagazin
{
    public class Produs
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public string Desriere { get; set; }
        public DateTime DataIntrareMagazin { get; set; }
        public DateTime TermenValabilitate { get; set; }
        public int Cantitate { get; set; }
    }
}
