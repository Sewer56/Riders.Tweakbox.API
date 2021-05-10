using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Riders.Tweakbox.API.Application.Commands.v1.Browser;
using Riders.Tweakbox.API.Application.Commands.v1.Browser.Result;
using Riders.Tweakbox.API.Application.Models;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Domain.Common;
using Riders.Tweakbox.API.Domain.Models.Memory;
using StructLinq;

namespace Riders.Tweakbox.API.Infrastructure.Services
{
    /// <summary>
    /// Singleton service that keeps track of current servers.
    /// </summary>
    public class ServerBrowserService : IServerBrowserService, IDisposable
    {
        public IDateTimeService  _dateTimeService;
        private Dictionary<ServerAddressPortPair, ServerInfo> _infos = new Dictionary<ServerAddressPortPair, ServerInfo>();
        private Timer _refreshTimer;

        public ServerBrowserService(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
            _refreshTimer = new Timer(RefreshServerList, null, TimeSpan.Zero, TimeSpan.FromSeconds(Constants.ServerBrowser.DeletedServerPollTimeSeconds));
        }

        /// <inheritdoc />
        public GetAllServerResult GetAll()
        {
            var infos = _infos.ToStructEnumerable().Select(x => Mapping.Mapper.Map<GetServerResult>(x.Value), x => x);
            return new GetAllServerResult(infos.ToEnumerable());
        }

        /// <inheritdoc />
        public ServerCreatedResult CreateOrRefresh(IPAddress source, PostServerRequest item)
        {
            var pair     = new ServerAddressPortPair() { Address = source, Port = item.Port };
            var hasValue = _infos.TryGetValue(pair, out var existing);
            if (!hasValue)
                existing = new ServerInfo() { Address = source.ToString() };

            existing.LastRefreshTime = _dateTimeService.GetCurrentDateTime();
            existing.Id = Guid.NewGuid();
            Mapping.Mapper.From(item).AdaptTo(existing);
            _infos[pair] = existing;

            return Mapping.Mapper.Map<ServerCreatedResult>(existing);
        }

        /// <inheritdoc />
        public bool Delete(IPAddress source, int port, Guid id)
        {
            var pair = new ServerAddressPortPair() { Address = source, Port = port };
            if (_infos.TryGetValue(pair, out var value) && value.Id == id)
            {
                _infos.Remove(pair);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public void Dispose() => _refreshTimer?.Dispose();

        /// <summary>
        /// Removes dead servers.
        /// This is automatically triggered at a fixed time interval.
        /// </summary>
        public void RefreshServerList(object? state)
        {
            var itemsToRemove = new List<ServerAddressPortPair>(1024);

            foreach (var info in _infos)
            {
                var expiry      = info.Value.LastRefreshTime;
                var timeElapsed = _dateTimeService.GetCurrentDateTime() - expiry;
                if (timeElapsed > TimeSpan.FromSeconds(Constants.ServerBrowser.RefreshTimeSeconds))
                    itemsToRemove.Add(info.Key);
            }

            foreach (var item in itemsToRemove)
                _infos.Remove(item);
        }
    }
}
