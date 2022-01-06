using P2N_Pet_API.Models.Category;
using P2N_Pet_API.Query.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Query
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly IP2NPetDapper _p2NPetDapper;

        public CategoryQuery(IP2NPetDapper p2NPetDapper)
        {
            _p2NPetDapper = p2NPetDapper;
        }

        public async Task<List<BreedListModel>> QueryListBreedChild(ulong breedid)
        {
            var query =
                @"select id, name BreedName
                from breed
                where id != breedid and status = @Status and breedid = @BreedId";

            return await _p2NPetDapper.QueryAsync<BreedListModel>(query, new
            {
                Status = 10,
                BreedId = breedid
            });
        }

        public async Task<List<BreedListModel>> QueryListBreedParent()
        {
            var query =
                @"select Id, name BreedName
                from breed
                where id = breedid and status = 10";

            return await _p2NPetDapper.QueryAsync<BreedListModel>(query);
        }

        public async Task<List<SupplierListModel>> QueryListSupplierChild()
        {
            var query =
                @"select Id, name SupplierName
                from supplier
                where status = @Status ";

            return await _p2NPetDapper.QueryAsync<SupplierListModel>(query, new
            {
                Status = 10
            });
        }
    }
}
