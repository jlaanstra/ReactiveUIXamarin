using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ReactiveUI;
using ReactiveUIXamarin.Core.ViewModels;

namespace ReactiveUIXamarin.Core.Views
{    
    public partial class ImageListView : ContentPage, IViewFor<ImageListViewModel>
    {    
        public ImageListView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.Bind(this.ViewModel, x => x.AppState.Red, x => x.Red.Text));
                d(this.Bind(this.ViewModel, x => x.AppState.Green, x => x.Green.Text));
                d(this.Bind(this.ViewModel, x => x.AppState.Blue, x => x.Blue.Text));

                d(this.OneWayBind(this.ViewModel, x => x.FinalColor, x => x.Color.BackgroundColor, 
                    c => c.ToNative()));                

                d(this.OneWayBind(ViewModel, x => x.Images, x => x.ImageTiles.ItemsSource));
            });
        }

        public ImageListViewModel ViewModel
        {
            get { return (ImageListViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<ImageListView, ImageListViewModel>(x => x.ViewModel, default(ImageListViewModel), BindingMode.OneWay);

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ImageListViewModel)value; }
        }
    }
}

