using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ReactiveUI;
using ReactiveUIXamarin.Core.ViewModels;

namespace ReactiveUIXamarin.Android
{
    public class AndroidApplication : Application
    {
        AutoSuspendHelper autoSuspendHelper;

        public AndroidApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Coolness: AutoSuspendHelper will automatically save and reload 
            // exactly *one* object of your choice when the app gets suspended. 
            // If the object can't be reloaded (i.e. if the app is starting for the first time),
            // we're telling ReactiveUI here how to create a new one from
            // scratch.
            autoSuspendHelper = new AutoSuspendHelper(this);

            RxApp.SuspensionHost.CreateNewAppState = () => {
                return new AppBootstrapper();
            };

            // Coolness: This attaches the AutoSuspendHelper to 
            // the application lifecycle events. This way,
            // ReactiveUI knows when and app gets suspended or resumed.
            RxApp.SuspensionHost.SetupDefaultSuspendResume();
        }
    }
}