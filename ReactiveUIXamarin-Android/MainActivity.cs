using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using ReactiveUIXamarin.Core;

namespace ReactiveUIXamarin.Android
{
    // Since Xamarin Forms 1.3, the main activity needs to inherit
    // from FormsApplicationActivity.
    [Activity(Label = "ReactiveUIXamarin-Android", MainLauncher = true)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Xamarin Forms initialization.
            Forms.Init(this, bundle);

            LoadApplication(new ReactiveUIXamarinApp());
        }
    }
}

