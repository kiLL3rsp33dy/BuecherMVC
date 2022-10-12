namespace BuecherMVC.DatabaseConfiguration
{

    public interface IKonfigurationsLeser
    {
        string LiesDatenbankVerbindungZurMariaDB();
    }

    // Ermöglicht Auslesen der Schnittstellendaten
    public class KonfigurationsLeser : IKonfigurationsLeser
    {
        private readonly IConfiguration _configuration;

        public KonfigurationsLeser(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // Liest den ConnectionString der MariaDB aus der appsettings.json
        public string LiesDatenbankVerbindungZurMariaDB()
        {
            return _configuration.GetConnectionString("MariaDB");
        }

    }


}
