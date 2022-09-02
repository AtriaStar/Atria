namespace Models; 

[Flags]
public enum UserRights {
    CreateWse = 1 << 0,
    ModerateTags = 1 << 1,

    DeleteWseOverride = 1 << 16,

    None = 0,
    Default = CreateWse,
    Admin = Default | ModerateTags | DeleteWseOverride,
}
