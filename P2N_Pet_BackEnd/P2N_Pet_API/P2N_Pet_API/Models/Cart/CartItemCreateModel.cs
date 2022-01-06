using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace P2N_Pet_API.Models.Cart
{
    public class CartItemCreateModel
    {
        public ulong PetDetailId { get; set; }

        [DefaultValue(1)]
        public int Quantity { get; set; }

        [JsonIgnore]
        public float PriceDiscount { get; set; }
    }
}
