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


    public class ObjectLists {
        public static List<CommObject> potions { get; } = new List<CommObject>() {
            new CommObject() {id = 0, name = "Poción 1", price = 100, displayPrice = "100 C", desc = "Lore Ipsum 1" },
            new CommObject() {id = 1, name = "Poción 2", price = 200, displayPrice = "200 C", desc = "Lore Ipsum 2"  },
            new CommObject() {id = 2, name = "Poción 3", price = 300, displayPrice = "300 C", desc = "Lore Ipsum 3"  },
            new CommObject() {id = 3,  name = "Poción 4", price = 400, displayPrice = "400 C", desc = "Lore Ipsum 4"  },
            new CommObject() {id = 4,  name = "Poción 5", price = 500, displayPrice = "500 C", desc = "Lore Ipsum 5"  },
            new CommObject() {id = 5,  name = "Poción 6", price = 600, displayPrice = "600 C", desc = "Lore Ipsum 6"  },
            new CommObject() {id = 6,  name = "Poción 7", price = 700, displayPrice = "700 C", desc = "Lore Ipsum 7"  },
            new CommObject() {id = 7,  name = "Poción 8", price = 800, displayPrice = "800 C", desc = "Lore Ipsum 8"  },
            new CommObject() {id = 8,  name = "Poción 9", price = 900, displayPrice = "900 C", desc = "Lore Ipsum 9"  },
            new CommObject() {id = 9,  name = "Poción 10", price = 1000, displayPrice = "1000 C", desc = "Lore Ipsum 10"  },
            new CommObject() {id = 10,  name = "Poción 11", price = 1100, displayPrice = "1100 C", desc = "Lore Ipsum 11"  }
        };

        public static List<Weapon> weapons { get; } = new List<Weapon>() {
            new Weapon() {id = 0, name = "Arma 1", price = 100, displayPrice = "100 C", dmg = 10, effect = "Efecto 1", reach = 1 },
            new Weapon() {id = 1,  name = "Arma 2", price = 200, displayPrice = "200 C", dmg = 20, effect = "Efecto 2", reach = 2 },
            new Weapon() {id = 2,  name = "Arma 3", price = 300, displayPrice = "300 C", dmg = 30, effect = "Efecto 3", reach = 3 },
            new Weapon() {id = 3,  name = "Arma 4", price = 400, displayPrice = "400 C", dmg = 40, effect = "Efecto 4", reach = 4 },
            new Weapon() {id = 4,  name = "Arma 5", price = 500, displayPrice = "500 C", dmg = 50, effect = "Efecto 5", reach = 5 },
            new Weapon() {id = 5,  name = "Arma 6", price = 600, displayPrice = "600 C", dmg = 60, effect = "Efecto 6", reach = 6 },
            new Weapon() {id = 6,  name = "Arma 7", price = 700, displayPrice = "700 C", dmg = 70, effect = "Efecto 7", reach = 7 },
            new Weapon() {id = 7, name = "Arma 8", price = 800, displayPrice = "800 C", dmg = 80, effect = "Efecto 8", reach = 8 },
            new Weapon() {id = 8,  name = "Arma 9", price = 900, displayPrice = "900 C", dmg = 90, effect = "Efecto 9", reach = 9 },
            new Weapon() {id = 9,  name = "Arma 10", price = 1000, displayPrice = "1000 C", dmg = 100, effect = "Efecto 10", reach = 10 },
            new Weapon() {id = 10,  name = "Arma 11", price = 1100, displayPrice = "1100 C" , dmg = 110, effect = "Efecto 11", reach = 11}
        };


        public static List<CommObject> accessories { get; } = new List<CommObject>() {
            new CommObject() {id = 0, name = "Accesorio 1", price = 100, displayPrice = "100 C", desc = "Lore Ipsum 1" },
            new CommObject() {id = 1,  name = "Accesorio 2", price = 200, displayPrice = "200 C", desc = "Lore Ipsum 2" },
            new CommObject() {id = 2,  name = "Accesorio 3", price = 300, displayPrice = "300 C", desc = "Lore Ipsum 3" },
            new CommObject() {id = 3,  name = "Accesorio 4", price = 400, displayPrice = "400 C", desc = "Lore Ipsum 4" },
            new CommObject() {id = 4,  name = "Accesorio 5", price = 500, displayPrice = "500 C", desc = "Lore Ipsum 5" },
            new CommObject() {id = 5,  name = "Accesorio 6", price = 600, displayPrice = "600 C", desc = "Lore Ipsum 6" },
            new CommObject() {id = 6,  name = "Accesorio 7", price = 700, displayPrice = "700 C", desc = "Lore Ipsum 7" },
            new CommObject() {id = 7,  name = "Accesorio 8", price = 800, displayPrice = "800 C", desc = "Lore Ipsum 8" },
            new CommObject() {id = 8,  name = "Accesorio 9", price = 900, displayPrice = "900 C", desc = "Lore Ipsum 9" },
            new CommObject() {id = 9,  name = "Accesorio 10", price = 1000, displayPrice = "1000 C", desc = "Lore Ipsum 10"},
            new CommObject() {id = 10,  name = "Accesorio 11", price = 1100, displayPrice = "1100 C", desc = "Lore Ipsum 11" }
        };

        public static IList<CommObject> getPotions() { return potions; }
        public static IList<Weapon> getWeapons() { return weapons; }
        public static IList<CommObject> getAccessories() { return accessories; }

        public static CommObject getPotByID(int id) { return potions[id]; }
        public static Weapon getWeapByID(int id) { return weapons[id]; }
        public static CommObject getAccByID(int id) { return accessories[id]; }


    }


}
