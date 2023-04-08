using System;
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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Interfaz_Proyecto_DSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Tienda : Page
    {
        public Tienda()
        {
            this.InitializeComponent();
            for (int i = 0; i < 10; i++) MyListView1.Items.Add("Poción " + i.ToString());
            for (int i = 0; i < 10; i++) MyListView2.Items.Add("Arma " + i.ToString());
            for (int i = 0; i < 10; i++) MyListView3.Items.Add("Accesorio " + i.ToString());
            //for (int i = 0; i < 3; i++) Filters.Items.Add("Filtro " + i.ToString());

        }

        private void returnToMain(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }

        private void hideQuantitySelection(object sender, RoutedEventArgs e)
        {
            BuyQuantity.Visibility= Visibility.Collapsed;
        }
    }






}
