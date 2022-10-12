using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buecher
{

    // Klasse enthält grundlegende Daten eines Buches
    public abstract class Buch
    {
        public Buch()
        {

        }
        public string? Titel { get; set; }
        public int Id { get; set; }

        public string? Autor { get; set; }
    }
}
