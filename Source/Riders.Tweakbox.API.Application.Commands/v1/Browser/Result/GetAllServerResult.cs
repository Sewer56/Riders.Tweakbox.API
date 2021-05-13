using System.Collections.Generic;

namespace Riders.Tweakbox.API.Application.Commands.v1.Browser.Result
{
    public class GetAllServerResult
    {
        /// <summary>
        /// A list of server entries.
        /// </summary>
        public List<GetServerResult> Results { get; set; }

        public GetAllServerResult(List<GetServerResult> results)
        {
            Results = results;
        }
    }
}
