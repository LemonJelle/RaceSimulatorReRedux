using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Controller;
using Model;

namespace RaceSimulatorWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Initialise controller, initialise data, appoint event handlers to events
            Data.Initalise();
            Data.NextRaceEvent += OnNextRaceEvent;
            Data.NextRace();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            
        }
        
        
        //Event handler every time drivers change
        public void OnDriversChanged(object sender, DriversChangedEventArgs dcea)
        {
            this.FullTrack.Dispatcher.BeginInvoke(
                DispatcherPriority.Render, new Action(() =>
                {
                    this.FullTrack.Source = null;
                    this.FullTrack.Source = WpfVisualisation.DrawTrack(dcea.EventTrack);
                }));
        }

        //Event handler for next race event, clears the image for the next race to begin
        public void OnNextRaceEvent(object sender, NextRaceEventArgs nre)
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }


    }
}