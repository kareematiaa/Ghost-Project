using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utilities
{
    public static class Base64Utils
    {
        public static string GetMimeTypeFromBase64(string base64String)
        {
            var data = base64String.Substring(0, 5);
            return data.ToUpper() switch
            {
                "IVBOR" => "image/png",
                "/9J/4" => "image/jpeg",
                "R0LGO" => "image/gif",
                _ => throw new ArgumentException("Unsupported base64 image format"),
            };
        }
    }
}
