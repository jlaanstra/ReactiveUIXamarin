using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUIXamarin.Core.Helpers
{
    public class ColorHelper
    {
        /// <summary>
        /// Creates a Color from three integers if possible.
        /// </summary>
        /// <param name="red">Integer representing red.</param>
        /// <param name="green">Integer representing green.</param>
        /// <param name="blue">Integer representing blue.</param>
        /// <returns>A Color or null.</returns>
        public static Color? FromIntegers(int red, int green, int blue)
        {
            //make sure the integeres fit in a byte.
            byte? r = red < 0 || red > 255 ? (byte?)null : (byte?)red;
            byte? g = green < 0 || green > 255 ? (byte?)null : (byte?)green;
            byte? b = blue < 0 || blue > 255 ? (byte?)null : (byte?)blue;
            
            if (r == null || g == null || b == null) return null;
            return Color.FromArgb(0xFF, r.Value, g.Value, b.Value);
        }
    }
}
