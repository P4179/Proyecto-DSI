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

        public TiendaLogic() {
            coins = (App.Current as App).coins;
        }

        public ObservableCollection<CommObject> potionsList { get; } = new ObservableCollection<CommObject>();
        public ObservableCollection<Weapon> weaponList { get; } = new ObservableCollection<Weapon>();
        public ObservableCollection<CommObject> accessoriesList { get; } = new ObservableCollection<CommObject>();


        private Objeto _selObj;
        public Objeto selectedObject {
            get => _selObj;
            set { Set(ref _selObj, value); }
        }
        private CommObject _selCommObj;
        public CommObject selectedCommonObj
        {
            get => _selCommObj;
            set { Set(ref _selCommObj, value); }
        }

        private CommObject _selPot;
        public CommObject selectedPotion {
            get => _selPot;
            set { 
                Set(ref _selPot, value);
                selectedObject = _selPot;
                selectedCommonObj = _selPot;
            }
        }
        private Weapon _selWeap;
        public Weapon selectedWeapon {
            get => _selWeap;
            set { 
                Set(ref _selWeap, value);
                selectedObject = _selWeap;
            }
        }
        private CommObject _selAcc;
        public CommObject selectedAccessory {
            get => _selAcc;
            set { 
                Set(ref _selAcc, value);
                selectedObject = _selAcc;
                selectedCommonObj = _selAcc;
            }
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
