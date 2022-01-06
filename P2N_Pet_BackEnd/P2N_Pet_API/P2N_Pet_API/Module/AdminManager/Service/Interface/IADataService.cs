using P2N_Pet_API.Module.AdminManager.Models.AData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Service.Interface
{
    public interface IADataService
    {
        Task<List<AStatusSelectionModel>> GetNormalStatusSelection();
        Task<List<AAgeSelectionModel>> GetNormalAgeSelection();
        Task<List<AColorSelectionModel>> GetNormalColorSelection();
        Task<List<ASizeSelectionModel>> GetNormalSizeSelection();
        Task<List<ASexSelectionModel>> GetNormalSexSelection();
        Task<List<ABreedDefaultSelectionModel>> GetNormalBreedDefaultSelection();
        Task<List<ABreedSelectionModel>> GetNormalBreedSelection();
        Task<List<ASupplierSelectionModel>> GetNormalSupplierSelection();
        Task<List<ABreedSelectionModel>> GetNormalBreedPetDetailSelection(ulong supplierid);
        Task<List<ASupplierSelectionModel>> GetNormalSupplierPetDetailSelection(ulong breedid);
        Task<List<AStatusDetailSelectionModel>> GetNormalStatusDetailSelection();
    }
}
