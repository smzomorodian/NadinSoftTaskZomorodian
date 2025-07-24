using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Domain.NadinSoft.Model
{
    public class ApplicationUser : IdentityUser
    {
        private ApplicationUser() { }

        public ApplicationUser(string nationalcode) : base(nationalcode)
        {
            Nationalcode = nationalcode;
        }

        public string Nationalcode { get; private set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }

}
