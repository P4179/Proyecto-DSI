using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Interfaz_Proyecto_DSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Mapa : Page
    {
        public Mapa()
        {
            this.InitializeComponent();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Combate), null, new DrillInNavigationTransitionInfo());
        }

        private void TeamButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Equipo), null, new DrillInNavigationTransitionInfo());
        }

        private void Level_Checked(object sender, RoutedEventArgs e)
        {
            Info.Visibility = Visibility.Visible;
        }

        private void keyDown(object sender, KeyRoutedEventArgs e) {
            if (e.Key == Windows.System.VirtualKey.Escape) togglePause();
        }


        private void togglePause() {
            if (PauseMenu.Visibility == Visibility.Collapsed) PauseMenu.Visibility = Visibility.Visible;
            else PauseMenu.Visibility = Visibility.Collapsed;
        }

        private void exitPause(object sender, RoutedEventArgs e) {
            togglePause();
        }

        private void goToOptions(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(Opciones), null, new DrillInNavigationTransitionInfo());
        }

        private void returnToMain(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if(Frame.BackStack.Last().SourcePageType == typeof(Opciones))
                PauseMenu.Visibility = Visibility.Visible;
        }


    }
}
