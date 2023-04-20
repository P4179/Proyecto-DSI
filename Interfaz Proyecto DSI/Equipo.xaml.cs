using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Interfaz_Proyecto_DSI
{
    public class Character
    {
        public string Name { get; set; }
        public string Glyph { get; set; }
        public Character() { }
    }

    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Equipo : Page
    {
        private CoreCursor cursorHand = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        private CoreCursor cursorArrow = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        private bool dragginObject = false;
        public bool itemTeamPressed = false;
        private bool itemTemplatePressed = false;

        // al añadir o quitar un objeto, se actualiza el grid solo porque se trata de una
        // ObservableCollection, es decir, que manda notificaciones cuando se modifican sus elementos
        private ObservableCollection<Character> TeamList { get; } = new ObservableCollection<Character>()
        {
            new Character() { Name="Character1", Glyph = "\xEA8C" },
            new Character() { Name="None", Glyph = "" },
            new Character() { Name="None", Glyph = "" },
            new Character() { Name="None", Glyph = "" }
        };

        private ObservableCollection<Character> TemplateList { get; } = new ObservableCollection<Character>()
        {
            // fila 1
            new Character() { Name="Character2", Glyph = "\xEA8C" },
            new Character() { Name="Character3", Glyph = "\xEA8C" },
            new Character() { Name="Character4", Glyph = "\xEA8C" },
            new Character() { Name="Character5", Glyph = "\xEA8C" },
            // fila 2
            new Character() { Name="None", Glyph = "" },
            new Character() { Name="None", Glyph = "" },
            new Character() { Name="None", Glyph = "" },
            new Character() { Name="None", Glyph = "" },
            // fila 3
            new Character() { Name="None", Glyph = "" },
            new Character() { Name="None", Glyph = "" },
            new Character() { Name="None", Glyph = "" },
            new Character() { Name="None", Glyph = "" },
        };

        public Equipo()
        {
            this.InitializeComponent();
            for (int i = 0; i < 10; i++) MyListView1.Items.Add("Arma " + i.ToString());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        private void ButtonTree_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Abilities), null, new DrillInNavigationTransitionInfo());
        }

        private void openWeaponList(object sender, RoutedEventArgs e)
        {
            weaponList.Visibility = Visibility.Visible;
        }
        private void closeWeaponList(object sender, RoutedEventArgs e)
        {
            weaponList.Visibility = Visibility.Collapsed;
        }

        private void TeamGrid_DragOver(object sender, DragEventArgs e)
        {
            // se comprueba que no está lleno
            bool full = true;
            foreach (Character cTeam in TeamList)
            {
                if (cTeam.Glyph == "")
                {
                    full = false;
                    break;
                }
            }

            // dependiendo de si está lleno o no, se realiza una operación u otra
            if (full)
            {
                e.AcceptedOperation = DataPackageOperation.None;

            }
            else
            {
                e.AcceptedOperation = DataPackageOperation.Move;
            }
        }

        private async void TeamGrid_Drop(object sender, DragEventArgs e)
        {
            Character cTemplate = null;
            string name = await e.DataView.GetTextAsync();

            // se encuentra el que se había enviado
            foreach (Character character in TemplateList)
            {
                if (character.Name == name)
                {
                    cTemplate = character;
                    break;
                }
            }

            foreach (Character cTeam in TeamList)
            {
                // se encuentra el primero vacío
                if (cTeam.Glyph == "")
                {
                    // se intercambian
                    int indexTeam = TeamList.IndexOf(cTeam);
                    TeamList[indexTeam] = cTemplate;
                    (TeamGrid.ContainerFromItem(cTemplate) as GridViewItem).IsEnabled = true;

                    int indexTemplate = TemplateList.IndexOf(cTemplate);
                    TemplateList[indexTemplate] = cTeam;
                    (TemplateGrid.ContainerFromItem(cTeam) as GridViewItem).IsEnabled = false;

                    break;
                }
            }

            Window.Current.CoreWindow.PointerCursor = cursorArrow;
            dragginObject = false;
        }

        private void TemplateGrid_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            Character character = e.Items[0] as Character;
            if (character.Glyph == "")
            {
                // se cancela
                e.Cancel = true;
            }
            else
            {
                e.Data.SetText(character.Name);
                e.Data.RequestedOperation = DataPackageOperation.Move;
                dragginObject = true;
            }
        }

        private void TemplateGrid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = cursorHand;
        }

        private void TemplateGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (!dragginObject)
            {
                Window.Current.CoreWindow.PointerCursor = cursorArrow;
            }
        }

        private void TeamGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*
            GridView g = sender as GridView;
            // devuelve el contenedor que guarda el item
            GridViewItem item = g.ContainerFromItem(e.ClickedItem) as GridViewItem;
            Character character = e.ClickedItem as Character;
            if (character.Glyph == "")
            {
                VisualStateManager.GoToState(item, "Normal", false);
                itemTeamPressed = false;
            }
            else
            {
                if (!itemTemplatePressed)
                {
                    itemTeamPressed = true;
                }
                else
                {
                    VisualStateManager.GoToState(item, "Normal", false);
                }
            }
            */
        }

        private void TemplateGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*
            GridView g = sender as GridView;
            // devuelve el contenedor que guarda el item
            GridViewItem item = g.ContainerFromItem(e.ClickedItem) as GridViewItem;
            Character character = e.ClickedItem as Character;
            if (character.Glyph == "")
            {
                VisualStateManager.GoToState(item, "Normal", false);
                itemTemplatePressed = false;
            }
            else
            {
                if (!itemTeamPressed)
                {
                    itemTemplatePressed = true;
                }
                else
                {
                    VisualStateManager.GoToState(item, "Normal", false);
                }
            }
            */
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            GridView gridView = sender as GridView;
            foreach(Character character in gridView.Items)
            {
                if (character.Glyph == "")
                {
                    GridViewItem item = gridView.ContainerFromItem(character) as GridViewItem;
                    item.IsEnabled = false;
                }
            }
        }
    }
}