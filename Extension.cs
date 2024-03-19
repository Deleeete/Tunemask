using System.Drawing;

namespace Tunemask;

internal static class Extension
{
    public static bool IsBlack(this Color color)
    {
        return (color.ToArgb() & 0xFFFFFF) == 0;
    }
    public static bool IsNotBlack(this Color color)
    {
        return (color.ToArgb() & 0xFFFFFF) != 0;
    }
}
