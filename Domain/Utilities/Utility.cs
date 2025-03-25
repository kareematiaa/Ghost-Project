using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utilities
{
    public static class ClaimsUtility
    {
        public const string Token = "token";
        public const string Otp = "otp";
        public const string Id = "id";
        public const string Role = "role";
        public const string Expired = "exp";
    }
    public static class SettingsUtility {

        /// <summary>
        /// Minimum Quantity of Product to Notify Store
        /// </summary>
        public const int MinQuantity = 10;
        /// <summary>
        /// Number of Elements that take from a list
        /// </summary>
        public const int Top = 10;
    }
}
