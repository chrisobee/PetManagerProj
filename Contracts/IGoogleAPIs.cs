using PetManager.Models;
using PetManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IGoogleAPIs
    {
        Task<PetOwner> GetOwnersCoordinates(PetOwner petOwner);
        Task<List<NearbyPlace>> GetNearbyPetStores(PetOwner petOwner);
        Task<List<NearbyPlace>> GetNearbyVets(PetOwner petOwner);
        List<NearbyPlace> PareDownList(List<NearbyPlace> nearbyPlaces);
    }
}
