using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Models.AStatistics;
using P2N_Pet_API.Module.AdminManager.Query.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Query
{
    public class AStatisticsQuery : IAStatisticsQuery
    {
        private readonly IP2NPetDapper _p2NPetDapper;

        public AStatisticsQuery(IP2NPetDapper p2NPetDapper)
        {
            _p2NPetDapper = p2NPetDapper;
        }

        public async Task<AStatisticsModel> QueryStatistics()
        {
            var aStatistics = new AStatisticsModel();

            //TotalCustomer
            var totalCustomer =
                @"select count(1) 
                from users  
                where status != @StatusExcep and role = @RoleCusomter";

            aStatistics.TotalCustomer = await _p2NPetDapper.QuerySingleAsync<int>(totalCustomer, new
            {
                StatusExcep = 190,
                RoleCusomter = 30
            });

            //TotalManager
            var totalManager =
                @"select count(1) 
                from users 
                where status != @StatusExcep and role = @RoleManager";

            aStatistics.TotalManager = await _p2NPetDapper.QuerySingleAsync<int>(totalManager, new
            {
                StatusExcep = 190,
                RoleManager = 20
            });

            //TotalPetInStore
            var totalPetInStore =
                @"select count(distinct(id))
                from pet 
                where status != @StatusExcep;";

            aStatistics.TotalPetInStore = await _p2NPetDapper.QuerySingleAsync<int>(totalPetInStore, new
            {
                StatusExcep = 190
            });

            //TotalPetQuatityInStore
            var totalPetQuatityInStore =
                @"select ifnull(sum(quantity), 0) TotalPetQuatityInStore, ifnull(sum(quantity *price), 0) TotalMoneyPetInStore 
                from petdetail
                where status != @StatusExcep and statusdetailid = @StatusDetail";

            var inStore = await _p2NPetDapper.QuerySingleAsync<AStatisticsInStoreModel>(totalPetQuatityInStore, new
            {
                StatusExcep = 190,
                StatusDetail = 1
            });

            aStatistics.TotalPetQuatityInStore = inStore.TotalPetQuatityInStore;
            aStatistics.TotalMoneyPetInStore = inStore.TotalMoneyPetInStore;

            //TotalPetQuantitySold
            var totalPetQuantitySold =
                @"select sum(c.quantity)
                from `order` o left join cartitem c on c.orderid = o.id
                where o.status != @StatusExcep and c.status != @StatusExcep and o.statusorderid = @StatusOrder";

            aStatistics.TotalPetQuantitySold = await _p2NPetDapper.QuerySingleAsync<int>(totalPetQuantitySold, new
            {
                StatusExcep = 190,
                StatusOrder = 3
            });

            //TotalAmountSold
            var totalAmountSold =
                @"select sum(totalmoney) 
                from `order`
                where status != @StatusExcep and statusorderid = @StatusOrder";

            aStatistics.TotalAmountSold = await _p2NPetDapper.QuerySingleAsync<double>(totalAmountSold, new
            {
                StatusExcep = 190,
                StatusOrder = 3
            });

            //AStatisticsMonthSaleModels

            var aStatisticsMonthSale =
                @"select Month(createdate) MonthSale, count(id) TotalOrder, sum(totalmoney) TotalMoneySale
                from `order`
                where status != @StatusExcep and statusorderid = @StatusOrder and createdate> @DateNow - INTERVAL 6 month
                group by Month(createdate)";

            aStatistics.AStatisticsMonthSaleModels = await _p2NPetDapper.QueryAsync<AStatisticsMonthSaleModel>(aStatisticsMonthSale, new
            {
                StatusExcep = 190,
                StatusOrder = 3,
                DateNow = Utils.DateNow()
            });

            return aStatistics;
        }

        public async Task<List<AStatisticsBreedModel>> QueryGetStatisticsBreed(AOSearchStatisticsBreed aOSearchStatisticsBreed)
        {
            aOSearchStatisticsBreed.Limit = string.IsNullOrEmpty(aOSearchStatisticsBreed.Limit) ? "10" : aOSearchStatisticsBreed.Limit;
            aOSearchStatisticsBreed.CurrentPage = string.IsNullOrEmpty(aOSearchStatisticsBreed.CurrentPage) ? "0" : aOSearchStatisticsBreed.CurrentPage;
            
            var query =
                @"select b.id Id, ifnull(b.name, '') BreedName, sum(pd.quantity) TotalQuantityBreed, sum(pd.quantity * pd.price) TotalMoneyBreed
                from petdetail pd inner join pet p on p.id = pd.petid 
		                inner join breed b on b.id = p.breedid	
                where pd.status != @StatusExcep and p.status != @StatusExcep and b.status!= @StatusExcep  
                group by b.id
                order by b.name collate utf8_unicode_ci asc, pd.status asc, p.status asc, p.status asc 
                limit " + Convert.ToInt32(aOSearchStatisticsBreed.Limit) * Convert.ToInt32(aOSearchStatisticsBreed.CurrentPage) + @", " + aOSearchStatisticsBreed.Limit + @";";

            return await _p2NPetDapper.QueryAsync<AStatisticsBreedModel>(query, new
            {
                StatusExcep = 190
            });
        }

        public async Task<int> QueryCountStatisticsBreed(AOSearchStatisticsBreed aOSearchStatisticsBreed)
        {
            aOSearchStatisticsBreed.Limit = string.IsNullOrEmpty(aOSearchStatisticsBreed.Limit) ? "10" : aOSearchStatisticsBreed.Limit;
            aOSearchStatisticsBreed.CurrentPage = string.IsNullOrEmpty(aOSearchStatisticsBreed.CurrentPage) ? "0" : aOSearchStatisticsBreed.CurrentPage;

            var query =
                @"select count(1)
                from (select count(b.id) 
                    from petdetail pd inner join pet p on p.id = pd.petid 
		                    inner join breed b on b.id = p.breedid	
                    where pd.status != @StatusExcep and p.status != @StatusExcep and b.status!= @StatusExcep  
                    group by b.id) a";

            return await _p2NPetDapper.QuerySingleAsync<int>(query, new
            {
                StatusExcep = 190
            });
        }
    }
}
