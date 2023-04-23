using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    public sealed partial class Opciones : Page
    {
        OpcionesLogic optionsLogic = new OpcionesLogic();


        public Opciones() {
            this.InitializeComponent();

            foreach (Opcion opc in OptionLists.getGraphs()) {
                optionsLogic.graphList.Add(opc);
            }
            foreach (Opcion opc in OptionLists.getSound()) {
                optionsLogic.soundList.Add(opc);
            }
            foreach (Opcion opc in OptionLists.getAccess()) {
                optionsLogic.accList.Add(opc);
            }
        }

        private void closeOptions(object sender, RoutedEventArgs e) {
            if (Frame.BackStack.Last().SourcePageType == typeof(MainPage))
                Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
            else if (Frame.BackStack.Last().SourcePageType == typeof(Mapa))
                Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
            else if (Frame.BackStack.Last().SourcePageType == typeof(Combate))
                Frame.Navigate(typeof(Combate), null, new DrillInNavigationTransitionInfo());
        }



        
        // Hace que las pestañas sean del mismo tamaño y ocupen todo el ancho del pivot
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            double widthOffset = 0.26;

            IEnumerable<PivotHeaderPanel> headerpanel = FindVisualChildren<PivotHeaderPanel>(optionsTabs);
            double totalwidth = headerpanel.ElementAt<PivotHeaderPanel>(0).ActualWidth;
            IEnumerable<PivotHeaderItem> items = FindVisualChildren<PivotHeaderItem>(optionsTabs);
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

            if (currTabName == "ControlsTab") {
                //optionInfo.Visibility = Visibility.Collapsed;
            }
            else {
                //optionInfo.Visibility = Visibility.Visible;
                if (currTabName == "GraphicsTab")
                {
                    optionsLogic.selectedOption = optionsLogic.graphList[0];
                }
                else if (currTabName == "SoundTab")
                {
                    optionsLogic.selectedOption = optionsLogic.soundList[0];
                }
                else if (currTabName == "AccessTab")
                {
                    optionsLogic.selectedOption = optionsLogic.accList[0];
                }
            }
            
            
        }


        private void graphSelChanged(object sender, SelectionChangedEventArgs e) {
            if(optionsLogic.graphList.Count() > 0) {
                var sel = sender as ListView;
                int index = sel.SelectedIndex;

                optionsLogic.selectedOption = optionsLogic.graphList[index];
                optionsLogic.selectedGraph = optionsLogic.graphList[index];

            }
        }

        private void soundSelChanged(object sender, SelectionChangedEventArgs e) {
            if (optionsLogic.soundList.Count() > 0) {
                var sel = sender as ListView;
                int index = sel.SelectedIndex;

                optionsLogic.selectedOption = optionsLogic.soundList[index];
                optionsLogic.selectedSound = optionsLogic.soundList[index];

            }
        }

        private void accSelChanged(object sender, SelectionChangedEventArgs e) {
            if (optionsLogic.accList.Count() > 0) {
                var sel = sender as ListView;
                int index = sel.SelectedIndex;

                optionsLogic.selectedOption = optionsLogic.accList[index];
                optionsLogic.selectedAccess = optionsLogic.accList[index];
            }
        }





    }
}
