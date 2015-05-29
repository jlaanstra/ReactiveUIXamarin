using ReactiveUI;
using ReactiveUIXamarin.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReactiveUIXamarin.Core
{
    public class ReactiveUIXamarinApp : Application
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ReactiveUIXamarinApp"/> class.
        /// </summary>
        public ReactiveUIXamarinApp()
        {
            // Coolness: we retrieve the main page from the bootstrapper
            // and set is as the main page for this application.
            var bootstrapper = RxApp.SuspensionHost.GetAppState<AppBootstrapper>();

            this.MainPage = bootstrapper.CreateMainPage();
        }
    }
}
