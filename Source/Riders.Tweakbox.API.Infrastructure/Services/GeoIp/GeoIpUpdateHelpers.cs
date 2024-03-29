﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using SharpCompress.Readers;

namespace Riders.Tweakbox.API.Infrastructure.Services.GeoIp
{
    public static class GeoIpUpdateHelpers
    {
        private const string Format    = "https://download.maxmind.com/app/geoip_download?edition_id=GeoLite2-City&license_key={0}&suffix=tar.gz";
        private const string Extension = ".mmdb";
        private static readonly string DatabaseDownloadPath = $"{Path.GetDirectoryName(typeof(GeoIpUpdateHelpers).Assembly.Location)}/Assets/GeoIP.Database.Update";
           
        /// <summary>
        /// Downloads the current City database.
        /// </summary>
        /// <param name="licenseKey">The user license key.</param>
        /// <returns>Path to the database.</returns>
        public static async Task<string> DownloadAsync(string licenseKey)
        {
            // Download
            string downloadPath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName()) + ".tar.gz";
            using var webClient = new WebClient();
            string downloadUrl  = string.Format(Format, licenseKey);
            await webClient.DownloadFileTaskAsync(new Uri(downloadUrl), downloadPath);

            // Extract 
            var extracted = DecompressArchive(downloadPath, null);
            File.Delete(downloadPath);

            return extracted;
        }

        /// <summary>
        /// Tries to clean the download directory.
        /// </summary>
        public static void TryCleanDirectory()
        {
            try
            {
                var directories = Directory.GetDirectories(DatabaseDownloadPath);
                foreach (var directory in directories)
                {
                    try { Directory.Delete(directory, true); }
                    catch (Exception e) { /* Ignore */ }
                }
            }
            catch (Exception e) { /* Ignore */ }
        }

        /// <summary>
        /// Searches for an existing unpacked database in a given folder.
        /// </summary>
        /// <param name="directory">The folder to search.</param>
        /// <param name="archiveFile">Path to the found archive.</param>
        /// <returns>The archive file.</returns>
        public static bool TryFindExistingArchive(string directory, out string archiveFile)
        {
            Directory.CreateDirectory(directory);
            var files = Directory.GetFiles(directory, $"*{Extension}");
            if (files.Length > 0)
            {
                archiveFile = files[0];
                return true;
            }

            archiveFile = null;
            return false;
        }

        /// <summary>
        /// Decompresses an archive containing a GeoIp Database.
        /// </summary>
        /// <param name="archivePath">Path to the archive file.</param>
        /// <param name="outputPath">The folder to put the resulting file in. Autogenerated if null.</param>
        /// <returns>The archive file.</returns>
        [ExcludeFromCodeCoverage] // This gets executed only after a clean build and extracted result cached for faster test runtime.
        public static string DecompressArchive(string archivePath, string outputPath)
        {
            outputPath ??= Path.Combine(DatabaseDownloadPath, Path.GetRandomFileName());
            Directory.CreateDirectory(outputPath);

            using Stream stream = File.OpenRead(archivePath);
            using var reader   = ReaderFactory.Open(stream);

            while (reader.MoveToNextEntry())
            {
                var entry = reader.Entry;
                if (entry.IsDirectory || !entry.Key.EndsWith(Extension, StringComparison.OrdinalIgnoreCase)) 
                    continue;

                var location = Path.Combine(outputPath, entry.Key);
                Directory.CreateDirectory(Path.GetDirectoryName(location));
                reader.WriteEntryTo(location);
                return location;
            }

            return "";
        }
    }
}
