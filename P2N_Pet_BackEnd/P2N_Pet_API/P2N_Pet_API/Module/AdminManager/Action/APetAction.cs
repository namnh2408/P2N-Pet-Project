using P2N_Pet_API.Database;
using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.Module.AdminManager.Action.Interface;
using P2N_Pet_API.Module.AdminManager.Models.APet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Action
{
    public class APetAction : IAPetAction
    {
        private readonly PetShopContext _petShopContext;

        public APetAction(PetShopContext petShopContext)
        {
            _petShopContext = petShopContext;
        }

        public async Task<Pet> Create(ForceInfo forceInfo, APetCreateModel aPetCreateModel)
        {
            var pet = new Pet
            {
                Breedid = aPetCreateModel.BreedId,
                Supplierid = aPetCreateModel.SupplierId,
                Content = aPetCreateModel.Content,
                Status = aPetCreateModel.Status,
                Createuser = forceInfo.UserId,
                Createdate = forceInfo.DateNow,
                Updateuser = forceInfo.UserId,
                Updatedate = forceInfo.DateNow
            };

            _petShopContext.Pets.Add(pet);
            await _petShopContext.SaveChangesAsync();

            return pet;
        }

        public async Task<Pet> Update(ForceInfo forceInfo, APetUpdateModel aPetUpdateModel)
        {
            var pet = _petShopContext.Pets.Where(a => a.Id == aPetUpdateModel.Id).FirstOrDefault();

            pet.Breedid = aPetUpdateModel.BreedId;
            pet.Supplierid = aPetUpdateModel.SupplierId;
            pet.Content = aPetUpdateModel.Content;
            pet.Status = aPetUpdateModel.Status;
            pet.Updateuser = forceInfo.UserId;
            pet.Updatedate = forceInfo.DateNow;

            _petShopContext.Pets.Update(pet);
            await _petShopContext.SaveChangesAsync();

            return pet;
        }

        public async Task<Pet> Delete(ForceInfo forceInfo, ulong Id)
        {
            var pet = _petShopContext.Pets.Where(a => a.Id == Id).FirstOrDefault();

            pet.Status = 190;
            pet.Updateuser = forceInfo.UserId;
            pet.Updatedate = forceInfo.DateNow;

            _petShopContext.Pets.Update(pet);
            await _petShopContext.SaveChangesAsync();

            return pet;
        }
    }
}
