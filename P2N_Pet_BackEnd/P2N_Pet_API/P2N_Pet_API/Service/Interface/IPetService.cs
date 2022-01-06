using P2N_Pet_API.Models.Pet;
using P2N_Pet_API.Models.UtilsProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Service.Interface
{
    public interface IPetService
    {
        Task<List<PetListModel>> GetListPet(OSearchPetModel oSearch);
        Task<PetDetailModel> GetDetailPet(ulong petDetailId);
        Task<PaginationModel> GetListPetPagination(OSearchPetModel oSearch);

        Task<PetDetailModel> GetMultiPetDetail(PetDetailConditionModel petDetailCondition);
    }
}
