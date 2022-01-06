using System;
using System.Collections.Generic;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Pet
    {
        public Pet()
        {
            Petdetails = new HashSet<Petdetail>();
        }

        public ulong Id { get; set; }
        public ulong? Breedid { get; set; }
        public ulong? Supplierid { get; set; }
        public string Content { get; set; }
        public int? Status { get; set; }
        public ulong? Createuser { get; set; }
        public DateTime? Createdate { get; set; }
        public ulong? Updateuser { get; set; }
        public DateTime? Updatedate { get; set; }

        public virtual Breed Breed { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Petdetail> Petdetails { get; set; }
    }
}
