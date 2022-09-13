namespace Frontend.Services; 

public static class NiceTimeSpanFormatterService {
    private static string BeforeX(int value, string x, string extra)
        => $"Vor {value} {x}{(value == 1 ? "" : extra)}";

    public static string NiceFormat(TimeSpan time) {
        if (time.Ticks < 0) {
            throw new ArgumentOutOfRangeException(nameof(time), time, "Time must be positive");
        }

        const double AVG_DAYS_PER_MONTH = 365.25 / 12;

        if (time.TotalSeconds < 1) { return "Vor wenigen Momenten"; }
        if (time.TotalMinutes < 1) { return BeforeX((int)time.TotalSeconds, "Sekunde", "n"); }
        if (time.TotalHours < 1) { return BeforeX((int)time.TotalMinutes, "Minute", "n"); }
        if (time.TotalDays < 1) { return BeforeX((int)time.TotalHours, "Stunde", "n"); }

        return time.TotalDays switch {
            < 1 => BeforeX((int)time.TotalHours, "Stunde", "n"),
            < 31 => BeforeX((int)time.TotalDays, "Tag", "en"),
            < 365 => BeforeX((int)(time.TotalDays / AVG_DAYS_PER_MONTH), "Monat", "e"),
            _ => BeforeX((int)(time.TotalDays / 365.25), "Jahr", "e"),
        };
    }
}
