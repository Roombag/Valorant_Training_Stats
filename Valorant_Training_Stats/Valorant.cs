using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Valorant
{
    public static class Colors
    {
        public static SolidColorBrush ActiveButton = (SolidColorBrush)(new BrushConverter().ConvertFrom("#969971"));
        public static SolidColorBrush InactiveButton = (SolidColorBrush)(new BrushConverter().ConvertFrom("#858585"));

    }

    public class WeaponType
    {
        public string name { get; private set; }
        public int id { get; private set; }
        public WeaponType(string name = "", int id = 0)
        {
            this.name = name;
            this.id = id;
        }

    }

    public class Weapon
    {

        public string name { get; private set; }
        public WeaponType type { get; private set; }

        public Weapon(string name, WeaponType type)
        {
            this.name = name;
            this.type = type;
        }

    }
    public static class Weapons
    {
        // Trying to make generation of WeaponType elements easier
        // internal static List<String> WeaponTypeList = new List<string> { "Sidearm", "SMG", "Shotgun", "Rifle", "Sniper", "Heavy" };
        // public static List<String> WeaponTypeList = new List<string> { "Sidearm", "SMG", "Shotgun", "Rifle", "Sniper", "Heavy" };


        public static WeaponType Sidearm = new WeaponType("Sidearm", 1);
        public static WeaponType SMG = new WeaponType("SMG", 2);
        public static WeaponType Shotgun = new WeaponType("Shotgun", 3);
        public static WeaponType Rifle = new WeaponType("Rifle", 4);
        public static WeaponType Sniper = new WeaponType("Sniper", 5);
        public static WeaponType Heavy = new WeaponType("Heavy", 6);

        public static Weapon Classic = new Weapon("Classic", Sidearm);
        public static Weapon Shorty = new Weapon("Shorty", Sidearm);
        public static Weapon Frenzy = new Weapon("Frenzy", Sidearm);
        public static Weapon Ghost = new Weapon("Ghost", Sidearm);
        public static Weapon Sheriff = new Weapon("Sheriff", Sidearm);

        public static Weapon Stinger = new Weapon("Stinger", SMG);
        public static Weapon Spectre = new Weapon("Spectre", SMG);

        public static Weapon Bucky = new Weapon("Bucky", Shotgun);
        public static Weapon Judge = new Weapon("Judge", Shotgun);

        public static Weapon Bulldog = new Weapon("Bulldog", Rifle);
        public static Weapon Guardian = new Weapon("Guardian", Rifle);
        public static Weapon Phantom = new Weapon("Phantom", Rifle);
        public static Weapon Vandal = new Weapon("Vandal", Rifle);

        public static Weapon Marshal = new Weapon("Marshal", Sniper);
        public static Weapon Operator = new Weapon("Operator", Rifle);

        public static Weapon Ares = new Weapon("Ares", Heavy);
        public static Weapon Odin = new Weapon("Odin", Heavy);

        public static List<Weapon> WeaponList = new List<Weapon> 
        {   Classic, Shorty, Frenzy, Ghost, Sheriff, 
            Stinger, Spectre, Bucky, Judge,
            Bulldog, Guardian, Phantom, Vandal, 
            Marshal, Operator, Ares, Odin
        };


    }

    
}
