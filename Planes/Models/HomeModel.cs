using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planes.Models
{

    [Serializable]
    public class HomeModel
    {

        public int current { get; set; }
        public int rowCount { get; set; }
        public List<AirportModel> rows { get; set; }
        public int total { get; set; }
    }
}