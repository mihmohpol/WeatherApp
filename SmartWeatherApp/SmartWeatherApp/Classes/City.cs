using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace SmartWeatherApp.Classes
{
    public class City
    {
        // specifies city object from json array
        // except coordinates

        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public void ShowData()
        {
            Console.WriteLine("ID: {0}, Name: {1}, Country: {2}", ID, Name, Country);
        }
    
    }
}
