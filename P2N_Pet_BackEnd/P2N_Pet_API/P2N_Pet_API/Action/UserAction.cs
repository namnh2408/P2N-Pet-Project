using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using P2N_Pet_API.Action.Interface;
using P2N_Pet_API.Database;
using P2N_Pet_API.Database.PetShopModels;
using P2N_Pet_API.Models.CloudMedia;
using P2N_Pet_API.Models.User;
using P2N_Pet_API.Models.UtilsProject;
using P2N_Pet_API.UtilsService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Action
{
    public class UserAction : IUserAction
    {
        private readonly PetShopContext _petShopContext;
        private readonly ICloudMediaService _cloudMediaService;

        public UserAction(PetShopContext petShopContext,
            ICloudMediaService cloudMediaService)
        {
            _petShopContext = petShopContext;
            _cloudMediaService = cloudMediaService;
        }

        public async Task<User> Register(UserRegisterModel userRegister, ForceInfo forceInfo)
        {
            var user = new User
            {
                Name = userRegister.Name.Trim(),
                Email = userRegister.Email.Trim(),
                Phone = userRegister.Phone.Trim(),
                Password = Encryptor.MD5Hash(userRegister.Password.Trim()),
                Address = userRegister.Address.Trim(),
                Status = 10,
                Role = 30,
                Createdate = forceInfo.DateNow,
                Updatedate = forceInfo.DateNow,
            };

            _petShopContext.Users.Add(user);
            await _petShopContext.SaveChangesAsync();

            user.Createuser = user.Id;
            user.Updateuser = user.Id;

            _petShopContext.Users.Update(user);
            await _petShopContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> EditProfile(UserProfileUpdateModel editProfile, ForceInfo forceInfo)
        {
            var userInfo = await _petShopContext.Users.FirstOrDefaultAsync(a => a.Id == forceInfo.UserId &&
                    a.Status == 10 &&
                    a.Email == editProfile.Email);

            if (userInfo != null)
            {
                userInfo.Name = editProfile.Name.Trim();
                userInfo.Email = editProfile.Email.Trim();
                userInfo.Phone = editProfile.Phone.Trim();
                userInfo.Address = editProfile.Address.Trim();

                userInfo.Updatedate = forceInfo.DateNow;
                userInfo.Updateuser = forceInfo.UserId;

                _petShopContext.Users.Update(userInfo);
                await _petShopContext.SaveChangesAsync();
            }

            return userInfo;
        }

        public async Task UpdateAvatarUser(ForceInfo forceInfo, CloudOneMediaModel CloudOneMedia)
        {
            var user = await _petShopContext.Users.FirstOrDefaultAsync(a => a.Id == forceInfo.UserId);

            if(user != null)
            {
                user.Avatar = CloudOneMedia.FileName;
                user.Updateuser = forceInfo.UserId;
                user.Updatedate = forceInfo.DateNow;

                _petShopContext.Users.Update(user);
                await _petShopContext.SaveChangesAsync();
            }
        }

        public async Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avatar, ulong userId)
        {
            var cloudOneMediaConfig = new CloudOneMediaConfig
            {
                Folder = "Upload/Avatar/Avatar_" + userId,
                FileName = "Image_Avatar",
                FormFile = avatar
            };

            return await _cloudMediaService.SaveOneFileData(cloudOneMediaConfig);
        }

        public async Task<User> ForgotPassword(UserForgotPasswordModel userForgotPassword, ForceInfo forceInfo)
        {
            var user = await _petShopContext.Users.FirstOrDefaultAsync(a => a.Email == userForgotPassword.Email);

            if (user != null)
            {
                user.Password = Encryptor.MD5Hash(userForgotPassword.Password.Trim());

                user.Updateuser = user.Id;
                user.Updatedate = forceInfo.DateNow;

                _petShopContext.Users.Update(user);
                await _petShopContext.SaveChangesAsync();
            }

            return user;
        }

        public async Task ChangePassword(string newPassword, ForceInfo forceInfo)
        {
            var user = await _petShopContext.Users.FirstOrDefaultAsync(a => a.Id == forceInfo.UserId && a.Status == 10);

            if(user != null)
            {
                user.Password = Encryptor.MD5Hash(newPassword.Trim());

                user.Updateuser = forceInfo.UserId;
                user.Updatedate = forceInfo.DateNow;

                _petShopContext.Users.Update(user);
                await _petShopContext.SaveChangesAsync();
            }
        }

        public async Task<User> CreateAccountAdmin(UserRegisterModel userRegister, ForceInfo forceInfo)
        {
            var user = new User
            {
                Name = userRegister.Name.Trim(),
                Email = userRegister.Email.Trim(),
                Phone = userRegister.Phone.Trim(),
                Password = Encryptor.MD5Hash(userRegister.Password.Trim()),
                Address = userRegister.Address.Trim(),
                Status = 10,
                Role = 10,
                Createdate = forceInfo.DateNow,
                Updatedate = forceInfo.DateNow,
            };

            _petShopContext.Users.Add(user);
            await _petShopContext.SaveChangesAsync();

            user.Createuser = user.Id;
            user.Updateuser = user.Id;

            _petShopContext.Users.Update(user);
            await _petShopContext.SaveChangesAsync();

            return user;
        }
    }
}
