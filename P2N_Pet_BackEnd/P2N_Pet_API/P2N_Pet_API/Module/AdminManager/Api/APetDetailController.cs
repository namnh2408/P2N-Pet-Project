using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2N_Pet_API.Manager.FilterAttr;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Models.APetDetail;
using P2N_Pet_API.Module.AdminManager.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ManagerAccess]
    public class APetDetailController : ControllerBase
    {
        private readonly IAPetDetailService _aPetDetailService;

        public APetDetailController(IAPetDetailService aPetDetailService)
        {
            _aPetDetailService = aPetDetailService;
        }

        [HttpPost]
        public async Task<IActionResult> Index(AOSearchPetDetail aOSearchPetDetail)
        {
            try
            {
                var petDetails = await _aPetDetailService.GetListPetDetail(aOSearchPetDetail);

                var pagination = await _aPetDetailService.GetListPetDetailPagination(aOSearchPetDetail);

                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "",
                    content = new
                    {
                        PetDetails = petDetails,
                        Pagination = pagination
                    }
                });
            }
            catch (Exception e)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Đã có lỗi xảy ra. Vui lòng thử lại.",
                    content = e.Message.ToString()
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailPetDetail(ulong Id)
        {
            var petDetail = await _aPetDetailService.GetInPetDetail(Id);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    PetDetail = petDetail
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePetDetail([FromForm] APetDetailCreateModel aPetDetailCreateModel)
        {
            var dateNow = Utils.DateNow();
            var userId = Utils.GetUserIdFromToken(Request);

            var forceInfo = new ForceInfo
            {
                DateNow = dateNow,
                UserId = userId
            };

            var petDetailEntity = await _aPetDetailService.CreatePetDetail(forceInfo, aPetDetailCreateModel);

            var petDetail = await _aPetDetailService.GetInPetDetail(petDetailEntity.Id);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    PetDetail = petDetail
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePetDetail([FromForm] APetDetailUpdateModel aPetDetailUpdateModel)
        {
            var dateNow = Utils.DateNow();
            var userId = Utils.GetUserIdFromToken(Request);

            var forceInfo = new ForceInfo
            {
                DateNow = dateNow,
                UserId = userId
            };

            var petDetailEntity = await _aPetDetailService.UpdatePetDetail(forceInfo, aPetDetailUpdateModel);

            var petDetail = await _aPetDetailService.GetInPetDetail(petDetailEntity.Id);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    PetDetail = petDetail
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePetDetail(ulong Id)
        {
            var dateNow = Utils.DateNow();
            var userId = Utils.GetUserIdFromToken(Request);

            var forceInfo = new ForceInfo
            {
                DateNow = dateNow,
                UserId = userId
            };

            var petDetailEntity = await _aPetDetailService.DeletePetDetail(forceInfo, Id);

            var petDetail = await _aPetDetailService.GetInPetDetail(petDetailEntity.Id);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    PetDetail = petDetail
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePetDetailImage(ulong petImageId)
        {
            var dateNow = Utils.DateNow();
            var userId = Utils.GetUserIdFromToken(Request);

            var forceInfo = new ForceInfo
            {
                DateNow = dateNow,
                UserId = userId
            };

            await _aPetDetailService.DeletePetDetailImage(forceInfo, petImageId);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Xóa hình ảnh thành công!"
            });
        }
    }
}
