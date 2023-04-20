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
        TiendaLogic shopLogic = new TiendaLogic();
        public Tienda() {
            this.InitializeComponent();

            shopLogic.coins = 2000;
            foreach(CommObject pot in ObjectLists.getPotions()) {
                shopLogic.potionsList.Add(pot);
            }
            foreach (Weapon weap in ObjectLists.getWeapons()) {
                shopLogic.weaponList.Add(weap);
            }
            foreach (CommObject acc in ObjectLists.getAccessories()) {
                shopLogic.accessoriesList.Add(acc);
            }
        }

        private void returnToMap(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        private void hideQuantitySelection(object sender, RoutedEventArgs e) {
            BuyQuantity.Visibility= Visibility.Collapsed;
            ReturnButton.IsTabStop = true;
            tabs.IsTabStop = true;

            PotionsListView.IsTabStop = true;
            WeaponsListView.IsTabStop = true;
            AccessoriesListView.IsTabStop = true;
        }
        private void showQuantitySelection(object sender, RoutedEventArgs e)
        {
            BuyQuantity.Visibility = Visibility.Visible;
            Quantity.Text = "1";
            ReturnButton.IsTabStop = false;
            tabs.IsTabStop = false;
            PotionsListView.IsTabStop = false;
            WeaponsListView.IsTabStop = false;
            AccessoriesListView.IsTabStop = false;
            TotalPrice.Text = (shopLogic.selectedObject.price * int.Parse(Quantity.Text)).ToString() + " C";

        }

        int total;
        private void changeQuant(object sender, TextChangedEventArgs e) {
            if (int.TryParse(Quantity.Text, out int parsedNumber)) {
                if (parsedNumber > 1000) {
                    Quantity.Text = "999";
                    parsedNumber = 999;
                }
                total = shopLogic.selectedObject.price * parsedNumber;
                TotalPrice.Text = total.ToString() + " C";

                if (total <= shopLogic.coins) BuyButton.IsEnabled = true;
                else BuyButton.IsEnabled = false;
            }
            else {
                Quantity.Text = Quantity.Text.TrimEnd(Quantity.Text.LastOrDefault());
            }
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
                if (currTabName == "PotionsTab") {
                    shopLogic.selectedObject = shopLogic.potionsList[0];
                    PotionsListView.SelectedIndex = 0;
                    ItemName.Text = shopLogic.potionsList[0].name;
                    ItemDescription.Text = shopLogic.potionsList[0].desc;
                }
                else {
                    shopLogic.selectedObject = shopLogic.accessoriesList[0];
                    AccessoriesListView.SelectedIndex = 0;
                    ItemName.Text = shopLogic.accessoriesList[0].name;
                    ItemDescription.Text = shopLogic.accessoriesList[0].desc;
                }
                WeaponsInfo.Visibility = Visibility.Collapsed;
                ItemDescription.Visibility = Visibility.Visible;

            }
            else {
                shopLogic.selectedObject = shopLogic.weaponList[0];
                WeaponsListView.SelectedIndex= 0;
                ItemName.Text = shopLogic.weaponList[0].name;
                WeaponDmg.Text= shopLogic.weaponList[0].dmg.ToString();
                WeaponEff.Text = shopLogic.weaponList[0].effect;
                WeaponReach.Text = shopLogic.weaponList[0].reach.ToString();
                WeaponType.Text = shopLogic.weaponList[0].type;
                WeaponsInfo.Visibility = Visibility.Visible;
                ItemDescription.Visibility = Visibility.Collapsed;
            }
        }

        private void potItClick(object sender, ItemClickEventArgs e) {
            shopLogic.coins = 2;

            var clicked = e.ClickedItem as CommObject;
            shopLogic.selectedObject = clicked;

            ItemName.Text = clicked.name;
            ItemDescription.Text = clicked.desc;
        }

        private void weapItClick(object sender, ItemClickEventArgs e) {
            var clicked = e.ClickedItem as Weapon;
            shopLogic.selectedObject = clicked;

            ItemName.Text = clicked.name;
            WeaponDmg.Text = clicked.dmg.ToString();
            WeaponEff.Text = clicked.effect;
            WeaponReach.Text = clicked.reach.ToString();
            WeaponType.Text = clicked.type;
        }

        private void accItClick(object sender, ItemClickEventArgs e) {
            var clicked = e.ClickedItem as CommObject;
            shopLogic.selectedObject = clicked;

            ItemName.Text = clicked.name;
            ItemDescription.Text = clicked.desc;
        }

        private void buyItem(object sender, RoutedEventArgs e) {
            shopLogic.coins -= total;
            shopLogic.coinsTxt = shopLogic.coins.ToString() + " C";
            CoinsBox.Text = shopLogic.coinsTxt;

            hideQuantitySelection(null, null);
        }
    }






}
