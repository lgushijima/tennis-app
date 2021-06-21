using System;

namespace TennisApi.Shared.Configs
{
    public class AuthConfig
    {
        public Guid ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
    }
}
