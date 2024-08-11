using Recipe.Core.DTOs.Comment;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.Entities;

namespace Recipe.Infrastructure.Mappings
{
    public static class CommentMapper
    {
        public static CommentDto? ToDto(Comment? entity)
        {
            if (entity is null) return null;

            return new CommentDto
            {
                Id = entity.Id,
                Message = entity.Message,
                User = UserMapper.ToSummaryDto(entity.User),
                Rate = entity.Rate,
                ParentComment = entity.ParentComment,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }   
        public static CommentDto? ToDtoWithRecipe(Comment? entity)
        {
            if (entity is null) return null;

            return new CommentDto
            {
                Id = entity.Id,
                Message = entity.Message,
                User = UserMapper.ToSummaryDto(entity.User),
                Recipe = RecipeMapper.ToSummaryDto(entity.Recipe),
                Rate = entity.Rate,
                ParentComment = entity.ParentComment,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }


        public static List<CommentDto?>? ToListDtoWithRecipe(IEnumerable<Comment>? entities)
        {
            return entities?.Select(ToDtoWithRecipe).ToList();
        }
        
        public static List<CommentDto?>? ToListDto(IEnumerable<Comment>? entities)
        {
            return entities?.Select(ToDto).ToList();
        }

        public static Comment ToEntity(CommentCreateDto commentCreateDto)
        {
            return new Comment
            {
                UserId = commentCreateDto.UserId,
                Message = commentCreateDto.Message,
                RecipeId = commentCreateDto.RecipeId,
                ParentComment = commentCreateDto.ParentComment,
                Rate = commentCreateDto.Rate
            };
        }

        public static void ToEntity(CommentUpdateDto commentUpdateDto, Comment entity)
        {
            entity.Message = commentUpdateDto.Message;
            entity.Rate = commentUpdateDto.Rate;
        }
    }
}