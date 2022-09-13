namespace Frontend.Services; 

public static class NiceNumberFormatterService {
    private static (byte Exp, long Pow) FloorLogK(long num) {
        long init = 1000;
        byte i = 0;
        while (num >= init * 1000) {
            init *= 1000;
            i++;
        }
        return (i, init);
    }

    private static string ExpToStr(byte exp)
        => exp switch {
            0 => "Tsd.",
            1 => "Mio.",
            2 => "Mrd.",
            3 => "Bio.",
            4 => "Brd.",
            5 => "Trill.",
            _ => throw new ArgumentOutOfRangeException(nameof(exp), exp, "Not a valid long number exponent"),
        };

    private static byte DetermineDigits(int remainder)
        => remainder switch {
            >= 100 => 0,
            >= 10 => 1,
            _ => 2,
        };

    public static string NiceFormat(long num) {
        if (num < 0) {
            throw new ArgumentOutOfRangeException(nameof(num), num, "Number must be positive");
        }

        if (num < 10_000) {
            return num.ToString();
        }
        
        var (exp, pow) = FloorLogK(num);
        var sig = (float)num / pow;
        var digits = DetermineDigits((byte) sig);
        var powFmt = Math.Pow(10, digits);
        sig = (float)(Math.Floor(sig * powFmt) / powFmt);
        return sig.ToString("N" + digits) + " " + ExpToStr(exp);
    }
}
