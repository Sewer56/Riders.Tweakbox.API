using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Riders.Tweakbox.API.Application.Services.Base;

namespace Riders.Tweakbox.API.Application.Services
{
    public interface IMatchService : IRestService<GetMatchResult, PostMatchRequest>
    {

    }
}