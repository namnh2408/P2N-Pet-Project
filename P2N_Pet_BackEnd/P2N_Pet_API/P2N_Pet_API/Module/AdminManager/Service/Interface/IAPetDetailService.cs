using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Models.APetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Service.Interface
{
    public interface IAPetDetailService
    {
        Task<List<APetDetailListModel>> GetListPetDetail(AOSearchPetDetail aOSearchPetDetail);
        Task<PaginationModel> GetListPetDetailPagination(AOSearchPetDetail aOSearchPetDetail);
        Task<APetDetailModel> GetInPetDetail(ulong Id);
        Task<Petdetail> CreatePetDetail(ForceInfo forceInfo, APetDetailCreateModel aPetDetailCreateModel);
        Task<Petdetail> UpdatePetDetail(ForceInfo forceInfo, APetDetailUpdateModel aPetDetailUpdateModel);
        Task<Petdetail> DeletePetDetail(ForceInfo forceInfo, ulong Id);
        Task UpdatePetDetailImage(ForceInfo forceInfo, APetDetailUpdateModel aPetDetailUpdateModel);
        Task DeletePetDetailImage(ForceInfo forceInfo, ulong petImageId);
    }
}
