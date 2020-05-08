using System;
using System.Collections.Generic;
using System.Linq;
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


        public MainWindow()
        {
            InitializeComponent();
            Initialize_Buttons();

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
                element.Background = Brushes.LightGray;
            }
            Pressed_Button.Background = Brushes.LightYellow;

            Settings.Practice_Mode = Convert.ToString(Pressed_Button.Content);

        }

        void Config_Button(Button Pressed_Button)
        {
            switch (Pressed_Button.Name)
            {
                case "btn_Strafe":
                    if (Settings.Bots_Strafe == true)
                    {
                        btn_Strafe.Background = Brushes.LightGray;
                        Settings.Bots_Strafe = false;
                        break;
                    }
                    else
                    {
                        btn_Strafe.Background = Brushes.LightYellow;
                        Settings.Bots_Strafe = true;
                        break;
                    }
                case "btn_Armor_On":
                    btn_Armor_On.Background = Brushes.LightYellow;
                    btn_Armor_Off.Background = Brushes.LightGray;
                    Settings.Bot_Armor = true;
                    break;
                case "btn_Armor_Off":
                    btn_Armor_On.Background = Brushes.LightGray;
                    btn_Armor_Off.Background = Brushes.LightYellow;
                    Settings.Bot_Armor = false;
                    break;
                case "btn_Inf_Ammo_On":
                    btn_Inf_Ammo_On.Background = Brushes.LightYellow;
                    btn_Inf_Ammo_Off.Background = Brushes.LightGray;
                    Settings.Inf_Ammo = true;
                    break;
                case "btn_Inf_Ammo_Off":
                    btn_Inf_Ammo_On.Background = Brushes.LightGray;
                    btn_Inf_Ammo_Off.Background = Brushes.LightYellow;
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


    }
}
