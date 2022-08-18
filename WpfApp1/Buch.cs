using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    //unsere Klasse im Modell muss ein interface implementieren
    //dieses interface legt fest, was unsere Klasse braucht, damit auf Änderungen von eigenschaften
    //reagiert werden kann (das interface wird vom Listview gebraucht)
    class Buch:INotifyPropertyChanged

    {
        //eigenschaften für ein Buch
        //Bild soll für jedes Buch gespeichert werden
        //wir speichern nur den Pfad zum Bild
        private string _bild
        {
            get
            {
                return _bild;
            }
            set
            {
                _bild = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Bild"));
                }
            }
        }
        public string Bild { get; set; }

        private string _titel;
        public string Titel 
        { 
            get 
            { 
                return _titel; 
            } 
            set 
            { 
                _titel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Titel"));
                }
            }  
        }
        private double _preis;
        public double Preis
        {
            get 
            { 
                return _preis; 
            }
            set
            {
                _preis = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Preis"));
            }
        }

        private string _inhalt;
        public string Inhalt
        {
            get
            {
                return _inhalt;
            }
            set
            {
                _inhalt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Inhalt"));
            }
        }

        //ereignis aus dem Interface
        //Name: Propertychange (nicht änderbar)
        //Delegate: PropertyChangedEventHandler -- bestimmt wie die Methode aussehen muss die bei ereigniss ausgeführt wird
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
