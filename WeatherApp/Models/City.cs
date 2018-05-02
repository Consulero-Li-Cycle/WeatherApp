using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public class City:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }
}