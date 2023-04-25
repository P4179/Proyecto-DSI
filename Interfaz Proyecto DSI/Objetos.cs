using P4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz_Proyecto_DSI
{
    public class Objeto : ObservableObject {
        public int id { get; set; }

        public string name { get; set; }
        public int price { get; set; }
        public string displayPrice { get; set; }

        public Objeto() { }
    }

    public class CommObject : Objeto {
        public string desc { get; set; }
        public CommObject() { }
    }

    public class Weapon : Objeto {
        public string type { get; set; }
        public int dmg { get; set; }
        public string effect { get; set; }
        public int reach { get; set; }

        public Weapon() { }
    }

    public class Potion : CommObject {
        public int amount { get; set; }
        public string displayAmount { get; set; }
        public Potion() { }
    }


    public class ObjectLists {
        public static List<CommObject> potions { get; } = new List<CommObject>() {
            new CommObject() {id = 0, name = "Poción 1", price = 100, displayPrice = "100 C", desc = "Lore Ipsum 1 poc" },
            new CommObject() {id = 1, name = "Poción 2", price = 200, displayPrice = "200 C", desc = "Lore Ipsum 2 poc"  },
            new CommObject() {id = 2, name = "Poción 3", price = 300, displayPrice = "300 C", desc = "Lore Ipsum 3 poc"  },
            new CommObject() {id = 3,  name = "Poción 4", price = 400, displayPrice = "400 C", desc = "Lore Ipsum 4 poc"  },
            new CommObject() {id = 4,  name = "Poción 5", price = 500, displayPrice = "500 C", desc = "Lore Ipsum 5 poc"  },
            new CommObject() {id = 5,  name = "Poción 6", price = 600, displayPrice = "600 C", desc = "Lore Ipsum 6 poc"  },
            new CommObject() {id = 6,  name = "Poción 7", price = 700, displayPrice = "700 C", desc = "Lore Ipsum 7 poc"  },
            new CommObject() {id = 7,  name = "Poción 8", price = 800, displayPrice = "800 C", desc = "Lore Ipsum 8 poc"  },
            new CommObject() {id = 8,  name = "Poción 9", price = 900, displayPrice = "900 C", desc = "Lore Ipsum 9 poc"  },
            new CommObject() {id = 9,  name = "Poción 10", price = 1000, displayPrice = "1000 C", desc = "Lore Ipsum 10 poc"  },
            new CommObject() {id = 10,  name = "Poción 11", price = 1100, displayPrice = "1100 C", desc = "Lore Ipsum 11 poc"  }
        };

        public static List<Weapon> weapons { get; } = new List<Weapon>() {
            new Weapon() {id = 0, name = "Arma 1", price = 100, displayPrice = "100 C", type="Escudo", dmg = 10, effect = "Efecto 1", reach = 1 },
            new Weapon() {id = 1,  name = "Arma 2", price = 200, displayPrice = "200 C", type="Espada", dmg = 20, effect = "Efecto 2", reach = 2 },
            new Weapon() {id = 2,  name = "Arma 3", price = 300, displayPrice = "300 C", type="Lanza", dmg = 30, effect = "Efecto 3", reach = 3 },
            new Weapon() {id = 3,  name = "Arma 4", price = 400, displayPrice = "400 C", type="Espada", dmg = 40, effect = "Efecto 4", reach = 4 },
            new Weapon() {id = 4,  name = "Arma 5", price = 500, displayPrice = "500 C", type="Lanza", dmg = 50, effect = "Efecto 5", reach = 5 },
            new Weapon() {id = 5,  name = "Arma 6", price = 600, displayPrice = "600 C", type="Lanza", dmg = 60, effect = "Efecto 6", reach = 6 },
            new Weapon() {id = 6,  name = "Arma 7", price = 700, displayPrice = "700 C", type="Escudo", dmg = 70, effect = "Efecto 7", reach = 7 },
            new Weapon() {id = 7, name = "Arma 8", price = 800, displayPrice = "800 C", type="Escudo", dmg = 80, effect = "Efecto 8", reach = 8 },
            new Weapon() {id = 8,  name = "Arma 9", price = 900, displayPrice = "900 C", type="Espada", dmg = 90, effect = "Efecto 9", reach = 9 },
            new Weapon() {id = 9,  name = "Arma 10", price = 1000, displayPrice = "1000 C", type="Espada", dmg = 100, effect = "Efecto 10", reach = 10 },
            new Weapon() {id = 10,  name = "Arma 11", price = 1100, displayPrice = "1100 C", type="Lanza", dmg = 110, effect = "Efecto 11", reach = 11}
        };


        public static List<CommObject> accessories { get; } = new List<CommObject>() {
            new CommObject() {id = 0, name = "Accesorio 1", price = 100, displayPrice = "100 C", desc = "Lore Ipsum 1 acc" },
            new CommObject() {id = 1,  name = "Accesorio 2", price = 200, displayPrice = "200 C", desc = "Lore Ipsum 2 acc" },
            new CommObject() {id = 2,  name = "Accesorio 3", price = 300, displayPrice = "300 C", desc = "Lore Ipsum 3 acc" },
            new CommObject() {id = 3,  name = "Accesorio 4", price = 400, displayPrice = "400 C", desc = "Lore Ipsum 4 acc" },
            new CommObject() {id = 4,  name = "Accesorio 5", price = 500, displayPrice = "500 C", desc = "Lore Ipsum 5 acc" },
            new CommObject() {id = 5,  name = "Accesorio 6", price = 600, displayPrice = "600 C", desc = "Lore Ipsum 6 acc" },
            new CommObject() {id = 6,  name = "Accesorio 7", price = 700, displayPrice = "700 C", desc = "Lore Ipsum 7 acc" },
            new CommObject() {id = 7,  name = "Accesorio 8", price = 800, displayPrice = "800 C", desc = "Lore Ipsum 8 acc" },
            new CommObject() {id = 8,  name = "Accesorio 9", price = 900, displayPrice = "900 C", desc = "Lore Ipsum 9 acc" },
            new CommObject() {id = 9,  name = "Accesorio 10", price = 1000, displayPrice = "1000 C", desc = "Lore Ipsum 10 acc"},
            new CommObject() {id = 10,  name = "Accesorio 11", price = 1100, displayPrice = "1100 C", desc = "Lore Ipsum 11 acc" }
        };

        public static IList<CommObject> getPotions() { return potions; }
        public static IList<Weapon> getWeapons() { return weapons; }
        public static IList<CommObject> getAccessories() { return accessories; }

        public static CommObject getPotByID(int id) { return potions[id]; }
        public static Weapon getWeapByID(int id) { return weapons[id]; }
        public static CommObject getAccByID(int id) { return accessories[id]; }


    }


    public class Opcion : ObservableObject {
        public int id { get; set; }

        public string name { get; set; }
        public string desc { get; set; }

        public Opcion() { }
    }

    public class OptionLists {
        public static List<Opcion> graphicOptions { get; } = new List<Opcion>() {
            new Opcion() {id = 0, name = "Opción graf 1", desc = "Lore Ipsum graf 1"},
            new Opcion() {id = 1, name = "Opción graf 2", desc = "Lore Ipsum graf 2"},
            new Opcion() {id = 2, name = "Opción graf 3", desc = "Lore Ipsum graf 3"},
            new Opcion() {id = 3, name = "Opción graf 4", desc = "Lore Ipsum graf 4"}
        };
        public static List<Opcion> soundOptions { get; } = new List<Opcion>() {
            new Opcion() {id = 0, name = "Opción son 1", desc = "Lore Ipsum son 1"},
            new Opcion() {id = 1, name = "Opción son 2", desc = "Lore Ipsum son 2"},
            new Opcion() {id = 2, name = "Opción son 3", desc = "Lore Ipsum son 3"},
            new Opcion() {id = 3, name = "Opción son 4", desc = "Lore Ipsum son 4"}
        };
        public static List<Opcion> accessOptions { get; } = new List<Opcion>() {
            new Opcion() {id = 0, name = "Opción access 1", desc = "Lore Ipsum acc 1"},
            new Opcion() {id = 1, name = "Opción access 2", desc = "Lore Ipsum acc 2"},
            new Opcion() {id = 2, name = "Opción access 3", desc = "Lore Ipsum acc 3"},
            new Opcion() {id = 3, name = "Opción access 4", desc = "Lore Ipsum acc 4"}
        };

        public static IList<Opcion> getGraphs() { return graphicOptions; }
        public static IList<Opcion> getSound() { return soundOptions; }
        public static IList<Opcion> getAccess() { return accessOptions; }

    }
    



}
