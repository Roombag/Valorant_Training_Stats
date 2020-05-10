using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using Valorant;
using NHotkey.Wpf;
using NHotkey;

namespace Valorant_Training_Stats
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            InitializeButtons();
            CreateDropdown();
            SetHotkeys();

            string teast = Weapons.WeaponList[1].name;
        }

        void InitializeButtons()
        {
            ModeButtonPressed(btn_Easy);
            ConfigButtonPressed(btn_Inf_Ammo_On);
            ConfigButtonPressed(btn_Armor_Off);
        }

        void CreateDropdown()
        {
            {// I didn't know how to foreach....

            // List<String> WeaponNames = new List<string>();
            //for (int i = 0; i < Valorant.Weapons.WeaponList.Count; i++)
            }
            foreach (Weapon item in Valorant.Weapons.WeaponList)
            {
                cbx_Weapon_Select.Items.Add(item.name);
            }
            {
                //WeaponNames.Add(Valorant.Weapons.WeaponList[i].name);
                //cbx_Weapon_Select.Items.Add(Valorant.Weapons.WeaponList[i].name
                // cbx_Weapon_Select.ItemsSource = WeaponNames;
                //int i = 1;
                //cbx_Weapon_Select.ItemsSource = Valorant.Weapons.WeaponList[i].name;
            }
        }

        void SetHotkeys()
        {
            HotkeyManager.Current.AddOrReplace("Increment", Key.PageUp, ModifierKeys.Control, OnIncrement);
            HotkeyManager.Current.AddOrReplace("Decrement", Key.PageDown, ModifierKeys.Control, OnDecrement);
            HotkeyManager.Current.AddOrReplace("Done", Key.End, ModifierKeys.Control, OnDone);
        }

        // Methods called by MainWindow below

        static class Settings
        {
            public static string Practice_Mode;
            public static bool Bots_Strafe = false;
            public static bool Bot_Armor = false;
            public static bool Inf_Ammo = true;
            // public static Weapon CurrentWeapon;
        }

        public void OnIncrement(object sender, HotkeyEventArgs e)
        {
            IncrementResult();
            e.Handled = true;
        }

        public void OnDecrement(object sender, HotkeyEventArgs e)
        {
            DecrementResult();
            e.Handled = true;
        }

        private void OnDone(object sender, HotkeyEventArgs e)
        {
            SaveResult();
            e.Handled = true;
        }


      
        void ModeButtonPressed(Button Pressed_Button)
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
                element.Background = Valorant.Colors.InactiveButton;
            }
            Pressed_Button.Background = Valorant.Colors.ActiveButton;

            Settings.Practice_Mode = Convert.ToString(Pressed_Button.Content);
        }

        void ConfigButtonPressed(Button Pressed_Button)
        {
            // messy... can Probably clean this up

            switch (Pressed_Button.Name)
            {
                case "btn_Strafe":
                    if (Settings.Bots_Strafe == true)
                    {
                        btn_Strafe.Background = Valorant.Colors.InactiveButton;
                        Settings.Bots_Strafe = false;
                        break;
                    }
                    else
                    {
                        btn_Strafe.Background = Valorant.Colors.ActiveButton;
                        Settings.Bots_Strafe = true;
                        break;
                    }
                case "btn_Armor_On":
                    btn_Armor_On.Background = Valorant.Colors.ActiveButton;
                    btn_Armor_Off.Background = Valorant.Colors.InactiveButton;
                    Settings.Bot_Armor = true;
                    break;
                case "btn_Armor_Off":
                    btn_Armor_On.Background = Valorant.Colors.InactiveButton;
                    btn_Armor_Off.Background = Valorant.Colors.ActiveButton;
                    Settings.Bot_Armor = false;
                    break;
                case "btn_Inf_Ammo_On":
                    btn_Inf_Ammo_On.Background = Valorant.Colors.ActiveButton;
                    btn_Inf_Ammo_Off.Background = Valorant.Colors.InactiveButton;
                    Settings.Inf_Ammo = true;
                    break;
                case "btn_Inf_Ammo_Off":
                    btn_Inf_Ammo_On.Background = Valorant.Colors.InactiveButton;
                    btn_Inf_Ammo_Off.Background = Valorant.Colors.ActiveButton;
                    Settings.Inf_Ammo = false;
                    break;
                default:
                    break;
            }
        }

        private void IncrementResult()
        {
            int result = Convert.ToInt32(txt_Result.Text);
            result++;
            txt_Result.Text = Convert.ToString(result);
        }

        private void DecrementResult()
        {
            int result = Convert.ToInt32(txt_Result.Text);
            result--;
            txt_Result.Text = Convert.ToString(result);
        }

        void SaveResult()
        {
            // TODO: Clean this fucking mess up

            String CurrentTime = DateTime.Now.ToString();
            //var currentweapon = cbx_Weapon_Select.Text;
            string Output = CurrentTime + ", " + txt_Result.Text + ", " + Settings.Practice_Mode + ", " + 
                Settings.Bots_Strafe + ", " + Settings.Bot_Armor + ", " + Settings.Inf_Ammo + ", " + 
                cbx_Weapon_Select.Text + "\n";
            //string Output = "Mode: " + Settings.Practice_Mode + ", Score: " +txt_Result.Text+"\n";
            System.IO.File.AppendAllText(@"C:\Users\Public\TestFolder\Valorant_Practice_Stats_Test.csv", Output);
        }

        // All Button click Methods below
        private void btn_Easy_Click(object sender, RoutedEventArgs e)
        {
            ModeButtonPressed((Button)sender);
        }

        private void btn_Medium_Click(object sender, RoutedEventArgs e)
        {
            ModeButtonPressed((Button)sender);
        }

        private void btn_Hard_Click(object sender, RoutedEventArgs e)
        {
            ModeButtonPressed((Button)sender);
        }

        private void btn_Elim_50_Click(object sender, RoutedEventArgs e)
        {
            ModeButtonPressed((Button)sender);
        }

        private void btn_Elim_100_Click(object sender, RoutedEventArgs e)
        {
            ModeButtonPressed((Button)sender);
        }

        private void btn_Strafe_Click(object sender, RoutedEventArgs e)
        {
            ConfigButtonPressed((Button)sender);
        }

        private void btn_Armor_On_Click(object sender, RoutedEventArgs e)
        {
            ConfigButtonPressed((Button)sender);
        }

        private void btn_Armor_Off_Click(object sender, RoutedEventArgs e)
        {
            ConfigButtonPressed((Button)sender);
        }

        private void btn_Inf_Ammo_On_Click(object sender, RoutedEventArgs e)
        {
            ConfigButtonPressed((Button)sender);
        }

        private void btn_Inf_Ammo_Off_Click(object sender, RoutedEventArgs e)
        {
            ConfigButtonPressed((Button)sender);
        }


        private void btn_Done_Click(object sender, RoutedEventArgs e)
        {
            SaveResult();
        }


        private void btn_Plus_Click(object sender, RoutedEventArgs e)
        {
            IncrementResult();
        }

        

        private void btn_Minus_Click(object sender, RoutedEventArgs e)
        {
            DecrementResult();
        }

        

        private void cbx_Weapon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
