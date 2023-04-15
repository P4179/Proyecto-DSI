using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class Equipo : Page
    {
        public Equipo()
        {
            this.InitializeComponent();
            for (int i = 0; i < 10; i++) MyListView1.Items.Add("Arma " + i.ToString());

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        private void ButtonTree_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Abilities), null, new DrillInNavigationTransitionInfo());
        }

        private void openWeaponList(object sender, RoutedEventArgs e)
        {
            weaponList.Visibility = Visibility.Visible;
        }
        private void closeWeaponList(object sender, RoutedEventArgs e)
        {
            weaponList.Visibility = Visibility.Collapsed;
        }
    }
}
