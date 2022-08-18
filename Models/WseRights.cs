namespace Models;

[Flags]
public enum WseRights {
    EditData = 1 << 0,
    RandomTest = 1 << 1,

    None = 0,
    Default = EditData,
    Owner = Default | RandomTest,
}
