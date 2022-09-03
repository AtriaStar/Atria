using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class TagDto {
    [RegularExpression(@"^[a-zA-Z0-9-.]*(,[a-zA-Z0-9-.]+)*$", ErrorMessage = "Tags are not separated with a comma.")]
    public string? NewTagsStr { get; set; }
}
