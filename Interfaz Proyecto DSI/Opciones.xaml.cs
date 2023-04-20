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

        private List<String> settingsList = new List<string>() { "Configuración 1", "Configuración 2", "Configuración 3", "Configuración 4" };
        private List<String> comboBoxSettingsList = new List<string>() { "Alto", "Medio", "Bajo" };


        public Opciones()
        {
            this.InitializeComponent();
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


    }
}
