using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Interfaz_Proyecto_DSI
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage() {
            this.InitializeComponent();
            //Window.Current.CoreWindow.SizeChanged += CoreWindow_SizeChanged;
        }

        
        //private void CoreWindow_SizeChanged(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.WindowSizeChangedEventArgs args) => setTabSize();

        private void openOptions(object sender, RoutedEventArgs e) {
            OptionsMenu.Visibility = Visibility.Visible;
        }

        private void closeOptions(object sender, RoutedEventArgs e) {
            OptionsMenu.Visibility = Visibility.Collapsed;
        }

        private void togglePause(object sender, RoutedEventArgs e) {
            if (PauseMenu.Visibility == Visibility.Collapsed)
                PauseMenu.Visibility = Visibility.Visible;
            else
                PauseMenu.Visibility = Visibility.Collapsed;
        }



    }
}
