using System;
using System.IO;
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
using System.Text.RegularExpressions;
using Microsoft.Win32;

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
            public static string PracticeMode = "";
            public static bool BotsStrafe = false;
            public static bool BotArmor = false;
            public static bool InfAmmo = true;
            public static string Path = @"C:\Users\Public\TestFolder\Valorant_Practice_Stats_Test3.csv";
            // the @ indicates that the string ignores escape characters like '\'
            public static SaveFileDialog saveFileDialog = new SaveFileDialog();

        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeButtons();
            CreateDropdown();
            SetHotkeys();
        }

        /*************************************************************************************************************/

        // Methods called by MainWindow

        /*************************************************************************************************************/

        void InitializeButtons()
        {
            ModeButtonPressed(btn_Easy);
            ConfigButtonPressed(btn_Inf_Ammo_On);
            ConfigButtonPressed(btn_Armor_Off);
        }

        void CreateDropdown()
        {
            foreach (Weapon item in Valorant.Weapons.WeaponList)
            {
                cbx_Weapon_Select.Items.Add(item.name);
            }
        }

        void SetHotkeys()
        {
            HotkeyManager.Current.AddOrReplace("Increment", Key.PageUp, ModifierKeys.Control, OnIncrement);
            HotkeyManager.Current.AddOrReplace("Decrement", Key.PageDown, ModifierKeys.Control, OnDecrement);
            HotkeyManager.Current.AddOrReplace("save", Key.End, ModifierKeys.Control, Onsave);
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

        private void Onsave(object sender, HotkeyEventArgs e)
        {
            SaveResult();
            e.Handled = true;
        }

        /*************************************************************************************************************/

        // Methods called by Button Methods

        /*************************************************************************************************************/

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

            Settings.PracticeMode = Convert.ToString(Pressed_Button.Content);
        }

        void ConfigButtonPressed(Button Pressed_Button)
        {
            // messy... can Probably clean this up

            switch (Pressed_Button.Name)
            {
                case "btn_Strafe":
                    if (Settings.BotsStrafe == true)
                    {
                        btn_Strafe.Background = Valorant.Colors.InactiveButton;
                        Settings.BotsStrafe = false;
                        break;
                    }
                    else
                    {
                        btn_Strafe.Background = Valorant.Colors.ActiveButton;
                        Settings.BotsStrafe = true;
                        break;
                    }
                case "btn_Armor_On":
                    btn_Armor_On.Background = Valorant.Colors.ActiveButton;
                    btn_Armor_Off.Background = Valorant.Colors.InactiveButton;
                    Settings.BotArmor = true;
                    break;
                case "btn_Armor_Off":
                    btn_Armor_On.Background = Valorant.Colors.InactiveButton;
                    btn_Armor_Off.Background = Valorant.Colors.ActiveButton;
                    Settings.BotArmor = false;
                    break;
                case "btn_Inf_Ammo_On":
                    btn_Inf_Ammo_On.Background = Valorant.Colors.ActiveButton;
                    btn_Inf_Ammo_Off.Background = Valorant.Colors.InactiveButton;
                    Settings.InfAmmo = true;
                    break;
                case "btn_Inf_Ammo_Off":
                    btn_Inf_Ammo_On.Background = Valorant.Colors.InactiveButton;
                    btn_Inf_Ammo_Off.Background = Valorant.Colors.ActiveButton;
                    Settings.InfAmmo = false;
                    break;
                default:
                    break;
            }
        }

        void SaveResult()
        {


            //if (System.IO.File.Exists(dir) == false)
            //{
            //    // Create CSV Header if new File
            //    string csvHeader = "Time,Score,Mode,Bots Strafe,Bot Armor,Infinite Ammo,Weapon,Weapon Type\n";
            //    System.IO.File.WriteAllText(Settings.Path, csvHeader);
            //}

            string CurrentTime = DateTime.Now.ToString();
            string delim = ",";

            List<string> output = new List<string> 
            { 
                CurrentTime, txt_Result.Text, Settings.PracticeMode ,  Convert.ToString(Settings.BotsStrafe), Convert.ToString(Settings.BotArmor),
                Convert.ToString(Settings.InfAmmo), cbx_Weapon_Select.Text, GetWeapon(cbx_Weapon_Select.Text).type.name
            };

            for (int i = 1; i < output.Count; i+=2)
            {
                output.Insert(i, delim);
            }

            output.Add("\n");

            {
                // Original Output
                /* string Output = CurrentTime + ", " + txt_Result.Text + ", " + Settings.PracticeMode + ", " +
                    Settings.BotsStrafe + ", " + Settings.BotArmor + ", " + Settings.InfAmmo + ", " +
                    cbx_Weapon_Select.Text + ", " + GetWeapon(cbx_Weapon_Select.Text).type.name + "\n";
                    */
                //System.IO.File.AppendAllText(@"C:\Users\Public\TestFolder\Valorant_Practice_Stats_Test.csv", Output);
            }
            try
            {
                foreach (var item in output)
                {
                    System.IO.File.AppendAllText(Settings.saveFileDialog.FileName, item);
                }
            }
            catch (System.IO.IOException)
            {

                SendNotification("Error, file is opened by another program. Please close all programs accessing the file.");
            }
        }

        private void IncrementResult()
        {
            List<string> speed = new List<string> { "Easy", "Medium", "Hard" };

            try
            {
                if ((Settings.PracticeMode == speed[0] || Settings.PracticeMode == speed[1] ||
                    Settings.PracticeMode == speed[2]) && Convert.ToInt32(txt_Result.Text) < 30)
                // checks for gamemode and result txt < 30
                {
                    int result = Convert.ToInt32(txt_Result.Text);
                    result++;
                    txt_Result.Text = Convert.ToString(result);
                }
                else if (Settings.PracticeMode != speed[0] && Settings.PracticeMode != speed[1] &&
                    Settings.PracticeMode != speed[2])
                {
                    int result = Convert.ToInt32(txt_Result.Text);
                    result++;
                    txt_Result.Text = Convert.ToString(result);
                }

            }
            catch (Exception)
            {
                txt_Result.Text = "30";
            }

        }

        private void DecrementResult()
        {
            try
            {
                int result = Convert.ToInt32(txt_Result.Text);
                if (result > 0) result--;
                txt_Result.Text = Convert.ToString(result);
            }
            catch (Exception)
            {

                txt_Result.Text = "30";
            }

        }

        /*************************************************************************************************************/


        public Weapon GetWeapon(string weap)
        {
            Weapon ret = Weapons.WeaponList.Find(x => x.name.Contains(weap));
            // ???
            return ret;
        }
        public void SendNotification(string text)
        {
            lbl_Notification.Content = text;
            lbl_Notification.Foreground = Brushes.Black;
            lbl_Notification.Background = Brushes.OrangeRed;
        }

        public void CloseNotification()
        {
            lbl_Notification.Background = Brushes.Transparent;
            lbl_Notification.Foreground = Brushes.Transparent;
        }

        /*************************************************************************************************************/

        // GUI Event Methods

        /*************************************************************************************************************/

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


        private void btn_Save_Click(object sender, RoutedEventArgs e)
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

        private void txt_Result_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_Result.Text = Regex.Replace(txt_Result.Text, "[^0-9]+", "");
            // this replaces Text that is not a number with an empty string
        }

        private void btn_ChooseFilePath_Click(object sender, RoutedEventArgs e)
        {
            string csvHeader = "Time,Score,Mode,Bots Strafe,Bot Armor,Infinite Ammo,Weapon,Weapon Type\n";

            // SaveFileDialog saveFileDialog = new SaveFileDialog();
            Settings.saveFileDialog.Filter = "CSV (Comma delimited) (*.csv)|*.csv|All files (*.*)|*.*";
            if (Settings.saveFileDialog.ShowDialog() == true)
            {

                File.WriteAllText(Settings.saveFileDialog.FileName, csvHeader);
                Settings.Path = @File.ReadAllText(Settings.saveFileDialog.FileName);
                txt_Test.Text = Settings.saveFileDialog.FileName;

            }

        }

        private void btn_RemoveRow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Close_Notification_Click(object sender, RoutedEventArgs e)
        {
            CloseNotification();
        }
    }
}
