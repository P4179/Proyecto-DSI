using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Animation;
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
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationMenu.Visibility = Visibility.Visible;
            ConfirmationBg.Visibility = Visibility.Visible;
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationMenu.Visibility = Visibility.Collapsed;
            ConfirmationBg.Visibility = Visibility.Collapsed;

        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }


        private void openOptions(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Opciones), null, new DrillInNavigationTransitionInfo());
        }
        private void Equipo(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Tienda), null, new DrillInNavigationTransitionInfo());
        }




        
    }
}
