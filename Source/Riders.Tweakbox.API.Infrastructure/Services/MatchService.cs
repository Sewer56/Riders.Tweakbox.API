using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Riders.Tweakbox.API.Application.Models;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Domain.Models.Database;
using Riders.Tweakbox.API.Infrastructure.Common;

namespace Riders.Tweakbox.API.Infrastructure.Services
{
    public class MatchService : IMatchService
    {
        private ApplicationDbContext _context;
        private ICurrentUserService _currentUserService;

        public MatchService(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<List<GetMatchResult>> GetAll(PaginationQuery paginationQuery, CancellationToken token)
        {
            int skip = paginationQuery.PageSize * paginationQuery.PageNumber;
            var matches = await _context.Matches.Include(x => x.Players)
                .Skip(skip)
                .Take(paginationQuery.PageSize)
                .ToListAsync(token);

            var result  = new List<GetMatchResult>(matches.Count);
            foreach (var match in matches)
                result.Add(Mapping.Mapper.Map<GetMatchResult>(match));

            return result;
        }

        /// <inheritdoc/>
        public async Task<GetMatchResult> Get(int id, CancellationToken token)
        {
            var result = await _context.Matches.Include(x => x.Players).SingleOrDefaultAsync(match => match.Id == id, token);
            if (result != null)
                return Mapping.Mapper.Map<GetMatchResult>(result);
            
            return null;
        }

        /// <inheritdoc/>
        public async Task<bool> Update(int id, PostMatchRequest item, CancellationToken token)
        {
            var existingItem = await _context.Matches.Include(x => x.Players).SingleOrDefaultAsync(x => x.Id == id, token);
            if (existingItem == null)
                return false;

            Mapping.Mapper.From(item).AdaptTo(existingItem);
            await _context.SaveChangesAsync(token);
            return true;
        }

        /// <inheritdoc/>
        public async Task<GetMatchResult> Create(PostMatchRequest item, CancellationToken token)
        {
            var match = Mapping.Mapper.Map<Match>(item);
            await _context.Matches.AddAsync(match, token);
            await _context.SaveChangesAsync(token);
            return Mapping.Mapper.Map<GetMatchResult>(item);
        }

        /// <inheritdoc/>
        public async Task<bool> Delete(int id, CancellationToken token)
        {
            var item = await _context.Matches.FindAsync(id, token);
            if (item == null)
                return false;

            _context.Matches.Remove(item);
            await _context.SaveChangesAsync(token);
            return true;
        }
    }
}
