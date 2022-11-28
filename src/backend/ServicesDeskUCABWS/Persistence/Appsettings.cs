namespace ServicesDeskUCABWS.Persistence
{
    public class Appsettings
    {

        private readonly IConfiguration _configuration;
        public Appsettings(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string DbConnectionString()
        {
            return _configuration["ConnectionStrings:MyConn"];
        }
    }
}