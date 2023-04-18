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
using Windows.UI.Xaml.Documents;
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
    public sealed partial class Combate : Page
    {
        public Combate()
        {
            this.InitializeComponent();
            for (int i = 0; i < 10; i++) PotionsList.Items.Add("Poción " + i.ToString());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Frame.BackStack.Last().SourcePageType == typeof(Opciones))
                PauseMenu.Visibility = Visibility.Visible;
        }

        private void enableFocus(bool enabled)
        {
            EndButton.IsTabStop = enabled;
            foreach (Button button in Buttons.Children)
            {
                button.IsTabStop = enabled;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // el último parámetro es la transición a realizar al cambiar de página
            // en este caso, se suprime la transición
            Frame.Navigate(typeof(Combate2), null, new SuppressNavigationTransitionInfo());
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        private void openPotionsMenu(object sender, RoutedEventArgs e)
        {
            enableFocus(false);
            UsePotionMenu.Visibility = Visibility.Visible;
        }

        private void closePotionsMenu(object sender, RoutedEventArgs e)
        {
            enableFocus(true);
            UsePotionMenu.Visibility = Visibility.Collapsed;
        }

        private void keyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (UsePotionMenu.Visibility == Visibility.Visible)
                {
                    enableFocus(true);
                    UsePotionMenu.Visibility = Visibility.Collapsed;
                }
                else
                {
                    togglePause();
                }
            }
        }

        private void togglePause()
        {
            if (PauseMenu.Visibility == Visibility.Collapsed)
            {
                enableFocus(false);
                PauseMenu.Visibility = Visibility.Visible;
            }
            else
            {
                enableFocus(true);
                PauseMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void exitPause(object sender, RoutedEventArgs e)
        {
            togglePause();
        }

        private void goToOptions(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Opciones), null, new DrillInNavigationTransitionInfo());
        }

        private void returnToMain(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
        }
    }
}
