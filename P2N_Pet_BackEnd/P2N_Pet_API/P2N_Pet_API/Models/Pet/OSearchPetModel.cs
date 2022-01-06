using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace P2N_Pet_API.Models.Pet
{
    public class OSearchPetModel
    {
        public int CurrentPage { get; set; }

        [DefaultValue("")]
        public string CurrentDate { get; set; }
        public int Limit { get; set; }
        public ulong BreedId { get; set; }
        public ulong BreedIdRoot { get; set; }
        public ulong SupplierId { get; set; }

        [DefaultValue("")]
        public string FindString { get; set; }

        public int TopPet { get; set; }
    }
}
