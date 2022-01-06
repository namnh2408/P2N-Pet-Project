using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Models.APetDetail
{
    public class AOSearchPetDetail
    {
        public string Limit { get; set; }
        public string CurrentDate { get; set; }
        public string CurrentPage { get; set; }
        public string BreedId { get; set; }
        public string SupplierId { get; set; }
        public string ColorId { get; set; }
        public string SizeId { get; set; }
        public string AgeId { get; set; }
        public string SexId { get; set; }
        public string StatusDetailId { get; set; }
        public string Status { get; set; }
    }
}
