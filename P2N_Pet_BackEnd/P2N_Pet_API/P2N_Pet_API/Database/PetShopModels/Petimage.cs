using System;
using System.Collections.Generic;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Petimage
    {
        public Petimage()
        {
            Petimagefors = new HashSet<Petimagefor>();
        }

        public ulong Id { get; set; }
        public string Image { get; set; }
        public int? Status { get; set; }
        public ulong? Createuser { get; set; }
        public DateTime? Createdate { get; set; }
        public ulong? Updateuser { get; set; }
        public DateTime? Updatedate { get; set; }

        public virtual ICollection<Petimagefor> Petimagefors { get; set; }
    }
}
