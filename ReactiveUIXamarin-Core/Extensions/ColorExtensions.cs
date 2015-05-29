using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReactiveUIXamarin.Core
{
    public static class ColorExtensions
    {
        public static Color ToNative(this System.Drawing.Color This)
        {
            return Color.FromRgba(This.R, This.G, This.B, This.A);
        }
    }
}
