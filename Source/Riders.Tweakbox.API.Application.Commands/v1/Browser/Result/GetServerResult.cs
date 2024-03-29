﻿
using System.Collections.Generic;
using Riders.Tweakbox.API.Application.Commands.v1.User;

namespace Riders.Tweakbox.API.Application.Commands.v1.Browser.Result
{
    /// <summary>
    /// Gets a listing of all active servers.
    /// </summary>
    public class GetServerResult
    {
        /// <summary>
        /// Delimiter for the list of mods in the <see cref="Mods"/> field.
        /// </summary>
        public const char ModsDelimiter = ';';

        /// <summary>
        /// IP Address of the Server Host.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Name of the server.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The port the host is listening on.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The type associated with this server.
        /// </summary>
        public MatchTypeDto Type { get; set; }

        /// <summary>
        /// The built-in game mode assigned to this match.
        /// </summary>
        public GameModeDto GameMode { get; set; }

        /// <summary>
        /// The country associated with this server.
        /// Derived from the IP Address.
        /// </summary>
        public CountryDto Country { get; set; }

        /// <summary>
        /// True if there is a password in the hosted game.
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// A list of semicolon ';' separated mods.
        /// In alphabetical order (ordinal ignore case).
        /// </summary>
        public string Mods { get; set; }

        /// <summary>
        /// List of players from the server.
        /// </summary>
        public List<ServerPlayerInfoResult> Players { get; set; }
    }
}
