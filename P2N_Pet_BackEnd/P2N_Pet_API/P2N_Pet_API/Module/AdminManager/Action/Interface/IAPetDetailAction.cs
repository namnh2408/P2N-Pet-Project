using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.CloudMedia;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Models.APetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Action.Interface
{
    public interface IAPetDetailAction
    {
        Task<Petdetail> Create(ForceInfo forceInfo, APetDetailCreateModel aPetDetailCreateModel);
        Task<Petdetail> Update(ForceInfo forceInfo, APetDetailUpdateModel aPetDetailUpdateModel);
        Task<Petdetail> Delete(ForceInfo forceInfo, ulong Id);
        Task UpdatePetDetailImageOld(ForceInfo forceInfo, ulong petDetailId, List<ulong> imageOldIds);
        Task UpdatePetDetailImage(ForceInfo forceInfo, APetDetailUpdateModel aPetDetailUpdateModel, List<CloudMediaModel> CloudMedias, List<ulong> idPetDetailDuplicates);
        Task DeletePetDetailImage(ForceInfo forceInfo, ulong petImageId, List<ulong> idPetImageForDuplicates);
        Task<List<CloudMediaModel>> SaveMediaData(APetDetailUpdateModel aPetDetailUpdateModel);
    }
}
