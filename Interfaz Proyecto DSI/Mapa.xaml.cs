using P4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public class Level
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Infor { get; set; }
    }

    public class MapaLogic : ObservableObject
    {
        public ObservableCollection<Level> levelsList { get; } = new ObservableCollection<Level>()
        {
            new Level() {Id="1-1", Name="Nivel 1-1",
                Infor="Lorem ipsum1 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
            new Level() {Id="Tienda", Name="Tienda",
                Infor="Lorem ipsum2 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
            new Level() {Id="Jefe 1", Name="Nivel 1-",
                Infor = "Lorem ipsum3 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
        };

        private Level selectedLevel;
        public Level selLevel
        {
            get => selectedLevel;
            set { Set(ref selectedLevel, value); }
        }
    }

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
        private MapaLogic mapaLogic = new MapaLogic();
        private Image boss;
        private Controller control = null;
        private ControllerLoop ctrlLoop = null;

        public Mapa()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(100000);

            boss = createBoss();

            control = new Controller(this);
            ctrlLoop = new ControllerLoop(this, control);
        }

        private Image createBoss()
        {
            Image boss = new Image();
            string path = System.IO.Directory.GetCurrentDirectory() + "\\Assets\\boss.png";
            // se crea un bitmap a partir del uri de la imagen
            boss.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(path));
            boss.Width = 50;
            // estructura thickness para poder darle valores al margen
            Thickness margin = boss.Margin;
            margin.Left = 10;
            boss.Margin = margin;

            return boss;
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

        private void AccessLevel()
        {
            switch (levelSelected.DataContext)
            {
                case "Tienda":
                    Frame.Navigate(typeof(Tienda), null, new DrillInNavigationTransitionInfo());
                    break;
                case "1-1":
                case "Jefe 1":
                    Frame.Navigate(typeof(Combate), null, new DrillInNavigationTransitionInfo());
                    break;
            }
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            AccessLevel();
        }

        private void TeamButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Equipo), null, new DrillInNavigationTransitionInfo());
        }


        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            togglePause();
        }

        private void ChangeInfo(Level level)
        {
            mapaLogic.selLevel = level;

            if (level.Id == "Jefe 1")
            {
                TextInfo.Children.Add(boss);
            }
            else
            {
                if (TextInfo.Children.Contains(boss))
                {
                    TextInfo.Children.Remove(boss);
                }
            }
        }

        private void Level_Checked(object sender, RoutedEventArgs e)
        {
            levelSelected = sender as RadioButton;
            Info.Visibility = Visibility.Visible;

            foreach(Level level in mapaLogic.levelsList)
            {
                if((string)levelSelected.DataContext == level.Id)
                {
                    ChangeInfo(level);
                    break;
                }
            }
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

        private void Level_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (control.isKeyDown(VirtualKey.Enter)
                || control.isKeyDown(VirtualKey.GamepadX))
            {
                AccessLevel();
            }
        }
    }
}
