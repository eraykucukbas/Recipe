namespace Recipe.Core.DTOs.Token
{
    public class TokenDto
    {
        public string AccessToken { get; set; } = null!;

        public DateTime AccessTokenExpiration { get; set; }

        public string RefreshToken { get; set; } = null!;

        public DateTime RefreshTokenExpiration { get; set; }
    }
}