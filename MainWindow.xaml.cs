using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;
using System.IO;
using IniParser;
using IniParser.Model;
using System.Text.RegularExpressions;
using DirectShowLib;
namespace TMhdtr_gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string new_line = "[main]";
            if (File.Exists("TMheadtrack_config.ini"))
            {
                IniReader.lineChanger(new_line, "TMheadtrack_config.ini", 1);
                getValueForRadioBtn("TMheadtrack_config.ini", rect_method, point_method, "tmheadtrack_tracking_method");
                getValueForRadioBtn("TMheadtrack_config.ini", first_camera, second_camera, "tmheadtrack_camera_nummer");
                getValueForRadioBtn("TMheadtrack_config.ini", interval_click, mouth_click, "tmheadtrack_mouse_click_method");
                getValueForRadioBtn("TMheadtrack_config.ini", no_cam, with_cam, "tmheadtrack_camera");
                getValueForRadioBtn("TMheadtrack_config.ini", cam_left_upper, cam_right_upper, "camera_window_pos", cam_left_lower, cam_right_lower);
                getValueForRadioBtn("TMheadtrack_config.ini", btn_left_upper, btn_right_upper, "button_window_pos", btn_left_lower, btn_right_lower);

            }
            if (File.Exists("TMheadtrack_config_user.ini"))
            {
                IniReader.lineChanger(new_line, "TMheadtrack_config_user.ini", 1);
                getValueForRadioBtn("TMheadtrack_config_user.ini", rect_method, point_method, "tmheadtrack_tracking_method");
                getValueForRadioBtn("TMheadtrack_config_user.ini", first_camera, second_camera, "tmheadtrack_camera_nummer");
                getValueForRadioBtn("TMheadtrack_config_user.ini", interval_click, mouth_click, "tmheadtrack_mouse_click_method");
                getValueForRadioBtn("TMheadtrack_config_user.ini", no_cam, with_cam, "tmheadtrack_camera");
                getValueForRadioBtn("TMheadtrack_config_user.ini", cam_left_upper, cam_right_upper, "camera_window_pos", cam_left_lower, cam_right_lower);
                getValueForRadioBtn("TMheadtrack_config.ini", btn_left_upper, btn_right_upper, "button_window_pos", btn_left_lower, btn_right_lower);

            }
            if (!File.Exists("TMheadtrack_config_user_diffvar.ini"))
            {
                //File.Create("TMheadtrack_config_user_diffvar.ini");
                FileStream file = new FileStream("TMheadtrack_config_user_diffvar.ini", FileMode.Create);
                file.Close();
                IniReader.lineChanger(new_line, "TMheadtrack_config_user_diffvar.ini", 1);
            }
                //if (File.Exists("TMheadtrack_config_user_diffvar.ini"))
                //{
                //    IniReader.lineChanger(new_line, "TMheadtrack_config_user_diffvar.ini", 1);
                //    getValueForRadioBtn("TMheadtrack_config_user_diffvar.ini", rect_method, point_method, "tmheadtrack_tracking_method");

                //}

            var devices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            var cameraNames = new List<string>();
            int aivable_cams = 0;
            foreach (var device in devices)
            {
                cameraNames.Add(device.Name);
                //ini_content.Text += device.Name + "\n";
                if (aivable_cams == 0)
                {
                    first_camera.Visibility = Visibility.Visible;
                    first_camera.Content += device.Name;
                }
                else if (aivable_cams == 1)
                {
                    second_camera.Visibility = Visibility.Visible;
                    second_camera.Content += device.Name;
                }
                else if (aivable_cams == 2)
                {
                    third_camera.Visibility = Visibility.Visible;
                    third_camera.Content += device.Name;
                }
                aivable_cams++;

            }
        }



        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            string new_line = "##[main]";
            IniReader.lineChanger(new_line, "TMheadtrack_config.ini", 1);
            if (File.Exists("TMheadtrack_config_user.ini"))
            {
                IniReader.lineChanger(new_line, "TMheadtrack_config_user.ini", 1);
            }
            if (File.Exists("TMheadtrack_config_user_diffvar.ini"))
            {
                File.Delete("TMheadtrack_config_user_diffvar.ini");
            }
            if (File.Exists("TMheadtrack_config_user_diffall.ini"))
            {
                File.Delete("TMheadtrack_config_user_diffall.ini");
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            string new_line = "##[main]";
            IniReader.lineChanger(new_line, "TMheadtrack_config.ini", 1);
            Close();

        }
        private void Version(object sender, RoutedEventArgs e)
        {
            if (tab.SelectedIndex > 0)
            {
                tab.SelectedIndex = 0;
            }
            tab.SelectedIndex += 1;
        }



        private void blauer_quadrat_Checked(object sender, RoutedEventArgs e)
        {
            //if (File.Exists("TMheadtrack_config.ini"))
            //{
            //    text_box.Visibility = Visibility.Visible;
            //    text_box.Text = "";
            //    text_box.Text += "Aktive Methode: Blauer Quadrat steuerung \nKlike die spaicher Taste um zu speichern";
            //}
        }

        private void nase_punkt_Checked(object sender, RoutedEventArgs e)
        {
            //if (File.Exists("TMheadtrack_config.ini"))
            //{
            //    text_box.Visibility = Visibility.Visible;
            //    text_box.Text = "";
            //    text_box.Text += "Aktive Methode: Nase Punkt steuerung \nKlike die spaicher Taste um zu speichern";
            //}
        }

        private void return_bt(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex -= 1;
        }

        private void Standardeinstellungen(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bist du sicher das du deine Konfiguration löschen willst?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                File.Delete("TMheadtrack_config_user.ini");
            }

        }
        private void Start(object sender, RoutedEventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var combinedPath = Path.Combine(exePath, "TMhdtrqt.exe");
            Process.Start(combinedPath);
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bist du sicher das du die Konfiguration Speichern willst?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //if (rect_method.IsChecked == true)
                //{
                //    if (File.Exists("TMheadtrack_config.ini"))
                //    {
                //        IniReader.setNewValue("TMheadtrack_config.ini", "main", "tmheadtrack_tracking_method", "1", "TMheadtrack_config_user_diffall.ini", "TMheadtrack_config_user_diffvar.ini", true);
                //        IniReader.saveConfigUser("TMheadtrack_config_user.ini", "TMheadtrack_config_user_diffvar.ini");
                //    }
                //    else
                //    {
                //        System.Windows.MessageBox.Show("Kein ini gefunden", "error", 0);
                //    }
                //}
                //else if (point_method.IsChecked == true)
                //{
                //    if (File.Exists("TMheadtrack_config.ini"))
                //    {
                //        IniReader.setNewValue("TMheadtrack_config.ini", "main", "tmheadtrack_tracking_method", "2", "TMheadtrack_config_user_diffall.ini", "TMheadtrack_config_user_diffvar.ini", true);
                //        IniReader.saveConfigUser("TMheadtrack_config_user.ini", "TMheadtrack_config_user_diffvar.ini");

                //    }
                //    else
                //    {
                //        System.Windows.MessageBox.Show("Kein ini gefunden", "error", 0);
                //    }
                //}
                saveValueForRadioBtn(rect_method, point_method, "tmheadtrack_tracking_method", "1", "2");
                saveValueForRadioBtn(first_camera, second_camera , "tmheadtrack_camera_nummer", "1", "2", third_camera, null, "3");
                saveValueForRadioBtn(interval_click, mouth_click, "tmheadtrack_mouse_click_method", "1", "2");
                saveValueForRadioBtn(no_cam, with_cam, "tmheadtrack_camera", "1", "2");
                saveValueForRadioBtn(cam_left_upper, cam_right_upper, "camera_window_pos", "1", "2", cam_left_lower, cam_right_lower, "3", "4");
                saveValueForRadioBtn(btn_left_upper, btn_right_upper, "button_window_pos", "1", "2", btn_left_lower, btn_right_lower, "3", "4");
                
            }

        }

        private void getValueForRadioBtn(string IniToRead, RadioButton first_radio_btn_name, RadioButton second_radio_btn_name, string key, RadioButton third_radio_btn_name = null, RadioButton fourth_radio_btn_name = null)
        {
            string value = IniReader.getValue(IniToRead, "main", key);

            if (value == "1")
            {
                first_radio_btn_name.IsChecked = true;
            }
            else if (value == "2")
            {
                second_radio_btn_name.IsChecked = true;
            }
            else if (value == "3")
            {
                third_radio_btn_name.IsChecked = true;
            }
            else if (value == "4")
            {
                fourth_radio_btn_name.IsChecked = true;
            }
        }

        private void saveValueForRadioBtn(RadioButton first_radio_btn_name, RadioButton second_radio_btn_name, string key, string first_radioBtn_value, string second_radioBtn_value, RadioButton third_radio_btn_name = null, RadioButton fourth_radio_btn_name = null, string third_radioBtn_value = null, string fourth_radioBtn_value = null )
        {
            if (first_radio_btn_name.IsChecked == true)
            {
                if (File.Exists("TMheadtrack_config.ini"))
                {
                    IniReader.setNewValue("TMheadtrack_config.ini", "main", key, first_radioBtn_value, "TMheadtrack_config_user_diffall.ini", "TMheadtrack_config_user_diffvar.ini", true);
                    IniReader.saveConfigUser("TMheadtrack_config_user.ini", "TMheadtrack_config_user_diffvar.ini");
                }
                else
                {
                    System.Windows.MessageBox.Show("Kein ini gefunden", "error", 0);
                }
            }
            else if (second_radio_btn_name.IsChecked == true)
            {
                if (File.Exists("TMheadtrack_config.ini"))
                {
                    IniReader.setNewValue("TMheadtrack_config.ini", "main", key, second_radioBtn_value, "TMheadtrack_config_user_diffall.ini", "TMheadtrack_config_user_diffvar.ini", true);
                    IniReader.saveConfigUser("TMheadtrack_config_user.ini", "TMheadtrack_config_user_diffvar.ini");
                }
                else
                {
                    System.Windows.MessageBox.Show("Kein ini gefunden", "error", 0);
                }
            }
            else if (third_radio_btn_name.IsChecked == true)
            {
                if (File.Exists("TMheadtrack_config.ini"))
                {
                    IniReader.setNewValue("TMheadtrack_config.ini", "main", key, third_radioBtn_value, "TMheadtrack_config_user_diffall.ini", "TMheadtrack_config_user_diffvar.ini", true);
                    IniReader.saveConfigUser("TMheadtrack_config_user.ini", "TMheadtrack_config_user_diffvar.ini");

                }
                else
                {
                    System.Windows.MessageBox.Show("Kein ini gefunden", "error", 0);
                }
            }
            else if (fourth_radio_btn_name.IsChecked == true)
            {
                if (File.Exists("TMheadtrack_config.ini"))
                {
                    IniReader.setNewValue("TMheadtrack_config.ini", "main", key, fourth_radioBtn_value, "TMheadtrack_config_user_diffall.ini", "TMheadtrack_config_user_diffvar.ini", true);
                    IniReader.saveConfigUser("TMheadtrack_config_user.ini", "TMheadtrack_config_user_diffvar.ini");

                }
                else
                {
                    System.Windows.MessageBox.Show("Kein ini gefunden", "error", 0);
                }
            }
        }
    }
}
