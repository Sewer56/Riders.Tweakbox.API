using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.SDK.Common;

namespace Riders.Tweakbox.API.SDK.Handler
{
    /// <summary>
    /// A handler that returns exceptions as <see cref="Riders.Tweakbox.API.Application.Commands.v1.Error.ErrorResponse"/>.
    /// </summary>
    public class ReturnExceptionAsErrorHandler : DelegatingHandler
    {
        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                // Get outer exceptions
                var errors = new List<string>();
                errors.Add(e.Message);

                // Get Inner Exceptions
                var ex = e.InnerException;
                while (ex != null)
                {
                    errors.Add(ex.Message);
                    ex = ex.InnerException;
                }

                var error = new ErrorReponse() { Errors = errors };
                
                return new HttpResponseMessage()
                {
                    Content = new StringContent(JsonSerializer.Serialize(error, RefitConstants.SerializerOptions)),
                    RequestMessage = request,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        }
    }
}
