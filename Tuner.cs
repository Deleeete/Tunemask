using System.Drawing;

namespace Tunemask;

internal static class Tune
{
    public static void ExpandMask(Color[,] pixels, int radius)
    {
        int width = pixels.GetLength(0);
        int height = pixels.GetLength(1);
        bool[,] isBlack = new bool[width, height];
        Parallel.For(0, width, x =>
        {
            for (int y = 0; y < height; y++)
            {
                isBlack[x, y] = (pixels[x, y].ToArgb() & 0xFFFFFF) == 0;
            }
        });
        List<Point> edgePoints = new(pixels.Length);
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                if (isBlack[x, y]) // Skip non-mask pixels
                    continue;
                if (
                    isBlack[x - 1, y - 1] ||
                    isBlack[x - 1, y] ||
                    isBlack[x - 1, y + 1] ||
                    isBlack[x, y - 1] ||
                    isBlack[x, y + 1] ||
                    isBlack[x + 1, y - 1] ||
                    isBlack[x + 1, y] ||
                    isBlack[x + 1, y + 1])
                    DrawCircleAt(pixels, width, height, x, y, radius, Color.White);
            }
        }
    }

    public static void DrawCircleAt(Color[,] pixels, int width, int height, int cx, int cy, int radius, Color color)
    {
        int x_min = Math.Max(0, cx - radius);
        int y_min = Math.Max(0, cy - radius);
        int x_max = Math.Min(cx + radius, width);
        int y_max = Math.Min(cy + radius, height);
        for (int x = x_min; x < x_max; x++)
        {
            for (int y = y_min; y < y_max; y++)
            {
                int dx = cx - x;
                int dy = cy - y;
                if (dx * dx + dy * dy <= radius * radius)
                    pixels[x, y] = color;
            }
        }
    }

    public static void SetPoints(Color[,] pixels, Point[] points, Color color)
    {
        Parallel.For(0, points.Length, i =>
        {
            pixels[points[i].X, points[i].Y] = color;
        });
    }
}
