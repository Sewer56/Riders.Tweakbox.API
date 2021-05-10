using System.Collections.Generic;

namespace Riders.Tweakbox.API.Application.Commands.v1.Error
{
    public class ErrorReponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}