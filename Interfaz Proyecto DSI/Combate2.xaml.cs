using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
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
    public sealed partial class Combate2 : Page
    {
        private DispatcherTimer timer;
        public Combate2()
        {
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(100000);

            this.InitializeComponent();
        }

        void timer_Tick(object sender, object e)
        {
            Object focus = FocusManager.GetFocusedElement();
            RadioButton character = focus as RadioButton;
            if (character != null && character.Parent == CanvasMap)
            {
                character.IsChecked = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Combate), null, new SuppressNavigationTransitionInfo());
        }

        private void Character_Checked(object sender, RoutedEventArgs e)
        {
            const double scale = 2;

            RadioButton character = sender as RadioButton;
            // centro del personaje seleccionado en el mundo
            double posX = (double)character.GetValue(Canvas.LeftProperty) + GridMap.Margin.Left + character.ActualWidth / 2;
            double posY = (double)character.GetValue(Canvas.TopProperty) + GridMap.Margin.Top + character.ActualHeight / 2;

            // centro del canvas en el mundo
            double centerX = RootGrid.Width / 2;
            double centerY = RootGrid.Height / 2;

            // diferencia entre ambos, es decir, lo que debería moverse para estar en el centro
            double translateX = centerX - posX;
            double translateY = centerY - posY;

            TransformMap2.TranslateX = translateX;
            TransformMap2.TranslateY = translateY;
            TransformMap1.ScaleX = scale;
            TransformMap1.ScaleY = scale;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            TransformMap1.CenterX = RootGrid.Width / 2;
            TransformMap1.CenterY = RootGrid.Height / 2;
        }
    }
}
