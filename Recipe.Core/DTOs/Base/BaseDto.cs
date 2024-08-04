namespace Recipe.Core.DTOs.Base;

public abstract class BaseDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}