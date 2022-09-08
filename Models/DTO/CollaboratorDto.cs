namespace Models.DTO; 

public class CollaboratorDto {
    public long WseId { get; set; }
    public long UserId { get; set; }
    public WseRights Rights { get; set; }

    public static implicit operator CollaboratorDto(Collaborator collaborator)
        => new() {UserId = collaborator.UserId, WseId = collaborator.WseId, Rights = collaborator.Rights};
}
