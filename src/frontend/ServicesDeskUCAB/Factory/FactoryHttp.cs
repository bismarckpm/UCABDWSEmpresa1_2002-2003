using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCAB.Factory
{
    public class FactoryHttp
    {
        public static HttpClient CreateClient()
        {
            return new HttpClient();
        }
    }    
}