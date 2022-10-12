using Microsoft.AspNetCore.Mvc;
using BuecherMVC.Models;
using BuecherMVC.DatabaseConfiguration;
using BuchDatenbank;
using Buecher;
using System.Dynamic;

//Funktionalität: Das Programm ruft 2 Tabellen (AktuelleBuecher, ArchivierteBuecher) von einer MariaDB-Datenbank ab und stellt diese im Browser dar. Außerdem können Bücher zwischen den 2 Tabellen verschoben werden.

namespace BuecherMVC.Controllers
{
    public class BuchController : Controller
    {
        private readonly KonfigurationsLeser _konfigurationsLeser;
        public BuchController(KonfigurationsLeser konfigurationsleser)
        {
            this._konfigurationsLeser = konfigurationsleser;
        }

        // Holt die Anmeldedaten für die Datenbank
        private string GetConnectionString()
        {
            return _konfigurationsLeser.LiesDatenbankVerbindungZurMariaDB();
        }

        // Stellt die Anmeldedaten der Datenbank für die Schnitttelle bereit
        public DatenbankKontext holeDBKonfiguration()
        {
            return new DatenbankKontext(GetConnectionString());
        }

        // Anzeige der beiden BuecherListen
        public async Task<IActionResult> Index()
        {
            
            var repository = new BuchOrmRepository(holeDBKonfiguration()); // Erstellt neue Instanz der Schnittstelle
            var model = new BuecherModell(repository); // Erstellt ein neues BuecherModell
            
            // BuecherModell wird mit Daten befüllt
            await Task.Run(() => model.FaktuelleBuecher());
            await Task.Run(() => model.FarchivierteBuecher());
        
            return View(model); // Gibt eine Ansicht zurück
        }


        // Verschiebt Buch von Liste Aktuell in Liste Archiviert
        [HttpGet]
        public IActionResult VerschiebeAktuelleBuecher(int id)
        {
            var repository = new BuchOrmRepository(holeDBKonfiguration()); // Erstellt neue Instanz der Schnittstelle
            repository.VerschiebeAktuellesBuch(id);
            return RedirectToAction(nameof(Index)); // Weiterleitung auf Index-Seite
        }


        // Verschiebt Buch von Liste Archiviert in Liste Aktuell
        [HttpGet]
        public IActionResult VerschiebeArchivierteBuecher(int id)
        {
            var repository = new BuchOrmRepository(holeDBKonfiguration()); // Erstellt neue Instanz der Schnittstelle
            repository.VerschiebeArchiviertesBuch(id);
            return RedirectToAction(nameof(Index)); // Weiterleitung auf Index-Seite
        }
    }
}


