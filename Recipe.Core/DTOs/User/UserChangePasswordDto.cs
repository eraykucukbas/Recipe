
namespace Recipe.Core.DTOs.User
{
    public class UserChangePasswordDto
    {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}