﻿using BuchDatenbank;
using Buecher;

namespace BuecherMVC.Models
{
    public class BuecherModell
    {
        private readonly IBuchRepository _repository;


        
        public BuecherModell(IBuchRepository repository)
        {
            this._repository = repository;
        }

        // Erstellen der Buecherobjekte und hinzufügen zur aktuellen BuecherListe
        public void FaktuelleBuecher()
        {
            List<BuchDTO>? buecher = _repository.HoleAktuelleBuecher();

            foreach (var buchDTO in buecher)
            {
                var buch = new Buch()
                {

                    Id = buchDTO.Id,
                    Titel = buchDTO.Titel,
                    Autor = buchDTO.Autor
                };
                this.Buecher.Add(buch);
            }

           
        }

        // Erstellen der Buecherobjekte und hinzufügen zur archivierten BuecherListe
        public void FarchivierteBuecher()
        {
            List<archiviertesBuchDTO>? buecher = _repository.HoleArchivierteBuecher();

            foreach (var buchDTO in buecher)
            {
                var buch = new Buch()
                {

                    Id = buchDTO.Id,
                    Titel = buchDTO.Titel,
                    Autor = buchDTO.Autor
                };
                this.ArchivierteBuecher.Add(buch);
            }
        }

        
        public List<Buch> Buecher { get; set; } = new();
        public List<Buch> ArchivierteBuecher { get; set; } = new();
    }
}    


