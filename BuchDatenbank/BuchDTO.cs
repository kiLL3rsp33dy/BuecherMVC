using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuchDatenbank
{

    [Table("Aktuelle_Buecher")]
    
    public class BuchDTO
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("titel")]
        public string? Titel { get; set; }
        [Column("Autor")]
        public string? Autor { get; set; }

    }

    [Table("Archivierte_Buecher")]
    public class archiviertesBuchDTO
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("titel")]
        public string? Titel { get; set; }
        [Column("Autor")]
        public string? Autor { get; set; }

    }

    //sddskdlksd

}
