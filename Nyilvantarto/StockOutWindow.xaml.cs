using System;
using System.Collections;
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
using static Nyilvantarto.MainWindow;

namespace Nyilvantarto
{
    /// <summary>
    /// Interaction logic for StockOutWindow.xaml
    /// </summary>
    public partial class StockOutWindow : Window
    {
        private List<Material> _teljesLista;
        public StockOutWindow(List<Material> list)
        {
            InitializeComponent();
            _teljesLista = list;
            IdTxt.Focus();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var searchedId = IdTxt.Text.Trim();
            if (string.IsNullOrEmpty(searchedId))
            {
                MessageBox.Show("Kérem adja meg a keresett anyag azonosítóját!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var foundedId = _teljesLista.FirstOrDefault(m => m.Id == searchedId);

            if (foundedId == null)
            {
                MessageBox.Show("Nincs ilyen azonosítóval anyag a készletben!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (int.TryParse(QuantityTxt.Text.Trim(), out int number))
            {
                MessageBox.Show("A mennyisegnek számnak kell lennie", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (foundedId != null && int.TryParse(QuantityTxt.Text.Trim(), out int quantity))
            {
                if (quantity > foundedId.Quantity)
                {
                    MessageBox.Show("Nincs elég anyag a készletben!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foundedId.Quantity -= quantity;
                this.DialogResult = true;
                this.Close();
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
