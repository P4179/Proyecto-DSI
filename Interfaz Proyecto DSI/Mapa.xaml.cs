using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
    public sealed partial class Mapa : Page
    {
        private DispatcherTimer timer = null;
        private RadioButton levelSelected = null;
        private bool draggingMap = false;
        private PointerPoint firstTouchPointer = null;
        private CoreCursor cursorHand = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        private CoreCursor cursorArrow = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);

        public Mapa()
        {
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(100000);

            this.InitializeComponent();
        }

        void timer_Tick(object sender, object e)
        {
            Object focus = FocusManager.GetFocusedElement();
            RadioButton level = focus as RadioButton;
            if (level != null && level.Parent == LevelsMap)
            {
                level.IsChecked = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Frame.BackStack.Last().SourcePageType == typeof(Opciones)) {
                PauseMenu.Visibility = Visibility.Visible;
                ResumeButton.Focus(FocusState.Programmatic);
            }

            // se inicia el timer
            timer.Start();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // se para el timer
            timer.Stop();
            base.OnNavigatedFrom(e);
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            switch (levelSelected.DataContext)
            {
                case "Tienda":
                    Frame.Navigate(typeof(Tienda), null, new DrillInNavigationTransitionInfo());
                    break;
                case "Nivel 1":
                    Frame.Navigate(typeof(Combate), null, new DrillInNavigationTransitionInfo());
                    break;
            }
        }

        private void TeamButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Equipo), null, new DrillInNavigationTransitionInfo());
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            togglePause();
        }

        private void Level_Checked(object sender, RoutedEventArgs e)
        {
            levelSelected = sender as RadioButton;
            Info.Visibility = Visibility.Visible;
        }

        private void keyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                togglePause();
            }
        }

        private void enableMapFocus(bool enabled)
        {
            TeamButton.IsTabStop = enabled;
            PauseButton.IsTabStop = enabled;
            foreach (RadioButton level in LevelsMap.Children)
            {
                level.IsTabStop = enabled;
            }
            StartGameButton.IsTabStop = enabled;
        }

        private void togglePause()
        {
            if (PauseMenu.Visibility == Visibility.Collapsed)
            {
                enableMapFocus(false);
                PauseMenu.Visibility = Visibility.Visible;
                ResumeButton.Focus(FocusState.Programmatic);
            }
            else
            {
                enableMapFocus(true);
                PauseMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void exitPause(object sender, RoutedEventArgs e)
        {
            togglePause();
        }

        private void goToOptions(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Opciones), null, new DrillInNavigationTransitionInfo());
        }

        private void returnToMain(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
        }

        // se clica dentro del mapa
        private void ScrollViewer_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // OriginalSource es quien verdaderamente generó el evento, es decir, la imagen del mapa
            // Sender es el remitente del evento, en este caso, ScrollViewer
            PointerPoint pointer = e.GetCurrentPoint(e.OriginalSource as Image);

            if (pointer.Properties.IsRightButtonPressed && e.OriginalSource == ImageMap)
            {
                draggingMap = true;
                firstTouchPointer = pointer;
                Window.Current.CoreWindow.PointerCursor = cursorHand;

                e.Handled = true;
            }
        }


        // se mueve el puntero dentro del mapa
        private void ScrollViewer_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint secondTouchPointer = e.GetCurrentPoint(e.OriginalSource as Image);
            // remitente del evento
            ScrollViewer scroll = sender as ScrollViewer;

            if (draggingMap)
            {
                double x = scroll.HorizontalOffset - (secondTouchPointer.Position.X - firstTouchPointer.Position.X);
                double y = scroll.VerticalOffset - (secondTouchPointer.Position.Y - firstTouchPointer.Position.Y);
                // se mantiene el zoom
                scroll.ChangeView(x, y, scroll.ZoomFactor);

                e.Handled = true;
            }
        }

        // se suelta el puntero, que se había clicado previamente
        private void ScrollViewer_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            draggingMap = false;
            firstTouchPointer = null;
            Window.Current.CoreWindow.PointerCursor = cursorArrow;
        }

        // se saca el puntero del mapa
        private void ScrollViewer_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            draggingMap = false;
            firstTouchPointer = null;
            Window.Current.CoreWindow.PointerCursor = cursorArrow;
        }
    }
}
