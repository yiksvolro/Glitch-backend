using Glitch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Glitch.Helpers.Seed
{
    public class AppUserStore : UserStore<User, AppRole, GlitchContext, int>
    {
        public AppUserStore(GlitchContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
