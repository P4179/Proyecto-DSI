using P4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz_Proyecto_DSI
{
    public class EquipoLogic : ObservableObject {
        public ObservableCollection<Weapon> weaponList1 { get; } = new ObservableCollection<Weapon>();
        public ObservableCollection<Weapon> weaponList2 { get; } = new ObservableCollection<Weapon>();

        private Weapon _selWeap1;
        public Weapon selectedWeapon1 {
            get => _selWeap1;
            set { Set(ref _selWeap1, value); }
        }

        private Weapon _selWeap2;
        public Weapon selectedWeapon2 {
            get => _selWeap2;
            set { Set(ref _selWeap2, value); }
        }
    }
}
