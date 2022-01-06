using P2N_Pet_API.Models.Pet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Query.Interface
{
    public interface IPetQuery
    {
        Task<List<PetListModel>> QueryListPet(OSearchPetModel oSearch);
        Task<PetDetailModel> QueryDetailPet(ulong petDetailId);
        Task<List<PetColorModel>> QueryListColorOfPet(PetDetailConditionModel petDetailCondition);
        Task<List<PetSizeModel>> QueryListSizeOfPet(PetDetailConditionModel petDetailCondition);
        Task<List<PetAgeModel>> QueryListAgeOfPet(PetDetailConditionModel petDetailCondition);
        Task<List<PetSexModel>> QueryListSexOfPet(PetDetailConditionModel petDetailCondition);

        Task<int> QueryCountListPet(OSearchPetModel oSearch);

        Task<PetDetailModel> QueryMultiPetDetail(PetDetailConditionModel petDetailCondition);
        Task<List<string>> QueryPetImages(ulong petDetailId);

    }
}
