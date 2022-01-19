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
using System.Windows.Shapes;

namespace SDA2035_WPF_HW6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }



        private void ButtonWeather_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            double speedWind = random.Next(10) + random.NextDouble();
            var angleWind = random.Next(1, 8);
            int temper = random.Next(-50, 50);
            int sky = random.Next(1, 4);
            WeatherControl newWeather = new WeatherControl(speedWind, angleWind, temper, sky);

            tb_WindAngle.Text = newWeather.WindAngle;
            tb_WindSpeed.Text = Math.Round(newWeather.WindSpeed, 2).ToString();
            tb_Temperature.Text = newWeather.Temperature.ToString();
            tb_Skystatus.Text = newWeather.Skystatus;

            if (newWeather.Temperature > 0)
            {
                BitmapImage tempratureImage = new BitmapImage(new Uri("Images/temphot.jpg", UriKind.Relative));
                im_Temperature.Source = tempratureImage;
            }
            else
            {
                BitmapImage tempratureImage = new BitmapImage(new Uri("Images/tempcold.jpg", UriKind.Relative));
                im_Temperature.Source = tempratureImage;
            }

            switch (sky)
            {
                case 1:
                    BitmapImage sunSkyImage = new BitmapImage(new Uri("Images/skysun.jpg", UriKind.Relative));
                    im_Skystatus.Source = sunSkyImage;
                    break;
                case 2:
                    BitmapImage cloudSkyImage = new BitmapImage(new Uri("Images/skycloud.jpg", UriKind.Relative));
                    im_Skystatus.Source = cloudSkyImage;
                    break;
                case 3:
                    BitmapImage rainSkyImage = new BitmapImage(new Uri("Images/skyrain.jpg", UriKind.Relative));
                    im_Skystatus.Source = rainSkyImage;
                    break;
                case 4:
                    BitmapImage snowSkyImage = new BitmapImage(new Uri("Images/skysnow.jpg", UriKind.Relative));
                    im_Skystatus.Source = snowSkyImage;
                    break;
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonWeather_Click(sender, e);
        }
    }
    class WeatherControl : DependencyObject
    {
        private double wind;
        public double WindSpeed
        {
            get
            {
                return wind;
            }
            set
            {
                if (value >= 0)
                {
                    wind = value;
                }
                else
                {
                    wind = 0;
                }
            }
        }
        public string WindAngle { get; set; }
        public string Skystatus { get; set; }
        public enum AngleEnum
        {
            North = 1,
            South,
            East,
            West,
            NorthEast,
            NorthWest,
            SouthEast,
            SouthWest
        }
        public int Temperature
        {
            get => (int)GetValue(TempratureProperty);
            set => SetValue(TempratureProperty, value);
        }

        public static readonly DependencyProperty TempratureProperty =
            DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    null,
                    new CoerceValueCallback(CoerceTemprature)));

        private static object CoerceTemprature(DependencyObject d, object baseValue)
        {
            int t = (int)baseValue;
            if (t >= -50 && t <= 50)
            {
                return t;
            }
            else
            {
                return 0;
            }
        }

        public enum SkyEnum
        {
            Sun = 1,
            Cloud,
            Rain,
            Snow
        }

        public WeatherControl(double speedWind, int angleWind, int temperature, int sky)
        {
            this.WindSpeed = speedWind;
            var angleWindCounter = (AngleEnum)angleWind;
            switch (angleWindCounter)
            {
                case AngleEnum.North:
                    WindAngle = "Северный";
                    break;
                case AngleEnum.South:
                    WindAngle = "Южный";
                    break;
                case AngleEnum.East:
                    WindAngle = "Восточный";
                    break;
                case AngleEnum.West:
                    WindAngle = "Западный";
                    break;
                case AngleEnum.NorthEast:
                    WindAngle = "Северо-Восточный";
                    break;
                case AngleEnum.NorthWest:
                    WindAngle = "Северо-Западный";
                    break;
                case AngleEnum.SouthEast:
                    WindAngle = "Юго-Восточный";
                    break;
                case AngleEnum.SouthWest:
                    WindAngle = "Юго-западный";
                    break;
                default:
                    break;
            }
            var skyCount = (SkyEnum)sky;
            switch (skyCount)
            {
                case SkyEnum.Sun:
                    Skystatus = "Солнечно";
                    break;
                case SkyEnum.Cloud:
                    Skystatus = "Облачно";
                    break;
                case SkyEnum.Rain:
                    Skystatus = "Дождливо";
                    break;
                case SkyEnum.Snow:
                    Skystatus = "Снежно";
                    break;
                default:
                    break;
            }
            Temperature = temperature;
        }
    }
}
