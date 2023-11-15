using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAppShop.Models;

namespace WpfAppShop
{
    public partial class App: Application
    {
        static public AppDbContext dbContext = null!;
        static public User currentUser;

        public App()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Shop;Trusted_Connection=True;Trust Server Certificate=true;");

            dbContext = new AppDbContext(optionsBuilder.Options);
        }
    }
}
