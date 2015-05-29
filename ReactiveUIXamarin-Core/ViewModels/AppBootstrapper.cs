using ModernHttpClient;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReactiveUIXamarin.Core.ViewModels
{
    // Coolness: This class and anything under it will automatically get
    // saved and restored by ReactiveUI. This is a great place to put all
    // of your startup code - think of it as the "ViewModel for your app".
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.
        public RoutingState Router { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AppBootstrapper"/> class.
        /// </summary>
        public AppBootstrapper()
        {
            Router = new RoutingState();
            // Coolness: ReactiveUI uses Splat for its DI container. The
            // built-in one is super simple and easy to use, but it is also
            // possible to plugin your own awesome DI container.
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            // Coolness: Set up Fusillade and ModernHttpClient
            //
            // Fusillade is a super cool library that will make it so that whenever
            // we issue web requests, we'll only issue 4 concurrently, and if we
            // end up issuing multiple requests to the same resource, it will
            // de-dupe them. We're saying here, that we want our *backing*
            // HttpMessageHandler to be ModernHttpClient.
            Locator.CurrentMutable.RegisterConstant(new NativeMessageHandler(), typeof(HttpMessageHandler));
        }

        public Page CreateMainPage()
        {
            // Coolness: This returns the opening page that the platform-specific
            // boilerplate code will look for. It will know to find us because
            // we've registered our AppBootstrapper as an IScreen.
            return new RoutedViewHost();
        }
    }
}
