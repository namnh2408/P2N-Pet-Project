using P2N_Pet_API.Models.Pet;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Query.Interface;
using P2N_Pet_API.Service.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Service
{
    public class PetService : IPetService
    {
        private readonly IPetQuery _petQuery;
        private readonly IPaginationService _paginationService;

        public PetService(IPetQuery petQuery,
            IPaginationService paginationService)
        {
            _petQuery = petQuery;
            _paginationService = paginationService;
        }
        public async Task<PetDetailModel> GetDetailPet(ulong petDetailId)
        {
            var petDetail = await _petQuery.QueryDetailPet(petDetailId);

            if (petDetail != null)
            {
                var petId = petDetail.PetId;

                PetDetailConditionModel petDetailCondition = new PetDetailConditionModel();

                petDetailCondition.PetId = petId;
                petDetailCondition.ColorId = petDetail.ColorId;
                petDetailCondition.AgeId = petDetail.AgeId;
                petDetailCondition.SizeId = petDetail.SizeId;

                var petImages = _petQuery.QueryPetImages(petDetailId);

                var taskSize = _petQuery.QueryListSizeOfPet(petDetailCondition);
                var taskColor = _petQuery.QueryListColorOfPet(petDetailCondition);
                var taskAge = _petQuery.QueryListAgeOfPet(petDetailCondition);
                var taskSex = _petQuery.QueryListSexOfPet(petDetailCondition);

                await Task.WhenAll(petImages, taskSize, taskColor, taskAge, taskSex);

                petDetail.petImages = petImages.Result;

                petDetail.petSizes = taskSize.Result;
                petDetail.petColors = taskColor.Result;
                petDetail.petAges = taskAge.Result;
                petDetail.petSexes = taskSex.Result;
            }

            return petDetail;
        }

        public async Task<List<PetListModel>> GetListPet(OSearchPetModel oSearch)
        {
            return await _petQuery.QueryListPet(oSearch); 
        }

        public async Task<PaginationModel> GetListPetPagination(OSearchPetModel oSearch)
        {
            var count = await _petQuery.QueryCountListPet(oSearch);

            var pagination = await _paginationService.BuildPagination(count, Convert.ToInt32(oSearch.CurrentPage),
                oSearch.CurrentDate, Convert.ToInt32(oSearch.Limit));

            return pagination;
        }

        public async Task<PetDetailModel> GetMultiPetDetail(PetDetailConditionModel petDetailCondition)
        {
            var petDetail = await _petQuery.QueryMultiPetDetail(petDetailCondition);

            if (petDetail != null)
            {
                var petId = petDetail.PetId;

                var petImages = _petQuery.QueryPetImages(petDetail.PetDetailId);

                var taskColor = _petQuery.QueryListColorOfPet(petDetailCondition);
                var taskAge = _petQuery.QueryListAgeOfPet(petDetailCondition);
                var taskSize = _petQuery.QueryListSizeOfPet(petDetailCondition);
                var taskSex = _petQuery.QueryListSexOfPet(petDetailCondition);

                await Task.WhenAll(petImages, taskSize, taskColor, taskAge, taskSex);

                petDetail.petImages = petImages.Result;

                petDetail.petColors = taskColor.Result;
                petDetail.petAges = taskAge.Result;
                petDetail.petSizes = taskSize.Result;
                petDetail.petSexes = taskSex.Result;
            }

            return petDetail;
        }
    }
}
