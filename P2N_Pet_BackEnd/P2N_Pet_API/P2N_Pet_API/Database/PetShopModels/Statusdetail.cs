using System;
using System.Collections.Generic;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Statusdetail
    {
        public Statusdetail()
        {
            Petdetails = new HashSet<Petdetail>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Petdetail> Petdetails { get; set; }
    }
}
