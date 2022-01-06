﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2N_Pet_API.Manager.FilterAttr;
using P2N_Pet_API.Models.Order;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [UserAccess]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateModel orderCreate)
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var objectResponse = await _orderService.CreateOrder(orderCreate, forceInfo);

            return Ok(objectResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetListHistoryOrder()
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var objectResponse = await _orderService.GetListHistoryOrder(forceInfo);

            return Ok(objectResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetail(ulong orderId)
        {
            if (orderId <= 0)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Vui lòng dien orderId",
                });
            }

            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var objectResponse = await _orderService.GetOrderDetail(forceInfo, orderId);

            return Ok(objectResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(ulong orderId)
        {
            if(orderId <= 0)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Vui lòng dien orderId",
                });
            }

            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var objectResponse = await _orderService.CancelOrder(forceInfo, orderId);

            return Ok(objectResponse);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailOrder(ulong orderid)
        {
            try
            {
                var forceInfo = new ForceInfo
                {
                    UserId = Utils.GetUserIdFromToken(Request),
                    DateNow = Utils.DateNow()
                };

                await _orderService.SendEmailOrder(forceInfo, orderid);

                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "Gửi thông báo đơn hàng thành công!"
                });
            }
            catch (Exception e)
            {

                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = e.Message.ToString()
                });
            }
        }
    }
}
