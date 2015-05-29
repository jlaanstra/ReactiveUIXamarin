using System;
using System.Linq;
using ReactiveUI;
using System.Runtime.Serialization;
using Splat;
using System.Collections.Generic;
using ReactiveUIXamarin.Core.Models;
using System.Drawing;
using System.Reactive.Linq;
using ReactiveUIXamarin.Core.Helpers;
using ReactiveUIXamarin.Core.Services;
using System.Collections.ObjectModel;

namespace ReactiveUIXamarin.Core.ViewModels
{
    // Coolness: this viewmodel indicates that it supports activation by
    // implementing the ISupportsActivation interface.
    // This allows a viewmodel to clean itself up if it is no longer needed.
    // Using this is key in keeping performance up and memory down.
    [DataContract]
    public class ImageListViewModel : ReactiveObject, IRoutableViewModel, ISupportsActivation
    {
        [IgnoreDataMember]
        public ViewModelActivator Activator { get; protected set; }

        // CoolStuff: This badly-named parameter ends up being the Title on
        // iOS UINavigationViewController and Android ActionBar
        [IgnoreDataMember]
        public string UrlPathSegment {
            get { return "Pick a color"; }
        }

        // Not serializing the HostScreen is very important, because you
        // will create a loop in the object graph => crash
        [IgnoreDataMember]
        public IScreen HostScreen { get; protected set; }

        [IgnoreDataMember]
        public ITinEyeApi TinEye { get; protected set; }

        [IgnoreDataMember]
        public IAppState AppState { get; protected set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ImageListViewModel"/> class.
        /// </summary>
        /// <param name="hostScreen">The host screen.</param>
        public ImageListViewModel(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            TinEye = Locator.Current.GetService<ITinEyeApi>();
            AppState = Locator.Current.GetService<IAppState>();

            Activator = new ViewModelActivator();

            Images = new ObservableCollection<ImageTileViewModel>();

            // Coolness: We're describing here, in a *declarative way*, how a color when
            // a color is created.
            // This is perfectly eficient as we're only
            // updating the value in the scenario when it should change.
            var whenAnyColorChanges = this.WhenAny(
                x => x.AppState.Red, 
                x => x.AppState.Green, 
                x => x.AppState.Blue,
                (r, g, b) => ColorHelper.FromIntegers(r.Value, g.Value, b.Value));
            
            // Coolness: This block is executed when the viewmodel gets activated by a view.
            // It collects a list of IDisposable objects that will be disposed when the viewmodel
            // gets deactivated.
            this.WhenActivated(d =>
            {
                // Coolness: here we create a property from an observable stream of
                // values by using ToProperty(). ToProperty() will expose the value 
                // as a property 
                // on the viewmodel and automatically raise a change notification 
                // when the value changes.
                d(whenAnyColorChanges
                    .Where(c => c != null)
                    .Select(c => c.Value)
                    .ToProperty(this, x => x.FinalColor, out this.color));

                // Coolness: ReactiveCommands have built-in support for background
                // operations. RxCmd guarantees that this block will only be run exactly
                // once at a time.
                //
                // Coolness: We pass in an observable stream of booleans to indicate if 
                // the command can be executed. When the most recent value was false,
                // CanExecute will be false and CanExecute will also auto-disable while the
                // command is running.
                d(this.SearchCommand = ReactiveCommand.CreateAsyncTask(
                    whenAnyColorChanges.Select(x => x != null),
                    c => this.TinEye.GetImagesForColor((Color)c)));

                // Coolness: ReactiveCommands are themselves IObservables, whose values
                // are the results from the async method, guaranteed to arrive on the UI
                // thread. We're going to take the list of images that the background
                // operation loaded, and put them into our Images list.
                d(this.SearchCommand.Subscribe(ie =>
                {
                    // Coolness: SuppressChangeNoifications() suppresses change notifications
                    // and raises a "reset" notification at the end.
                    // Clear() and Addrange() will both raise a "Reset" notification,
                    // but we only want one, so we use Suppress here.
                    //using (this.Images.SuppressChangeNotifications())
                    //{
                    this.Images.Clear();
                    foreach (var image in ie.Select(i => new ImageTileViewModel(i)))
                    {
                        this.Images.Add(image);
                    }
                    //}
                }));

                // CoolStuff: Whenever the color changes, we're going to wait
                // for half a second of "dead airtime", then invoke the SearchCommand
                // command.
                d(whenAnyColorChanges
                    .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                    .InvokeCommand(this, x => x.SearchCommand));
            });
        }

        // CoolStuff: All read-write properties in ReactiveUI are declared
        // in exactly this fashion. You never write custom code in the setter,
        // if you are you're Doing It Wrong™.
        [IgnoreDataMember]
        private ReactiveCommand<List<Result>> searchCommand;
        [IgnoreDataMember]
        public ReactiveCommand<List<Result>> SearchCommand
        {
            get { return this.searchCommand; }
            private set { this.RaiseAndSetIfChanged(ref this.searchCommand, value); }
        }        

        // Coolness: Here wesee the derived property FinalColor.
        // This is how you define a property that is the result of
        // ToProperty().
        //
        // Coolness: We can use Color in our view models. Splat makes
        // this possible by making a bunch of things portable that 
        // should be, in addition to offering a DI container.
        [IgnoreDataMember]
        private ObservableAsPropertyHelper<Color> color;
        [IgnoreDataMember]
        public Color FinalColor
        {
            get { return color.Value; }
        }

        [IgnoreDataMember]
        private ObservableCollection<ImageTileViewModel> images;
        [DataMember]
        public ObservableCollection<ImageTileViewModel> Images
        {
            get { return this.images; }
            set { this.RaiseAndSetIfChanged(ref this.images, value); }
        }
    }
}