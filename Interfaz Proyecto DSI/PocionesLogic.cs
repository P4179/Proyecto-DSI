using P4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz_Proyecto_DSI
{
    public class PocionesLogic : ObservableObject {
        public ObservableCollection<Potion> potionsList { get; } = new ObservableCollection<Potion>();

        private CommObject _selPot;
        public CommObject selectedPotion {
            get => _selPot;
            set { Set(ref _selPot, value); }
        }
    }




}
