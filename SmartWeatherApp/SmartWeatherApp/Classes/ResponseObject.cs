using System;
using Newtonsoft.Json.Linq;

namespace SmartWeatherApp.Classes
{
    public class ResponseObject
    {
        private readonly JObject content; // stores parsed string as JObject
        // Response properties 
        public string CityName { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Description { get; set; }
        public double WindSpeed { get; set; }
        public double Temperature { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double CloudPercentage { get; set; }
             

        public ResponseObject(string response)
        {
            // constructor
            JObject data = JObject.Parse(response); // parse string
            content = data; // set content
            // set properties
            Lat = data["coord"]["lat"].ToString();
            Lon = data["coord"]["lon"].ToString();
            Description = data["weather"][0]["description"].ToString();
            var main = data["main"];
            Temperature = Convert.ToDouble(main["temp"].ToString());
            TemperatureMin = Convert.ToDouble(main["temp_min"].ToString());
            TemperatureMax = Convert.ToDouble(main["temp_max"].ToString());
            Humidity = Convert.ToDouble(main["humidity"].ToString());
            Pressure = Convert.ToDouble(main["pressure"].ToString());
            WindSpeed = Convert.ToDouble(data["wind"]["speed"].ToString());
            CloudPercentage = Convert.ToDouble(data["clouds"]["all"].ToString());
            CityName = data["name"].ToString();
        }

        public JObject GetContent()
        {
            // returns content 
            return content;
        }
    }
}
