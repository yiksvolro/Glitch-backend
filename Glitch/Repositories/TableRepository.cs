﻿using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Glitch.Repositories
{
    public class TableRepository : BaseRepository<Table>, ITableRepository
    {
        public TableRepository(GlitchContext glitchContext, ILogger<BaseRepository<Table>> logger) : base(glitchContext, logger)
        {
        }
    }
}
