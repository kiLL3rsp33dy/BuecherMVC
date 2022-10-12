using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace BuchDatenbank
{
    public interface IBuchRepository
    {
        List<archiviertesBuchDTO> HoleArchivierteBuecher();
        List<BuchDTO> HoleAktuelleBuecher();
        void VerschiebeAktuellesBuch(int id);
        void VerschiebeArchiviertesBuch(int id);
    }


    // Repository-Schnittstelle mit Abhängigkeit DatenbankKontext
    public class BuchOrmRepository : IBuchRepository
    {
        private readonly DatenbankKontext _kontext;
        
        public BuchOrmRepository(DatenbankKontext kontext)
        {
            this._kontext = kontext;
        }

        // Fragt die Liste AktuelleBuecher und ArchivierteBuecher in Datenbank ab und gibt diese in Listen zurück
       
        public List<BuchDTO> HoleAktuelleBuecher()
        {
            return _kontext.Aktuelle_Buecher.ToList();
        }

        public List<archiviertesBuchDTO> HoleArchivierteBuecher()
        {
            return _kontext.Archivierte_Buecher.ToList();
        }

        // Verschiebt aktuelles Buch in Datenbank in Table ArchivierteBuecher anhand der ID
        public void VerschiebeAktuellesBuch(int id)
        {
            var BuchInDB = _kontext.Aktuelle_Buecher.First(f => f.Id == id);
            var titel = BuchInDB.Titel;
            var autor = BuchInDB.Autor;

            _kontext.Aktuelle_Buecher.Remove(BuchInDB);
            
            var buch = new archiviertesBuchDTO();
            buch.Titel = titel;
            buch.Autor = autor;
            _kontext.Archivierte_Buecher.Add(buch);
            _kontext.SaveChanges();
        }

        // Verschiebt archiviertes Buch in Datenbank in Table AktuelleBuecher anhand der ID
        public void VerschiebeArchiviertesBuch(int id)
        {
            var BuchInDB = _kontext.Archivierte_Buecher.First(f => f.Id == id);
            var titel = BuchInDB.Titel;
            var autor = BuchInDB.Autor;

            _kontext.Archivierte_Buecher.Remove(BuchInDB);

            var buch = new BuchDTO();
            buch.Titel = titel;
            buch.Autor = autor;
            _kontext.Aktuelle_Buecher.Add(buch);
            _kontext.SaveChanges();
        }
    }
}