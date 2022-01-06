using System;
using System.Collections.Generic;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Cart
    {
        public Cart()
        {
            Cartitems = new HashSet<Cartitem>();
            Orders = new HashSet<Order>();
        }

        public ulong Id { get; set; }
        public ulong? Userid { get; set; }
        public int? Status { get; set; }
        public ulong? Createuser { get; set; }
        public DateTime? Createdate { get; set; }
        public ulong? Updateuser { get; set; }
        public DateTime? Updatedate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Cartitem> Cartitems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
