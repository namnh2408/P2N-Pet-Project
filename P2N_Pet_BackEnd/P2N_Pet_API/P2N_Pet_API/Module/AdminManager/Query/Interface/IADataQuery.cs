using P2N_Pet_API.Module.AdminManager.Models.AData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Query.Interface
{
    public interface IADataQuery
    {
        Task<List<AStatusSelectionModel>> QueryNormalStatusSelection();
        Task<List<AAgeSelectionModel>> QueryNormalAgeSelection();
        Task<List<AColorSelectionModel>> QueryNormalColorSelection();
        Task<List<ASizeSelectionModel>> QueryNormalSizeSelection();
        Task<List<ASexSelectionModel>> QueryNormalSexSelection();
        Task<List<ABreedDefaultSelectionModel>> QueryNormalBreedDefaultSelection();
        Task<List<ABreedSelectionModel>> QueryNormalBreedSelection();
        Task<List<ASupplierSelectionModel>> QueryNormalSupplierSelection();
        Task<List<ABreedSelectionModel>> QueryNormalBreedPetDetailSelection(ulong supplierid);
        Task<List<ASupplierSelectionModel>> QueryNormalSupplierPetDetailSelection(ulong breedid);
        Task<List<AStatusDetailSelectionModel>> QueryNormalStatusDetailSelection();
    }
}
