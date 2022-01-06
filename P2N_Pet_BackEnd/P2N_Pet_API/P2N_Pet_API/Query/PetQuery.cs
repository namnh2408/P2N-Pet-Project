using P2N_Pet_API.Models.Pet;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Query.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Query
{
    public class PetQuery : IPetQuery
    {
        private readonly IP2NPetDapper _p2NPetDapper;

        public PetQuery(IP2NPetDapper p2NPetDapper)
        {
            _p2NPetDapper = p2NPetDapper;
        }

        public async Task<PetDetailModel> QueryDetailPet(ulong petDetailId)
        {
			var query =
				@"select detail.id PetDetailId, concat(pet.breedname, ' - ', col.title) PetTitle, 
					pet.breedid BreedId, pet.breedname BreedName, 
					pet.supplierid SupplierId, pet.suppliername SupplierName,
					detail.sizeid, size.title sizetitle,
					detail.ageid AgeId, age.title AgeTitle, detail.price Price, detail.discount Discount, 
					(detail.price * (1 - detail.discount / 100)) PriceDiscount,
					case 
					when ifnull(pimage.image, N'') != '' 
					then CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/PetDetail/PetDetail_Image/', pimage.image)
					else ifnull(pimage.image, '') 
					end PetImage, pet.Id PetId, pet.content Content, col.id ColorId, col.title ColorName,
					detail.sexid SexId, x.title SexTitle, detail.quantity Quantity
				from petdetail detail
					left join (
							select pdim.petdetailid, ifnull(pim.image, '') image
							from (
								select pif.petdetailid, min(pim.id) imageid
								from petimage pim 
									inner join petimagefor pif on pif.petimageid = pim.id
								where pim.status = 10 and pif.status = 10
								group by pif.petdetailid
								) pdim 
								inner join petimage pim on pim.id = pdim.imageid
							order by pdim.petdetailid asc
						) pimage
						on pimage.petdetailid = detail.id
					inner join (
						select p.id, br.id breedid, br.name breedname,sup.id supplierid, sup.name suppliername, p.content
						from pet p
							left join petdetail detail on detail.petid = p.id
							left join breed br on br.id = p.breedid
							left join supplier sup on sup.id = p.supplierid
						where p.status = 10 and br.status = 10 and sup.status = 10
						group by p.id
						order by p.id asc
					) pet on pet.id = detail.petid
					left join color col on col.id = detail.colorid
					left join size on size.id = detail.sizeid
					left join age on age.id = detail.ageid
					left join sex x on x.id = detail.sexid
				where detail.status = 10 and detail.statusdetailid = 1 
					and col.status = 10 and size.status = 10 and age.status = 10
					and detail.id = @PetId";

			return await _p2NPetDapper.QuerySingleAsync<PetDetailModel>(query, new
			{
				PetId = petDetailId
			});

		}

        public async Task<List<PetListModel>> QueryListPet(OSearchPetModel oSearch)
        {
			oSearch.CurrentPage = oSearch.CurrentPage < 0 ? 0 : oSearch.CurrentPage;
			oSearch.CurrentDate = string.IsNullOrEmpty(oSearch.CurrentDate)
				? Utils.DateNow().ToString("yyyy-MM-dd HH:mm:ss:fff")
				: oSearch.CurrentDate;

			oSearch.Limit = oSearch.Limit == 0 ? 25 : oSearch.Limit;

			var condition = @"";

			var limit = " limit @offset, @limit ";

			if(oSearch.BreedId > 0)
            {
				condition += @" and pet.breedid = @BreedId";

				limit = " limit @offset, @limit ";
			}

			if (oSearch.BreedIdRoot > 0)
			{
				condition += @" and pet.breedidroot = @BreedIdRoot";

				limit = " limit @offset, @limit ";
			}

			if (oSearch.SupplierId > 0)
            {
				condition += @" and pet. supplierid = @SupplierId";

				limit = " limit @offset, @limit ";
			}

            if (!string.IsNullOrEmpty(oSearch.FindString))
            {
				condition += @" and (
									pet.breedname like @FindString
									or
									col.title like @FindString
								)";

				limit = " limit @offset, @limit ";
			}

			if(oSearch.TopPet > 0)
            {
				condition = @" and detail.discount > @Discount
								and datediff( @DateNow, detail.createdate) < 30";

				limit = @" limit @TopPet";
            }

			var query =
				@"select detail.id PetDetailId, concat(pet.breedname, ' - ', col.title) PetTitle, pet.breedid BreedId, pet.breedname BreedName, pet.supplierid SupplierId, 
					detail.price Price, detail.discount Discount, (detail.price * (1 - detail.discount / 100)) PriceDiscount,
					case 
					when ifnull(pimage.image, N'') != '' 
					then CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/PetDetail/PetDetail_Image/', pimage.image)
					else ifnull(pimage.image, '') 
					end PetImage, pet.id PetId, pet.breedidroot BreedIdRoot, pet.breedrootname BreedRootName, 
					pet.SupplierName, detail.quantity PetQuantity
				from petdetail detail
				left join (
						select pdim.petdetailid, ifnull(pim.image, '') image
						from (
							select pif.petdetailid, min(pim.id) imageid
							from petimage pim 
								inner join petimagefor pif on pif.petimageid = pim.id
							where pim.status = 10 and pif.status = 10
							group by pif.petdetailid
							) pdim 
							inner join petimage pim on pim.id = pdim.imageid
						order by pdim.petdetailid asc
					) pimage
					on pimage.petdetailid = detail.id
				inner join (
					select p.id, br.id breedid, br.name breedname, br.breedid breedidroot, root.name breedrootname,
						sup.id supplierid, sup.name suppliername, p.content
					from pet p
						left join petdetail detail on detail.petid = p.id
						left join breed br on br.id = p.breedid
						left join breed root on root.id = br.breedid
						left join supplier sup on sup.id = p.supplierid
					where p.status = 10 and br.status = 10 and sup.status = 10 and root.status = 10
					group by p.id
					order by p.id asc
				) pet on pet.id = detail.petid
				left join color col on col.id = detail.colorid
				where detail.status = 10 and detail.statusdetailid = 1 
					and col.status = 10 " + condition + @" 
				"+ limit + @"";

			return await _p2NPetDapper.QueryAsync<PetListModel>(query, new
			{
				Breedid = oSearch.BreedId,
				BreedIdRoot = oSearch.BreedIdRoot,
				SupplierId = oSearch.SupplierId,
				FindString = "%" + oSearch.FindString.Trim() + "%",
				offset = oSearch.CurrentPage * oSearch.Limit,
				limit = oSearch.Limit,
				Discount = 10,
				TopPet = oSearch.TopPet,
				DateNow = Utils.DateNow()
			});

		}

        public async Task<List<PetSizeModel>> QueryListSizeOfPet(PetDetailConditionModel petDetailCondition)
        {
			var query =
				@"select detail.sizeid SizeId, sz.title SizeTitle
				from pet p
					left join petdetail detail on p.id = detail.petid
					left join size sz on sz.id = detail.sizeid 
					left join color c on c.id = detail.colorid 
					left join age a on a.id = detail.ageid 
				where detail.statusdetailid = 1 and p.status = 10 and c.status = 10 and a.status = 10 
				and detail.status = 10 and sz.status = 10 and detail.petid = @Id and detail.colorid = @ColorId and detail.ageid = @AgeId 
				group by sz.id";

			return await _p2NPetDapper.QueryAsync<PetSizeModel>(query, new
			{
				Id = petDetailCondition.PetId,
				ColorId = petDetailCondition.ColorId,
				AgeId = petDetailCondition.AgeId
			});
		}

		public async Task<List<PetAgeModel>> QueryListAgeOfPet(PetDetailConditionModel petDetailCondition)
		{
			var query =
				@"select detail.ageid AgeId, a.title AgeTitle
				from pet p
					left join petdetail detail on p.id = detail.petid
					left join age a on a.id = detail.ageid 
					left join color c on c.id = detail.colorid 
				where detail.statusdetailid = 1 and p.status = 10 
				and detail.status = 10 and a.status = 10 and c.status = 10 and detail.petid = @Id and detail.colorid = @ColorId 
				group by a.id";

			return await _p2NPetDapper.QueryAsync<PetAgeModel>(query, new
			{
				Id = petDetailCondition.PetId,
				ColorId = petDetailCondition.ColorId
			});
		}

		public async Task<List<PetColorModel>> QueryListColorOfPet(PetDetailConditionModel petDetailCondition)
		{
			var query =
				@"select detail.colorid ColorId, col.title ColorName
				from pet p
				left join petdetail detail on p.id = detail.petid
				left join color col on col.id = detail.colorid
				where detail.statusdetailid = 1 and p.status = 10 
				and detail.status = 10 and col.status = 10 and detail.petid = @Id
				group by col.id";

			return await _p2NPetDapper.QueryAsync<PetColorModel>(query, new
			{
				Id = petDetailCondition.PetId
			});
		}

        public async Task<List<PetSexModel>> QueryListSexOfPet(PetDetailConditionModel petDetailCondition)
        {
			var query =
				@"select detail.sexid SexId, x.title SexTitle
				from pet p
					left join petdetail detail on p.id = detail.petid
					left join sex x on x.id = detail.sexid 
					left join size sz on sz.id = detail.sizeid 
					left join color c on c.id = detail.colorid 
					left join age a on a.id = detail.ageid 
				where detail.statusdetailid = 1 and p.status = 10 and sz.status = 10 and c.status = 10 and a.status = 10 
				and detail.status = 10 and detail.petid = @Id and detail.colorid = @Colorid and detail.ageid = @AgeId and detail.sizeid = @SizeId 
				group by x.id";

			return await _p2NPetDapper.QueryAsync<PetSexModel>(query, new
			{
				Id = petDetailCondition.PetId,
				ColorId = petDetailCondition.ColorId,
				AgeId = petDetailCondition.AgeId,
				SizeId = petDetailCondition.SizeId
			});

		}

        public async Task<int> QueryCountListPet(OSearchPetModel oSearch)
        {
			oSearch.CurrentPage = oSearch.CurrentPage < 0 ? 0 : oSearch.CurrentPage;
			oSearch.CurrentDate = string.IsNullOrEmpty(oSearch.CurrentDate)
				? Utils.DateNow().ToString("yyyy-MM-dd HH:mm:ss:fff")
				: oSearch.CurrentDate;

			oSearch.Limit = oSearch.Limit == 0 ? 25 : oSearch.Limit;

			var condition = @"";

			if (oSearch.BreedId > 0)
			{
				condition += @" and pet.breedid = @BreedId";
			}

			if (oSearch.BreedIdRoot > 0)
			{
				condition += @" and pet.breedidroot = @BreedIdRoot";
			}

			if (oSearch.SupplierId > 0)
			{
				condition += @" and pet. supplierid = @SupplierId";
			}

			if (!string.IsNullOrEmpty(oSearch.FindString))
			{
				condition += @" and (
									pet.breedname like @FindString
									or
									col.title like @FindString
								)";
			}

			var query =
				@"select count(1)
				from petdetail detail
				left join (
						select pdim.petdetailid, ifnull(pim.image, '') image
						from (
							select pif.petdetailid, min(pim.id) imageid
							from petimage pim 
								inner join petimagefor pif on pif.petimageid = pim.id
							where pim.status = 10 and pif.status = 10
							group by pif.petdetailid
							) pdim 
							inner join petimage pim on pim.id = pdim.imageid
						order by pdim.petdetailid asc
					) pimage
					on pimage.petdetailid = detail.id
				inner join (
					select p.id, br.id breedid, br.name breedname, br.breedid breedidroot, root.name breedrootname,
						sup.id supplierid, sup.name suppliername, p.content
					from pet p
						left join petdetail detail on detail.petid = p.id
						left join breed br on br.id = p.breedid
						left join breed root on root.id = br.breedid
						left join supplier sup on sup.id = p.supplierid
					where p.status = 10 and br.status = 10 and sup.status = 10 and root.status = 10
					group by p.id
					order by p.id asc
				) pet on pet.id = detail.petid
				left join color col on col.id = detail.colorid
				where detail.status = 10 and detail.statusdetailid = 1 
					and col.status = 10 " + condition + @" ;";

			return await _p2NPetDapper.QuerySingleAsync<int>(query, new
			{
				Breedid = oSearch.BreedId,
				BreedIdRoot = oSearch.BreedIdRoot,
				SupplierId = oSearch.SupplierId,
				FindString = "%" + oSearch.FindString.Trim() + "%"
			});
		}

        public async Task<PetDetailModel> QueryMultiPetDetail(PetDetailConditionModel petDetailCondition)
        {
			var conditionWhere = "";

			if(petDetailCondition.PetDetailId > 0)
            {
				conditionWhere += @" and detail.id = @PetDetailId ";
            }

			if(petDetailCondition.PetId > 0)
            {
				conditionWhere += @" and detail.petid = @PetId ";

				if(petDetailCondition.SizeId > 0)
                {
					conditionWhere += @" and detail.sizeid = @SizeId ";
                }

				if (petDetailCondition.ColorId > 0)
                {
					conditionWhere += @" and detail.colorid = @ColorId ";
                }

                if(petDetailCondition.AgeId > 0)
                {
					conditionWhere += @" and detail.ageid = @AgeId ";
                }

                if(petDetailCondition.SexId > 0)
                {
					conditionWhere += @" and detail.sexid = @SexId ";
                }
            }

			var query =
				@"select detail.id PetDetailId, concat(pet.breedname, ' - ', col.title) PetTitle, 
					pet.breedid BreedId, pet.breedname BreedName, 
					pet.supplierid SupplierId, pet.suppliername SupplierName,
					detail.sizeid, size.title sizetitle,
					detail.ageid AgeId, age.title AgeTitle, detail.price Price, detail.discount Discount, 
					(detail.price * (1 - detail.discount / 100)) PriceDiscount, pet.Id PetId, pet.content Content, col.id ColorId, col.title ColorName,
					detail.sexid SexId, x.title SexTitle, detail.quantity Quantity
				from petdetail detail 
					inner join (
						select p.id, br.id breedid, br.name breedname,sup.id supplierid, sup.name suppliername, p.content
						from pet p
							left join petdetail detail on detail.petid = p.id
							left join breed br on br.id = p.breedid
							left join supplier sup on sup.id = p.supplierid
						where p.status = 10 and br.status = 10 and sup.status = 10
						group by p.id
						order by p.id asc
					) pet on pet.id = detail.petid
					left join color col on col.id = detail.colorid
					left join size on size.id = detail.sizeid
					left join age on age.id = detail.ageid
					left join sex x on x.id = detail.sexid
				where detail.status = 10 and detail.statusdetailid = 1 
					and col.status = 10 and size.status = 10 and age.status = 10 " + conditionWhere + @" ";

			return await _p2NPetDapper.QuerySingleAsync<PetDetailModel>(query, new
			{
				PetDetailId = petDetailCondition.PetDetailId,
				PetId = petDetailCondition.PetId,
				SizeId = petDetailCondition.SizeId,
				ColorId = petDetailCondition.ColorId,
				AgeId = petDetailCondition.AgeId,
				SexId = petDetailCondition.SexId
			});
		}

		public async Task<List<string>> QueryPetImages(ulong petDetailId)
        {
			var query =
				@"select case 
					when ifnull(pim.image, N'') != '' 
					then CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/PetDetail/PetDetail_Image/', pim.image)
					else ifnull(pim.image, '') 
					end PetImage 
				from petimage pim
					left join petimagefor pif on pif.petimageid = pim.id 
				where pim.status = @Status and pif.status = @Status and pif.petdetailid = @PetDetailId 
				order by pim.id asc;";
			return await _p2NPetDapper.QueryAsync<string>(query, new
			{
				Status = 10,
				PetDetailId = petDetailId
			});
        }
    }
}
