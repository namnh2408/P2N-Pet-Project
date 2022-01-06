using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Module.AdminManager.Models.APet
{
    public class APetModel
    {
        public ulong Id { get; set; }
        public ulong BreedId { get; set; }
        public ulong SupplierId { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
    }
}
