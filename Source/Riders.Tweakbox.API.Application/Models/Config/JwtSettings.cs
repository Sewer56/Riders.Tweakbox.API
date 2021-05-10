using System;

namespace Riders.Tweakbox.API.Application.Models.Config
{
    public class JwtSettings
    {
        /// <summary>
        /// Do not share ♥
        /// </summary>
        public string Secret { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }
}
