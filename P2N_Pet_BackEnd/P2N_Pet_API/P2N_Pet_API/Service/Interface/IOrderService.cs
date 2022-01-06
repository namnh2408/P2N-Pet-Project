using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.Order;
using P2N_Pet_API.Models.UtilsProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Service.Interface
{
    public interface IOrderService
    {
        Task<Customer> CreateInforCustomer(OrderCustomerCreateModel orderCustomerCreate, ForceInfo forceInfo);
        Task<ObjectResponse> CreateOrder(OrderCreateModel orderCreate, ForceInfo forceInfo);
        Task<ObjectResponse> GetListHistoryOrder(ForceInfo forceInfo);
        Task<ObjectResponse> GetOrderDetail(ForceInfo forceInfo, ulong orderId);
        Task<ObjectResponse> CancelOrder(ForceInfo forceInfo, ulong orderId);
        Task SendEmailOrder(ForceInfo forceInfo, ulong orderId);
    }
}
