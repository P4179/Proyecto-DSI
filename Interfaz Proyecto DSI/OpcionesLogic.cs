using P4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz_Proyecto_DSI
{
    public class OpcionesLogic : ObservableObject {

        public ObservableCollection<Opcion> graphList { get; } = new ObservableCollection<Opcion>();
        public ObservableCollection<Opcion> soundList { get; } = new ObservableCollection<Opcion>();
        public ObservableCollection<Opcion> accList{ get; } = new ObservableCollection<Opcion>();


        private Opcion _selOpt;
        public Opcion selectedOption {
            get => _selOpt;
            set { Set(ref _selOpt, value); }
        }

        private Opcion _selGraph;
        public Opcion selectedGraph {
            get => _selGraph;
            set { Set(ref _selGraph, value); }
        }
        private Opcion _selSound;
        public Opcion selectedSound {
            get => _selSound;
            set { Set(ref _selSound, value); }
        }
        private Opcion _selAcc;
        public Opcion selectedAccess {
            get => _selAcc;
            set { Set(ref _selAcc, value); }
        }

        private Opcion _control=  new Opcion() { desc = "Lore Ipsum cont", id = 0, name = "Controles" };
        public Opcion controlsOption {
            get => _control;

        }
    }
}
