using P2N_Pet_API.Module.AdminManager.Models.AData;
using P2N_Pet_API.Module.AdminManager.Query.Interface;
using P2N_Pet_API.Module.AdminManager.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Service
{
    public class ADataService : IADataService
    {
        private readonly IADataQuery _aDataQuery;

        public ADataService(IADataQuery aDataQuery)
        {
            _aDataQuery = aDataQuery;
        }

        public async Task<List<AStatusSelectionModel>> GetNormalStatusSelection()
        {
            return await _aDataQuery.QueryNormalStatusSelection();
        }

        public async Task<List<AAgeSelectionModel>> GetNormalAgeSelection()
        {
            return await _aDataQuery.QueryNormalAgeSelection();
        }

        public async Task<List<AColorSelectionModel>> GetNormalColorSelection()
        {
            return await _aDataQuery.QueryNormalColorSelection();
        }

        public async Task<List<ASizeSelectionModel>> GetNormalSizeSelection()
        {
            return await _aDataQuery.QueryNormalSizeSelection();
        }
        public async Task<List<ASexSelectionModel>> GetNormalSexSelection()
        {
            return await _aDataQuery.QueryNormalSexSelection();
        }

        public async Task<List<ABreedDefaultSelectionModel>> GetNormalBreedDefaultSelection()
        {
            return await _aDataQuery.QueryNormalBreedDefaultSelection();
        }

        public async Task<List<ABreedSelectionModel>> GetNormalBreedSelection()
        {
            return await _aDataQuery.QueryNormalBreedSelection();
        }

        public async Task<List<ASupplierSelectionModel>> GetNormalSupplierSelection()
        {
            return await _aDataQuery.QueryNormalSupplierSelection();
        }

        public async Task<List<ABreedSelectionModel>> GetNormalBreedPetDetailSelection(ulong supplierid)
        {
            return await _aDataQuery.QueryNormalBreedPetDetailSelection(supplierid);
        }

        public async Task<List<ASupplierSelectionModel>> GetNormalSupplierPetDetailSelection(ulong breedid)
        {
            return await _aDataQuery.QueryNormalSupplierPetDetailSelection(breedid);
        }

        public async Task<List<AStatusDetailSelectionModel>> GetNormalStatusDetailSelection()
        {
            return await _aDataQuery.QueryNormalStatusDetailSelection();
        }
    }
}
