using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input.Preview.Injection;
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
        // Controlador con su bucle de lectura
        Controller control = null;
        ControllerLoop ctrlLoop = null;

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

            SaveButton.IsEnabled = false;
            control = new Controller(this);
            ctrlLoop = new ControllerLoop(this, control);
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
            optionsTabs.Focus(FocusState.Programmatic);

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
                optionsLogic.selectedOption = optionsLogic.controlsOption;
            }
            else {
                if (currTabName == "GraphicsTab") {
                    optionsLogic.selectedOption = optionsLogic.graphList[0];
                }
                else if (currTabName == "SoundTab") {
                    optionsLogic.selectedOption = optionsLogic.soundList[0];
                }
                else if (currTabName == "AccessTab") {
                    optionsLogic.selectedOption = optionsLogic.accList[0];
                }
            }
        }


        private void graphSelChanged(object sender, SelectionChangedEventArgs e) {
            if(optionsLogic.graphList.Count() > 0) {
                var sel = sender as ListView;
                int index = sel.SelectedIndex;

                if(index != -1 && currTabName == "GraphicsTab") {
                    optionsLogic.selectedOption = optionsLogic.graphList[index];
                    optionsLogic.selectedGraph = optionsLogic.graphList[index];
                }
            }
        }

        private void soundSelChanged(object sender, SelectionChangedEventArgs e) {
            if (optionsLogic.soundList.Count() > 0) {
                var sel = sender as ListView;
                int index = sel.SelectedIndex;

                if (index != -1 && currTabName == "SoundTab") {
                    optionsLogic.selectedOption = optionsLogic.soundList[index];
                    optionsLogic.selectedSound = optionsLogic.soundList[index];
                }
            }
        }

        private void accSelChanged(object sender, SelectionChangedEventArgs e) {
            if (optionsLogic.accList.Count() > 0) {
                var sel = sender as ListView;
                int index = sel.SelectedIndex;

                if (index != -1 && currTabName == "AccessTab") {
                    optionsLogic.selectedOption = optionsLogic.accList[index];
                    optionsLogic.selectedAccess = optionsLogic.accList[index];
                }
            }
        }



        int defaultComboBoxInd1 = 0;
        int defaultComboBoxInd2 = 2;
        int defaultSliderValue = 50;
        bool defaultCheckBoxValue = false;

        int lastCombo11 = 0;
        int lastCombo12 = 2;
        int lastCombo21 = 0;
        int lastCombo22 = 2;
        int lastCombo31 = 0;
        int lastCombo32 = 2;

        double lastSlider1 = 50;
        double lastSlider2 = 50;
        double lastSlider3 = 50;

        bool check1 = false;
        bool check2 = false;
        bool check3 = false;
        bool lastCheck1 = false;
        bool lastCheck2 = false;
        bool lastCheck3 = false;

        private void saveButtonComboBox(object sender, SelectionChangedEventArgs e) {
            if (!Combo11.IsLoaded)
                return;

            if (Combo11.SelectedIndex != lastCombo11 || Combo12.SelectedIndex != lastCombo12 ||
                Combo21.SelectedIndex != lastCombo21 || Combo22.SelectedIndex != lastCombo22 ||
                Combo31.SelectedIndex != lastCombo31 || Combo32.SelectedIndex != lastCombo32)
                SaveButton.IsEnabled = true;
            else SaveButton.IsEnabled = false;
        }
        private void saveButtonSlider(object sender, RangeBaseValueChangedEventArgs e) {
            if (!Slider1.IsLoaded)
                return;

            if (Slider1.Value != lastSlider1 || Slider2.Value != lastSlider2 || Slider3.Value != lastSlider3)
                SaveButton.IsEnabled = true;
            else SaveButton.IsEnabled = false;
        }

        private void saveButtonCheckBox(object sender, RoutedEventArgs e) {
            if (!Check1.IsLoaded)
                return;

            var check = sender as CheckBox;
            if (check.Name == "Check1") check1 = !check1;
            else if (check.Name == "Check2") check2 = !check2;
            else if (check.Name == "Check3") check3= !check3;

            if (check1 != lastCheck1 || check2 != lastCheck2 || check3 != lastCheck3)
                SaveButton.IsEnabled = true;
            else SaveButton.IsEnabled = false;

        }

        private void saved(object sender, RoutedEventArgs e) {
            SaveButton.IsEnabled = false;
            RestoreButon.IsEnabled = true;

            updateOptions();
        }

        private void restore(object sender, RoutedEventArgs e) {

            Combo11.SelectedIndex = defaultComboBoxInd1;
            Combo21.SelectedIndex = defaultComboBoxInd1;
            Combo31.SelectedIndex = defaultComboBoxInd1;
            Combo12.SelectedIndex = defaultComboBoxInd2;
            Combo22.SelectedIndex = defaultComboBoxInd2;
            Combo32.SelectedIndex = defaultComboBoxInd2;

            Slider1.Value = defaultSliderValue;
            Slider2.Value = defaultSliderValue;
            Slider3.Value = defaultSliderValue;

            Check1.IsChecked = defaultCheckBoxValue;
            Check2.IsChecked = defaultCheckBoxValue;
            Check3.IsChecked = defaultCheckBoxValue;

            updateOptions();

            RestoreButon.IsEnabled = false;
            SaveButton.IsEnabled = false;
        }

        private void updateOptions(){
            lastCombo11 = Combo11.SelectedIndex;
            lastCombo12 = Combo12.SelectedIndex;
            lastCombo21 = Combo21.SelectedIndex;
            lastCombo22 = Combo22.SelectedIndex;
            lastCombo31 = Combo31.SelectedIndex;
            lastCombo32 = Combo32.SelectedIndex;

            lastSlider1 = Slider1.Value;
            lastSlider2 = Slider2.Value;
            lastSlider3 = Slider3.Value;

            lastCheck1 = check1;
            lastCheck2 = check2;
            lastCheck3 = check3;
        }



        private void keyDown(object sender, KeyRoutedEventArgs e) {
            var focused = FocusManager.GetFocusedElement();

            if (focused == Slider1 || focused == Slider2 || focused == Slider3) {
                if (control.gamepadState.RightThumbstickX < 0) {
                    if (focused == Slider1) Slider1.Value--;
                    else if (focused == Slider2) Slider2.Value--;
                    else if (focused == Slider3) Slider3.Value--;
                }
                else if (control.gamepadState.RightThumbstickX > 0) {
                    if (focused == Slider1) Slider1.Value++;
                    else if (focused == Slider2) Slider2.Value++;
                    else if (focused == Slider3) Slider3.Value++;
                }
            }

            if (e.Key == Windows.System.VirtualKey.Enter) selOption(null, null);
            else if (e.Key == Windows.System.VirtualKey.Escape) {
                var focusedContent = FocusManager.GetFocusedElement() as ContentControl;
                if (focusedContent == null) unselOption();
                else {
                    if (focusedContent.Name == "HeaderClipper" || focused == ReturnButton || focused == SaveButton || focused == RestoreButon)
                        closeOptions(null, null);

                    else if (focused  == Graph1 || focused == Graph2 || focused == Graph3 || focused == Graph4 ||
                             focused == Sound1 || focused == Sound2 || focused == Sound3 || focused == Sound4 ||
                             focused == Access1 || focused == Access2 || focused == Access3 || focused == Access4)
                    {
                        GraphicsListView.SelectedIndex = 0;
                        SoundsListView.SelectedIndex = 0;
                        AccessListView.SelectedIndex = 0;

                        optionsTabs.Focus(FocusState.Programmatic);
                    }
                }
            }


        }

        private void selOption(object sender, ItemClickEventArgs e) {
            var focused = FocusManager.GetFocusedElement();
            if (focused == Graph1)
                Combo11.Focus(FocusState.Programmatic);
            else if (focused == Graph2)
                Combo12.Focus(FocusState.Programmatic);
            else if (focused == Graph3)
                Slider1.Focus(FocusState.Programmatic);
            else if (focused == Graph4)
                Check1.Focus(FocusState.Programmatic);

            else if (focused == Sound1)
                Combo21.Focus(FocusState.Programmatic);
            else if (focused == Sound2 )
                Combo22.Focus(FocusState.Programmatic);
            else if (focused == Sound3)
                Slider2.Focus(FocusState.Programmatic);
            else if (focused == Sound4)
                Check2.Focus(FocusState.Programmatic);

            else if (focused == Access1)
                Combo31.Focus(FocusState.Programmatic);
            else if (focused == Access2)
                Combo32.Focus(FocusState.Programmatic);
            else if (focused == Access3)
                Slider3.Focus(FocusState.Programmatic);
            else if (focused == Access4)
                Check3.Focus(FocusState.Programmatic);


        }


        private void unselOption() {
            var focused = FocusManager.GetFocusedElement();

            if (focused == Combo11)
                Graph1.Focus(FocusState.Programmatic);
            else if (focused == Combo12)
                Graph2.Focus(FocusState.Programmatic);
            else if (focused == Slider1)
                Graph3.Focus(FocusState.Programmatic);
            else if (focused == Check1)
                Graph4.Focus(FocusState.Programmatic);

            else if (focused == Combo21)
                Sound1.Focus(FocusState.Programmatic);
            else if (focused == Combo22)
                Sound2.Focus(FocusState.Programmatic);
            else if (focused == Slider2)
                Sound3.Focus(FocusState.Programmatic);
            else if (focused == Check2)
                Sound4.Focus(FocusState.Programmatic);

            else if (focused == Combo31)
                Access1.Focus(FocusState.Programmatic);
            else if (focused == Combo32)
                Access2.Focus(FocusState.Programmatic);
            else if (focused == Slider3)
                Access3.Focus(FocusState.Programmatic);
            else if (focused == Check3)
                Access4.Focus(FocusState.Programmatic);
        }

    }
}
