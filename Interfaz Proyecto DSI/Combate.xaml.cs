using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        PocionesLogic potsLogic = new PocionesLogic();


        public Combate() {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            AttackButton.Focus(FocusState.Programmatic);
            VisualStateManager.GoToState(AttackButton, "Focused", true);

            potsLogic.potionsList.Clear();

            Potion aux = new Potion() { amount = 10, desc = "Lorem Ipsum 0 poc", displayAmount ="x 4", displayPrice="", id = 0, name = "Poción 0", price = 0 };
            potsLogic.potionsList.Add(aux);
            potsLogic.selectedPotion = aux;
            foreach (var pot in (App.Current as App).boughtPotions)
                potsLogic.potionsList.Add(pot);

        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (Frame.BackStack.Last().SourcePageType == typeof(Opciones))
                PauseMenu.Visibility = Visibility.Visible;
        }

        private void enableFocus(bool enabled) {
            EndButton.IsTabStop = enabled;
            foreach (Button button in Buttons.Children) {
                button.IsTabStop = enabled;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            // el último parámetro es la transición a realizar al cambiar de página
            // en este caso, se suprime la transición
            Frame.Navigate(typeof(Combate2), null, new SuppressNavigationTransitionInfo());
        }

        private void EndButton_Click(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        private async void openPotionsMenu(object sender, RoutedEventArgs e) {
            UsePotionMenu.Visibility = Visibility.Visible;
            enableFocus(false);
            ReturnButton.Focus(FocusState.Programmatic);

            ListViewItem item = PotionsList.ContainerFromIndex(0) as ListViewItem;
            if (item != null) {
                item.Focus(FocusState.Programmatic);
                item.IsSelected = true;
                VisualStateManager.GoToState(item, "Pressed", true);
            }
            else
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () => {
                    item = PotionsList.ContainerFromIndex(0) as ListViewItem;
                    if (item != null) {
                        item.Focus(FocusState.Programmatic);
                        item.IsSelected = true;
                        VisualStateManager.GoToState(item, "Pressed", true);
                    }
                });
        }

        private void closePotionsMenu(object sender, RoutedEventArgs e) {
            enableFocus(true);
            UsePotionMenu.Visibility = Visibility.Collapsed;
            PotionsButton.Focus(FocusState.Programmatic);
        }

        private void keyDown(object sender, KeyRoutedEventArgs e) {
            if (e.Key == Windows.System.VirtualKey.Escape) {
                if (UsePotionMenu.Visibility == Visibility.Visible) {
                    ReturnButton.Focus(FocusState.Programmatic);
                }
                else {
                    togglePause();
                }
            }
        }

        private void togglePause() {
            if (PauseMenu.Visibility == Visibility.Collapsed) {
                enableFocus(false);
                PauseMenu.Visibility = Visibility.Visible;
            }
            else {
                enableFocus(true);
                PauseMenu.Visibility = Visibility.Collapsed;
            }
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

        private void potChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
