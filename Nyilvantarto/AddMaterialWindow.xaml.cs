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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static Nyilvantarto.MainWindow;
using Material = Nyilvantarto.MainWindow.Material;

namespace Nyilvantarto
{
    /// <summary>
    /// Interaction logic for AddMaterialWindow.xaml
    /// </summary>
    public partial class AddMaterialWindow : Window
    {
        public Material CreatedMaterial { get; private set; }

        private List<Material> _materials;

        public AddMaterialWindow(List<Material> currentMaterials)
        {
            InitializeComponent();
            _materials = currentMaterials;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(QuantityTxt.Text, out int quantity))
            {
                MessageBox.Show("Kérem, adjon meg egy égy érvényes mennyiséget.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CreatedMaterial = new Material
            {
                Id = IdTxt.Text.Trim(),
                Name = NameTxt.Text.Trim(),
                Quantity = quantity,
                Unit = "kg"
            };

            if (string.IsNullOrWhiteSpace(NameTxt.Text) ||
                string.IsNullOrWhiteSpace(QuantityTxt.Text) ||
                string.IsNullOrWhiteSpace(IdTxt.Text))
            {
                MessageBox.Show("Kérem, töltse ki az összes mezőt.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_materials.Any(m => m.Id == IdTxt.Text.Trim()))
            {
                MessageBox.Show("Ez az ID már létezik. Kérem, adjon meg egy egyedi ID-t.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(quantity <= 0)
            {
                MessageBox.Show("A mennyiségnek pozitív számnak kell lennie.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }






            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NameTxt.Text = string.Empty;
            QuantityTxt.Text = string.Empty;
            IdTxt.Text = string.Empty;
            this.Close();
        }
    }
}
