using Microsoft.AspNetCore.Mvc;
using BuecherMVC.Models;
using BuecherMVC.DatabaseConfiguration;
using BuchDatenbank;
using Buecher;
using System.Dynamic;

namespace BuecherMVC.Controllers
{
    public class BuchController : Controller
    {

        // Holt die Anmeldedaten für die Datenbank
        private readonly KonfigurationsLeser _konfigurationsLeser;
        public BuchController(KonfigurationsLeser konfigurationsleser)
        {
            this._konfigurationsLeser = konfigurationsleser;
        }

        private string GetConnectionString() // Holt Anmeldedaten für Datenbank
        {
            return _konfigurationsLeser.LiesDatenbankVerbindungZurMariaDB();
        }

        /*
        public BuecherModell vier (BuchOrmRepository repository)
        {
            var model2 = new BuecherModell(repository);
            model2.FaktuelleBuecher();
            model2.FarchivierteBuecher();

            return model2;
        }
        */
        
        public async Task<BuecherModell> Vier (BuchOrmRepository repository)
        {
            var model2 = new BuecherModell(repository);
            model2.FaktuelleBuecher();
            model2.FarchivierteBuecher();

             return model2;
        }
        



        // Wird aufgerufen wenn die Buch-Seite angeklickt wird --> Standard-Weiterleitung auf die Index-Seite von Buecher
        public async Task<IActionResult> Index()
        {
            string connectionString = this.GetConnectionString(); // Holt die Anmeldedaten für die Datenbank
            var mariadb = new DatenbankKontext(connectionString); // Speichert ConnectionString in Format DatenbankKontext zur Kommunikation mit der Datenbank
            var repository = new BuchOrmRepository(mariadb); // Erstellt neue Instanz der Schnittstelle
            var model = Vier(repository);
            
           


            /*
           
            var model = new BuecherModell(repository);

            
            model.FaktuelleBuecher();
            model.FarchivierteBuecher();
            */



            return View(await model); // Gibt eine Ansicht zurück
        }


        // Verschiebt Buch von Aktuell in Archiviert
        [HttpGet]
        public IActionResult VerschiebeAktuelleBuecher(int id)
        {
            string connectionString = this.GetConnectionString(); // Holt die Anmeldedaten für die Datenbank
            var mariadb = new DatenbankKontext(connectionString); // Speichert ConnectionString in Format DatenbankKontext zur Kommunikation mit der Datenbank
            var repository = new BuchOrmRepository(mariadb);
            repository.VerschiebeAktuellesBuch(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult VerschiebeArchivierteBuecher(int id)
        {
            string connectionString = this.GetConnectionString(); // Holt die Anmeldedaten für die Datenbank
            var mariadb = new DatenbankKontext(connectionString); // Speichert ConnectionString in Format DatenbankKontext zur Kommunikation mit der Datenbank
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




    


    
}


