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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace WpfApp1
{
    //Methode die bei Klick auf button speichern ausgeführt werden kann
    //delegate definieren was methode übergibt
    public delegate void SaveEventhandler(string titel, double preis, string inhalt, string bild);
    /// <summary>
    /// Interaktionslogik für BuchNeu.xaml
    /// </summary>
    public partial class BuchNeu : Window
    {


        //es gibt ein ereignis, auf welches reagiert werden kann
        public event SaveEventhandler OnSave;

        string datei;
        public BuchNeu()
        {
            InitializeComponent();
        }

        private void btnsspeichern_Click(object sender, RoutedEventArgs e)
        {
            string titel = txtTitel.Text;
            double preis;
            double.TryParse(txtPreis.Text, out preis);
            string inhalt = txtInhalt.Text;


            //Callback
            OnSave?.Invoke(titel, preis, inhalt, datei);
            this.Close();
        }

        private void btnBild_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //bool? ist optional true - wenn ok, false - wenn cancel, ansonsten null
            bool? result = dialog.ShowDialog();
            if(result == true)
            {
                datei = dialog.FileName;
                bild.Source = new BitmapImage(new Uri(datei));
            }
        }

        private void txtInhalt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
