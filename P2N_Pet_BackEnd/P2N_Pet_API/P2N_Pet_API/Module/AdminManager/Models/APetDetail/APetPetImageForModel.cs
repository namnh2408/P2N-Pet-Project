using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Models.APetDetail
{
    public class APetPetImageForModel
    {
        public ulong Id { get; set; }
        public ulong PetImageId { get; set; }
        public string Image { get; set; }
    }
}
