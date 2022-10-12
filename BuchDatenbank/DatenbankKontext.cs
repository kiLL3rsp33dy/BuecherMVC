using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuchDatenbank
{

    // Stellt Verbindung zur Tabelle in der Datenbank her
    public class DatenbankKontext : DbContext
    {
        private readonly string _connectionString;

        public DatenbankKontext(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Gibt an auf welche Datenbank verbunden werden soll --> Verweis mit connectioonstring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));

        }

        // Erstellt Instanz der Entitäten
        public DbSet<BuchDTO> Aktuelle_Buecher { get; set; } = null!;
        public DbSet<archiviertesBuchDTO> Archivierte_Buecher { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuchDTO>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<archiviertesBuchDTO>()
                .HasKey(e =>e.Id);
        }
    }
}
