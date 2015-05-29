using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ReactiveUIXamarin.Core.ViewModels;
using ReactiveUI;

namespace ReactiveUIXamarin.Core.Views
{
    public partial class ImageTileView : ContentView, IViewFor<ImageTileViewModel>
    {
        public ImageTileView()
        {
            InitializeComponent();
            
            this.OneWayBind(ViewModel, vm => vm.ImagePath, v => v.Image.Source,
                    uri => new UriImageSource() { Uri = new Uri(uri) });
        }
                
        public ImageTileViewModel ViewModel
        {
            get { return (ImageTileViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<ImageTileView, ImageTileViewModel>(x => x.ViewModel, default(ImageTileViewModel), BindingMode.OneWay);

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ImageTileViewModel)value; }
        }
    }
}