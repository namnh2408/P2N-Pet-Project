using P2N_Pet_API.Models.Order;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Query.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Query
{
    public class OrderQuery : IOrderQuery
    {
        private readonly IP2NPetDapper _p2NPetDapper;

        public OrderQuery(IP2NPetDapper p2NPetDapper)
        {
            _p2NPetDapper = p2NPetDapper;
        }

        public async Task<(ulong, ulong)> QueryCartIdAndTotalMoney(ulong userId)
        {
            var query =
                @"select item.cartid, sum(item.pricediscount * item.quantity) totalmoney
                from cartitem item
		            left join cart c on c.id = item.cartid
                    left join users u on u.id = c.userid
	            where item.status = 10 and c.status = 10 and c.userid = @Id and ifnull(item.orderid, 0) = 0
                group by item.cartid";

            return await _p2NPetDapper.QuerySingleAsync<(ulong, ulong)>(query, new
            {
                Id = userId
            });
        }

        public async Task<List<OrderHistoryListModel>> QueryListHistoryOrder(ulong userId)
        {
            var query =
                @"select o.id OrderId,  o.createdate CreateOrder, item.totalmoney TotalMoney, 
	                item.numorder NumOrder, c.address Address, o.statusorderid StatusOrderId, std.title StatusOrderText,
                    item.numbreed NumBreed, c.name CustomerName, c.email Email, c.phone Phone
                from `order` o
                left join (
	                select item.orderid orderid, count(1) numbreed, sum(item.quantity) numorder, sum(item.pricediscount * item.quantity) totalmoney
                    from cartitem item
		                left join cart c on c.id = item.cartid
	                where item.status = 10
                    group by item.orderid
                ) item on item.orderid = o.id
                left join customer c on c.id = o.customerid
                left join users u on u.id = c.userid
                left join statusorder std on std.id = o.statusorderid
                where o.status = 10 and std.status = 10 and u.status = 10 and u.id = @Id
	                and datediff( @DateNow , o.createdate) < 365 
                order by o.createdate desc ";

            return await _p2NPetDapper.QueryAsync<OrderHistoryListModel>(query, new
            {
                Id = userId,
                DateNow = Utils.DateNow()
            });

        }

        public async Task<List<OrderDetailModel>> QueryListOrderDetail(ulong userId, ulong orderId)
        {
            var query =
                @"select concat(pet.breedname, ' - ', col.title) PetTitle, detail.price Price, 
                    detail.discount Discount, item.pricediscount PriceDiscount,
                    item.quantity Quantity,
                    case 
	                    when ifnull(pimage.image, N'') != '' 
	                    then CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/PetDetail/PetDetail_Image/', pimage.image)
	                    else ifnull(pimage.image, '') 
                    end PetImage, item.quantity * item.pricediscount TotalPriceItem
                from `order` o
                    left join cartitem item on item.orderid = o.id
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
                    and detail.status = 10 
                    and col.status = 10 and c.userid = @UserId and o.id = @OrderId";

            return await _p2NPetDapper.QueryAsync<OrderDetailModel>(query, new
            {
                UserId = userId,
                OrderId = orderId
            });
        }

        public async Task<OrderModel> QueryOrderDetail(ulong OrderId)
        {
            var query =
                @"select o.Id, ifnull(u.Name, N'') UserName, ifnull(u.Phone, N'') UserPhone, ifnull(u.Email, N'') UserEmail, 
                    ifnull(u.Address, N'') UserAddress, ifnull(cu.Name, N'') CustomerName, ifnull(cu.Phone, N'') CustomerPhone, 
                    ifnull(cu.Email, N'') CustomerEmail, ifnull(cu.Address, N'') CustomerAddress, o.TotalMoney, ifnull(o.Note, N'') Note 
                from `order` o inner join cart c on c.id = o.cartid
	                inner join users u on u.id = c.userid 
                    inner join customer cu on cu.id = o.customerid
                where o.status = @Status and c.status = @Status and u.status = @Status and cu.status = @Status and o.id = @OrderId;";

            return await _p2NPetDapper.QuerySingleAsync<OrderModel>(query, new
            {
                Status = 10,
                OrderId = OrderId
            });
        }
    }
}
