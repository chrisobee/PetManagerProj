using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetManager.Contracts;
using PetManager.Models;
using PetManager.Services;
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
            HttpResponseMessage response = await client.PostAsync($"https://www.googleapis.com/geolocation/v1/geolocate?key={API_Key.googleAPIKey}", null);
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

        public async Task<NearbyPlace> GetNearbyPetStores(PetOwner petOwner)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://maps.googleapis.com/maps/api/" +
                $"place/nearbysearch/json?location={petOwner.Lat},{petOwner.Lng}" +
                $"&radius=5000&type=pet_store&name&key={API_Key.googleAPIKey}");
            
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<NearbyPlace>(jsonResult);                 
            }
            return null;
        }

        public async Task<NearbyPlace> GetNearbyVets(PetOwner petOwner)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://maps.googleapis.com/maps/api/" +
                $"place/nearbysearch/json?location={petOwner.Lat},{petOwner.Lng}" +
                $"&radius=5000&type=veterinary_care&name&key={API_Key.googleAPIKey}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                var nearbyPlaces = JsonConvert.DeserializeObject<NearbyPlace>(jsonResult);
                return nearbyPlaces;
            }
            return null;
        }

        public NearbyPlace PareDownList(NearbyPlace nearbyPlaces)
        {
            if(nearbyPlaces.results.Length > 10)
            {
                NearbyPlace paredNearbyPlaces = new NearbyPlace();
                paredNearbyPlaces.results = new Result[10];
                for (int i = 0; i < 10; i++)
                {
                    paredNearbyPlaces.results[i] = nearbyPlaces.results[i];
                }
                return paredNearbyPlaces;
            }
            return nearbyPlaces;
        }      

        
    }
}
