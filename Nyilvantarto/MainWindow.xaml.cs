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

namespace Nyilvantarto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MaterialsGrid.ItemsSource = new List<Material>
            {
                new Material {  Name = "Cement", Quantity = 10, Id = "C001", Unit = "kg" },
                new Material { Name = "Homok", Quantity = 20, Id = "H002", Unit = "kg" },
                new Material { Name = "Víz", Quantity = 15, Id = "V003", Unit = "liter" }
            };
        }

        public class Material
        {
            public string? Id { get; set; }
            public string? Name { get; set; }
            public int? Quantity { get; set; }
            public string? Unit { get; set; }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchItem = SearchTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchItem))
            {
                MessageBox.Show("Kérem, adjon meg egy keresési kifejezést.");
                return;
            }

            var currentList = (List<Material>)MaterialsGrid.ItemsSource;

            var filteredList = currentList.FirstOrDefault(m => m.Name != null && m.Name.Contains(searchItem, StringComparison.OrdinalIgnoreCase));
            MaterialsGrid.ItemsSource = currentList;

            if(filteredList !=null)
            {
                MessageBox.Show($"Találat:\n\n" +
                                               $"ID: {filteredList.Id}\n" +
                                               $"Név: {filteredList.Name}\n" +
                                               $"Mennyiség: {filteredList.Quantity} {filteredList.Unit}");
            }


        }

        private void StockOutButton_Click(object sender, RoutedEventArgs e)
        {
            var currentList = (List<Material>)MaterialsGrid.ItemsSource;

            StockOutWindow stockWindow = new StockOutWindow(currentList);

            if (stockWindow.ShowDialog() == true)
            {
                MaterialsGrid.Items.Refresh();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var actualList = (List<Material>)MaterialsGrid.ItemsSource;

            AddMaterialWindow addWindow = new AddMaterialWindow(actualList);

            if (addWindow.ShowDialog() == true)
            {
                Material newItem = addWindow.CreatedMaterial;
                var currentList = (List<Material>)MaterialsGrid.ItemsSource;

                currentList.Add(newItem);
                MaterialsGrid.Items.Refresh();
            }
        }
    }
}