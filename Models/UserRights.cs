namespace Models; 

[Flags]
public enum UserRights {
    CreateWSE = 1 << 0,
    DeleteWSE = 1 << 1,

    None = 0,
    Default = CreateWSE,
    Admin = Default | DeleteWSE,
}
