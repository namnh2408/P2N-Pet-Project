using P2N_Pet_API.Module.AdminManager.Models.AData;
using P2N_Pet_API.Module.AdminManager.Query.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Query
{
    public class ADataQuery : IADataQuery
    {
        private readonly IP2NPetDapper _p2NPetDapper;

        public ADataQuery(IP2NPetDapper p2NPetDapper)
        {
            _p2NPetDapper = p2NPetDapper;
        }

        public async Task<List<AStatusSelectionModel>> QueryNormalStatusSelection()
        {
            var query =
                @"select Id, ifnull(Title, N'') Title 
                from status
                where id != 190;";

            return await _p2NPetDapper.QueryAsync<AStatusSelectionModel>(query);
        }

        public async Task<List<AAgeSelectionModel>> QueryNormalAgeSelection()
        {
            var query =
                @"select Id, ifnull(Title, N'') Title 
                from age 
                where status = @Status 
                order by orderview desc, title asc;";

            return await _p2NPetDapper.QueryAsync<AAgeSelectionModel>(query, new
            {
                Status = 10
            });
        }

        public async Task<List<AColorSelectionModel>> QueryNormalColorSelection()
        {
            var query =
                @"select Id, ifnull(Title, N'') Title 
                from color 
                where status = @Status 
                order by title collate utf8_unicode_ci asc;";

            return await _p2NPetDapper.QueryAsync<AColorSelectionModel>(query, new
            {
                Status = 10
            });
        }

        public async Task<List<ASizeSelectionModel>> QueryNormalSizeSelection()
        {
            var query =
                @"select Id, ifnull(Title, N'') Title 
                from size  
                where status = @Status 
                order by orderview desc;";

            return await _p2NPetDapper.QueryAsync<ASizeSelectionModel>(query, new
            {
                Status = 10
            });
        }

        public async Task<List<ASexSelectionModel>> QueryNormalSexSelection()
        {
            var query =
                @"select Id, ifnull(Title, N'') Title 
                from sex   
                where status = @Status;";

            return await _p2NPetDapper.QueryAsync<ASexSelectionModel>(query, new
            {
                Status = 10
            });
        }

        public async Task<List<ABreedDefaultSelectionModel>> QueryNormalBreedDefaultSelection()
        {
            var query =
                @"select Id, ifnull(Name, N'') Name 
                from breed   
                where status = @Status and id = breedid 
                order by name collate utf8_unicode_ci asc;";

            return await _p2NPetDapper.QueryAsync<ABreedDefaultSelectionModel>(query, new
            {
                Status = 10
            });
        }

        public async Task<List<ABreedSelectionModel>> QueryNormalBreedSelection()
        {
            var query =
                @"select Id, ifnull(Name, N'') Name  
                from breed    
                where status = @Status and id != breedid 
                order by name collate utf8_unicode_ci asc;";

            return await _p2NPetDapper.QueryAsync<ABreedSelectionModel>(query, new
            {
                Status = 10
            });
        }

        public async Task<List<ASupplierSelectionModel>> QueryNormalSupplierSelection()
        {
            var query =
                @"select Id, ifnull(Name, N'') Name  
                from supplier     
                where status = @Status 
                order by name collate utf8_unicode_ci asc;";

            return await _p2NPetDapper.QueryAsync<ASupplierSelectionModel>(query, new
            {
                Status = 10
            });
        }

        public async Task<List<ABreedSelectionModel>> QueryNormalBreedPetDetailSelection(ulong supplierid)
        {
            var condition = @"";

            if (supplierid != 0) 
            {
                condition = " and p.supplierid = @SupplierId ";
            }

            var query =
                @"select b.Id, b.Name
                from (select 0 Id, N'' Name 
                    union 
                    (select distinct b.Id, ifnull(b.name, N'') Name 
                    from pet p inner join breed b on b.id = p.breedid 
                                inner join supplier su on su.id = p.supplierid 
                    where p.status = @Status and b.status = @Status and su.status = @Status " + condition + @" 
                    )) b 
                order by b.Name collate utf8_unicode_ci asc;";

            return await _p2NPetDapper.QueryAsync<ABreedSelectionModel>(query, new
            {
                Status = 10,
                SupplierId = supplierid
            });
        }

        public async Task<List<ASupplierSelectionModel>> QueryNormalSupplierPetDetailSelection(ulong breedid)
        {
            var condition = @"";

            if (breedid != 0)
            {
                condition = " and p.breedid = @BreedId ";
            }

            var query =
                @"select su.Id, su.Name
                from (select 0 Id, N'' Name 
                    union 
                    (select distinct su.Id, ifnull(su.name, N'') Name 
                    from pet p inner join breed b on b.id = p.breedid 
                               inner join supplier su on su.id = p.supplierid 
                    where p.status = @Status and b.status = @Status and su.status = @Status " + condition + @" 
                    )) su 
                order by su.Name collate utf8_unicode_ci asc;";

            return await _p2NPetDapper.QueryAsync<ASupplierSelectionModel>(query, new
            {
                Status = 10,
                BreedId = breedid
            });
        }

        public async Task<List<AStatusDetailSelectionModel>> QueryNormalStatusDetailSelection()
        {
            var query =
                @"select Id, ifnull(Title, N'') Title 
                from statusdetail      
                where status = @Status;";

            return await _p2NPetDapper.QueryAsync<AStatusDetailSelectionModel>(query, new
            {
                Status = 10
            });
        }
    }
}
