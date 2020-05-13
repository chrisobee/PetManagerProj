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
        Task<NearbyPlace> GetNearbyPetStores(PetOwner petOwner);
        Task<NearbyPlace> GetNearbyVets(PetOwner petOwner);
        NearbyPlace PareDownList(NearbyPlace nearbyPlaces);
    }
}
