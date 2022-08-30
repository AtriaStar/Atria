namespace Models; 

[Flags]
public enum UserRights {
    CreateWse = 1 << 0,
    DeleteWse = 1 << 1,

    None = 0,
    Default = CreateWse,
    Admin = Default | DeleteWse,
}
