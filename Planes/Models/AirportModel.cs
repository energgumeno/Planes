using System;

namespace Planes.Models
{

    [Serializable]
    public class AirportModel
    {


        /*
        "iata": "UTK",
        "lon": "169.86667",
        "iso": "MH",
        "status": 1,
        "name": "Utirik Airport",
        "continent": "OC",
        "type": "airport",
        "lat": "11.233333",
        "size": "small"
        */
        public string       iata        { get; set; }
        public decimal?     lon         { get; set; }
        public string       iso         { get; set; }
        public int?         status      { get; set; }
        public string       name        { get; set; }
        public string       continent { get; set; }
        public string       type         { get; set; }
        public decimal?     lat         { get; set; }
        public string       size        { get; set; }

        public AirportModel()
        {



        }


        public override string ToString()
        {
            return $"{iata} {lon} {iso} {status} {name} {continent} {type} {lat} {size}";
        }


    }
}