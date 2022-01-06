using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Models.Pet
{
    public class PetListModel
    {
        public ulong PetDetailId { get; set; }
        public string PetTitle { get; set; }
        public ulong PetId { get; set; }
        public ulong BreedId { get; set; }
        public string BreedName { get; set; }
        public ulong BreedIdRoot { get; set; }
        public string BreedRootName { get; set; }
        public ulong SupplierId { get; set; }
        public string SupplierName { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double PriceDiscount { get; set; }
        public string PetImage { get; set; }
        public int PetQuantity { get; set; }


    }
}
