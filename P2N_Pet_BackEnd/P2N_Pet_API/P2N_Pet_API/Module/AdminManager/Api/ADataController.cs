using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2N_Pet_API.Manager.FilterAttr;
using P2N_Pet_API.Models.UtilsProject;
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

    public class ADataController : ControllerBase
    {
        private readonly IADataService _aDataService;

        public ADataController(IADataService aDataService)
        {
            _aDataService = aDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalStatusSelection()
        {
            var statusSelection = await _aDataService.GetNormalStatusSelection();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    StatusSelection = statusSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalAgeSelection()
        {
            var ageSelection = await _aDataService.GetNormalAgeSelection();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    AgeSelection = ageSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalColorSelection()
        {
            var colorSelection = await _aDataService.GetNormalColorSelection();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    ColorSelection = colorSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalSizeSelection()
        {
            var sizeSelection = await _aDataService.GetNormalSizeSelection();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    SizeSelection = sizeSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalSexSelection()
        {
            var sexSelection = await _aDataService.GetNormalSexSelection();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    SexSelection = sexSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalBreedDefault()
        {
            var breedDefaultSelection = await _aDataService.GetNormalBreedDefaultSelection();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    BreedDefaultSelection = breedDefaultSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalBreedSelection()
        {
            var breedSelection = await _aDataService.GetNormalBreedSelection();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    BreedSelection = breedSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalSupplierSelection()
        {
            var supplierSelection = await _aDataService.GetNormalSupplierSelection();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    SupplierSelection = supplierSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalBreedPetDetailSelection(ulong supplierid)
        {
            var breedSelection = await _aDataService.GetNormalBreedPetDetailSelection(supplierid);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    BreedSelection = breedSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalSupplierPetDetailSelection(ulong breedid)
        {
            var supplierSelection = await _aDataService.GetNormalSupplierPetDetailSelection(breedid);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    SupplierSelection = supplierSelection
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNormalStatusDetailSelection()
        {
            var statusDetailSelection = await _aDataService.GetNormalStatusDetailSelection();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "",
                content = new
                {
                    StatusDetailSelection = statusDetailSelection
                }
            });
        }
    }
}
