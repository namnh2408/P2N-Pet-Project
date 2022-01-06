using System;
using System.Collections.Generic;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Petdetail
    {
        public Petdetail()
        {
            Cartitems = new HashSet<Cartitem>();
            Petimagefors = new HashSet<Petimagefor>();
        }

        public ulong Id { get; set; }
        public ulong? Petid { get; set; }
        public ulong? Colorid { get; set; }
        public ulong? Sizeid { get; set; }
        public ulong? Ageid { get; set; }
        public int? Sexid { get; set; }
        public int? Statusdetailid { get; set; }
        public float? Price { get; set; }
        public float? Discount { get; set; }
        public int? Quantity { get; set; }
        public int? Status { get; set; }
        public ulong? Createuser { get; set; }
        public DateTime? Createdate { get; set; }
        public ulong? Updateuser { get; set; }
        public DateTime? Updatedate { get; set; }

        public virtual Age Age { get; set; }
        public virtual Color Color { get; set; }
        public virtual Pet Pet { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual Size Size { get; set; }
        public virtual Statusdetail Statusdetail { get; set; }
        public virtual ICollection<Cartitem> Cartitems { get; set; }
        public virtual ICollection<Petimagefor> Petimagefors { get; set; }
    }
}
