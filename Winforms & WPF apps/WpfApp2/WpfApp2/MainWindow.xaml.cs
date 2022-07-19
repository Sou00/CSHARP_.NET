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
using System.Windows.Threading;


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double Kghm;
        double Pko;
        double Lotos;
        double Orlen;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
            Kghm = 100;
            Pko = 100;
            Lotos = 100;
            Orlen = 100;
            KghmDiffLabel.Content = "0 PLN";
            PkoDiffLabel.Content = "0 PLN";
            LotosDiffLabel.Content = "0 PLN";
            OrlenDiffLabel.Content = "0 PLN";

            KghmLabel.Content = Kghm.ToString() + " PLN";
            PkoLabel.Content = Pko.ToString() + " PLN";
            LotosLabel.Content = Lotos.ToString() + " PLN";
            OrlenLabel.Content = Orlen.ToString() + " PLN";
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            double drop1 = (double)rnd.Next(-100,100) / 100;
            double drop2 = (double)rnd.Next(-100, 100) / 100;
            double drop3 = (double)rnd.Next(-100, 100) / 100;
            double drop4 = (double)rnd.Next(-100, 100) / 100;
            
            KghmDiffLabel.Content = drop1.ToString() + " PLN";
            PkoDiffLabel.Content = drop2.ToString() + " PLN";
            LotosDiffLabel.Content = drop3.ToString() + " PLN";
            OrlenDiffLabel.Content = drop4.ToString() + " PLN";

            Kghm += drop1;
            Pko += drop2;
            Lotos += drop3;
            Orlen += drop4;

            KghmLabel.Content = Kghm.ToString() + " PLN";
            PkoLabel.Content = Pko.ToString() + " PLN";
            LotosLabel.Content = Lotos.ToString() + " PLN";
            OrlenLabel.Content = Orlen.ToString() + " PLN";
            KghmStack.Background = drop1 < 0 ?  Brushes.Red : Brushes.Green;
            PkoStack.Background = drop2 < 0 ? Brushes.Red : Brushes.Green;
            LotosStack.Background = drop3 < 0 ? Brushes.Red : Brushes.Green;
            OrlenStack.Background = drop4 < 0 ? Brushes.Red : Brushes.Green;

        }
    }
}
