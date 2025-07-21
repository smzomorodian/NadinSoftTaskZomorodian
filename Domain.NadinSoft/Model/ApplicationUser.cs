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

        public string Nationalcode { get; private set; }
        public List<Product> Products { get; private set; } = new List<Product>();
    }
}
