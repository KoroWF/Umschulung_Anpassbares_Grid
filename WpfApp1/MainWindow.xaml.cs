using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string datei;
        //wird verwendet von ListView zur automatischen Prüfung, ob Änderungen und Anpassung der Anzeige
        ObservableCollection<Buch> Liste = new ObservableCollection<Buch>();

        public MainWindow()
        {
            InitializeComponent();
            Liste.Add(new Buch { Titel = "Der Gartenzwerg", Preis = 89.99, Bild = "C:/Users/geierT/Documents/ordner/bild1.png", Inhalt ="Inhalt1" });
            Liste.Add(new Buch { Titel = "Biss zum Morgengraun", Preis = 59.99, Bild = "C:/Users/geierT/Documents/ordner/bild2.png", Inhalt = "Inhalt2" });
            Liste.Add(new Buch { Titel = "Der Herr der Dinge", Preis = 49.99, Bild = "C:/Users/geierT/Documents/ordner/bild3.png", Inhalt = "Inhalt3" });

            //liste an Listview binden
            ListAnzeige.ItemsSource = Liste;

            //das erste element in der Liste wird ausgewählt
            ListAnzeige.SelectedIndex = 0;

            //in den textfeldern aus dem angezeigen liswiev den ausgewählten item anzeigen
            txtTitel.DataContext = ListAnzeige.SelectedItem;
            txtPreis.DataContext = ListAnzeige.SelectedItem;
            txtInhalt.DataContext = ListAnzeige.SelectedItem;
            bildmain.DataContext = ListAnzeige.SelectedItem;
        }

        private void ListAnzeige_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //in den textfeldern aus dem angezeigen liswiev den ausgewählten item anzeigen
            txtTitel.DataContext = ListAnzeige.SelectedItem;
            txtPreis.DataContext = ListAnzeige.SelectedItem;
            txtInhalt.DataContext = ListAnzeige.SelectedItem;
            bildmain.DataContext = ListAnzeige.SelectedItem;
        }

        private void txtTitel_KeyUp(object sender, KeyEventArgs e)
        {
            int index = ListAnzeige.SelectedIndex;

            if (index == -1) return;

            //wenn die entertaste gedrückt wurde
            if (e.Key == Key.Enter)
            {
                //bei dem ausgewählten Item in der liste wird der titel geändert
                Buch auswahl = Liste[index];
                auswahl.Titel = txtTitel.Text;
                
            }

        }

        private void txtPreis_KeyUp(object sender, KeyEventArgs e)
        {
            int index = ListAnzeige.SelectedIndex;

            if (index == -1) return;

            //wenn die entertaste gedrückt wurde
            if (e.Key == Key.Enter)
            {
                //bei dem ausgewählten Item in der liste wird der titel geändert
                Buch auswahl = Liste[index];
                double preis;
                bool geht = double.TryParse(txtPreis.Text, out preis);
                if (!geht)
                {
                    MessageBox.Show("Achtung - das war keine Zahl für Preis! -- habe 0 eingetragen");
                }

                //bei einem Objeklt wird etwas geändert
                //ereignis PropertyChanged (in buch.cs)
                //es wird die Methode ausgeführt für das ereignis (methode wird von ListView gestgelegt)

                auswahl.Preis = preis;

            }
        }

        private void txtInhalt_KeyUp(object sender, KeyEventArgs e)
        {
            int index = ListAnzeige.SelectedIndex;

            if (index == -1) return;

            //wenn die entertaste gedrückt wurde
            if (e.Key == Key.Enter)
            {
                //bei dem ausgewählten Item in der liste wird der titel geändert
                Buch auswahl = Liste[index];
                auswahl.Inhalt = txtInhalt.Text;

            }
        }

        private void btnNeu_Click(object sender, RoutedEventArgs e)
        {
            BuchNeu fenster = new BuchNeu();
            fenster.OnSave += Fenster_OnSave; 
            fenster.ShowDialog();

        }

        private void Fenster_OnSave(string titel, double preis, string inhalt, string bild)
        {
            Liste.Add(new Buch{Titel = titel, Preis = preis, Bild = bild, Inhalt = inhalt});
        }

        private void MainBild_Click(object sender, RoutedEventArgs e)
        {
            int index = ListAnzeige.SelectedIndex;

            if (index == -1) return;

            OpenFileDialog dialog = new OpenFileDialog();
            //bool? ist optional true - wenn ok, false - wenn cancel, ansonsten null
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string titel = Liste[index].Titel;
                double preis = Liste[index].Preis;
                string inhalt = Liste[index].Inhalt;


                datei = dialog.FileName;
                bildmain.Source = new BitmapImage(new Uri(datei));
                Buch auswahl = Liste[index];
                auswahl.Bild = datei;
                Liste[index] = (new Buch { Titel = titel, Preis = preis, Bild = datei, Inhalt = inhalt });
            }


        }

        private void btnLöschen_Click(object sender, RoutedEventArgs e)
        {
            int index = ListAnzeige.SelectedIndex;
            if (index == -1) return;


            MessageBoxResult result = MessageBox.Show("Willst du es wirklich löschen","Löschvorgang", MessageBoxButton.OKCancel);

            if(result == MessageBoxResult.OK)
            {
                Liste.RemoveAt(index);
            }
        }
    }
}
