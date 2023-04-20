using P4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Tienda : Page
    {
        public ObservableCollection<CommObject> potionsList { get; } = new ObservableCollection<CommObject>();
        public ObservableCollection<Weapon> weaponList { get; } = new ObservableCollection<Weapon>();

        public ObservableCollection<CommObject> accessoriesList { get; } = new ObservableCollection<CommObject>();

        public CommObject selObj { get; set; }
        public Weapon selWeapon { get; set; }

        public int coins;
        public string coinsTxt;

        public Tienda() {
            this.InitializeComponent();

            coins = 2000;
            coinsTxt = coins.ToString() + " C";
            foreach(CommObject pot in ObjectLists.getPotions()) {
                potionsList.Add(pot);
            }
            foreach (Weapon weap in ObjectLists.getWeapons()) {
                weaponList.Add(weap);
            }
            foreach (CommObject acc in ObjectLists.getAccessories()) {
                accessoriesList.Add(acc);
            }
        }

        private void returnToMain(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        private void hideQuantitySelection(object sender, RoutedEventArgs e)
        {
            BuyQuantity.Visibility= Visibility.Collapsed;
        }






        // Hace que las pestañas sean del mismo tamaño y ocupen todo el ancho del pivot
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            double widthOffset = 0.5f;

            IEnumerable<PivotHeaderPanel> headerpanel = FindVisualChildren<PivotHeaderPanel>(tabs);
            double totalwidth = headerpanel.ElementAt<PivotHeaderPanel>(0).ActualWidth;
            IEnumerable<PivotHeaderItem> items = FindVisualChildren<PivotHeaderItem>(tabs);
            int headitemcount = items.Count();
            for (int i = 0; i < headitemcount; i++)
                items.ElementAt<PivotHeaderItem>(i).Width = (totalwidth / headitemcount) - widthOffset;
        }
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
            if (depObj != null) {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }

        string currTabName;
        private void tabChanged(object sender, SelectionChangedEventArgs e) {
            PivotItem selected = e.AddedItems[0] as PivotItem;
            currTabName = selected.Name;
            if (currTabName == "PotionsTab" || currTabName == "AccessoriesTab") {
                if (currTabName == "PotionsTab") selObj = potionsList[0];
                else selObj = accessoriesList[0];
                WeaponsInfo.Visibility = Visibility.Collapsed;
                ItemDescription.Visibility = Visibility.Visible;

            }
            else {
                selWeapon = weaponList[0];
                WeaponsInfo.Visibility = Visibility.Visible;
                ItemDescription.Visibility = Visibility.Collapsed;
            }
        }


        private void itemPointerEntered(object sender, PointerRoutedEventArgs e) {
            if (currTabName == "PotionsTab") {
                int ind = PotionsListView.IndexFromContainer((DependencyObject)sender);
                selObj = potionsList[ind];
            }
            else if (currTabName == "AccessoriesTab") {
                int ind = AccessoriesListView.IndexFromContainer((DependencyObject)sender);
                selObj = accessoriesList[ind];
            }
            else {
                int ind = WeaponsListView.IndexFromContainer((DependencyObject)sender);
                selWeapon = weaponList[ind];
            }
        }



    }






}
