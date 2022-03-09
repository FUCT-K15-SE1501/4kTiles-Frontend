using System;
using System.Collections.Generic;

namespace _4kTiles_Frontend.MVVM.Models.Account
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string Email { get; set; } = null!;
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
