using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;
using Recipe.Core.Exceptions;

// using Recipe.Core.Interfaces.Mappings;

namespace Recipe.Infrastructure.Mappings
{
    public static class UserMapper
    {
        public static UserAppDto? ToDto(UserApp? entity)
        {
            if (entity is null) return null;

            return new UserAppDto
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Name = entity.Name,
                Surname = entity.Surname,
                Email = entity.Email
            };
        }

        public static UserAppSummaryDto ToSummaryDto(UserApp entity)
        {
            if (entity is null) throw new NotFoundException("User not found in ToMinimalDto Mapper");
            ;

            return new UserAppSummaryDto
            {
                Id = entity.Id,
                UserName = entity.UserName
            };
        }

        public static UserApp? ToEntity(UserAppDto? userAppDto)
        {
            if (userAppDto is null) return null;

            return new UserApp
            {
                Id = userAppDto.Id,
                UserName = userAppDto.UserName,
                Name = userAppDto.Name,
                Surname = userAppDto.Surname,
                Email = userAppDto.Email
            };
        }
    }
}