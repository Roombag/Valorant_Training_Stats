using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Valorant_Training_Stats
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        static class Settings
        {
            public static string Practice_Mode;
            public static bool Bots_Strafe = false;
            public static bool Bot_Armor = false;
            public static bool Inf_Ammo = true;
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


        public MainWindow()
        {
            InitializeComponent();
            Initialize_Buttons();
            CreateDropdown();

        }

        public static class ValorantColors
        {
            public static SolidColorBrush ActiveButton = (SolidColorBrush)(new BrushConverter().ConvertFrom("#969971"));
            public static SolidColorBrush InactiveButton = (SolidColorBrush)(new BrushConverter().ConvertFrom("#858585"));

        }

        void CreateDropdown()
        {
            List<String> WeaponNames = new List<string>();
            //var test = typeof(Weapon).GetProperty;

        }

        public static class WeaponList
        {

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

            
            

        }



        void Initialize_Buttons()
        {
            Mode_Button(btn_Easy);
            Config_Button(btn_Inf_Ammo_On);
            Config_Button(btn_Armor_Off);
        }

        void Mode_Button(Button Pressed_Button)
        {

            List<Button> Mode_Buttons = new List<Button>()
            {
                btn_Easy,
                btn_Medium,
                btn_Hard,
                btn_Elim_50,
                btn_Elim_100
            };

            foreach(Button element in Mode_Buttons)
            {
                element.Background = ValorantColors.InactiveButton;
            }
            Pressed_Button.Background = ValorantColors.ActiveButton;

            Settings.Practice_Mode = Convert.ToString(Pressed_Button.Content);

        }

        void Config_Button(Button Pressed_Button)
        {
            switch (Pressed_Button.Name)
            {
                case "btn_Strafe":
                    if (Settings.Bots_Strafe == true)
                    {
                        btn_Strafe.Background = ValorantColors.InactiveButton;
                        Settings.Bots_Strafe = false;
                        break;
                    }
                    else
                    {
                        btn_Strafe.Background = ValorantColors.ActiveButton;
                        Settings.Bots_Strafe = true;
                        break;
                    }
                case "btn_Armor_On":
                    btn_Armor_On.Background = ValorantColors.ActiveButton;
                    btn_Armor_Off.Background = ValorantColors.InactiveButton;
                    Settings.Bot_Armor = true;
                    break;
                case "btn_Armor_Off":
                    btn_Armor_On.Background = ValorantColors.InactiveButton;
                    btn_Armor_Off.Background = ValorantColors.ActiveButton;
                    Settings.Bot_Armor = false;
                    break;
                case "btn_Inf_Ammo_On":
                    btn_Inf_Ammo_On.Background = ValorantColors.ActiveButton;
                    btn_Inf_Ammo_Off.Background = ValorantColors.InactiveButton;
                    Settings.Inf_Ammo = true;
                    break;
                case "btn_Inf_Ammo_Off":
                    btn_Inf_Ammo_On.Background = ValorantColors.InactiveButton;
                    btn_Inf_Ammo_Off.Background = ValorantColors.ActiveButton;
                    Settings.Inf_Ammo = false;
                    break;
                default:
                    break;
            }
        }

        void Save_Result()
        {
            string Output = txt_Result.Text + ", " + Settings.Practice_Mode + ", " + Settings.Bots_Strafe + ", " + Settings.Bot_Armor + ", " + Settings.Inf_Ammo + "\n";
            //string Output = "Mode: " + Settings.Practice_Mode + ", Score: " +txt_Result.Text+"\n";
            System.IO.File.AppendAllText(@"C:\Users\Public\TestFolder\Valorant_Practice_Stats.csv", Output);
        }

        private void btn_Easy_Click(object sender, RoutedEventArgs e)
        {
            Mode_Button((Button)sender);
        }

        private void btn_Medium_Click(object sender, RoutedEventArgs e)
        {
            Mode_Button((Button)sender);
        }

        private void btn_Hard_Click(object sender, RoutedEventArgs e)
        {
            Mode_Button((Button)sender);
        }

        private void btn_Elim_50_Click(object sender, RoutedEventArgs e)
        {
            Mode_Button((Button)sender);
        }

        private void btn_Elim_100_Click(object sender, RoutedEventArgs e)
        {
            Mode_Button((Button)sender);
        }

        private void btn_Strafe_Click(object sender, RoutedEventArgs e)
        {
            Config_Button((Button)sender);
        }

        private void btn_Armor_On_Click(object sender, RoutedEventArgs e)
        {
            Config_Button((Button)sender);
        }

        private void btn_Armor_Off_Click(object sender, RoutedEventArgs e)
        {
            Config_Button((Button)sender);
        }

        private void btn_Inf_Ammo_On_Click(object sender, RoutedEventArgs e)
        {
            Config_Button((Button)sender);
        }

        private void btn_Inf_Ammo_Off_Click(object sender, RoutedEventArgs e)
        {
            Config_Button((Button)sender);
        }


        private void btn_Done_Click(object sender, RoutedEventArgs e)
        {
            Save_Result();
        }


        private void btn_Plus_Click(object sender, RoutedEventArgs e)
        {
            int result = Convert.ToInt32(txt_Result.Text);
            result++;
            txt_Result.Text = Convert.ToString(result);
        }

        private void btn_Minus_Click(object sender, RoutedEventArgs e)
        {
            int result = Convert.ToInt32(txt_Result.Text);
            result--;
            txt_Result.Text = Convert.ToString(result);
        }

        private void cbx_Weapon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
