using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Cartitem
    {
        public ulong Id { get; set; }
        public ulong? Cartid { get; set; }
        public ulong? Petdetailid { get; set; }
        public ulong? Orderid { get; set; }
        public float? Pricediscount { get; set; }
        public int? Quantity { get; set; }
        public int? Status { get; set; }
        public ulong? Createuser { get; set; }
        public DateTime? Createdate { get; set; }
        public ulong? Updateuser { get; set; }
        public DateTime? Updatedate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Cart Cart { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Order Order { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Petdetail Petdetail { get; set; }
    }
}
