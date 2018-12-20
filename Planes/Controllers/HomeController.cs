using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

using Planes.Models;
using System.Web.Script.Serialization;

namespace Planes.Controllers
{
    public class HomeController : Controller
    {

        public static string ResourceURL = "https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json";


        public async Task<JsonResult> GetAirports(int current, int rowCount, string searchPhrase, string id)
        {

            List<AirportModel> airportModels = new List<AirportModel>();


            airportModels = System.Web.Helpers.WebCache.Get("airportModels");

            if (airportModels == null)
            {
                try
                {
                    var proxyHost = "http://10.49.1.1";
                    var proxyPort = "8080";

                    var proxy = new WebProxy()
                    {
                        Address = new Uri($"{proxyHost}:{proxyPort}"),
                        UseDefaultCredentials = true,
                    };



                    var httpClientHandler = new HttpClientHandler()
                    {
                        Proxy = proxy,
                    };

                    using (HttpClient httpClient = new HttpClient(handler: httpClientHandler))
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(ResourceURL);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        airportModels = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<AirportModel>>(responseBody).ToList();

                       Response.Headers.Add("Name", "from-feed");
                    }
                    System.Web.Helpers.WebCache.Set("airportModels", airportModels, 5);


                }
                catch (Exception e)
                {


                }
            }

            List<AirportModel> airportModelsaux = airportModels;
            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                airportModelsaux = airportModelsaux.Where(d =>

                contains<string>(d.iata, searchPhrase)||
                contains<decimal?>(d.lon, searchPhrase) ||
                contains<string>(d.iso, searchPhrase) ||
                contains<int?>(d.status, searchPhrase) ||
                contains<string>(d.continent, searchPhrase) ||
                contains<string>(d.type, searchPhrase) ||
                contains<decimal?>(d.lat, searchPhrase) ||
                contains<string>(d.size, searchPhrase) 


          
               ).ToList();


            }
            //   airportModelsaux = airportModelsaux.GetRange(current * rowCount , (current * rowCount + rowCount) ??10);


            var minvalue = ((current - 1) * rowCount)> airportModelsaux.Count()?0: ((current - 1) * rowCount);
            int maxvalue = minvalue + rowCount > airportModelsaux.Count() ? rowCount - (minvalue + rowCount - airportModelsaux.Count()) : rowCount;



            airportModelsaux = airportModelsaux.GetRange(minvalue, maxvalue);

            var model = new HomeModel() { current = current, rows = airportModelsaux.ToList(), rowCount = rowCount, total = airportModels.Count };


            return new JsonResult() { Data = new JavaScriptSerializer().Serialize(model), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


      protected  bool contains<T>(T a, string b)
        {

            return a?.ToString().ToLower().Contains(b.Trim())??false;
        }

        public async Task<ActionResult> Index()
        {


            return View(new HomeModel() { });
        }





    }
}