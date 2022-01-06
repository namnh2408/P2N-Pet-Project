using P2N_Pet_API.Module.AdminManager.Models.APetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Query.Interface
{
    public interface IAPetDetailQuery
    {
        Task<List<APetDetailListModel>> QueryGetListPetDetail(AOSearchPetDetail aOSearchPetDetail);
        Task<int> QueryCountListPetDetail(AOSearchPetDetail aOSearchPetDetail);
        Task<APetDetailModel> QueryGetInPetDetail(ulong Id);
        Task<List<ulong>> QueryListImageOldCreate(APetDetailUpdateModel aPetDetailUpdateModel);
        Task<List<ulong>> QueryListPetDetailDuplicateImage(ulong petId, ulong colorId, ulong ageId);
        Task<List<ulong>> QueryListPetImageForDuplicateImage(ulong petImageId);
        Task<ulong> GetPetId(ulong breedId, ulong supplierId);
    }
}
