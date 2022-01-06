using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Models.APet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Action.Interface
{
    public interface IAPetAction
    {
        Task<Pet> Create(ForceInfo forceInfo, APetCreateModel aPetCreateModel);
        Task<Pet> Update(ForceInfo forceInfo, APetUpdateModel aPetUpdateModel);
        Task<Pet> Delete(ForceInfo forceInfo, ulong Id);
    }
}
