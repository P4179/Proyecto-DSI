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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // si se puede volver atrás, es que ya se ha entrado en el juego
            if (Frame.CanGoBack && Frame.BackStack.Last().SourcePageType != typeof(Opciones)) {
                SaveGame.IsEnabled = true;
            }
            base.OnNavigatedTo(e);
        }

        // NUEVA PARTIDA
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        // GUARDAR PARTIDA
        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        // OPCIONES
        private void openOptions(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Opciones), null, new DrillInNavigationTransitionInfo());
        }

        // MENÚ DE CONFIRMACIÓN
        private void ExitButton_Click(object sender, RoutedEventArgs e) {
            ConfirmationMenu.Visibility = Visibility.Visible;
            ConfirmationBg.Visibility = Visibility.Visible;

            NewGame.IsTabStop = false;
            SaveGame.IsTabStop = false;
            Options.IsTabStop = false;
            Exit.IsTabStop = false;
            VisualStateManager.GoToState(NoButton, "Focused", true);
            NoButton.Focus(FocusState.Programmatic);
        }
        private void ButtonYes_Click(object sender, RoutedEventArgs e) {
            Application.Current.Exit();
        }
        private void ButtonNo_Click(object sender, RoutedEventArgs e) {
            ConfirmationMenu.Visibility = Visibility.Collapsed;
            ConfirmationBg.Visibility = Visibility.Collapsed;

            NewGame.IsTabStop = true;
            SaveGame.IsTabStop = true;
            Options.IsTabStop = true;
            Exit.IsTabStop = true;
            NewGame.Focus(FocusState.Programmatic);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NewGame.Focus(FocusState.Programmatic);
            VisualStateManager.GoToState(NewGame, "Focused", true);
        }

        private void keyDown(object sender, KeyRoutedEventArgs e) {
            switch (e.Key) {
                case Windows.System.VirtualKey.Escape:
                    ButtonNo_Click(null, null);
                    break;

            }
        }
    }
}
