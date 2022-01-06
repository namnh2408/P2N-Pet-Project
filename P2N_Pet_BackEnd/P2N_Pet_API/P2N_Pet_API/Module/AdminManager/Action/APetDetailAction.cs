using P2N_Pet_API.Database;
using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.CloudMedia;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Action.Interface;
using P2N_Pet_API.Module.AdminManager.Models.APetDetail;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Action
{
    public class APetDetailAction : IAPetDetailAction
    {
        private readonly PetShopContext _petShopContext;
        private readonly ICloudMediaService _cloudMediaService;

        public APetDetailAction(PetShopContext petShopContext,
            ICloudMediaService cloudMediaService)
        {
            _petShopContext = petShopContext;
            _cloudMediaService = cloudMediaService;
        }

        public async Task<Petdetail> Create(ForceInfo forceInfo, APetDetailCreateModel aPetDetailCreateModel)
        {
            var petDetail = new Petdetail
            {
                Petid = aPetDetailCreateModel.PetId,
                Colorid = aPetDetailCreateModel.ColorId,
                Sizeid = aPetDetailCreateModel.SizeId,
                Ageid = aPetDetailCreateModel.AgeId,
                Sexid = aPetDetailCreateModel.SexId,
                Statusdetailid = aPetDetailCreateModel.StatusDetailId,
                Price = aPetDetailCreateModel.Price,
                Discount = aPetDetailCreateModel.Discount,
                Quantity = aPetDetailCreateModel.Quantity,
                Status = aPetDetailCreateModel.Status,
                Createuser = forceInfo.UserId,
                Createdate = forceInfo.DateNow,
                Updateuser = forceInfo.UserId,
                Updatedate = forceInfo.DateNow
            };

            _petShopContext.Petdetails.Add(petDetail);
            await _petShopContext.SaveChangesAsync();

            return petDetail;
        }

        public async Task<Petdetail> Update(ForceInfo forceInfo, APetDetailUpdateModel aPetDetailUpdateModel)
        {
            var petDetail = _petShopContext.Petdetails.Where(a => a.Id == aPetDetailUpdateModel.Id).FirstOrDefault();

            petDetail.Petid = aPetDetailUpdateModel.PetId;
            petDetail.Colorid = aPetDetailUpdateModel.ColorId;
            petDetail.Sizeid = aPetDetailUpdateModel.SizeId;
            petDetail.Ageid = aPetDetailUpdateModel.AgeId;
            petDetail.Sexid = aPetDetailUpdateModel.SexId;
            petDetail.Statusdetailid = aPetDetailUpdateModel.StatusDetailId;
            petDetail.Price = aPetDetailUpdateModel.Price;
            petDetail.Discount = aPetDetailUpdateModel.Discount;
            petDetail.Quantity = aPetDetailUpdateModel.Quantity;
            petDetail.Status = aPetDetailUpdateModel.Status;
            petDetail.Updateuser = forceInfo.UserId;
            petDetail.Updatedate = forceInfo.DateNow;

            _petShopContext.Petdetails.Update(petDetail);
            await _petShopContext.SaveChangesAsync();

            return petDetail;
        }

        public async Task<Petdetail> Delete(ForceInfo forceInfo, ulong Id)
        {
            var petDetail = _petShopContext.Petdetails.Where(a => a.Id == Id).FirstOrDefault();

            petDetail.Status = 190;
            petDetail.Updateuser = forceInfo.UserId;
            petDetail.Updatedate = forceInfo.DateNow;

            _petShopContext.Petdetails.Update(petDetail);
            await _petShopContext.SaveChangesAsync();

            return petDetail;
        }

        public async Task UpdatePetDetailImageOld(ForceInfo forceInfo, ulong petDetailId,List<ulong> imageOldIds)
        {
            var petImageFors = imageOldIds.Select(id => new Petimagefor
            {
                Petdetailid = petDetailId,
                Petimageid = id,
                Status = 10,
                Createuser = forceInfo.UserId,
                Createdate = forceInfo.DateNow,
                Updateuser = forceInfo.UserId,
                Updatedate = forceInfo.DateNow
            });

            _petShopContext.Petimagefors.AddRange(petImageFors);
            await _petShopContext.SaveChangesAsync();
        }

        public async Task UpdatePetDetailImage(ForceInfo forceInfo, 
            APetDetailUpdateModel aPetDetailUpdateModel, 
            List<CloudMediaModel> CloudMedias,
            List<ulong> idPetDetailDuplicates)
        {
            if (CloudMedias != null && CloudMedias.Count() > 0)
            {
                var petDetailImages = new List<Petimage>();

                foreach ( var CloudMedia in CloudMedias)
                {
                    var petDetailImage = new Petimage
                    {
                        Image = CloudMedia.FileName,
                        Status = 10,
                        Createdate = forceInfo.DateNow,
                        Createuser = forceInfo.UserId,
                        Updatedate = forceInfo.DateNow,
                        Updateuser = forceInfo.UserId
                    };

                    _petShopContext.Petimages.Add(petDetailImage);
                    await _petShopContext.SaveChangesAsync();

                    petDetailImages.Add(petDetailImage);
                }

                foreach (var id in idPetDetailDuplicates)
                {
                    var petImageFors = petDetailImages.Select(image => new Petimagefor
                    {
                        Petdetailid = id,
                        Petimageid = image.Id,
                        Status = 10,
                        Createdate = forceInfo.DateNow,
                        Createuser = forceInfo.UserId,
                        Updatedate = forceInfo.DateNow,
                        Updateuser = forceInfo.UserId
                    });

                    _petShopContext.Petimagefors.AddRange(petImageFors);
                    await _petShopContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeletePetDetailImage(ForceInfo forceInfo, ulong petImageId, List<ulong> idPetImageForDuplicates)
        {
            var petImage = _petShopContext.Petimages.Where(p => p.Id == petImageId).FirstOrDefault();

            if (petImage != null)
            {
                petImage.Status = 190;
                petImage.Updateuser = forceInfo.UserId;
                petImage.Updatedate = forceInfo.DateNow;
            }

            _petShopContext.Petimages.Update(petImage);
            await _petShopContext.SaveChangesAsync();

            foreach(var id in idPetImageForDuplicates)
            {
                var petImageFor = _petShopContext.Petimagefors.Where(p => p.Id == id).FirstOrDefault();

                if(petImageFor != null)
                {
                    petImageFor.Status = 190;
                    petImageFor.Updateuser = forceInfo.UserId;
                    petImageFor.Updatedate = forceInfo.DateNow;

                    _petShopContext.Petimagefors.Update(petImageFor);
                    await _petShopContext.SaveChangesAsync();
                }
            }
        }

        public async Task<List<CloudMediaModel>> SaveMediaData(APetDetailUpdateModel aPetDetailUpdateModel)
        {
            var cloudMediaConfig = new CloudMediaConfig
            {
                Folder = "Upload/PetDetail/PetDetail_Image",
                FileName = "Image_PetDetail",
                CloudFiles = aPetDetailUpdateModel.FileData.Select((a, index) => new CloudFileConfig
                {
                    Index = index,
                    FormFile = a,
                }).ToList(),
            };

            return await _cloudMediaService.SaveFileData(cloudMediaConfig);
        }
    }
}
