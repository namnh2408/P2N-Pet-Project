using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Models.Pet
{
    public class PetDetailModel
    {
        public ulong PetDetailId { get; set; }
        public string PetTitle { get; set; }
        public ulong PetId { get; set; }
        public string Content { get; set; }
        public ulong BreedId { get; set; }
        public string BreedName { get; set; }
        public ulong SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double PriceDiscount { get; set; }
        public List<string> petImages { get; set; }
        public ulong SizeId { get; set; }
        public string SizeTitle { get; set; }
        public List<PetSizeModel> petSizes { get; set; }
        public ulong AgeId { get; set; }
        public string AgeTitle { get; set; }
        public List<PetAgeModel> petAges { get; set; }
        public ulong ColorId { get; set; }
        public string ColorName { get; set; }
        public List<PetColorModel> petColors { get; set; }
        public int SexId { get; set; }
        public string SexTitle { get; set; }
        public List<PetSexModel> petSexes { get; set; }

    }
}
