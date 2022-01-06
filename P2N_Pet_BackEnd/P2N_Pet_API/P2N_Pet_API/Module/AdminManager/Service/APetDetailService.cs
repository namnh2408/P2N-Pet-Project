using P2N_Pet_API.Database;
using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.CloudMedia;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Action.Interface;
using P2N_Pet_API.Module.AdminManager.Models.APetDetail;
using P2N_Pet_API.Module.AdminManager.Query.Interface;
using P2N_Pet_API.Module.AdminManager.Service.Interface;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Service
{
    public class APetDetailService : IAPetDetailService
    {
        private readonly PetShopContext _petShopContext;
        private readonly IAPetDetailQuery _aPetDetailQuery;
        private readonly IAPetDetailAction _aPetDetailAction;
        private readonly IPaginationService _paginationService;

        public APetDetailService(IAPetDetailQuery aPetDetailQuery,
            IAPetDetailAction aPetDetailAction,
            PetShopContext petShopContext,
            IPaginationService paginationService)
        {
            _petShopContext = petShopContext;
            _aPetDetailQuery = aPetDetailQuery;
            _aPetDetailAction = aPetDetailAction;
            _paginationService = paginationService;
        }

        public async Task<List<APetDetailListModel>> GetListPetDetail(AOSearchPetDetail aOSearchPetDetail)
        {
            return await _aPetDetailQuery.QueryGetListPetDetail(aOSearchPetDetail);
        }

        public async Task<PaginationModel> GetListPetDetailPagination(AOSearchPetDetail aOSearchPetDetail)
        {
            var count = await _aPetDetailQuery.QueryCountListPetDetail(aOSearchPetDetail);

            var pagination = await _paginationService.BuildPagination(count, Convert.ToInt32(aOSearchPetDetail.CurrentPage),
                aOSearchPetDetail.CurrentDate, Convert.ToInt32(aOSearchPetDetail.Limit));

            return pagination;
        }

        public async Task<APetDetailModel> GetInPetDetail(ulong Id)
        {
            return await _aPetDetailQuery.QueryGetInPetDetail(Id);
        }

        public async Task<Petdetail> CreatePetDetail(ForceInfo forceInfo, APetDetailCreateModel aPetDetailCreateModel)
        {
            if(aPetDetailCreateModel != null && aPetDetailCreateModel.BreedId != 0 && aPetDetailCreateModel.SupplierId != 0)
            {
                aPetDetailCreateModel.PetId = await _aPetDetailQuery.GetPetId(aPetDetailCreateModel.BreedId, aPetDetailCreateModel.SupplierId);
            }    

            var petDetail = await _aPetDetailAction.Create(forceInfo, aPetDetailCreateModel);

            if (petDetail != null && aPetDetailCreateModel.FileData != null
                && aPetDetailCreateModel.FileData.Count > 0)
            {
                var petDetailUpdate = new APetDetailUpdateModel
                {
                    Id = petDetail.Id,
                    PetId = aPetDetailCreateModel.PetId,
                    ColorId = aPetDetailCreateModel.ColorId,
                    SizeId = aPetDetailCreateModel.SizeId,
                    AgeId = aPetDetailCreateModel.AgeId,
                    SexId = aPetDetailCreateModel.SexId,
                    StatusDetailId = aPetDetailCreateModel.StatusDetailId,
                    Price = aPetDetailCreateModel.Price,
                    Discount = aPetDetailCreateModel.Discount,
                    Quantity = aPetDetailCreateModel.Quantity,
                    Status = aPetDetailCreateModel.Status,
                    FileData = aPetDetailCreateModel.FileData
                };

                var imageOldIds = await _aPetDetailQuery.QueryListImageOldCreate(petDetailUpdate);

                if (imageOldIds != null && imageOldIds.Count > 0)
                {
                    await _aPetDetailAction.UpdatePetDetailImageOld(forceInfo, petDetailUpdate.Id, imageOldIds);
                }

                await UpdatePetDetailImage(forceInfo, petDetailUpdate);
            }

            return petDetail;
        }

        public async Task<Petdetail> UpdatePetDetail(ForceInfo forceInfo, APetDetailUpdateModel aPetDetailUpdateModel)
        {
            if (aPetDetailUpdateModel != null && aPetDetailUpdateModel.BreedId != 0 && aPetDetailUpdateModel.SupplierId != 0)
            {
                aPetDetailUpdateModel.PetId = await _aPetDetailQuery.GetPetId(aPetDetailUpdateModel.BreedId, aPetDetailUpdateModel.SupplierId);
            }

            var petDetail = await _aPetDetailAction.Update(forceInfo, aPetDetailUpdateModel);

            if (petDetail != null && aPetDetailUpdateModel.FileData != null
                && aPetDetailUpdateModel.FileData.Count > 0)
            {
                await UpdatePetDetailImage(forceInfo, aPetDetailUpdateModel);
            }

            return petDetail;
        }

        public async Task<Petdetail> DeletePetDetail(ForceInfo forceInfo, ulong Id)
        {
            return await _aPetDetailAction.Delete(forceInfo, Id);
        }

        public async Task UpdatePetDetailImage(ForceInfo forceInfo, APetDetailUpdateModel aPetDetailUpdateModel)
        {
            var idPetDetailDuplicates = await _aPetDetailQuery.QueryListPetDetailDuplicateImage(aPetDetailUpdateModel.PetId, 
                                                                                            aPetDetailUpdateModel.ColorId, 
                                                                                            aPetDetailUpdateModel.AgeId);

            if (idPetDetailDuplicates.Count > 0)
            {
                var cloudMedias = await _aPetDetailAction.SaveMediaData(aPetDetailUpdateModel);
                await _aPetDetailAction.UpdatePetDetailImage(forceInfo, aPetDetailUpdateModel, cloudMedias, idPetDetailDuplicates);
            }
        }

        public async Task DeletePetDetailImage(ForceInfo forceInfo, ulong petImageId)
        {
            var idPetImageForDuplicates = await _aPetDetailQuery.QueryListPetImageForDuplicateImage(petImageId);

            if(idPetImageForDuplicates.Count > 0)
            {
                await _aPetDetailAction.DeletePetDetailImage(forceInfo, petImageId, idPetImageForDuplicates);
            }
        }
    }
}
