using ReactiveUI;
using ReactiveUIXamarin.Core.Models;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUIXamarin.Core.ViewModels
{
    [DataContract]
    public class ImageTileViewModel : ReactiveObject
    {
        [IgnoreDataMember]
        public IScreen HostScreen { get; protected set; }

        [DataMember]
        public Result Image { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ImageTileViewModel"/> class.
        /// </summary>
        /// <param name="r"></param>
        public ImageTileViewModel(Result r, IScreen hostscreen = null)
        {
            this.HostScreen = hostscreen ?? Locator.Current.GetService<IScreen>();
            this.Image = r;
        }

        // Coolness: use C# 6 to define readonly property.
        public string ImagePath => "http://img.tineye.com/flickr-images/?filepath=labs-flickr-public/images/" + Image.filepath + "&size=400";
    }
}
