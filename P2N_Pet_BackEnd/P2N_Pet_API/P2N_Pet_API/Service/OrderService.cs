using P2N_Pet_API.Action.Interface;
using P2N_Pet_API.Database;
using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.Order;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Query.Interface;
using P2N_Pet_API.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Service
{
    public class OrderService: IOrderService
    {
        private readonly IOrderAction _orderAction;
        private readonly PetShopContext _petShopContext;
        private readonly ICartAction _cartAction;
        private readonly ICartQuery _cartQuery;
        private readonly IOrderQuery _orderQuery;

        public OrderService(IOrderAction orderAction,
            PetShopContext petShopContext,
            ICartAction cartAction,
            ICartQuery cartQuery,
            IOrderQuery orderQuery)
        {
            _orderAction = orderAction;
            _petShopContext = petShopContext;
            _cartAction = cartAction;
            _cartQuery = cartQuery;
            _orderQuery = orderQuery;
        }

        public async Task<Customer> CreateInforCustomer(OrderCustomerCreateModel orderCustomerCreate, ForceInfo forceInfo)
        {
            return await _orderAction.CreateInforCustomer(orderCustomerCreate, forceInfo);
        }

        public async Task<ObjectResponse> CreateOrder(OrderCreateModel orderCreate, ForceInfo forceInfo)
        {

            var customerCreate = new OrderCustomerCreateModel
            {
                Name = orderCreate.Name,
                Address = orderCreate.Address,
                Phone = orderCreate.Phone,
                Email = orderCreate.Email
            };

            try
            {
                var cartItem = await _orderQuery.QueryCartIdAndTotalMoney(forceInfo.UserId);

                if (cartItem == (null, null) || cartItem == (0, 0))
                {
                    return new ObjectResponse
                    {
                        result = 0,
                        message = "Không có sản phẩm để tiến hàng đặt hàng"
                    };
                }

                var customer = await CreateInforCustomer(customerCreate, forceInfo);

                if (customer == null)
                {
                    return new ObjectResponse
                    {
                        result = 0,
                        message = "Đặt hàng thất bại. Vui lòng thử lại"
                    };
                }

                orderCreate.CartId = cartItem.Item1;
                orderCreate.TotalMoney = cartItem.Item2;
                orderCreate.CustomerId = customer.Id;

                var order = await _orderAction.CreateOrder(orderCreate, forceInfo);

                if(order == null)
                {
                    return new ObjectResponse
                    {
                        result = 0,
                        message = "Đặt hàng thất bại. Vui lòng thử lại"
                    };
                }
                else
                {
                    var cartItemList = await _cartQuery.GetListCartItem(forceInfo.UserId);

                    var idList = new List<ulong>();
                    idList = cartItemList.Select(x => x.CartItemId).ToList();

                    await _cartAction.UpdateOrderCartItem(idList, order.Id, forceInfo);
                    await _orderAction.UpdateQuantityPetDetail(order.Id, forceInfo);
                    await _orderAction.DeleteItemWhenOtherOrdered(order.Id, forceInfo);
                    await SendEmailOrder(forceInfo, order.Id);

                    return new ObjectResponse
                    {
                        result = 1,
                        message = "Đặt hàng thành công"
                    };
                }

            }
            catch(Exception e)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Đã xảy ra lỗi. Vui lòng thử lại",
                    content = e.Message.ToString()
                };
            }

            
        }

        public async Task<ObjectResponse> GetListHistoryOrder(ForceInfo forceInfo)
        {
            var orderList = await _orderQuery.QueryListHistoryOrder(forceInfo.UserId);

            if(orderList == null || orderList.Count == 0)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Không có đơn hàng nào đã đặt"
                };
            }

            return new ObjectResponse
            {
                result = 1,
                message = "Lấy danh sách đặt hàng thành công",
                content = new
                {
                    Orders = orderList
                }
            };
        }

        public async Task<ObjectResponse> GetOrderDetail(ForceInfo forceInfo, ulong orderId)
        {
            var detail = await _orderQuery.QueryListOrderDetail(forceInfo.UserId, orderId);

            if (detail == null || detail.Count() == 0)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Xem chi tiết đơn hàng thất bại"
                };
            }

            ulong i = 1;

            detail.ForEach(a =>
            {
                a.Id = i++;
            });

            return new ObjectResponse
            {
                result = 1,
                message = "Xem chi tiết đơn hàng thành công",
                content = new
                {
                    OrderDetail = detail
                }
            };
        }

        public async Task<ObjectResponse> CancelOrder(ForceInfo forceInfo, ulong orderId)
        {
            var order = await _orderAction.CancelOrder(orderId, forceInfo);

            if (order == null)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Huỷ đặt hàng thất bại"
                };
            }

            return new ObjectResponse
            {
                result = 1,
                message = "Huỷ đặt hàng thành công",
            };
        }

        public async Task SendEmailOrder(ForceInfo forceInfo, ulong orderId)
        {
            var order = await _orderQuery.QueryOrderDetail(orderId);

            var detail = await _orderQuery.QueryListOrderDetail(forceInfo.UserId, orderId);

            await _orderAction.SendEmailOrder(orderId, order, detail);
        }
    }
}
