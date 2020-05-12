using Newtonsoft.Json.Linq;
using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetManager.Services
{
    public class GoogleAPI : IGoogleAPIs
    {
        public GoogleAPI()
        {

        }
        
        public async Task<PetOwner> GetOwnersCoordinates(PetOwner petOwner)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"www.googleapis.com/geolocation/v1/geolocate?key={API_Key.googleAPIKey}");
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                JObject jobject = JObject.Parse(json);         
                double latitude = (double)jobject["location"]["lat"];
                double longitude = (double)jobject["location"]["lng"];
                petOwner.Lat = latitude;
                petOwner.Lng = longitude;
                
            }

            return petOwner;

        }
    }
}
