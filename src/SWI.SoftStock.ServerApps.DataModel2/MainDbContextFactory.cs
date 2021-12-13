using System;
using Microsoft.EntityFrameworkCore;

namespace SWI.SoftStock.ServerApps.DataModel2
{
    public class MainDbContextFactory
    {
        private readonly DbContextOptions<MainDbContext> options;
        public MainDbContextFactory(DbContextOptions<MainDbContext> options)
        {
            this.options = options ?? throw new ArgumentNullException();
        }

        public MainDbContext Create()
        {
            return new MainDbContext(options);
        }
    }
}