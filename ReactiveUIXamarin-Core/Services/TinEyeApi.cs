using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUIXamarin.Core.Models;
using System.Net.Http;
using Fusillade;
using Newtonsoft.Json;

namespace ReactiveUIXamarin.Core.Services
{
    public class TinEyeApi : ITinEyeApi
    {
        private HttpClient httpClient;

        /// <summary>
        /// Creates a new instance of the <see cref="TinEyeApi"/> class.
        /// </summary>
        public TinEyeApi()
        {
            // Coolness: we set up the HttpClient using Fusillade.
            // Fusillade is a library to prioritize network requests
            // like Volley on Android.
            this.httpClient = new HttpClient(NetCache.UserInitiated)
            {
                BaseAddress = new Uri("http://labs.tineye.com/multicolr/rest/color_search/")
            };
        }

        /// <summary>
        /// Retrieves a list of images from TinEye api with the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="limit">The number of results.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>A Task for returning the images.</returns>
        public async Task<List<Result>> GetImages(string color, int limit, int offset)
        {
            string query = string.Format("?limit={0}&offset={1}&return_metadata=%3CuserID%2F%3E%3CphotoID%2F%3E%3CimageWidth%2F%3E%3CimageHeight%2F%3E&colors[0]={2}&weights[0]=100",
                limit, offset, color);

            try
            {
                var str = await this.httpClient.GetStringAsync(query);
                return JsonConvert.DeserializeObject<Results>(str).result;
            }
            catch
            {
                return null;
            }
        }
    }
}
