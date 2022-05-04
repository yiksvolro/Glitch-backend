using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Glitch.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GlitchContext glitchContext, ILogger<BaseRepository<User>> logger) : base(glitchContext, logger)
        {
        }
    }
}
