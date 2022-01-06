using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace P2N_Pet_API.Models.User
{
    public class UserChangePasswordModel
    {
        public string oldpassword { get; set; }
        public string newpassword { get; set; }
    }
}
