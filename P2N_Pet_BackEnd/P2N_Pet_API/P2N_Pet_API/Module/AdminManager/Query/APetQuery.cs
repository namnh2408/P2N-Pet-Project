using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Models.APet;
using P2N_Pet_API.Module.AdminManager.Query.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Query
{
    public class APetQuery : IAPetQuery
    {
        private readonly IP2NPetDapper _p2NPetDapper;

        public APetQuery(IP2NPetDapper p2NPetDapper)
        {
            _p2NPetDapper = p2NPetDapper;
        }

        public async Task<List<APetListModel>> QueryGetListPet(AOSearchPet aOSearchPet)
        {
            aOSearchPet.Limit = string.IsNullOrEmpty(aOSearchPet.Limit) ? "10" : aOSearchPet.Limit;
            aOSearchPet.CurrentDate = string.IsNullOrEmpty(aOSearchPet.CurrentDate)
                ? Utils.DateNow().ToString("yyyy-MM-dd HH:mm:ss:fff")
                : aOSearchPet.CurrentDate;
            aOSearchPet.CurrentPage = string.IsNullOrEmpty(aOSearchPet.CurrentPage) ? "0" : aOSearchPet.CurrentPage;
            aOSearchPet.BreedId = string.IsNullOrEmpty(aOSearchPet.BreedId) ? "0" : aOSearchPet.BreedId;
            aOSearchPet.SupplierId = string.IsNullOrEmpty(aOSearchPet.SupplierId) ? "0" : aOSearchPet.SupplierId;
            aOSearchPet.Status = string.IsNullOrEmpty(aOSearchPet.Status) ? "0" : aOSearchPet.Status;

            var condition = @"";

            if (Convert.ToInt32(aOSearchPet.BreedId) > 0)
            {
                condition += @" and p.breedid = @BreedId ";
            }

            if (Convert.ToInt32(aOSearchPet.SupplierId) > 0)
            {
                condition += @" and p.supplierid = @SupplierId ";
            }

            if (Convert.ToInt32(aOSearchPet.Status) > 0)
            {
                condition += @" and p.status = @Status ";
            }

            if (!string.IsNullOrEmpty(aOSearchPet.CurrentDate))
            {
                condition += @" and p.createdate <= cast(@CurrentDate as DateTime) ";
            }

            var query =
                @"select p.Id, ifnull(b.Name, N'') BreedName, ifnull(su.Name, N'') SupplierName, ifnull(p.Content, N'') Content, 
                    ifnull(st.Title, N'') StatusText, ifnull(uc.Name, N'') CreateUserName, p.CreateDate, ifnull(up.Name, N'') UpdateUserName, 
                    p.UpdateDate 
                from pet p  
                    left join breed b on b.id = p.breedid 
                    left join supplier su on su.id = p.supplierid 
	                left join status st on st.id = p.status
                    left join users uc on uc.id = p.createuser
                    left join users up on up.id = p.updateuser
                where p.status != @StatusExcep and b.status = @StatusRoot and su.status = @StatusRoot " + condition + @" 
                order by p.status asc, b.name collate utf8_unicode_ci asc, su.name collate utf8_unicode_ci asc  
                limit " + Convert.ToInt32(aOSearchPet.Limit) * Convert.ToInt32(aOSearchPet.CurrentPage) + @", " + aOSearchPet.Limit + @";";

            return await _p2NPetDapper.QueryAsync<APetListModel>(query, new
            {
                StatusExcep = 190,
                StatusRoot = 10,
                BreedId = aOSearchPet.BreedId,
                SupplierId = aOSearchPet.SupplierId,
                Status = aOSearchPet.Status,
                CurrentDate = aOSearchPet.CurrentDate
            });
        }

        public async Task<int> QueryCountListPet(AOSearchPet aOSearchPet)
        {
            aOSearchPet.Limit = string.IsNullOrEmpty(aOSearchPet.Limit) ? "10" : aOSearchPet.Limit;
            aOSearchPet.CurrentDate = string.IsNullOrEmpty(aOSearchPet.CurrentDate)
                ? Utils.DateNow().ToString("yyyy-MM-dd HH:mm:ss:fff")
                : aOSearchPet.CurrentDate;
            aOSearchPet.CurrentPage = string.IsNullOrEmpty(aOSearchPet.CurrentPage) ? "0" : aOSearchPet.CurrentPage;
            aOSearchPet.BreedId = string.IsNullOrEmpty(aOSearchPet.BreedId) ? "0" : aOSearchPet.BreedId;
            aOSearchPet.SupplierId = string.IsNullOrEmpty(aOSearchPet.SupplierId) ? "0" : aOSearchPet.SupplierId;
            aOSearchPet.Status = string.IsNullOrEmpty(aOSearchPet.Status) ? "0" : aOSearchPet.Status;

            var condition = @"";

            if (Convert.ToInt32(aOSearchPet.BreedId) > 0)
            {
                condition += @" and p.breedid = @BreedId ";
            }

            if (Convert.ToInt32(aOSearchPet.SupplierId) > 0)
            {
                condition += @" and p.supplierid = @SupplierId ";
            }

            if (Convert.ToInt32(aOSearchPet.Status) > 0)
            {
                condition += @" and p.status = @Status ";
            }

            if (!string.IsNullOrEmpty(aOSearchPet.CurrentDate))
            {
                condition += @" and p.createdate <= cast(@CurrentDate as DateTime) ";
            }

            var query =
                @"select count(1) 
                from pet p  
                    left join breed b on b.id = p.breedid 
                    left join supplier su on su.id = p.supplierid 
	                left join status st on st.id = p.status
                    left join users uc on uc.id = p.createuser
                    left join users up on up.id = p.updateuser
                where p.status != @StatusExcep and b.status = @StatusRoot and su.status = @StatusRoot " + condition + @" ;";

            return await _p2NPetDapper.QuerySingleAsync<int>(query, new
            {
                StatusExcep = 190,
                StatusRoot = 10,
                BreedId = aOSearchPet.BreedId,
                SupplierId = aOSearchPet.SupplierId,
                Status = aOSearchPet.Status,
                CurrentDate = aOSearchPet.CurrentDate
            });
        }

        public async Task<APetModel> QueryGetPetDetail(ulong Id)
        {
            var query = @"select p.Id, p.BreedId, p.SupplierId, p.Content, p.Status 
                        from pet p  
                        where p.status != @Status and p.id = @Id;";

            return await _p2NPetDapper.QuerySingleAsync<APetModel>(query, new
            {
                Status = 190,
                Id
            });
        }
    }
}
