using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Domain.NadinSoft.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }
        public ApplicationUser(string nationalcode)
        {
            Nationalcode = nationalcode;
        }

        public string Nationalcode { get;  set; }
        public List<Product> Products { get;  set; } = new List<Product>();
    }
}
