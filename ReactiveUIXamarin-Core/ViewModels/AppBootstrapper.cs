using ModernHttpClient;
using ReactiveUI;
using ReactiveUI.XamForms;
using ReactiveUIXamarin.Core.Services;
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
    public class AppBootstrapper : ReactiveObject, IScreen, IAppState
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
            Locator.CurrentMutable.RegisterConstant(this, typeof(IAppState));

            // Coolness: Set up Fusillade and ModernHttpClient
            //
            // Fusillade is a super cool library that will make it so that whenever
            // we issue web requests, we'll only issue 4 concurrently, and if we
            // end up issuing multiple requests to the same resource, it will
            // de-dupe them. We're saying here, that we want our *backing*
            // HttpMessageHandler to be ModernHttpClient.
            Locator.CurrentMutable.RegisterConstant(new NativeMessageHandler(), typeof(HttpMessageHandler));

            //Register API service
            Locator.CurrentMutable.RegisterLazySingleton(() => new TinEyeApi(), typeof(ITinEyeApi));

            // Kick off to the first page of our app. If we don't navigate to a
            // page on startup, Xamarin Forms will get real mad (and even if it
            // didn't, our users would!)
            Router.Navigate.Execute(new ImageListViewModel(this));
        }

        public Page CreateMainPage()
        {
            // Coolness: This returns the opening page that the platform-specific
            // boilerplate code will look for. It will know to find us because
            // we've registered our AppBootstrapper as an IScreen.
            return new RoutedViewHost();
        }

        private int red;
        public int Red
        {
            get { return red; }
            set { this.RaiseAndSetIfChanged(ref red, value); }
        }

        private int green;
        public int Green
        {
            get { return green; }
            set { this.RaiseAndSetIfChanged(ref green, value); }
        }

        private int blue;
        public int Blue
        {
            get { return blue; }
            set { this.RaiseAndSetIfChanged(ref blue, value); }
        }
    }
}
