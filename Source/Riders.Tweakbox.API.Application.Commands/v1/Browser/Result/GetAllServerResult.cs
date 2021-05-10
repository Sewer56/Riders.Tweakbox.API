using System.Collections.Generic;

namespace Riders.Tweakbox.API.Application.Commands.v1.Browser.Result
{
    public class GetAllServerResult
    {
        /// <summary>
        /// A list of server entries.
        /// </summary>
        public IEnumerable<GetServerResult> Results { get; set; }

        public GetAllServerResult(IEnumerable<GetServerResult> results)
        {
            Results = results;
        }
    }
}
