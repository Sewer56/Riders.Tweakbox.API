using Refit;
using Riders.Tweakbox.API.SDK.Interfaces;

namespace Riders.Tweakbox.API.SDK
{
    /// <summary>
    /// Encapsulates all accesses to the Tweakbox API.
    /// </summary>
    public class TweakboxApi
    {
        public IIdentity Identity        { get; private set; }
        public IMatchApi Match           { get; private set; }
        public IServerBrowserApi Browser { get; private set; }

        private string _url;

        public TweakboxApi(string url)
        {
            _url = url;
            Identity = MakeRestService<IIdentity>();
            Match    = MakeRestService<IMatchApi>();
            Browser  = MakeRestService<IServerBrowserApi>();
        }

        private TService MakeRestService<TService>()
        {
            return RestService.For<TService>(_url);
        }
    }
}
