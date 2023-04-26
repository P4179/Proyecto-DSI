using P4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class Ability
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Exp { get; set; }
        public string Infor { get; set; }
    }

    public class AbilitiesLogic : ObservableObject
    {
        public AbilitiesLogic()
        {
            var app = App.Current as App;
            exp = app.exp;
        }

        // Lista con las habilidades
        public ObservableCollection<Ability> abilitiesList { get; } = new ObservableCollection<Ability>()
        {
            new Ability() {Name="Habilidad 1", Exp=600,
                Infor="Lorem ipsum1 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
            new Ability() {Name="Habilidad 2", Exp=400,
                Infor="Lorem ipsum2 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
            new Ability() {Name="Habilidad 3", Exp=700,
                Infor="Lorem ipsum3 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
        };

        private Ability selectedAbility;
        // Notificación para la habilidad seleccionada
        public Ability selAbility
        {
            get => selectedAbility;
            set { Set(ref selectedAbility, value); }
        }

        private int experience;
        public int exp
        {
            get => experience;
            set
            {
                Set(ref experience, value);

                var app = App.Current as App;
                app.exp = experience;
            }
        }
    }

    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Abilities : Page
    {
        private DispatcherTimer timer;
        private AbilitiesLogic abilitiesLogic = new AbilitiesLogic();
        private RadioButton radioAbilitySelected;
        private ObservableCollection<RadioButton> radioAbilities = new ObservableCollection<RadioButton>();
        private ObservableCollection<FontIcon> fontIcons = new ObservableCollection<FontIcon>();

        public Abilities()
        {
            this.InitializeComponent();

            var app = (App.Current as App);
            radioAbilities.Add(Ability1);
            radioAbilities.Add(Ability2);
            radioAbilities.Add(Ability3);

            fontIcons.Add(TickAbility1);
            fontIcons.Add(TickAbility2);
            fontIcons.Add(TickAbility3);

            for (int i = 0; i < radioAbilities.Count; ++i)
            {
                radioAbilities[i].IsEnabled = !app.unlocked[i];
                if (!radioAbilities[i].IsEnabled)
                {
                    fontIcons[i].Opacity = 1;
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Equipo), null, new DrillInNavigationTransitionInfo());
        }

        private void Ability_GotFocus(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            radioButton.IsChecked = true;
        }

        private void Ability_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            radioAbilitySelected = radioButton;

            int index = radioAbilities.IndexOf(radioAbilitySelected);
            abilitiesLogic.selAbility = abilitiesLogic.abilitiesList[index];
        }

        private void CheckNextAbility()
        {
            foreach (RadioButton radioAbility in radioAbilities)
            {
                if (radioAbility.IsEnabled)
                {
                    radioAbility.IsChecked = true;
                    break;
                }
            }
        }

        private void UnlockAbility(int index)
        {
            var app = App.Current as App;
            Ability ability = abilitiesLogic.abilitiesList[index];

            if (ability.Exp <= abilitiesLogic.exp)
            {
                abilitiesLogic.exp -= ability.Exp;
                radioAbilitySelected.IsChecked = false;
                radioAbilitySelected.IsEnabled = false;
                fontIcons[index].Opacity = 1;
                app.unlocked[index] = true;

                CheckNextAbility();
            }
        }

        private void Unlock_Click(object sender, RoutedEventArgs e)
        {
            int index = radioAbilities.IndexOf(radioAbilitySelected);
            UnlockAbility(index);
        }
    }
}
