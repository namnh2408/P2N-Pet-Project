using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Models.APet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Service.Interface
{
    public interface IAPetService
    {
        Task<List<APetListModel>> GetListPet(AOSearchPet aOSearchPet);
        Task<PaginationModel> GetListPetPagination(AOSearchPet aOSearchPet);
        Task<APetModel> GetPetDetail(ulong Id);
        Task<Pet> CreatePet(ForceInfo forceInfo, APetCreateModel aPetCreateModel);
        Task<Pet> UpdatePet(ForceInfo forceInfo, APetUpdateModel aPetUpdateModel);
        Task<Pet> DeletePet(ForceInfo forceInfo, ulong Id);
    }
}
