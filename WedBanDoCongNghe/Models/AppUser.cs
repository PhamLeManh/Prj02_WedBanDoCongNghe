using Microsoft.AspNetCore.Identity;

namespace WedBanDoCongNghe.Models
{
    public class AppUser : IdentityUser
    {
        public string HoTen { get; set; }
    }
}
