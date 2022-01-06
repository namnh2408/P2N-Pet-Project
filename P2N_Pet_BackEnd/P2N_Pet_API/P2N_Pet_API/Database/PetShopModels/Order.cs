using System;
using System.Collections.Generic;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Order
    {
        public Order()
        {
            Cartitems = new HashSet<Cartitem>();
        }

        public ulong Id { get; set; }
        public ulong? Cartid { get; set; }
        public ulong? Customerid { get; set; }
        public int? Statusorderid { get; set; }
        public ulong? Totalmoney { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }
        public ulong? Createuser { get; set; }
        public DateTime? Createdate { get; set; }
        public ulong? Updateuser { get; set; }
        public DateTime? Updatedate { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Statusorder Statusorder { get; set; }
        public virtual ICollection<Cartitem> Cartitems { get; set; }
    }
}
