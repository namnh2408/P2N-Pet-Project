using System;
using System.Collections.Generic;

#nullable disable

namespace P2N_Pet_API.Database.PetShopModels
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
