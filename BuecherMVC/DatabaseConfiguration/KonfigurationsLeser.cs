namespace BuecherMVC.DatabaseConfiguration
{

    // Interface: Sammlung abstrakter Methoden
    public interface IKonfigurationsLeser
    {
        string LiesDatenbankVerbindungZurMariaDB();
    }



    // Schnittstelle: 
    public class KonfigurationsLeser : IKonfigurationsLeser
    {
        private readonly IConfiguration _configuration;

        // Speicherung von Konfigurationsdaten
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
