using Microsoft.AspNetCore.Mvc;
using BuecherMVC.Models;
using BuchDatenbank;
using Buecher;
using System.Dynamic;

namespace BuecherMVC.Controllers
{
    public class BuchController : Controller
    {

        private readonly KonfigurationsLeser _konfigurationsLeser;
        public BuchController(KonfigurationsLeser konfigurationsleser)
        {
            this._konfigurationsLeser = konfigurationsleser;
        }
        private string GetConnectionString() // Holt Anmeldedaten für Datenbank
        {
            return _konfigurationsLeser.LiesDatenbankVerbindungZurMariaDB();
        }


        // Wird aufgerufen wenn die Buch-Seite angeklickt wird --> Standard-Weiterleitung auf die Index-Seite von Buecher
        public IActionResult Index()
        {
            // 090 S.12
            string connectionString = this.GetConnectionString(); // Holt die Anmeldedaten für die Datenbank
            var mariadb = new DatenbankKontext(connectionString); // Speichert ConnectionString in Format DatenbankKontext zur Kommunikation mit der Datenbank


            var repository = new BuchOrmRepository(mariadb); // Erstellt neue Instanz der Schnittstelle
            var model1 = new BuecherModell(repository);
            model1.FaktuelleBuecher();
            model1.FarchivierteBuecher();

            //var worker = new ThreadT(model1);
            //worker.StarteThread();
            //

            //ViewBag.Modell = model1.Buecher;          
            //var model1 = repository.HoleAktuelleBuecher();
            //ViewBag.Model1 = model1;
            //ViewBag.Model2 = model2;       

            return View(model1); // Gibt eine Ansicht zurück
        }


        [HttpGet]
        public IActionResult VerschiebeAktuelleBuecher(int id)
        {
            string connectionString = this.GetConnectionString();
            var mariadb = new DatenbankKontext(connectionString);
            var repository = new BuchOrmRepository(mariadb);

            repository.VerschiebeAktuellesBuch(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult VerschiebeArchivierteBuecher(int id)
        {
            string connectionString = this.GetConnectionString();
            var mariadb = new DatenbankKontext(connectionString);
            var repository = new BuchOrmRepository(mariadb);
            repository.VerschiebeArchiviertesBuch(id);

            return RedirectToAction(nameof(Index));
        }






        

        public class ThreadT
        {
            private readonly BuecherModell _repository;
            public ThreadT(BuecherModell repository)
            {
                this._repository = repository;
            }
            public void StarteThread()
            {
                Thread thread1 = new Thread(() => _repository.FaktuelleBuecher());                
                Thread thread2 = new Thread(() => _repository.FarchivierteBuecher());
               
                thread1.Start();
                thread2.Start();

                thread1.Join();
                thread2.Join();

            }                     
        }

        



    }





    // Schnittstelle: 
    public class KonfigurationsLeser : IKonfigurationsLeser
    {
        private readonly IConfiguration _configuration;

        // Speichert Konfiguration. Von anfang an vorhanden
        public KonfigurationsLeser(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // Liest den ConnectionString aus der appsettings.json und gibt ihn zurück
        public string LiesDatenbankVerbindungZurMariaDB()
        {
            return _configuration.GetConnectionString("MariaDB");
        }

    }
}

// Interface: Sammlung abstrakter Methoden
public interface IKonfigurationsLeser
{
    string LiesDatenbankVerbindungZurMariaDB();
}
