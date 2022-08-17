using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models; 

public class Collaborator {
    // TODO: Investigate shadow property as primary key using data annotations (or don't, who cares)
    [Key, ForeignKey(nameof(User))]
    public long UserId { get; set; }

    public User User { get; set; } = null!;
    public WseRights Role { get; set; }
}
