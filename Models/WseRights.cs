namespace Models;

[Flags]
public enum WseRights
{
    EditData = 1 << 0,
    EditCollaborators = 1 << 1,
    DeleteWse = 1 << 2,

    Default = EditData,
    Owner = Default | EditCollaborators | DeleteWse,
}