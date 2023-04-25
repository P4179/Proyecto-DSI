using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Interfaz_Proyecto_DSI
{
    internal class ControllerLoop {

        Page page = null;
        Controller control = null;
        private DispatcherTimer GamePadTimer;


        // Constructora que recibe la página y el controlador
        public ControllerLoop(Page main, Controller ctrl)
        {
            page = main;
            control = ctrl;

            GamePadTimerSetup();
        }

        // Configura el bucle de eventos del mando
        public void GamePadTimerSetup()
        {
            GamePadTimer = new DispatcherTimer();
            GamePadTimer.Tick += GamePadTimer_Tick;
            GamePadTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            GamePadTimer.Start();
        }

        // Bucle de eventos del mando
        void GamePadTimer_Tick(object sender, object e)
        {
            if (page != null && control != null) {
                control.readGamepad();          //Lee GamePad
                //control.detectGestures();       //Detecta Gestos del Mando
                control.deadZone();             //ZonaMuerta (JoyStick y Triggers)
                //control.applyGesture();         //Aplica Gestos en IU
                //control.applyKeyboard();        //Aplica Teclado continuo en IU
                //control.updateUI();               //Aplica cambios en IU y VM
                //control.feedback();             //Activa motores del Mando
            }
        }





    }
}
