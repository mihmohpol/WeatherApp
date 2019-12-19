using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartWeatherApp.Classes
{
    public class Manager
    {
        private string token; // api token 
        public List<City> Cities { get; set; } // a list of city objects

        public void SetToken(string tokenToSet)
        {
            // simply initializes token
            // implemented as a method because of token's accessability level
            token = tokenToSet;
        }

        private ResponseObject GETRequest(string url)
        {
            // returns Response object
            // Response object is a result of a JSON deserialization
            // and contains all properties needed for a further work
            try
            {
                // first we make an HTTP Web Request with a specific URL
                HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(url);

                // specify request's parameters
                request.Method = "GET";
                request.Accept = "application/json";
                request.UserAgent = "Mozilla/5.0 ....";

                // get response 
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine("HTTP response received");
                // create stream reader to read received response
                StreamReader reader = new StreamReader(response.GetResponseStream());
                Console.WriteLine("Reading content");
                // read whole response
                string data = reader.ReadToEnd();
                Console.WriteLine("Finished reading content");
                // close response object
                response.Close();

                // finally return new Response object
                return new ResponseObject(data);
            }
            catch(Exception e)
            {
                // catch any emerged exceptions and display its message 
                Console.WriteLine(e.Message);
                // returns null if exception appears
                return null;
            }
            
        }

        public ResponseObject GetWeatherByCityName(string city_name)
        {
            // returns Response object returned by GETRequest method
            // city name should be specified
            string query = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&APPID={1}", city_name, token);
            Console.WriteLine(query);
            return GETRequest(query);
        }

        public ResponseObject GetWeatherByCoordinates(int lon, int lat)
        {
            // returns Response object returned by GETRequest method
            // coordinates should be specified
            string query = string.Format("http://api.openweathermap.org/data/2.5/lat={0}&lon={1}&units=metric&APPID={2}", lon, lat, token);
            Console.WriteLine(query);
            return GETRequest(query);
        }

        public void GetCities()
        {
            // initializes Cities list with a values
            // doesnot return anything
            try
            {
                // navigate three folders up universally
                string directory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString();
                // set current directory
                Directory.SetCurrentDirectory(directory);
                // open file containing cities data
                using (StreamReader r = new StreamReader(@"city.list.json"))
                {
                    // read file to a string
                    string json = r.ReadToEnd();
                    // deserialize json to a list of City objects
                    Cities = JsonConvert.DeserializeObject<List<City>>(json);
                }
            }
            catch(Exception e)
            {
                // catch any exception and output message 
                       Console.WriteLine(e.Message);
            }
        }

        public Manager(string auth_token)
        {
            // constructor

            // sets token 
            SetToken(auth_token);
            // initializes a list of cities to avoid null pointer exception
            Cities = new List<City>();
        }
    }
}
