using Microsoft.AspNetCore.Identity;

namespace Glitch.Helpers.Seed
{
    public sealed class AppRole : IdentityRole<int>
    {
        public AppRole()
        {

        }
        public AppRole(string roleName)
        {
            Name = roleName;
            NormalizedName = roleName.ToUpper();
        }
    }
}
