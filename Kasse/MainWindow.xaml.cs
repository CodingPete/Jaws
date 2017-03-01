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
using Kasse.ServiceReference1;
using DAL;

namespace Kasse
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private ServiceReference1.Service1Client client;

        public MainWindow()
        {
            InitializeComponent();
            client = new Service1Client();
        }

        private void button_num_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            String text = button.Content.ToString();

            TextBox textbox = textBox;

            textBox.Text = textBox.Text + text;
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TextBox text = textBox;
            textBox.Text = "";
        }

        private void button_Submit_Click(object sender, RoutedEventArgs e)
        {
            String GTIN = textBox.Text;

            if(GTIN != "")
            {
                Artikel artikel = client.getArtikelByGTIN(GTIN);

                if(artikel != null)
                {
                    listBox.Items.Add(artikel);
                    artikel_count.Content = listBox.Items.Count;
                    Double gesamt_betrag = 0;
                    foreach(Artikel artikels in listBox.Items)
                    {
                        gesamt_betrag += artikels.Nettoverkaufspreis;
                    }
                    gesamtbetrag.Content = Math.Round(gesamt_betrag, 2);
                }
                else
                {

                }
            }

            textBox.Text = "";
            
            

        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem == null)
            {
                listBox.Items.Clear();
                artikel_count.Content = listBox.Items.Count;
                gesamtbetrag.Content = 0;
            }
            else
            {
                Artikel delartikel = (Artikel) listBox.SelectedItem;
                listBox.Items.Remove(delartikel);
                artikel_count.Content = listBox.Items.Count;
                gesamtbetrag.Content = Math.Round((double)gesamtbetrag.Content - delartikel.Nettoverkaufspreis,2);
            }
            
        }

        private void button_book_Click(object sender, RoutedEventArgs e)
        {

            Lieferart lfa = client.getLieferartByName("Verkauf");
            Beleg beleg = new Beleg();
            beleg.LieferartId = lfa.Id;
            beleg.Datum = DateTime.Now;
            beleg.Id = client.setBeleg(beleg);
            List<int> artikelids = new List<int>();
            foreach(Artikel item in listBox.Items)
            {
                if (!artikelids.Contains(item.Id))
                {
                    artikelids.Add(item.Id);
                }
            }

            foreach(Artikel item in listBox.Items)
            {
                client.setArtikelBeleg(item, beleg);
            }

            var article = client.getArtikelbyBelegId(beleg.Id);
            foreach(int id in artikelids)
            {
                int artikelverkauft = article.Where((x) => x.Id == id).Count();
                Artikel artikel = client.getArtikelbyId(id);
                artikel.Bestand -= artikelverkauft;
                client.updateArtikel(artikel);
            }
            

            listBox.Items.Clear();
            artikel_count.Content = listBox.Items.Count;
            gesamtbetrag.Content = 0;
        }
    }
    
}
