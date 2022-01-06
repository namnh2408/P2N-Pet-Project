using P2N_Pet_API.Module.AdminManager.Models.APet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Query.Interface
{
    public interface IAPetQuery
    {
        Task<List<APetListModel>> QueryGetListPet(AOSearchPet aOSearchPet);
        Task<int> QueryCountListPet(AOSearchPet aOSearchPet);
        Task<APetModel> QueryGetPetDetail(ulong Id);
    }
}
