using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using WpfAppShop.Models;

namespace WpfAppShop
{
    public partial class MainWindow: Window
    {
        AuthWindow authWindow;
        bool isShutdown = true;

        public MainWindow(AuthWindow authWindow)
        {
            InitializeComponent();

            this.authWindow = authWindow;

            DGridProducts.ItemsSource = App.dbContext.Products.ToList();

            TBlockFullName.Text = App.currentUser.FullName;

            if (!App.currentUser.Role.Equals("Администратор"))
                ColumnAdmin.Width = new GridLength(0, GridUnitType.Star);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (isShutdown)
                Application.Current.Shutdown();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow pWindow = new ProductWindow();

            pWindow.ShowDialog();

            if (pWindow.Product != null)
            {
                App.dbContext.Products.Add(pWindow.Product);

                App.dbContext.SaveChanges();

                DGridProducts.ItemsSource = null;
                DGridProducts.ItemsSource = App.dbContext.Products.ToList();
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (DGridProducts.SelectedItem != null)
            {
                Product product = DGridProducts.SelectedItem as Product;

                App.dbContext.Products.Remove(product);

                App.dbContext.SaveChanges();

                DGridProducts.ItemsSource = null;
                DGridProducts.ItemsSource = App.dbContext.Products.ToList();
            }
            else
                MessageBox.Show("Выберите товар");
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGridProducts.SelectedItem != null)
            {
                Product product = DGridProducts.SelectedItem as Product;

                ProductWindow pWindow = new ProductWindow(product);

                pWindow.ShowDialog();

                App.dbContext.SaveChanges();

                DGridProducts.ItemsSource = null;
                DGridProducts.ItemsSource = App.dbContext.Products.ToList();
            }
            else
                MessageBox.Show("Выберите товар");
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            isShutdown = false;

            App.currentUser = null;

            authWindow.Show();

            Close();
        }
    }
}
