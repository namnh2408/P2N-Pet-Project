using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Action.Interface;
using P2N_Pet_API.Module.AdminManager.Models.APet;
using P2N_Pet_API.Module.AdminManager.Query.Interface;
using P2N_Pet_API.Module.AdminManager.Service.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Service
{
    public class APetService : IAPetService
    {
        private readonly IAPetQuery _aPetQuery;
        private readonly IAPetAction _aPetAction;
        private readonly IPaginationService _paginationService;

        public APetService(IAPetQuery aPetQuery,
            IAPetAction aPetAction,
            IPaginationService paginationService)
        {
            _aPetQuery = aPetQuery;
            _aPetAction = aPetAction;
            _paginationService = paginationService;
        }

        public async Task<List<APetListModel>> GetListPet(AOSearchPet aOSearchPet)
        {
            return await _aPetQuery.QueryGetListPet(aOSearchPet);
        }

        public async Task<PaginationModel> GetListPetPagination(AOSearchPet aOSearchPet)
        {
            var count = await _aPetQuery.QueryCountListPet(aOSearchPet);

            var pagination = await _paginationService.BuildPagination(count, Convert.ToInt32(aOSearchPet.CurrentPage),
                aOSearchPet.CurrentDate, Convert.ToInt32(aOSearchPet.Limit));

            return pagination;
        }

        public async Task<APetModel> GetPetDetail(ulong Id)
        {
            return await _aPetQuery.QueryGetPetDetail(Id);
        }

        public async Task<Pet> CreatePet(ForceInfo forceInfo, APetCreateModel aPetCreateModel)
        {
            return await _aPetAction.Create(forceInfo, aPetCreateModel);
        }

        public async Task<Pet> UpdatePet(ForceInfo forceInfo, APetUpdateModel aPetUpdateModel)
        {
            return await _aPetAction.Update(forceInfo, aPetUpdateModel);
        }

        public async Task<Pet> DeletePet(ForceInfo forceInfo, ulong Id)
        {
            return await _aPetAction.Delete(forceInfo, Id);
        }
    }
}
