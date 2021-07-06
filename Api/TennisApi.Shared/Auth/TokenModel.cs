using System;
using System.Collections.Generic;
using System.Text;

namespace TennisApi.Shared.Auth
{
    public class TokenModel
    {
        public Guid IDLogin { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string AccessToken { get; set; }
    }
}
