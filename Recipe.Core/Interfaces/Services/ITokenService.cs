using Recipe.Core.Entities;
using Recipe.Core.DTOs.Token;

namespace Recipe.Core.Interfaces.Services
{
    public interface ITokenService
    {
        Task<TokenDto>  CreateToken(UserApp userApp);
    }
}