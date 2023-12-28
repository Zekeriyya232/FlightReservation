using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebProje2023.Entity;

namespace FlightReservation.Controllers
{
    
    public class flightApiControllerResponse : Controller
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        List<KullaniciDB> kullanici = new List<KullaniciDB>();

        public flightApiControllerResponse()
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<KullaniciDB>> GetAllKullanici()
        {
            kullanici=new List<KullaniciDB>();
            using (var httpclient = new HttpClient(clientHandler))
            {
                using (var response = await httpclient.GetAsync("https://localhost:7170/api/flightApi"))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    kullanici = JsonConvert.DeserializeObject<List<KullaniciDB>>(apiresponse);
                }
            }
            return kullanici;
        }
    }
}
