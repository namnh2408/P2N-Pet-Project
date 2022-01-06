﻿using System;
using System.Collections.Generic;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Age
    {
        public Age()
        {
            Petdetails = new HashSet<Petdetail>();
        }

        public ulong Id { get; set; }
        public string Title { get; set; }
        public int? Orderview { get; set; }
        public int? Status { get; set; }
        public ulong? Createuser { get; set; }
        public DateTime? Createdate { get; set; }
        public ulong? Updateuser { get; set; }
        public DateTime? Updatedate { get; set; }

        public virtual ICollection<Petdetail> Petdetails { get; set; }
    }
}
