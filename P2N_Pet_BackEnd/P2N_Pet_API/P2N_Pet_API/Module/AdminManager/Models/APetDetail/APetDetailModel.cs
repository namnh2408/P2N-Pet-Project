using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Models.APetDetail
{
    public class APetDetailModel
    {
        public ulong Id { get; set; }
        public ulong BreedId { get; set; }
        public ulong SupplierId { get; set; }
        public ulong ColorId { get; set; }
        public ulong SizeId { get; set; }
        public ulong AgeId { get; set; }
        public int SexId { get; set; }
        public int StatusDetailId { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }

        public List<APetPetImageForModel> aPetPetImageForModels { get; set; }
    }
}
