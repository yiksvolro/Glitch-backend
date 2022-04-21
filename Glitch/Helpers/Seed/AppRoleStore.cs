using Glitch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Glitch.Helpers.Seed
{
    public class AppRoleStore : RoleStore<AppRole, GlitchContext, int>
    {
        public AppRoleStore(GlitchContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
