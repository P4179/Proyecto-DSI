using P4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Store;
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

    public class Potion : ObservableObject {
        public string name { get; set; }
        public int price { get; set; }
        public string displayPrice { get; set; }
        public Potion() { }
    }
    public class Weap : ObservableObject {
        public string name { get; set; }
        public int price { get; set; }
        public string displayPrice { get; set; }
        public Weap() { }

    }
    public class Acc : ObservableObject {
        public string name { get; set; }
        public int price { get; set; }
        public string displayPrice { get; set; }
        public Acc() { }
    }


    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Tienda : Page
    {

        private List<Potion> potionsList = new List<Potion>() {
            new Potion() { name = "Poción 1", price = 100, displayPrice = "100 C" },
            new Potion() { name = "Poción 2", price = 200, displayPrice = "200 C" },
            new Potion() { name = "Poción 3", price = 300, displayPrice = "300 C" },
            new Potion() { name = "Poción 4", price = 400, displayPrice = "400 C" },
            new Potion() { name = "Poción 5", price = 500, displayPrice = "500 C" },
            new Potion() { name = "Poción 6", price = 600, displayPrice = "600 C" },
            new Potion() { name = "Poción 7", price = 700, displayPrice = "700 C" },
            new Potion() { name = "Poción 8", price = 800, displayPrice = "800 C" },
            new Potion() { name = "Poción 9", price = 900, displayPrice = "900 C" },
            new Potion() { name = "Poción 10", price = 1000, displayPrice = "1000 C" },
            new Potion() { name = "Poción 11", price = 1100, displayPrice = "1100 C" }
        };
        private List<Weap> weaponsList = new List<Weap>() {
            new Weap() { name = "Arma 1", price = 100, displayPrice = "100 C" },
            new Weap() { name = "Arma 2", price = 200, displayPrice = "200 C" },
            new Weap() { name = "Arma 3", price = 300, displayPrice = "300 C" },
            new Weap() { name = "Arma 4", price = 400, displayPrice = "400 C" },
            new Weap() { name = "Arma 5", price = 500, displayPrice = "500 C" },
            new Weap() { name = "Arma 6", price = 600, displayPrice = "600 C" },
            new Weap() { name = "Arma 7", price = 700, displayPrice = "700 C" },
            new Weap() { name = "Arma 8", price = 800, displayPrice = "800 C" },
            new Weap() { name = "Arma 9", price = 900, displayPrice = "900 C" },
            new Weap() { name = "Arma 10", price = 1000, displayPrice = "1000 C" },
            new Weap() { name = "Arma 11", price = 1100, displayPrice = "1100 C" }
        };
        private List<Acc> accList = new List<Acc>() {
            new Acc() { name = "Accesorio 1", price = 100, displayPrice = "100 C" },
            new Acc() { name = "Accesorio 2", price = 200, displayPrice = "200 C" },
            new Acc() { name = "Accesorio 3", price = 300, displayPrice = "300 C" },
            new Acc() { name = "Accesorio 4", price = 400, displayPrice = "400 C" },
            new Acc() { name = "Accesorio 5", price = 500, displayPrice = "500 C" },
            new Acc() { name = "Accesorio 6", price = 600, displayPrice = "600 C" },
            new Acc() { name = "Accesorio 7", price = 700, displayPrice = "700 C" },
            new Acc() { name = "Accesorio 8", price = 800, displayPrice = "800 C" },
            new Acc() { name = "Accesorio 9", price = 900, displayPrice = "900 C" },
            new Acc() { name = "Accesorio 10", price = 1000, displayPrice = "1000 C" },
            new Acc() { name = "Accesorio 11", price = 1100, displayPrice = "1100 C" }
        };


        public Tienda()
        {
            this.InitializeComponent();
            
        }

        private void returnToMain(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        private void hideQuantitySelection(object sender, RoutedEventArgs e)
        {
            BuyQuantity.Visibility= Visibility.Collapsed;
        }



        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IEnumerable<PivotHeaderPanel> headerpanel = FindVisualChildren<PivotHeaderPanel>(tabs);
            double totalwidth = headerpanel.ElementAt<PivotHeaderPanel>(0).ActualWidth;
            IEnumerable<PivotHeaderItem> items = FindVisualChildren<PivotHeaderItem>(tabs);
            int headitemcount = items.Count();
            for (int i = 0; i < headitemcount; i++)
                items.ElementAt<PivotHeaderItem>(i).Width = (totalwidth / headitemcount) - 0.1f;
        }

    }






}
