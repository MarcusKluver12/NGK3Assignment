using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NGK3Assignment.Models
{
    public class WeatherStation
    {
        [Key]
        public string PlaceId { get; set; }
        public DateTime Date { get; set; }
        public decimal Celcius { get; set; }
        public double Humidity { get; set; }
        public double Airpressure { get; set; }

        /*
         * place: string name
         *      : double latitude (y-koordinat)
         *      : double longitude (x-koordinat)
         */
    }
}
