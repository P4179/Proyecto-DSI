using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Interfaz_Proyecto_DSI
{
    public class Controller {

        private readonly object myLock = new object();
        private List<Gamepad> myGamepads = new List<Gamepad>();
        public Gamepad mainGamepad = null;

        // Lectura del mando
        public GamepadReading gamepadState;
        Page page = null;

        // Constructora
        public Controller(Page main) {
            page = main;

            gamepadAdded();
            gamepadRemoved();
        }
        // Función llamada al conectar un mando
        public void gamepadAdded() {
            Gamepad.GamepadAdded += (object sender, Gamepad e) => {
                // Check if the just-added gamepad is already in myGamepads; if it isn't, add it.
                lock (myLock)
                {
                    bool gamepadInList = myGamepads.Contains(e);

                    if (!gamepadInList)
                    {
                        myGamepads.Add(e);
                        if (myGamepads[0] != null && mainGamepad == null)
                            mainGamepad = myGamepads[0];
                    }
                }
            };
        }
        // Función llamada al desconectar un mando
        public void gamepadRemoved()
        {
            Gamepad.GamepadRemoved += (object sender, Gamepad e) => {
                lock (myLock)
                {
                    int indexRemoved = myGamepads.IndexOf(e);

                    if (indexRemoved > -1)
                    {
                        if (mainGamepad == myGamepads[indexRemoved])
                            mainGamepad = null;

                        myGamepads.RemoveAt(indexRemoved);
                    }
                }
            };
        }


        // Obtiene el estado actual del gamepad
        public void readGamepad()
        {
            if (mainGamepad != null)
            {
                gamepadState = mainGamepad.GetCurrentReading();
            }
        }
        public bool isKeyDown(VirtualKey key) {
            var keystate = Window.Current.CoreWindow.GetKeyState(key);
            return (keystate & Windows.UI.Core.CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }


        // Deadzone del los sticks y triggers
        // (Si el valor del stick o el trigger hacia la dirección correspondiente superan la
        // deadzone, se mueven en dicha dirección +- la deadzone. Si no, pone su valor a 0)
        const float INPUT_DEADZONE = 0.1f;
        public void deadZone()
        {

            if (gamepadState.RightThumbstickX < -INPUT_DEADZONE)
                gamepadState.RightThumbstickX += INPUT_DEADZONE;
            else if (gamepadState.RightThumbstickX > INPUT_DEADZONE)
                gamepadState.RightThumbstickX -= INPUT_DEADZONE;
            else
                gamepadState.RightThumbstickX = 0;

            if (gamepadState.RightThumbstickY < -INPUT_DEADZONE)
                gamepadState.RightThumbstickY += INPUT_DEADZONE;
            else if (gamepadState.RightThumbstickY > INPUT_DEADZONE)
                gamepadState.RightThumbstickY -= INPUT_DEADZONE;
            else
                gamepadState.RightThumbstickY = 0;

            if (gamepadState.LeftTrigger < -INPUT_DEADZONE)
                gamepadState.LeftTrigger += INPUT_DEADZONE;
            else if (gamepadState.LeftTrigger > INPUT_DEADZONE)
                gamepadState.LeftTrigger -= INPUT_DEADZONE;
            else
                gamepadState.LeftTrigger = 0;

            if (gamepadState.RightTrigger < -INPUT_DEADZONE)
                gamepadState.RightTrigger += INPUT_DEADZONE;
            else if (gamepadState.RightTrigger > INPUT_DEADZONE)
                gamepadState.RightTrigger -= INPUT_DEADZONE;
            else
                gamepadState.RightTrigger = 0;
        }


        // Actualiza la UI dependiendo de los valores del gamepad
        public void updateUI()
        {
            //mainPage.updateUI();
        }


        // Vibración del mando
        // (motor izquierdo para los gatillos, motor derecho para el stick derecho)
        private GamepadVibration vibration;
        public void feedback()
        {
            if (mainGamepad != null)
            {
                // Si los valores del stick derecho (en cualquier dirección) son distintos de 0
                if ((gamepadState.RightThumbstickX != 0) || (gamepadState.RightThumbstickY != 0))
                {
                    // Si el cuadrado de los valores del eje X es mayor al de los valores del eje Y,
                    // la vibración del motor derecho es igual al cuadrado de los valores del eje X
                    if (gamepadState.RightThumbstickX * gamepadState.RightThumbstickX > gamepadState.RightThumbstickY * gamepadState.RightThumbstickY)
                        vibration.RightMotor = gamepadState.RightThumbstickX * gamepadState.RightThumbstickX;
                    // Si no, la vibración del motor derecho es igual al cuadrado de los valores del eje Y
                    else
                        vibration.RightMotor = gamepadState.RightThumbstickY * gamepadState.RightThumbstickY;
                }
                // Si no, los valores el stick son 0 y la vibración del motor izquierdo es 0
                else
                    vibration.RightMotor = 0;

                // Si el valor de cualquiera de los gatillos son distintos de 0
                if ((gamepadState.LeftTrigger != 0) || (gamepadState.RightTrigger != 0))
                {
                    // Si el valor del gatillo izquierdo es mayor al del gatillo derecho,
                    // la vibración del motor izquierdo es igual al valor del gatillo izquierdo
                    if (gamepadState.LeftTrigger > gamepadState.RightTrigger)
                        vibration.LeftMotor = gamepadState.LeftTrigger;
                    // Si no, la vibración del motor izquierdo es igual al valor del gatillo derecho
                    else
                        vibration.LeftMotor = gamepadState.RightTrigger;
                }
                // Si no, los valores de los gatillos son 0 y la vibración del motor izquierdo es 0;
                else
                    vibration.LeftMotor = 0;

                // Asigna a la vibración del mando el valor de la vibración obtenido
                mainGamepad.Vibration = vibration;
            }
        }


    }
}
