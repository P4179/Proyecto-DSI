using P4;
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
using Windows.System;
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
        public int Life { get; set; }
        public int Displacement { get; set; }
    }

    public class CharacterLogic : ObservableObject
    {
        // al añadir o quitar un objeto, se actualiza el grid solo porque se trata de una
        // ObservableCollection, es decir, que manda notificaciones cuando se modifican sus elementos
        public ObservableCollection<Character> TeamList { get; } = new ObservableCollection<Character>()
        {
            new Character() {Name="Personaje 1", Glyph = "\xEA8C", Life=60, Displacement=2},
            new Character() {Glyph = ""},
            new Character() {Glyph = ""},
            new Character() {Glyph = ""}
        };

        public ObservableCollection<Character> TemplateList { get; } = new ObservableCollection<Character>()
        {
            // fila 1
            new Character() {Name="Personaje 2", Glyph = "\xEA8C", Life=32, Displacement=9},
            new Character() {Name="Personaje 3", Glyph = "\xEA8C", Life=81, Displacement=1},
            new Character() {Name="Personaje 4", Glyph = "\xEA8C", Life=76, Displacement=5},
            new Character() {Name="Personaje 5", Glyph = "\xEA8C", Life=90, Displacement=3},
            // fila 2
            new Character() {Glyph = ""},
            new Character() {Glyph = ""},
            new Character() {Glyph = ""},
            new Character() {Glyph = ""},
            // fila 3
            new Character() {Glyph = ""},
            new Character() {Glyph = ""},
            new Character() {Glyph = ""},
            new Character() {Glyph = ""},
        };

        private Character selectedCharacter;
        public Character selCharacter
        {
            get => selectedCharacter;
            set { Set(ref selectedCharacter, value); }
        }
    }

    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Equipo : Page
    {
        private CoreCursor cursorHand = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        private CoreCursor cursorArrow = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        private bool dragginObject = false;
        EquipoLogic teamLogic = new EquipoLogic();
        // Controlador con su bucle de lectura
        Controller control = null;
        ControllerLoop ctrlLoop = null;
        CharacterLogic characterLogic = new CharacterLogic();

        public Equipo()
        {

            this.InitializeComponent();

            WeaponNumber.Text = "Arma princ.";

            Weapon aux1 = new Weapon() { id = 0, displayPrice = "0", dmg = 5, effect = "Ninguno", name = "Arma principal 0", price = 0, reach = 5, type = "Espada" };
            Weapon aux2 = new Weapon() { id = 0, displayPrice = "0", dmg = 5, effect = "Ninguno", name = "Arma secundaria 0", price = 0, reach = 5, type = "Escudo" };
            teamLogic.selectedWeapon1 = aux1;
            teamLogic.selectedWeapon2 = aux2;
            teamLogic.weaponList1.Add(aux1);
            teamLogic.weaponList2.Add(aux2);
            foreach (var weap in (App.Current as App).boughtWeapons1)
                teamLogic.weaponList1.Add(weap);
            foreach (var weap in (App.Current as App).boughtWeapons2)
                teamLogic.weaponList2.Add(weap);

            control = new Controller(this);
            ctrlLoop = new ControllerLoop(this, control);
        }

        // BOTONES
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mapa), null, new DrillInNavigationTransitionInfo());
        }

        private void ButtonTree_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Abilities), null, new DrillInNavigationTransitionInfo());
        }

        // ARMAS
        private async void openWeaponList(object sender, RoutedEventArgs e)
        {
            weaponList.Visibility = Visibility.Visible;
            WeaponNumber.Text = "Arma princ.";
            Weapons1.Visibility = Visibility.Visible;
            Weapons2.Visibility = Visibility.Collapsed;

            ConfirmButton.Focus(FocusState.Programmatic);

            ListViewItem item = Weapons1.ContainerFromIndex(0) as ListViewItem;
            if (item != null)
            {
                item.Focus(FocusState.Programmatic);
                item.IsSelected = true;
                VisualStateManager.GoToState(item, "Pressed", true);
            }
            else
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
                {
                    item = Weapons1.ContainerFromIndex(0) as ListViewItem;
                    if (item != null)
                    {
                        item.Focus(FocusState.Programmatic);
                        item.IsSelected = true;
                        VisualStateManager.GoToState(item, "Pressed", true);
                    }
                });
        }

        private void closeWeaponList(object sender, RoutedEventArgs e)
        {
            weaponList.Visibility = Visibility.Collapsed;
        }

        private void changeWeaponList(object sender, RoutedEventArgs e)
        {
            if (Weapons1.Visibility == Visibility.Visible)
            {
                WeaponNumber.Text = "Arma secun.";
                Weapons1.Visibility = Visibility.Collapsed;
                Weapons2.Visibility = Visibility.Visible;
            }
            else
            {
                WeaponNumber.Text = "Arma princ.";
                Weapons1.Visibility = Visibility.Visible;
                Weapons2.Visibility = Visibility.Collapsed;
            }
        }

        private void weaponSelected(object sender, ItemClickEventArgs e)
        {
            ConfirmButton.Focus(FocusState.Programmatic);
            VisualStateManager.GoToState(ConfirmButton, "Focused", true);
        }

        private void keyDown(object sender, KeyRoutedEventArgs e)
        {

            if (weaponList.Visibility == Visibility.Visible)
            {
                if (control.isKeyDown(VirtualKey.GamepadLeftShoulder) || control.isKeyDown(VirtualKey.GamepadRightShoulder) ||
                    e.Key == Windows.System.VirtualKey.Left || e.Key == Windows.System.VirtualKey.Right)
                {
                    changeWeaponList(null, null);
                }
                if (e.Key == Windows.System.VirtualKey.Escape)
                {
                    closeWeaponList(null, null);
                }
            }
        }

        // PLANTILLAS
        private bool IsTeamFull()
        {
            // se comprueba que no está lleno
            bool full = true;
            foreach (Character cTeam in characterLogic.TeamList)
            {
                if (cTeam.Glyph == "")
                {
                    full = false;
                    break;
                }
            }
            return full;
        }

        private Character GetCharacterSent(string name, ObservableCollection<Character> characterList)
        {
            Character characterAux = null;
            // se encuentra el que se había enviado
            foreach (Character character in characterList)
            {
                if (character.Name == name)
                {
                    characterAux = character;
                    break;
                }
            }
            return characterAux;
        }

        private void Deselect(GridView gridView)
        {
            // se deselecciona el personaje de la plantilla que está seleccionado
            foreach (Character character in gridView.Items)
            {
                GridViewItem item = gridView.ContainerFromItem(character) as GridViewItem;
                if (item.IsSelected)
                {
                    item.IsSelected = false;
                    break;
                }
            }
        }

        private void TeamGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            // se deselecciona el personaje de la plantilla que está seleccionado
            Deselect(TemplateGrid);
            characterLogic.selCharacter = e.ClickedItem as Character;
        }

        private void TemplateGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            // se deselecciona el personaje del equipo que está seleccionado
            Deselect(TeamGrid);
            characterLogic.selCharacter = e.ClickedItem as Character;
        }

        private void DisableEmpty(GridView gridView)
        {
            foreach (Character character in gridView.Items)
            {
                GridViewItem item = gridView.ContainerFromItem(character) as GridViewItem;
                if (character.Glyph == "")
                {
                    item.IsEnabled = false;
                }
            }
        }

        private void Team_Loaded(object sender, RoutedEventArgs e)
        {
            GridView teamGrid = sender as GridView;
            DisableEmpty(teamGrid);

            // se marca el primer personaje de la plantilla
            foreach (Character character in teamGrid.Items)
            {
                GridViewItem item = teamGrid.ContainerFromItem(character) as GridViewItem;
                if (item.IsEnabled)
                {
                    characterLogic.selCharacter = character;
                    item.IsSelected = true;
                    break;
                }
            }
        }

        private void Template_Loaded(object sender, RoutedEventArgs e)
        {
            DisableEmpty(sender as GridView);
        }

        private bool IsOnGridView(Character character, GridView gridView)
        {
            return gridView.Items.Contains(character);
        }

        private void Grid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = cursorHand;
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {

            if (!dragginObject)
            {
                Window.Current.CoreWindow.PointerCursor = cursorArrow;
            }
        }

        private void closeGapTemplate()
        {
            bool completelyEmpty = false;
            int i = 0;
            while (i < characterLogic.TemplateList.Count && !completelyEmpty)
            {
                // se busca el primer hueco vacío
                // para intercambiarlo con el hueco que viene después que no está vacío
                if (characterLogic.TemplateList[i].Glyph == "")
                {
                    Character empty = characterLogic.TemplateList[i];
                    int j = i + 1;
                    // si no se ha encontrado ningún hueco no vacío quiere decir que solo hay
                    // huecos vacíos después, por lo tanto, se termina la búsqueda
                    completelyEmpty = true;
                    while (j < characterLogic.TemplateList.Count && completelyEmpty)
                    {
                        // se busca el primer hueco que no está vacío
                        if (characterLogic.TemplateList[j].Glyph != "")
                        {
                            // se intercambian
                            characterLogic.TemplateList[i] = characterLogic.TemplateList[j];
                            characterLogic.TemplateList[j] = empty;
                            completelyEmpty = false;

                            i = j;
                        }
                        ++j;
                    }
                }
                else
                {
                    ++i;
                }
            }
        }

        private async void TeamGrid_Drop(object sender, DragEventArgs e)
        {
            string name = await e.DataView.GetTextAsync();
            Character cTemplate = GetCharacterSent(name, characterLogic.TemplateList);

            if (cTemplate != null)
            {
                foreach (Character cTeam in characterLogic.TeamList)
                {
                    // se encuentra el primero vacío
                    if (cTeam.Glyph == "")
                    {
                        // se intercambian

                        // de la plantilla al equipo (que es hacia donde se ha movido)
                        int indexTeam = characterLogic.TeamList.IndexOf(cTeam);
                        characterLogic.TeamList[indexTeam] = cTemplate;
                        // se habilita
                        var teamItem = (TeamGrid.ContainerFromItem(cTemplate) as GridViewItem);
                        teamItem.IsEnabled = true;
                        characterLogic.selCharacter = cTemplate;
                        teamItem.IsSelected = true;

                        // del equipo a la plantilla (casilla vacía)
                        int indexTemplate = characterLogic.TemplateList.IndexOf(cTemplate);
                        characterLogic.TemplateList[indexTemplate] = cTeam;
                        // se deshabilita
                        var templateItem = (TemplateGrid.ContainerFromItem(cTeam) as GridViewItem);
                        templateItem.IsEnabled = false;
                        Deselect(TemplateGrid);

                        // cerrar hueco en el template
                        closeGapTemplate();

                        break;
                    }
                }
            }
        }

        private async void TeamGrid_DragOver(object sender, DragEventArgs e)
        {
            string name = await e.DataView.GetTextAsync();
            Character characterTeam = GetCharacterSent(name, characterLogic.TeamList);

            // dependiendo de si está lleno o no, se realiza una operación u otra
            if (IsTeamFull() || IsOnGridView(characterTeam, TeamGrid))
            {
                e.AcceptedOperation = DataPackageOperation.None;

            }
            else
            {
                e.AcceptedOperation = DataPackageOperation.Move;
            }
        }

        private void Grid_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
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

        private async void TemplateGrid_Drop(object sender, DragEventArgs e)
        {
            string name = await e.DataView.GetTextAsync();
            Character cTeam = GetCharacterSent(name, characterLogic.TeamList);

            if (cTeam != null)
            {
                foreach (Character cTemplate in characterLogic.TemplateList)
                {
                    // se encuentra el primero vacío
                    if (cTemplate.Glyph == "")
                    {
                        // se intercambian

                        // del equipo a la plantilla (que es hacia donde se ha movido)
                        int indexTemplate = characterLogic.TemplateList.IndexOf(cTemplate);
                        characterLogic.TemplateList[indexTemplate] = cTeam;
                        // se habilita
                        var templateItem = (TemplateGrid.ContainerFromItem(cTeam) as GridViewItem);
                        templateItem.IsEnabled = true;
                        characterLogic.selCharacter = cTeam;
                        templateItem.IsSelected = true;

                        // de la plantilla al equipo (casilla vacía)
                        int indexTeam = characterLogic.TeamList.IndexOf(cTeam);
                        characterLogic.TeamList[indexTeam] = cTemplate;
                        // se deshabilita
                        var teamItem = (TeamGrid.ContainerFromItem(cTemplate) as GridViewItem);
                        teamItem.IsEnabled = false;
                        Deselect(TeamGrid);

                        break;
                    }
                }
            }
        }

        private async void TemplateGrid_DragOver(object sender, DragEventArgs e)
        {
            string name = await e.DataView.GetTextAsync();
            Character characterTemplate = GetCharacterSent(name, characterLogic.TemplateList);

            // dependiendo de si está lleno o no, se realiza una operación u otra
            if (IsOnGridView(characterTemplate, TemplateGrid))
            {
                e.AcceptedOperation = DataPackageOperation.None;

            }
            else
            {
                e.AcceptedOperation = DataPackageOperation.Move;
            }
        }

        private void Grid_DragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
        {
            Window.Current.CoreWindow.PointerCursor = cursorArrow;
            dragginObject = false;
        }
    }
}