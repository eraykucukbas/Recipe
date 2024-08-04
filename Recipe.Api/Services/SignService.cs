using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Recipe.API.Services
{
    public static class SignService
    {
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            if (string.IsNullOrEmpty(securityKey))
            {
                throw new ArgumentNullException(nameof(securityKey), "Security key cannot be null or empty");
            }

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            
        }
    }
}