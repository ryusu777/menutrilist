using System;

namespace Menutrilist.Helpers
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
        public bool SendEmailVerification { get; set; }
        public bool AutoEmailConfirmed { get; set; }
        public bool RequiredEmailConfirmed { get; set; }
    }
}