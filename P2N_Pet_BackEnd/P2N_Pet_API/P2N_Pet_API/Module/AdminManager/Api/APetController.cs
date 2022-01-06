using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2N_Pet_API.Manager.FilterAttr;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Models.APet;
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

    public class APetController : ControllerBase
    {
        private readonly IAPetService _aPetService;

        public APetController(IAPetService aPetService)
        {
            _aPetService = aPetService;
        }

        [HttpPost]
        public async Task<IActionResult> Index(AOSearchPet aOSearchPet)
        {
            try
            {
                var pets = await _aPetService.GetListPet(aOSearchPet);

                var pagination = await _aPetService.GetListPetPagination(aOSearchPet);

                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "",
                    content = new
                    {
                        Pets = pets,
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
        public async Task<IActionResult> GetDetailPet(ulong Id)
        {
            var pet = await _aPetService.GetPetDetail(Id);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    Pet = pet
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePet(APetCreateModel aPetCreateModel)
        {
            var dateNow = Utils.DateNow();
            var userId = Utils.GetUserIdFromToken(Request);

            var forceInfo = new ForceInfo
            {
                DateNow = dateNow,
                UserId = userId
            };

            var petEntity = await _aPetService.CreatePet(forceInfo, aPetCreateModel);

            var pet = await _aPetService.GetPetDetail(petEntity.Id);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    Pet = pet
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePet(APetUpdateModel aPetUpdateModel)
        {
            var dateNow = Utils.DateNow();
            var userId = Utils.GetUserIdFromToken(Request);

            var forceInfo = new ForceInfo
            {
                DateNow = dateNow,
                UserId = userId
            };

            var petEntity = await _aPetService.UpdatePet(forceInfo, aPetUpdateModel);

            var pet = await _aPetService.GetPetDetail(petEntity.Id);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    Pet = pet
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePet(ulong Id)
        {
            var dateNow = Utils.DateNow();
            var userId = Utils.GetUserIdFromToken(Request);

            var forceInfo = new ForceInfo
            {
                DateNow = dateNow,
                UserId = userId
            };

            var petEntity = await _aPetService.DeletePet(forceInfo, Id);

            var pet = await _aPetService.GetPetDetail(petEntity.Id);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    Pet = pet
                }
            });
        }
    }
}
