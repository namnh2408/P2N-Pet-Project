using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Models.APetDetail;
using P2N_Pet_API.Module.AdminManager.Query.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Query
{
    public class APetDetailQuery : IAPetDetailQuery
    {
        private readonly IP2NPetDapper _p2NPetDapper;

        public APetDetailQuery(IP2NPetDapper p2NPetDapper)
        {
            _p2NPetDapper = p2NPetDapper;
        }

        public async Task<List<APetDetailListModel>> QueryGetListPetDetail(AOSearchPetDetail aOSearchPetDetail)
        {
            aOSearchPetDetail.Limit = string.IsNullOrEmpty(aOSearchPetDetail.Limit) ? "10" : aOSearchPetDetail.Limit;
            aOSearchPetDetail.CurrentDate = string.IsNullOrEmpty(aOSearchPetDetail.CurrentDate)
                ? Utils.DateNow().ToString("yyyy-MM-dd HH:mm:ss:fff")
                : aOSearchPetDetail.CurrentDate;
            aOSearchPetDetail.CurrentPage = string.IsNullOrEmpty(aOSearchPetDetail.CurrentPage) ? "0" : aOSearchPetDetail.CurrentPage;
            aOSearchPetDetail.BreedId = string.IsNullOrEmpty(aOSearchPetDetail.BreedId) ? "0" : aOSearchPetDetail.BreedId;
            aOSearchPetDetail.SupplierId = string.IsNullOrEmpty(aOSearchPetDetail.SupplierId) ? "0" : aOSearchPetDetail.SupplierId;
            aOSearchPetDetail.ColorId = string.IsNullOrEmpty(aOSearchPetDetail.ColorId) ? "0" : aOSearchPetDetail.ColorId;
            aOSearchPetDetail.SizeId = string.IsNullOrEmpty(aOSearchPetDetail.SizeId) ? "0" : aOSearchPetDetail.SizeId;
            aOSearchPetDetail.AgeId = string.IsNullOrEmpty(aOSearchPetDetail.AgeId) ? "0" : aOSearchPetDetail.AgeId;
            aOSearchPetDetail.SexId = string.IsNullOrEmpty(aOSearchPetDetail.SexId) ? "0" : aOSearchPetDetail.SexId;
            aOSearchPetDetail.StatusDetailId = string.IsNullOrEmpty(aOSearchPetDetail.StatusDetailId) ? "0" : aOSearchPetDetail.StatusDetailId;
            aOSearchPetDetail.Status = string.IsNullOrEmpty(aOSearchPetDetail.Status) ? "0" : aOSearchPetDetail.Status;

            var condition = @"";

            if (Convert.ToInt32(aOSearchPetDetail.BreedId) > 0)
            {
                condition += @" and br.id = @BreedId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.SupplierId) > 0)
            {
                condition += @" and su.id = @SupplierId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.ColorId) > 0)
            {
                condition += @" and pd.colorid = @ColorId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.SizeId) > 0)
            {
                condition += @" and pd.sizeid = @SizeId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.AgeId) > 0)
            {
                condition += @" and pd.ageid = @AgeId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.SexId) > 0)
            {
                condition += @" and pd.sexid = @SexId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.StatusDetailId) > 0)
            {
                condition += @" and pd.statusdetailid = @StatusDetailId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.Status) > 0)
            {
                condition += @" and pd.status = @Status ";
            }

            if (!string.IsNullOrEmpty(aOSearchPetDetail.CurrentDate))
            {
                condition += @" and pd.createdate <= cast(@CurrentDate as DateTime) ";
            }

            var query =
                @"select pd.Id, ifnull(br.Name, N'') PetName, ifnull(su.Name, N'') SupplierName, ifnull(cl.Title, N'') ColorTitle, ifnull(si.Title, N'') SizeTitle, 
                    ifnull(ag.Title, N'') AgeTitle, ifnull(se.Title, N'') SexTitle, ifnull(sd.Title, N'') StatusDetailTitle, 
                    pd.Price, pd.Discount, pd.Quantity, ifnull(st.Title, N'') StatusText, ifnull(uc.Name, N'') CreateUserName, 
                    pd.CreateDate, ifnull(up.Name, N'') UpdateUserName, pd.UpdateDate 
                from petdetail pd  
                    left join pet p on p.id = pd.petid 
                    left join breed br on br.id = p.breedid 
                    left join color cl on cl.id = pd.colorid 
                    left join size si on si.id = pd.sizeid 
                    left join age ag on ag.id = pd.ageid 
                    left join sex se on se.id = pd.sexid 
                    left join statusdetail sd on sd.id = pd.statusdetailid 
	                left join status st on st.id = pd.status
                    left join users uc on uc.id = pd.createuser
                    left join users up on up.id = pd.updateuser
                    left join supplier su on su.id = p.supplierid 
                where pd.status != @StatusExcep and p.status = @StatusRoot and br.status = @StatusRoot and cl.status= @StatusRoot and si.status= @StatusRoot and
                      ag.status = @StatusRoot and se.status = @StatusRoot and sd.status = @StatusRoot and uc.status = @StatusRoot and up.status = @StatusRoot and 
                      su.status = @StatusRoot " + condition + @" 
                order by pd.status asc, pd.statusdetailid asc, br.name collate utf8_unicode_ci asc 
                limit " + Convert.ToInt32(aOSearchPetDetail.Limit) * Convert.ToInt32(aOSearchPetDetail.CurrentPage) + @", " + aOSearchPetDetail.Limit + @";";

            return await _p2NPetDapper.QueryAsync<APetDetailListModel>(query, new
            {
                StatusExcep = 190,
                StatusRoot = 10,
                BreedId = aOSearchPetDetail.BreedId,
                SupplierId = aOSearchPetDetail.SupplierId,
                ColorId = aOSearchPetDetail.ColorId,
                SizeId = aOSearchPetDetail.SizeId,
                AgeId = aOSearchPetDetail.AgeId,
                SexId = aOSearchPetDetail.SexId,
                StatusDetailId = aOSearchPetDetail.StatusDetailId,
                Status = aOSearchPetDetail.Status,
                CurrentDate = aOSearchPetDetail.CurrentDate
            });
        }

        public async Task<int> QueryCountListPetDetail(AOSearchPetDetail aOSearchPetDetail)
        {
            aOSearchPetDetail.Limit = string.IsNullOrEmpty(aOSearchPetDetail.Limit) ? "10" : aOSearchPetDetail.Limit;
            aOSearchPetDetail.CurrentDate = string.IsNullOrEmpty(aOSearchPetDetail.CurrentDate)
                ? Utils.DateNow().ToString("yyyy-MM-dd HH:mm:ss:fff")
                : aOSearchPetDetail.CurrentDate;
            aOSearchPetDetail.CurrentPage = string.IsNullOrEmpty(aOSearchPetDetail.CurrentPage) ? "0" : aOSearchPetDetail.CurrentPage;
            aOSearchPetDetail.BreedId = string.IsNullOrEmpty(aOSearchPetDetail.BreedId) ? "0" : aOSearchPetDetail.BreedId;
            aOSearchPetDetail.SupplierId = string.IsNullOrEmpty(aOSearchPetDetail.SupplierId) ? "0" : aOSearchPetDetail.SupplierId;
            aOSearchPetDetail.ColorId = string.IsNullOrEmpty(aOSearchPetDetail.ColorId) ? "0" : aOSearchPetDetail.ColorId;
            aOSearchPetDetail.SizeId = string.IsNullOrEmpty(aOSearchPetDetail.SizeId) ? "0" : aOSearchPetDetail.SizeId;
            aOSearchPetDetail.AgeId = string.IsNullOrEmpty(aOSearchPetDetail.AgeId) ? "0" : aOSearchPetDetail.AgeId;
            aOSearchPetDetail.SexId = string.IsNullOrEmpty(aOSearchPetDetail.SexId) ? "0" : aOSearchPetDetail.SexId;
            aOSearchPetDetail.StatusDetailId = string.IsNullOrEmpty(aOSearchPetDetail.StatusDetailId) ? "0" : aOSearchPetDetail.StatusDetailId;
            aOSearchPetDetail.Status = string.IsNullOrEmpty(aOSearchPetDetail.Status) ? "0" : aOSearchPetDetail.Status;

            var condition = @"";

            if (Convert.ToInt32(aOSearchPetDetail.BreedId) > 0)
            {
                condition += @" and br.id = @BreedId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.SupplierId) > 0)
            {
                condition += @" and su.id = @SupplierId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.ColorId) > 0)
            {
                condition += @" and pd.colorid = @ColorId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.SizeId) > 0)
            {
                condition += @" and pd.sizeid = @SizeId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.AgeId) > 0)
            {
                condition += @" and pd.ageid = @AgeId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.SexId) > 0)
            {
                condition += @" and pd.sexid = @SexId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.StatusDetailId) > 0)
            {
                condition += @" and pd.statusdetailid = @StatusDetailId ";
            }

            if (Convert.ToInt32(aOSearchPetDetail.Status) > 0)
            {
                condition += @" and pd.status = @Status ";
            }

            if (!string.IsNullOrEmpty(aOSearchPetDetail.CurrentDate))
            {
                condition += @" and pd.createdate <= cast(@CurrentDate as DateTime) ";
            }

            var query =
                @"select count(1) 
                from petdetail pd  
                    left join pet p on p.id = pd.petid 
                    left join breed br on br.id = p.breedid 
                    left join color cl on cl.id = pd.colorid 
                    left join size si on si.id = pd.sizeid 
                    left join age ag on ag.id = pd.ageid 
                    left join sex se on se.id = pd.sexid 
                    left join statusdetail sd on sd.id = pd.statusdetailid 
	                left join status st on st.id = pd.status
                    left join users uc on uc.id = pd.createuser
                    left join users up on up.id = pd.updateuser
                    left join supplier su on su.id = p.supplierid 
                where pd.status != @StatusExcep and p.status = @StatusRoot and br.status = @StatusRoot and cl.status= @StatusRoot and si.status= @StatusRoot and
                      ag.status = @StatusRoot and se.status = @StatusRoot and sd.status = @StatusRoot and uc.status = @StatusRoot and up.status = @StatusRoot and 
                      su.status = @StatusRoot " + condition + @" ;";

            return await _p2NPetDapper.QuerySingleAsync<int>(query, new
            {
                StatusExcep = 190,
                StatusRoot = 10,
                BreedId = aOSearchPetDetail.BreedId,
                SupplierId = aOSearchPetDetail.SupplierId,
                ColorId = aOSearchPetDetail.ColorId,
                SizeId = aOSearchPetDetail.SizeId,
                AgeId = aOSearchPetDetail.AgeId,
                SexId = aOSearchPetDetail.SexId,
                StatusDetailId = aOSearchPetDetail.StatusDetailId,
                Status = aOSearchPetDetail.Status,
                CurrentDate = aOSearchPetDetail.CurrentDate
            });
        }

        public async Task<APetDetailModel> QueryGetInPetDetail(ulong Id)
        {
            var query = 
                @"select p.Id, pe.BreedId, pe.SupplierId, p.ColorId, p.SizeId, p.AgeId, p.SexId, p.StatusDetailId, p.Price, p.Discount, p.Quantity, p.Status  
                from petdetail p  
                    left join pet pe on pe.id = p.petid 
                where p.status != @StatusExcep and pe.status = @Status and p.id = @Id;";

            var petDetail = await _p2NPetDapper.QuerySingleAsync<APetDetailModel>(query, new
            {
                StatusExcep = 190,
                Status = 10,
                Id
            });

            if(petDetail != null)
            {
                petDetail.aPetPetImageForModels = await QueryGetPetPetImageFor(petDetail.Id);
            }

            return petDetail;
        }

        public async Task<List<APetPetImageForModel>> QueryGetPetPetImageFor(ulong petDetailId)
        {
            var query =
                @"select pif.id Id, pif.petimageid PetImageId, 
                case 
					when ifnull(pim.image, N'') != '' 
					then CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/PetDetail/PetDetail_Image/', pim.image)
					else ifnull(pim.image, '') 
                end Image 
                from petimagefor pif inner join petimage pim on pim.id = pif.petimageid 
                where pif.status = @Status and pim.status = @Status and pif.petdetailid = @PetDetailId;";

            return await _p2NPetDapper.QueryAsync<APetPetImageForModel>(query, new
            {
                Status = 10,
                PetDetailId = petDetailId
            });
        }

        public async Task<List<ulong>> QueryListImageOldCreate(APetDetailUpdateModel aPetDetailUpdateModel)
        {
            var query =
                @"select distinct pif.petimageid
                from petimagefor pif inner join 
	                (select distinct pd.id
	                from petdetail pd inner join 
		                petimagefor pif on pif.petdetailid = pd.id 
	                where pd.status = @Status and pif.status = @Status and pd.id != @PetDetailId and 
		                pd.petid = @PetId and pd.colorid = @ColorId and pd.ageid = @AgeId) old 
	                on old.id = pif.petdetailid;";

            return await _p2NPetDapper.QueryAsync<ulong>(query, new
            {
                Status = 10,
                PetDetailId = aPetDetailUpdateModel.Id,
                PetId = aPetDetailUpdateModel.PetId,
                ColorId = aPetDetailUpdateModel.ColorId,
                AgeId = aPetDetailUpdateModel.AgeId
            });
        }

        public async Task<List<ulong>> QueryListPetDetailDuplicateImage(ulong petId, ulong colorId, ulong ageId)
        {
            var query = 
                @"select distinct p.id
                from petdetail p
                where p.status = @Status and p.petid = @PetId and p.colorid = @ColorId 
                    and p.ageid = @AgeId;";

            return await _p2NPetDapper.QueryAsync<ulong>(query, new
            {
                Status = 10,
                PetId = petId,
                ColorId = colorId,
                AgeId = ageId
            });
        }

        public async Task<List<ulong>> QueryListPetImageForDuplicateImage(ulong petImageId)
        {
            var query = 
                @"select p.id 
                from petimagefor p
                where p.status = @Status and p.petimageid = @PetImageId;";

            return await _p2NPetDapper.QueryAsync<ulong>(query, new
            {
                Status = 10,
                PetImageId = petImageId
            });
        }

        public async Task<ulong> GetPetId(ulong breedId, ulong supplierId)
        {
            var query =
                @"select id 
                from pet 
                where status = @Status and breedid = @BreedId and supplierid = @SupplierId;";

            return await _p2NPetDapper.QuerySingleAsync<ulong>(query, new
            {
                Status = 10,
                BreedId = breedId,
                SupplierId = supplierId
            });
        }
    }
}
