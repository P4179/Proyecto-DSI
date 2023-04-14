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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Interfaz_Proyecto_DSI
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<String> settingsList = new List<string>() { "Configuración 1", "Configuración 2", "Configuración 3", "Configuración 4" };
        private List<String> comboBoxSettingsList = new List<string>() { "Alto", "Medio", "Bajo"};


        public MainPage() {
            this.InitializeComponent();

            foreach(string setting in comboBoxSettingsList) {
                
            }
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationMenu.Visibility = Visibility.Visible;
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationMenu.Visibility = Visibility.Collapsed;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa));
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }


        private void openOptions(object sender, RoutedEventArgs e)
        {
            OptionsMenu.Visibility = Visibility.Visible;
        }
        private void closeOptions(object sender, RoutedEventArgs e)
        {
            OptionsMenu.Visibility = Visibility.Collapsed;
        }

        private void Equipo(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Tienda));
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
            IEnumerable<PivotHeaderPanel> headerpanel = FindVisualChildren<PivotHeaderPanel>(optionsTabs);
            double totalwidth = headerpanel.ElementAt<PivotHeaderPanel>(0).ActualWidth;
            IEnumerable<PivotHeaderItem> items = FindVisualChildren<PivotHeaderItem>(optionsTabs);
            int headitemcount = items.Count();
            for (int i = 0; i < headitemcount; i++)
                items.ElementAt<PivotHeaderItem>(i).Width = (totalwidth / headitemcount) - 0.1f ;
        }
    }
}
