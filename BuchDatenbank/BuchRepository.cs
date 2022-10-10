using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace BuchDatenbank
{

    // Schnittstelle: Gibt an was gemacht wird aber nicht wie
    public interface IBuchRepository
    {
        List<archiviertesBuchDTO> HoleArchivierteBuecher();
        List<BuchDTO> HoleAktuelleBuecher();
        void VerschiebeAktuellesBuch(int id);
        void VerschiebeArchiviertesBuch(int id);
    }



    public class BuchOrmRepository : IBuchRepository
    {
        private readonly DatenbankKontext _kontext;
        
        public BuchOrmRepository(DatenbankKontext kontext)
        {
            this._kontext = kontext;
        }


        // Aufbau Verbindung und Abfragen der Informationen aus der Datenbank
       
        public List<BuchDTO> HoleAktuelleBuecher()
        {
            return _kontext.Aktuelle_Buecher.ToList();
        }

        public List<archiviertesBuchDTO> HoleArchivierteBuecher()
        {
            return _kontext.Archivierte_Buecher.ToList();
        }


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