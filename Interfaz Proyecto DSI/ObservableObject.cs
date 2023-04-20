using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace P4 {
    // Clase pública con la interfaz INotifyPropertyChanged implementada
    public class ObservableObject : INotifyPropertyChanged {

        // Interfaz que produce la notificación de que CurrentTime ha cambiado
        // (la clase debe activar el evento PropertyChanged cuando la interfaz
        // de usuario tenga que cambiar. Hay que agregarla a la declaración de la clase
        public event PropertyChangedEventHandler PropertyChanged;


        // Función que comprueba si el evento es NULL o no y lo invoca si no.
        // Si puede, Invoke recibirá el remitente (this) y un objeto recién
        // creado PropertyChangedEventArgs (que a su vez recibe un string
        // como nombre de la propiedad). Los suscriptores del evento PropertyChanged
        // recibirán el nombre de la propiedad y actuarán en consecuencia.
        protected void RaisePropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Método genérico, que se asegura de que el campo de respaldo y el valor son del mismo tipo.

        // El parámetro propertyName se decora con el atributo [CallerMemberName] (si no lo definimos,
        // propertyName adoptará el nombre del objeto que llame al Set. Por ejemplo, al llamarlo desde
        // IsNameNeeded, se colocará dicho objeto como tercer parámetro sin necesidad de usar el nameof())

        // Después, Set invoca a EqualityComparer<T>.Default.Equals para comparar el valor nuevo y actual
        // del campo. Si son iguales, devuelve false, y si no, se redefine con el nuevo valor y se genera
        // el evento PropertyChanged antes de devolver true.
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, newValue)) {
                return false;
            }

            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
