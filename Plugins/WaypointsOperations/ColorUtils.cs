using System.Collections.Generic;
using System.Drawing;

namespace MissionActionsPlugin
{
    public class ColorUtils
    {
        public static List<(Color, Color)> GeneratePairPalette(int n)
        {
            var palette = new List<(Color, Color)>();
            for (var i = 0; i < n; i++)
            {
                var h = ((double)i) / n;
                var s = 1.0;
                
                var light = FromHsl(h, s, .65);
                var dark = FromHsl(h, s, .35);
                
                palette.Add((light, dark));
            }
            
            return palette;
        }

        private static Color FromHsl(double h, double s, double l)
        {
            double r, g, b;

            if (s == 0)
            {
                r = g = b = l; // achromatic
            }
            else
            {
                double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                double p = 2 * l - q;
                r = HueToRgb(p, q, h + 1.0 / 3);
                g = HueToRgb(p, q, h);
                b = HueToRgb(p, q, h - 1.0 / 3);
            }

            return Color.FromArgb(
                (int)(r * 255),
                (int)(g * 255),
                (int)(b * 255)
            );
        }

        public static string HslToHex(double h, double s, double l)
        {
            var color = FromHsl(h, s, l);
            return RgbToHex(color.R, color.G, color.B);
        }

        private static string RgbToHex(double r, double g, double b, double a = 1.0)
        {
            var alpha = (int)(a * 255);
            var red = (int)(r * 255);
            var green = (int)(g * 255);
            var blue = (int)(b * 255);
            
            return $"{alpha:X2}{red:X2}{green:X2}{blue:X2}";
        }

        private static double HueToRgb(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1.0 / 6) return p + (q - p) * 6 * t;
            if (t < 1.0 / 2) return q;
            if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
            return p;
        }
    }
}