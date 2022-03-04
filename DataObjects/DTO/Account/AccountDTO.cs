using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4kTiles_Frontend.DataObjects.DTO.Account
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string Email { get; set; } = null!;
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
