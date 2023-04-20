using P4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz_Proyecto_DSI
{
    public class TiendaLogic : ObservableObject {
        public ObservableCollection<CommObject> potionsList { get; } = new ObservableCollection<CommObject>();
        public ObservableCollection<Weapon> weaponList { get; } = new ObservableCollection<Weapon>();
        public ObservableCollection<CommObject> accessoriesList { get; } = new ObservableCollection<CommObject>();

        private Objeto _selObj;
        public Objeto selectedObject {
            get => _selObj;
            set { Set(ref _selObj, value); }
        }
        private Objeto _selWeap;
        public Objeto selectedWeapon {
            get => _selWeap;
            set { Set(ref _selWeap, value); }
        }

        private int _coins;
        public int coins {
            get => _coins;
            set { 
                Set(ref _coins, value);
                coinsTxt = coins.ToString() + " C";
            }
        }
        private string _coinsTxt;
        public string coinsTxt {
            get => _coinsTxt;
            set { Set(ref _coinsTxt, value); }
        }


    }
}
