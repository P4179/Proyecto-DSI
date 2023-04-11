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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            Buttons.Visibility = Visibility.Collapsed;
            EndButton.Visibility = Visibility.Collapsed;
            Objective.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Visible;

            Character1.IsEnabled = true;
            Character2.IsEnabled = true;
            Character3.IsEnabled = true;
            Character4.IsEnabled = true;
            Character5.IsEnabled = true;
            */

            Frame.Navigate(typeof(Combate2));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Buttons.Visibility = Visibility.Visible;
            EndButton.Visibility = Visibility.Visible;
            Objective.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Collapsed;

            Character1.IsEnabled = false;
            Character2.IsEnabled = false;
            Character3.IsEnabled = false;
            Character4.IsEnabled = false;
            Character5.IsEnabled = false;
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa));
        }

        private void openPotionsMenu(object sender, RoutedEventArgs e)
        {
            UsePotionMenu.Visibility = Visibility.Visible;
        }

        private void closePotionsMenu(object sender, RoutedEventArgs e)
        {
            UsePotionMenu.Visibility = Visibility.Collapsed;

        }
    }
}
