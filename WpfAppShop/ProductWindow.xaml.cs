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
using WpfAppShop.Models;

namespace WpfAppShop
{
    public partial class ProductWindow: Window
    {
        public Product? Product { get; set; }

        public ProductWindow()
        {
            InitializeComponent();

            ButtonSave.Content = "Добавить";
        }

        public ProductWindow(Product product)
        {
            InitializeComponent();

            Product = product;

            TBoxName.Text = product.Name;
            TBoxPrice.Text = product.Price.ToString();

            ButtonSave.Content = "Сохранить";
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBoxName.Text))
            {
                MessageBox.Show("Введите название");
                return;
            }

            if (string.IsNullOrWhiteSpace(TBoxPrice.Text))
            {
                MessageBox.Show("Введите цену");
                return;
            }

            if (Product != null)
            {
                Product.Name = TBoxName.Text;
                Product.Price = Convert.ToInt32(TBoxPrice.Text);

                Close();
            }

            Product = new Product() { Name = TBoxName.Text, Price = Convert.ToInt32(TBoxPrice.Text) };

            Close();
        }
    }
}
