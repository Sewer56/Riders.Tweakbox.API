using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MaxMind.Db;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using Microsoft.Extensions.Logging;
using Riders.Tweakbox.API.Application.Models.Config;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Infrastructure.Helpers;
using Riders.Tweakbox.API.Infrastructure.Services.GeoIp;
using SharpCompress.Archives;

namespace Riders.Tweakbox.API.Infrastructure.Services
{
    public class GeoIpService : IGeoIpService, IDisposable
    {
        private static readonly string DefaultDatabasePath = $"{Path.GetDirectoryName(typeof(GeoIpService).Assembly.Location)}/Assets/GeoLite2-City.mmdb.tar.xz";
        private static readonly string ExtractedDatabasePath = $"{Path.GetDirectoryName(typeof(GeoIpService).Assembly.Location)}/Assets/GeoIP.Database.Default";

        private GeoIpSettings _settings;
        private IDateTimeService _dateTimeService;
        private CronJobRunner _runner;
        private DatabaseReader _databaseReader;
        private Task _updateDatabaseTask;
        private ILogger<GeoIpService> _logger;
        private string _currentDbLocation;

        public GeoIpService(GeoIpSettings settings, IDateTimeService dateTimeService, ILogger<GeoIpService> logger)
        {
            _settings = settings;
            _dateTimeService = dateTimeService;
            _logger = logger;
            _runner = new CronJobRunner(settings.CronUpdateScheduleUtc, UpdateCallback, _dateTimeService);
            
            // Clean from older downloaded DBs
            GeoIpUpdateHelpers.TryCleanDirectory();
            LoadDefaultDatabase();
            UpdateCallback();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _runner?.Dispose();
            _databaseReader?.Dispose();
            _updateDatabaseTask?.Dispose();
        }

        private void LoadDefaultDatabase()
        {
            if (GeoIpUpdateHelpers.TryFindExistingArchive(ExtractedDatabasePath, out var path))
                SetNewDatabaseLocation(path);
            else
            {
                var defaultDbPath = GeoIpUpdateHelpers.DecompressArchive(DefaultDatabasePath, ExtractedDatabasePath);
                SetNewDatabaseLocation(defaultDbPath);
            }
        }

        public async Task<bool> UpdateDatabaseAsync()
        {
            string databasePath;
            try
            {
                // Download
                databasePath = await GeoIpUpdateHelpers.DownloadAsync(_settings.LicenseKey);
                if (databasePath == null)
                {
                    _logger?.LogWarning($"Failed to Download Database!");
                    return false;
                }
            
                SetNewDatabaseLocation(databasePath);
            }
            catch (Exception e)
            {
                _logger?.LogInformation($"Failed to Update The GeoIP Database\n" +
                                        $"{e.Message}");
                return false;
            }

            return true;
        }

        private void SetNewDatabaseLocation(string databasePath)
        {
            var reader = new DatabaseReader(databasePath, FileAccessMode.MemoryMapped);

            // Dispose Last
            _databaseReader?.Dispose();
            if (!string.IsNullOrEmpty(_currentDbLocation))
                File.Delete(_currentDbLocation);

            // Set New
            _databaseReader    = reader;
            _currentDbLocation = databasePath;
        }

        private void UpdateCallback()
        {
            // Do not update.
            if (string.IsNullOrEmpty(_settings.LicenseKey))
            {
                _logger?.LogInformation($"No License Key Specified, Skipping Update");
                return;
            }

            _updateDatabaseTask?.Wait();
            _updateDatabaseTask?.Dispose();
            _updateDatabaseTask = Task.Run(UpdateDatabaseAsync);
        }

        /// <inheritdoc />
        public CityResponse GetDetails(IPAddress address)
        {
            // No Database :/
            if (_databaseReader == null) 
                return null;

            // In case of updates.
            if (_updateDatabaseTask?.Status == TaskStatus.Running)
                _updateDatabaseTask.Wait();

            // Fetch from database.
            try
            {
                return _databaseReader.City(address);
            }
            catch (Exception e)
            {
                _logger?.LogInformation($"Failed to find IP Details for {address} " +
                                        $"Exception: ");

                Console.WriteLine(e);
                return null;
            }
        }
    }
}
