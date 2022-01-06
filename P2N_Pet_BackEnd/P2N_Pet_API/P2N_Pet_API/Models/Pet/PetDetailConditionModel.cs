using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Models.Pet
{
    public class PetDetailConditionModel
    {
        public ulong PetDetailId { get; set; }
        public ulong PetId { get; set; }
        public ulong SizeId { get; set; }
        public ulong ColorId { get; set; }
        public ulong AgeId { get; set; }
        public ulong SexId { get; set; }
    }
}
