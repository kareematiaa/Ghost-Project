using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AuthDTOs
{
   public class LoginResponseDto
    {
        public string Id { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
        public object Data { get; set; } = null!;
    }
}
