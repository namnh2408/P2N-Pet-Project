using System;
using System.Collections.Generic;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Petimagefor
    {
        public ulong Id { get; set; }
        public ulong Petdetailid { get; set; }
        public ulong Petimageid { get; set; }
        public int? Status { get; set; }
        public ulong? Createuser { get; set; }
        public DateTime? Createdate { get; set; }
        public ulong? Updateuser { get; set; }
        public DateTime? Updatedate { get; set; }

        public virtual Petdetail Petdetail { get; set; }
        public virtual Petimage Petimage { get; set; }
    }
}
