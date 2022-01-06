using P2N_Pet_API.Models.Cart;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Query.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Query
{
    public class CartQuery : ICartQuery
    {
        private readonly IP2NPetDapper _p2NPetDapper;

        public CartQuery(IP2NPetDapper p2NPetDapper)
        {
            _p2NPetDapper = p2NPetDapper;
        }

        public async Task<(int, int)> CheckPetExisted(ulong petDetailId)
        {
            var query =
                @"select count(1), pd.statusdetailid
                from pet p
                    left join petdetail pd on pd.petid = p.id
                where p.status = 10 and pd.status = 10 and pd.id = @Id ";

            return await _p2NPetDapper.QuerySingleAsync<(int, int)>(query, new
            {
                Id = petDetailId
            });
        }

        public async Task<List<CartItemListModel>> GetListCartItem(ulong userId)
        {
            var query =
                @"select item.id CartItemId, item.cartid CartId, item.petdetailid PetDetailId,
                    concat(pet.breedname, ' - ', col.title) PetTitle, detail.price Price, 
                    detail.discount Discount, item.pricediscount PriceDiscount,
                    item.quantity Quantity,
                    case 
	                    when ifnull(pimage.image, N'') != '' 
	                    then CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/PetDetail/PetDetail_Image/', pimage.image)
	                    else ifnull(pimage.image, '') 
	                end PetImage, item.quantity * item.pricediscount TotalPriceItem,
                    detail.Quantity QuantityPet
                from cartitem item
                    left join cart c on c.id =  item.cartid
                    left join petdetail detail on detail.id = item.petdetailid
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
                where item.status = 10 and c.status = 10 
                    and detail.statusdetailid = 1 and detail.status = 10 and ifnull(item.orderid, 0) = 0
                    and col.status = 10 and c.userid = @Id ";

            return await _p2NPetDapper.QueryAsync<CartItemListModel>(query, new
            {
                Id = userId
            });
        }

        public async Task<int> GetCountQuantityCartItem(ulong userId)
        {
            var query =
                @"select ifnull(sum(item.quantity), 0)  
                from cartitem item
                    left join cart c on c.id =  item.cartid
                    left join petdetail detail on detail.id = item.petdetailid
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
                where item.status = 10 and c.status = 10 
                    and detail.statusdetailid = 1 and detail.status = 10 and ifnull(item.orderid, 0) = 0
                    and col.status = 10 and c.userid = @Id ";

            return await _p2NPetDapper.QuerySingleAsync<int>(query, new
            {
                Id = userId
            });
        }

        public async Task<(int, int)> GetQuantityAddCart(ulong petdetailId, ulong cartId)
        {
            var query =
                @"select count(1), quantity
                from cartitem
                where status = 10 and ifnull(orderid, 0) = 0 and petdetailid = @PetDetailId and cartid = @CartId";

            return await _p2NPetDapper.QuerySingleAsync<(int, int)>(query, new
            {
                PetDetailId = petdetailId,
                CartId = cartId
            });
        }

        public async Task<ulong> GetCartIdByUser(ulong userId)
        {
            var query =
                @"select ifnull(id, 0) id
                from cart
                where status = 10 and userid = @Id ";

            return await _p2NPetDapper.QuerySingleAsync<ulong>(query, new
            {
                Id = userId
            });
        }

        public async Task<int> QueryCheckCartExisted(ulong userId)
        {
            var query =
                @"select count(1)
                from cart
                where status = 10 and userid = @Id ";

            return await _p2NPetDapper.QuerySingleAsync<int>(query, new
            {
                Id = userId
            });
        }

        public async Task<(int, float, int)> QueryCheckPetDetailExistedAndGetPrice(ulong petDetailId)
        {
            var query =
                @"select count(1), (price * (1 - discount / 100)) PriceDiscount, quantity
                from petdetail
                where statusdetailid = 1 and status = 10 and id = @Id";

            return await _p2NPetDapper.QuerySingleAsync<(int, float, int)>(query, new
            {
                Id = petDetailId
            });
        }

        public async Task<int> GetQuantityCartByIdAndUser(ulong petDetailId, ulong userId)
        {
            var query =
                @"select ifnull(item.quantity, 0) quantity
                from cartitem item
                    left join cart c on c.id = item.cartid
                where item.status = 10 and ifnull(item.orderid, 0) = 0
                    and item.petdetailid = @PetDetailId and c.userid = @UserId";

            return await _p2NPetDapper.QuerySingleAsync<int>(query, new
            {
                PetDetailId = petDetailId,
                UserId = userId
            });
        }
    }
}
