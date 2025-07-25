//using Domain.Users;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;

//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public class JwtService : IJwtService
//    {
//        private readonly UserManager<AppUser> _userManager;
//        private readonly JwtConfig _jwtConfig;

//        public JwtService(UserManager<AppUser> userManager, IOptions<JwtConfig> jwtConfig)
//        {
//            _userManager = userManager;
//            _jwtConfig = jwtConfig.Value;
//        }

//        public async Task<string> GenerateJwtToken(AppUser user)
//        {
//            var userClaims = await _userManager.GetClaimsAsync(user);
//            var userRoles = await _userManager.GetRolesAsync(user);
//            var roleClaims = new List<Claim>();

//            foreach (var role in userRoles)
//                userClaims.Add(new(AppUtility.Role, role));

//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//            new Claim(AppUtility.Id, user.Id),
//            new Claim("fullName", user.FullName),
//            new Claim("email", user.Email),
//            new Claim("phoneNumber", user.PhoneNumber),
//            new Claim(JwtRegisteredClaimNames.AuthTime, _jwtConfig.LoginDays.ToString())
//        }
//            .Union(userClaims)
//            .Union(roleClaims);

//            var jwtToken = new JwtSecurityToken(_jwtConfig.Issuer, _jwtConfig.Audience, claims,
//                 expires: DateTime.Now.AddDays(_jwtConfig.LoginDays),
//                 signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
//        }
//    }

//}
