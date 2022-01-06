using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2N_Pet_API.Models.Pet;
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
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        public async Task<IActionResult> GetListPet(OSearchPetModel oSearchPet)
        {
            var pets = await _petService.GetListPet(oSearchPet);
            var pagination = await _petService.GetListPetPagination(oSearchPet);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy danh sach thu cung thành công",
                content = new
                {
                    Pets = pets,
                    Pagination = pagination
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailPet(ulong PetDetailId)
        {
            if (PetDetailId == 0)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Vui lòng điền PetDetailId",
                });
            }

            var result = await _petService.GetDetailPet(PetDetailId);

            if(result == null)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Lay chi tiet thu cung that bai",
                });
            }

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy chi tiet thu cung thành công.",
                content = new
                {
                    PetDetail = result
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetMultiPetDetail(PetDetailConditionModel petDetailCondition)
        {
            if(petDetailCondition.PetDetailId <= 0 && petDetailCondition.PetId <= 0)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Vui lòng điền PetDetailId | PetId",
                });
            }

            if(petDetailCondition.PetId > 0)
            {
                if(petDetailCondition.SizeId <= 0 && petDetailCondition.ColorId <= 0 &&
                    petDetailCondition.AgeId <= 0 && petDetailCondition.SexId <= 0)
                {
                    return Ok(new ObjectResponse
                    {
                        result = 0,
                        message = "Vui lòng điền SizeId | ColorId | AgeId | SexId",
                    });
                }
            }

            var result = await _petService.GetMultiPetDetail(petDetailCondition);

            if (result == null)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Lấy chi tiết thú cưng thất bại",
                });
            }

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy chi tiết thú cưng thành công.",
                content = new
                {
                    PetDetail = result
                }
            });
        }
    }
}
