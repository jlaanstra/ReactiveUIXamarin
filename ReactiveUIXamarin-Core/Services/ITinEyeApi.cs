using ReactiveUIXamarin.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUIXamarin.Core.Services
{
    /// <summary>
    /// Interface for the TinEye api.
    /// </summary>
    public interface ITinEyeApi
    {
        /// <summary>
        /// Retrieves a list of images from TinEye api with the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="limit">The number of results.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>A Task for returning the images.</returns>
        Task<List<Result>> GetImages(string color, int limit, int offset);
    }

    public static class TinEyeApiExtensions
    {
        /// <summary>
        /// Extension method to retrieve a list of images based on the specified Color.
        /// </summary>
        /// <param name="This">An instance of the TinEye api.</param>
        /// <param name="color">The color object.</param>
        /// <returns>A Task for returning the images.</returns>
        public static Task<List<Result>> GetImagesForColor(this ITinEyeApi This, Color color)
        {
            return This.GetImages(string.Format("{0:x2}{1:x2}{2:x2}", color.R, color.G, color.B), 73, 0);
        }
    }
}
